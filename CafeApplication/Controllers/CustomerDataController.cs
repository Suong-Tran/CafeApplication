﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CafeApplication.Models;

namespace CafeApplication.Controllers
{
    public class CustomerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This will return a list of all customers as well the favorite item of the customers
        /// </summary>
        /// <returns>a list of customer and their associated item</returns>
        // GET: api/CustomerData/ListCustomers
        [HttpGet]
        public IEnumerable<CustomerDto> ListCustomers()
        {
          List<Customer> Customers= db.Customers.ToList();
          List<CustomerDto> CustomerDtos = new List<CustomerDto>();

          Customers.ForEach(c => CustomerDtos.Add(new CustomerDto()
          {
            CustomerID = c.CustomerID,
            CustomerFName = c.CustomerFName,
            CustomerLName = c.CustomerLName,
            CustomerGender = c.CustomerGender,
            CustomerAge = c.CustomerAge,
            isRegular = c.isRegular,
            ItemID = c.Items.ItemID,
            ItemName = c.Items.ItemName
          }));
          return CustomerDtos;
        }

    /// <summary>
    /// Gather information for all customers that has the same favorite item
    /// </summary>
    /// <param name="id">the item id</param>
    /// <returns>all the customers that has the same fav item</returns>
    //GET: api/CustomerData/ListCustomersForItem/5
    [HttpGet]
    public IEnumerable<CustomerDto> ListCustomersForItem(int id)
    {
      List<Customer> Customers = db.Customers.Where(c => c.ItemID == id).ToList();
      List<CustomerDto> CustomerDtos = new List<CustomerDto>();

      Customers.ForEach(c => CustomerDtos.Add(new CustomerDto()
      {
        CustomerID = c.CustomerID,
        CustomerFName = c.CustomerFName,
        CustomerLName = c.CustomerLName,
        CustomerGender = c.CustomerGender,
        CustomerAge = c.CustomerAge,
        isRegular = c.isRegular,
        ItemID = c.Items.ItemID,
        ItemName = c.Items.ItemName
      }));
      return CustomerDtos;
    }

      // GET: api/CustomerData/FindCustomer/5
      [ResponseType(typeof(Customer))]
        [HttpGet]
        public IHttpActionResult FindCustomer(int id)
        {
      
            Customer Customer = db.Customers.Find(id);
      Debug.WriteLine(Customer.ItemID);
          CustomerDto CustomerDto = new CustomerDto()
            {
              CustomerID = Customer.CustomerID,
              CustomerFName = Customer.CustomerFName,
              CustomerLName = Customer.CustomerLName,
              CustomerGender = Customer.CustomerGender,
              CustomerAge = Customer.CustomerAge,
              isRegular = Customer.isRegular,
              ItemID = Customer.Items.ItemID,
              ItemName = Customer.Items.ItemName
            };
            if (Customer == null)
            {
                return NotFound();
            }

            return Ok(CustomerDto);
        }

        // POST: api/CustomerData/UpdateCustomer/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CustomerData/AddCustomer
        [ResponseType(typeof(Customer))]
        [HttpPost]
        public IHttpActionResult AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerID }, customer);
        }

        // POST: api/CustomerData/DeleteCustomer/5
        [ResponseType(typeof(Customer))]
        [HttpPost]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}