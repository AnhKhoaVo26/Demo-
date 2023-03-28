using IPAddressManagement.Areas.Admin.Models;
using IPAddressManagement.Areas.Admin.Repositories;
using IPAddressManagement.Common;
using IPAddressManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace IPAddressManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private IUserRepository userRepository;
        private IGroupRepository groupRepository;

        public UsersController(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            this.userRepository = userRepository;
            this.groupRepository = groupRepository;
        }
        // GET: UsersController
        public ActionResult Index()
        {
            var users = userRepository.GetUsers();
            return View(users);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            var users = userRepository.GetUserByID(id);
            return View(users);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            var groups = groupRepository.GetGroups();
            ViewBag.Groups = groups;
            return View(new UserViewModel());
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel model)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào hợp lệ
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                // Kiểm tra email đã tồn tại
                if (userRepository.ExistsUserByEmail(model.Email.Trim())) 
                {
                    ModelState.AddModelError("Email", "Tên email đã tồn tại trong hệ thống.");
                    return View("Create", model);
                }
                // Tạo đối tượng GroupUser mới và gán giá trị từ model vào đối tượng này.
                User u = new User();
                u.FirstName = model.FirstName;
                u.LastName = model.LastName;
                u.Email = model.Email;
                if (model.NewPassWord != null)
                {
                    string encryptedPass = Encryptor.MD5Hash(model.NewPassWord.Trim());
                    u.PassWord = encryptedPass;
                }
                else
                {
                    string encryptedPass = Encryptor.MD5Hash("123456");
                    u.PassWord = encryptedPass;
                }
                u.PhoneNumber = model.PhoneNumber;
                u.ID_Group = model.ID_Group;

                // Lưu đối tượng GroupUser mới vào cơ sở dữ liệu.
                userRepository.InsertUser(u);
                userRepository.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                // Handle the exception here
                return View(model);
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {          
            var groups = groupRepository.GetGroups();
            ViewBag.Groups = groups;

            var user = userRepository.GetUserByID(id);
            UserViewModel model = new UserViewModel();
            model.ID_User = user.ID_User;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.ID_Group = user.ID_Group.HasValue ? (int) user.ID_Group : 0;
            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }            

                var user = userRepository.GetUserByID(id);
                if (user == null)
                {
                    return NotFound();
                }

                var userCheck = userRepository.GetUserByEmail(model.Email.Trim());
                if (userCheck != null && userCheck.ID_User != model.ID_User)
                {
                    ModelState.AddModelError("Email", "Tên email đã tồn tại trong hệ thống.");
                    return View("Create", model);
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                if (model.NewPassWord != null)
                {
                    string encryptedPass = Encryptor.MD5Hash(model.NewPassWord.Trim());
                    user.PassWord = encryptedPass;
                }
                user.PhoneNumber = model.PhoneNumber;
                user.ID_Group = model.ID_Group;

                userRepository.UpdateUser(user);
                userRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // Delete: UsersController/Delete/5
        [HttpDelete]
        public JsonResult Delete(int id)
         {
            try
            {
                var user = userRepository.GetUserByID(id);

                if (user != null)
                {
                    userRepository.DeleteUser(id);
                    userRepository.Save();
                }

                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = true
                });
            }
        }
    }
}
