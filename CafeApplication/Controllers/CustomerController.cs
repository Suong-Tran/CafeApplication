using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using CafeApplication.Models;

namespace CafeApplication.Controllers
{
    public class CustomerController : Controller
    {
        private static readonly HttpClient client;
        static CustomerController()
        {
          client = new HttpClient();
          client.BaseAddress = new Uri("https://localhost:44327/api/");
        }
        // GET: Customer/List
        public ActionResult List()
        {
            
            string url = "customerdata/listcustomers";

            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<CustomerDto> customers = response.Content.ReadAsAsync<IEnumerable<CustomerDto>>().Result;

            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            
            string url = "customerdata/findcustomer/"+id;

            HttpResponseMessage response = client.GetAsync(url).Result;
            CustomerDto customer = response.Content.ReadAsAsync<CustomerDto>().Result;

            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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
