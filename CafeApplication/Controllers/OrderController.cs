using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using CafeApplication.Models;
using System.Web.Script.Serialization;

namespace CafeApplication.Controllers
{
        public class OrderController : Controller
        {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static OrderController()
        {
          client = new HttpClient();
          client.BaseAddress = new Uri("https://localhost:44327/api/");
        }
        // GET: Order
        public ActionResult List()
        {

            string url = "orderdata/listorders";

            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<OrderDto> orders = response.Content.ReadAsAsync<IEnumerable<OrderDto>>().Result;

            return View(orders);

        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {

          string url = "orderdata/findorder/"+id;

          HttpResponseMessage response = client.GetAsync(url).Result;
          OrderDto order = response.Content.ReadAsAsync<OrderDto>().Result;

          return View(order);
        }
        public ActionResult Error()
        {

          return View();
        }

    // GET: Order/Create
    public ActionResult Create()
        {
          string url = "customerdata/listcustomers";
          HttpResponseMessage response = client.GetAsync(url).Result;
          IEnumerable<CustomerDto> CustomerOptions = response.Content.ReadAsAsync<IEnumerable<CustomerDto>>().Result;

          return View(CustomerOptions);
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
          string url = "orderdata/addorder/";

          string jsonpayload = jss.Serialize(order);
          HttpContent content = new StringContent(jsonpayload);
          content.Headers.ContentType.MediaType = "application/json";

          HttpResponseMessage response = client.PostAsync(url, content).Result;
          if (response.IsSuccessStatusCode)
          {
            return RedirectToAction("List");
          }
          else
          {
            return RedirectToAction("Error");
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
