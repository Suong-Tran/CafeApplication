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
    public class ItemDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ListItem
        [HttpGet]
        public IEnumerable<ItemDto> ListItems()
        {
            List<Item> Items = db.Items.ToList();
            List<ItemDto> ItemDtos = new List<ItemDto>();

            Items.ForEach(i => ItemDtos.Add(new ItemDto()
            {
              ItemID = i.ItemID,
              ItemName = i.ItemName,
              ItemCalories = i.ItemCalories,
              ItemPrice = i.ItemPrice,
              ItemUnit = i.ItemUnit
            }));
            return ItemDtos;
        }

        // GET: api/ItemData/FindItem/5
        [ResponseType(typeof(Item))]
        [HttpGet]
        public IHttpActionResult FindItem(int id)
        {
            Item Item = db.Items.Find(id);
            ItemDto ItemDto = new ItemDto()
            {
              ItemID = Item.ItemID,
              ItemName = Item.ItemName,
              ItemCalories = Item.ItemCalories,
              ItemPrice = Item.ItemPrice,
              ItemUnit = Item.ItemUnit
            };
            if (Item == null)
            {
                return NotFound();
            }

            return Ok(ItemDto);
        }

        // POST: api/ItemData/UpdateItem/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.ItemID)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/ItemData/AddItem
        [ResponseType(typeof(Item))]
        [HttpPost]
        public IHttpActionResult AddItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item.ItemID }, item);
        }

        // POST: api/ItemData/DeleteItem/5
        [ResponseType(typeof(Item))]
        [HttpPost]
        public IHttpActionResult DeleteItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            db.SaveChanges();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.ItemID == id) > 0;
        }
    }
}