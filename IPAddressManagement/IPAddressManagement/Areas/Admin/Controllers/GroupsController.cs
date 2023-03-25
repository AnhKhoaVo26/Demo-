using IPAddressManagement.Areas.Admin.Models;
using IPAddressManagement.Areas.Admin.Repositories;
using IPAddressManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System.Text;

namespace IPAddressManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GroupsController : Controller
    {
        private IGroupRepository groupRepository;

        public GroupsController(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        // GET: GroupsController
        public ActionResult Index()
        {
            var groups = groupRepository.GetGroups();
            return View(groups);
        }

        // GET: GroupsController/Create
        public ActionResult Create()
        {          
            return View(new GroupViewModel());
        }

        // POST: GroupsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (groupRepository.ExistsGroupByName(model.Name.Trim())) // "Admin" != "AdmiN " != " AdMin "
                    {
                        ModelState.AddModelError("Name", "Tên nhóm đã tồn tại trong hệ thống.");
                        return View("Create", model);
                    }
                    // Tạo đối tượng GroupUser mới và gán giá trị từ model vào đối tượng này.
                    GroupUser g = new GroupUser();
                    g.Name = model.Name;
                    g.Decription = model.Decription;

                    // Lưu đối tượng GroupUser mới vào cơ sở dữ liệu.
                    groupRepository.InsertGroup(g);
                    groupRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                // Handle the exception here
                return View(model);
            }
        }

        // GET: GroupsController/Edit/5
        public ActionResult Edit(int id)
        {
            var groups = groupRepository.GetGroupByID(id);
            return View(groups);
        }

        // POST: GroupsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GroupViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the existing group from the database by id.
                    var group = groupRepository.GetGroupByID(id);
                    if (group == null)
                    {
                        return NotFound();
                    }
                    var check = groupRepository.GetGroupByName(model.Name.Trim());
                    if (check != null && check.ID_Group != model.ID_Group) 
                    {
                        ModelState.AddModelError("Name", "Tên nhóm đã tồn tại trong hệ thống.");
                        return View("Create", model);
                    }
                    // Update the group's properties with the new data                   
                    group.Name = model.Name;
                    group.Decription = model.Decription;

                    // Save the changes to the database.
                    groupRepository.UpdateGroup(group);
                    groupRepository.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }


        // Delete: GroupsController/Delete/5
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                var group = groupRepository.GetGroupByID(id);

                if (group != null)
                {
                    groupRepository.DeleteGroup(id);
                    groupRepository.Save();
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
                    status = false
                });
            }
        }
    }
}
