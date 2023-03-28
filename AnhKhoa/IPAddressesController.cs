using IPAddressManagement.Areas.Admin.Models;
using IPAddressManagement.Areas.Admin.Repositories;
using IPAddressManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IPAddressManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IPAddressesController : BaseController
    {
        private IIpsRepository iPaddresssRepository;

        public IPAddressesController(IIpsRepository iPaddresssRepository)
        {
            this.iPaddresssRepository = iPaddresssRepository;
        }

        // GET: GroupsController
        public ActionResult Index()
        {
            var ipaddress = iPaddresssRepository.GetIps();
            return View(ipaddress);
        }        
        
        public ActionResult Available()
        {
            var ipaddress = iPaddresssRepository.GetIps().Where(i => i.Status == true);
            return View(ipaddress);
        }

        
        // GET: GroupsController/Create
        public ActionResult Create()
        {
            return View(new IPAddressViewModel());
        }

        // POST: GroupsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IPAddressViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (!IsIPValid(model.IPAddressName))
                {
                    ModelState.AddModelError("IPAddressName", "Địa chỉ IP không hợp lệ");
                    return View("Create", model);
                }

                if (iPaddresssRepository.ExistsIpByName(model.IPAddressName.Trim()))
                {
                    ModelState.AddModelError("IPAddressName", "Địa chỉ đã tồn tại trong hệ thống.");
                    return View("Create", model);
                }

                IPAddresss ip = new IPAddresss();
                ip.IPAddressName = model.IPAddressName.Trim();
                ip.Status = model.Available;
                ip.CreatedDate = DateTime.Now;
                ip.CreatedBy = GetCurrentUser();


                iPaddresssRepository.InsertIp(ip);
                iPaddresssRepository.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                // Handle the exception here
                return View(model);
            }
        }

        private bool IsIPValid(string ipAddress)
        {
            string[] octets = ipAddress.Split('.');
            if (octets.Length != 4)
                return false;

            foreach(var octect in octets)
            {
                if(!IsOctectValid(octect)) 
                    return false;
            }
            return true;
        }

        private bool IsOctectValid(string octect)
        {
            int num;
            bool isNumeric = int.TryParse(octect, out num);
            if (!isNumeric)
                return false;
            if(num < 0 || num > 255)
                return false;
            return true;
        }

        // GET: GroupsController/Edit/5
        public ActionResult Edit(int id)
        {
            var ips = iPaddresssRepository.GetIpByID(id);
            IPAddressViewModel vm = new IPAddressViewModel();
            vm.ID_IPAddress = ips.ID_IPAddress;
            vm.IPAddressName = ips.IPAddressName;
            vm.Available = ips.Status;
            return View(vm);
        }

        // POST: GroupsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IPAddressViewModel model)
        {
            try
            {
                var ip = iPaddresssRepository.GetIpByID(id);
                if (ip == null)
                {
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (!IsIPValid(model.IPAddressName))
                {
                    ModelState.AddModelError("IPAddressName", "Địa chỉ IP không hợp lệ");
                    return View("Edit", model);
                }
                var checkIp = iPaddresssRepository.GetIpByName(model.IPAddressName.Trim());
                if (checkIp != null && checkIp.ID_IPAddress != id)
                {
                    ModelState.AddModelError("IPAddressName", "Địa chỉ đã tồn tại trong hệ thống.");
                    return View("Edit", model);
                }
                ip.IPAddressName = model.IPAddressName.Trim();
                ip.Status = model.Available;
                iPaddresssRepository.UpdateIp(ip);
                iPaddresssRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }


        // POST: GroupsController/Delete/5
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                var ip = iPaddresssRepository.GetIpByID(id);

                if (ip != null)
                {
                    iPaddresssRepository.DeleteIp(id);
                    iPaddresssRepository.Save();
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
