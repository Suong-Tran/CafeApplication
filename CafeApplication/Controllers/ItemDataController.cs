using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using CafeApplication.Models;

namespace CafeApplication.Controllers
{
    public class ItemDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ListItems
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
              ItemUnit = i.ItemUnit,
              ItemHasPic = i.ItemHasPic,
              PicExtension = i.PicExtension
            }));
            return ItemDtos;
        }

        //GET: api/ListItemsForOrder/5
        [HttpGet]
        public IEnumerable<ItemDto> ListItemsForOrder(int id)
        {
          List<Item> Items = db.Items.Where(
            i => i.Orders.Any(
            o => o.OrderID == id)
            ).ToList();
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

        //GET: api/ListItemNotInOrder/5
        [HttpGet]
        public IEnumerable<ItemDto> ListItemsNotInOrder(int id)
        {
          List<Item> Items = db.Items.Where(
            i => !i.Orders.Any(
            o => o.OrderID == id)
            ).ToList();
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
              ItemUnit = Item.ItemUnit,
              ItemHasPic = Item.ItemHasPic,
              PicExtension = Item.PicExtension
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
            db.Entry(item).Property(i => i.ItemHasPic).IsModified = false;
            db.Entry(item).Property(i => i.PicExtension).IsModified = false;

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
     
        [HttpPost]
        public IHttpActionResult UploadItemPic(int id)
        {
          bool hasPic = false;
          string picextension;
          if (Request.Content.IsMimeMultipartContent())
          {
            int numfiles = HttpContext.Current.Request.Files.Count;
            if(numfiles == 1 && HttpContext.Current.Request.Files[0] != null)
            {
              var itemPic = HttpContext.Current.Request.Files[0];
              if(itemPic.ContentLength > 0)
              {
                var valtype = new[] { "jpeg", "jpg", "png", "gif" };
                var extension = Path.GetExtension(itemPic.FileName).Substring(1);
                if (valtype.Contains(extension))
                {
                  try
                  {
                    string fn = id + "." + extension;
                    string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Images/Items/"), fn);

                    itemPic.SaveAs(path);

                    hasPic = true;
                    picextension = extension;

                    Item SelectedItem = db.Items.Find(id);
                    SelectedItem.ItemHasPic = hasPic;
                    SelectedItem.PicExtension = picextension;
                    db.Entry(SelectedItem).State = EntityState.Modified;

                    db.SaveChanges();
                  } 
                  catch(Exception ex)
                  {
                    return BadRequest();
                  }
                }
              }
            }
            return Ok();
          } else
          {
            return BadRequest();
          }

      
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

            if (item.ItemHasPic && item.PicExtension != "")
            {
              //also delete image from path
              string path = HttpContext.Current.Server.MapPath("~/Content/Images/Items/" + id + "." + item.PicExtension);
              if (System.IO.File.Exists(path))
              {
                System.IO.File.Delete(path);
              }
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