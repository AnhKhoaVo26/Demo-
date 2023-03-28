using IPAddressManagement.Areas.Admin.Models;
using IPAddressManagement.Areas.Admin.Repositories;
using IPAddressManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IPAddressManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RentalContractController : Controller
    {
        private IRentalContractRepository rentalcontractRepository;

        public RentalContractController(IRentalContractRepository rentalcontractRepository)
        {
            this.rentalcontractRepository = rentalcontractRepository;
        }

        // GET: GroupsController
        public ActionResult Index()
        {
            var rentalcontract = rentalcontractRepository.GetRentalContracts();
            return View(rentalcontract);
        }

        // GET: GroupsController/Create
        public ActionResult Create()
        {
            return View(new ContractViewModel());
        }

        // POST: GroupsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContractViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (rentalcontractRepository.ExistsRenntalContractByName(model.Name.Trim())) // "Admin" != "AdmiN " != " AdMin "
                    {
                        ModelState.AddModelError("Name", "Tên nhóm đã tồn tại trong hệ thống.");
                        return View("Create", model);
                    }
                    else
                    {
                        // Tạo đối tượng GroupUser mới và gán giá trị từ model vào đối tượng này.
                        RentalContract rc = new RentalContract();
                        rc.Name = model.Name;
                        rc.StartDate = model.StartDate;
                        rc.EndDate = model.EndDate;

                        // Lưu đối tượng GroupUser mới vào cơ sở dữ liệu.
                        rentalcontractRepository.InsertRentalContract(rc);
                        rentalcontractRepository.Save();

                        return RedirectToAction("Index");
                    }
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
            var rentalcontracts = rentalcontractRepository.GetRentalContractByID(id);
            return View(rentalcontracts);
        }

        // POST: GroupsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ContractViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Retrieve the existing group from the database by id.
                    var rentalcontract = rentalcontractRepository.GetRentalContractByID(id);
                    if (rentalcontract == null)
                    {
                        return NotFound();
                    }

                    // Update the group's properties with the new data.

                    rentalcontract.Name = model.Name;
                    rentalcontract.StartDate = model.StartDate;
                    rentalcontract.EndDate = model.EndDate;

                    // Save the changes to the database.
                    rentalcontractRepository.UpdateRentalContract(rentalcontract);
                    rentalcontractRepository.Save();

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

        // GET: GroupsController/Delete/5
        public ActionResult Delete(int id)
        {
            var rentalcontracts = rentalcontractRepository.GetRentalContractByID(id);
            return View(rentalcontracts);
        }

        // POST: GroupsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var group = rentalcontractRepository.GetRentalContractByID(id);

                if (group != null)
                {
                    rentalcontractRepository.DeleteRentalContract(id);
                    rentalcontractRepository.Save();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
