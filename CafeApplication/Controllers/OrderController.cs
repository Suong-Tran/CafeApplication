using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using CafeApplication.Models;

namespace CafeApplication.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult List()
        {
            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44327/api/orderdata/listorders";

            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<OrderDto> orders = response.Content.ReadAsAsync<IEnumerable<OrderDto>>().Result;

            return View(orders);

        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
          HttpClient client = new HttpClient() { };
          string url = "https://localhost:44327/api/orderdata/findorder/"+id;

          HttpResponseMessage response = client.GetAsync(url).Result;
          OrderDto order = response.Content.ReadAsAsync<OrderDto>().Result;

          return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
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

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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
