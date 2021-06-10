using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using CafeApplication.Models;

namespace CafeApplication.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult List()
        {
          HttpClient client = new HttpClient() { };
          string url = "https://localhost:44327/api/itemdata/listitems";

          HttpResponseMessage response = client.GetAsync(url).Result;
          IEnumerable<ItemDto> items = response.Content.ReadAsAsync<IEnumerable<ItemDto>>().Result;

          return View(items);
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
          HttpClient client = new HttpClient() { };
          string url = "https://localhost:44327/api/itemdata/finditem/"+id;

          HttpResponseMessage response = client.GetAsync(url).Result;
          ItemDto item = response.Content.ReadAsAsync<ItemDto>().Result;

          return View(item);
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Item/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
