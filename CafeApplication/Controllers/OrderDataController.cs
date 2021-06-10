using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CafeApplication.Models;

namespace CafeApplication.Controllers
{
    public class OrderDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/OrderData
        [HttpGet]
        public IEnumerable<OrderDto> ListOrders()
        {
          List<Order> Orders = db.Orders.ToList();
          List<OrderDto> OrderDtos = new List<OrderDto>();

          Orders.ForEach(o => OrderDtos.Add(new OrderDto()
          {
            OrderID = o.OrderID,
            CustomerID = o.Customers.CustomerID,
            CustomerFName = o.Customers.CustomerFName,
            CustomerLName = o.Customers.CustomerLName,
            OrderDate = o.OrderDate
          }));
          return OrderDtos;
        }

        // GET: api/OrderData/FindOrder/5
        [ResponseType(typeof(Order))]
        [HttpGet]
        public IHttpActionResult FindOrder(int id)
        {
            Order Order = db.Orders.Find(id);
            OrderDto OrderDto = new OrderDto()
            {
              OrderID = Order.OrderID,
              CustomerID = Order.Customers.CustomerID,
              CustomerFName = Order.Customers.CustomerFName,
              CustomerLName = Order.Customers.CustomerLName,
              OrderDate = Order.OrderDate
            };
            if (Order == null)
            {
                return NotFound();
            }

            return Ok(OrderDto);
        }

        // POST: api/OrderData/Update/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderID)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/OrderData/AddItem
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult AddOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.OrderID }, order);
        }

        // POST: api/OrderData/DeleteOrder/5
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.OrderID == id) > 0;
        }
    }
}