using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize(Roles = RoleName.CanManageMovies)]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
           _context.Dispose();
        }
        // ******************************************************GET: Customers******************************
        public ActionResult Index()
        {
            return View();
        }
        //*******************************************************Create new Or Edit Customer Form*****************
        public ActionResult CustomerForm()
        {
            var memShipTypes = _context.membershipTypes.ToList();
            var custMemVM = new CustomerFormViewModel
            {
                customer=new Customer(),
                membershipTypes = memShipTypes
            };
            return View("CustomerForm",custMemVM);
        }
        //******************************************************Save New Or Edit Existing Customer******************
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var CustomerVM = new CustomerFormViewModel
                {
                    customer = customer,
                    membershipTypes = _context.membershipTypes.ToList()
                };
              return  View("CustomerForm", CustomerVM);
            }
            if(customer.Id==0)//new customer
            {
                _context.Customers.Add(customer);
            }
            else    //Update the existing customer
            {
                var dbCustomer = _context.Customers.Single(c=>c.Id==customer.Id);
                dbCustomer.Name = customer.Name;
                dbCustomer.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                dbCustomer.MembershipTypeId = customer.MembershipTypeId;
                dbCustomer.DateOfBirth = customer.DateOfBirth;
            }
           
            _context.SaveChanges();
            return RedirectToAction("Index","Customers");
        }

        //****************************************************************Retrieve Customer from Index  & Send to Customer Form to Update******************
        public ActionResult Edit(int id)
        {
            ViewBag.EditOrNew = "Edit";
            var customer = _context.Customers.SingleOrDefault(c=>c.Id==id);
            if (customer == null)
                return HttpNotFound();
            var viewModel = new CustomerFormViewModel
            {
                customer = customer,
                membershipTypes = _context.membershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }
    }
}