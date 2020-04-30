using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{

     [Authorize]
    public class CustomersController : ApiController
    {
        ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET Api/Customers
        public IHttpActionResult GetCustomers(string query=null)
        {
            var customersQuery = _context.Customers.Include(c => c.MembershipType);
            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));
            var customerDtos = customersQuery.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }

        //GET Api/Customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var dbCust = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (dbCust == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Customer,CustomerDto>(dbCust));
        }

        //Post Api/Customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = Mapper.Map<CustomerDto,Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri+"/"+customerDto.Id),customerDto);
        }

        //Put Api/Customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dbCust = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (dbCust == null)
            {
                return NotFound();
            }

            Mapper.Map(customerDto, dbCust);
            _context.SaveChanges();
            return Ok();
        }

        //Delete Api/Customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var dbCust = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (dbCust == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(dbCust);
            _context.SaveChanges();
            return Ok();
        }

    }
}







