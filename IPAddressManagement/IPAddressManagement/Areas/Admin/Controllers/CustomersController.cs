using IPAddressManagement.Areas.Admin.Models;
using IPAddressManagement.Areas.Admin.Repositories;
using IPAddressManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPAddressManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        private ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // GET: GroupsController
        public ActionResult Index()
        {
            var customers = customerRepository.GetCustomers();
            return View(customers);
        }


        // GET: GroupsController/Create
        public ActionResult Create()
        {
            return View(new CustomerViewModel());
        }

        // POST: GroupsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (customerRepository.ExistsCustomerByEmail(model.Email.Trim()))
                {
                    ModelState.AddModelError("Email", "Tên email đã tồn tại trong hệ thống.");
                    return View("Create", model);
                }

                Customer c = new Customer();
                c.FirstName = model.FirstName.Trim();
                c.LastName = model.LastName.Trim();
                c.Email = model.Email.Trim();
                c.PhoneNumber = model.PhoneNumber.Trim();
                c.Company = model.Company.Trim();

                customerRepository.InsertCustomer(c);
                customerRepository.Save();

                return RedirectToAction("Index");
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
            var customers = customerRepository.GetCustomerByID(id);
            return View(customers);
        }

        // POST: GroupsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var group = customerRepository.GetCustomerByID(id);
                if (group == null)
                {
                    return NotFound();
                }
                var checkCust = customerRepository.GetCustomerByEmail(model.Email.Trim());
                if (checkCust != null && checkCust.ID_Customer != id)
                {
                    ModelState.AddModelError("Email", "Tên email đã tồn tại trong hệ thống.");
                    return View("Edit", model);
                }
                group.FirstName = model.FirstName.Trim();
                group.LastName = model.LastName.Trim();
                group.Email = model.Email.Trim();
                group.PhoneNumber = model.PhoneNumber.Trim();
                group.Company = model.Company.Trim();

                customerRepository.UpdateCustomer(group);
                customerRepository.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }


        // POST: GroupsController/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {             
            try
            {
                var customers = customerRepository.GetCustomerByID(id);

                if (customers != null)
                {
                    customerRepository.DeleteCustomer(id);
                    customerRepository.Save();
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
