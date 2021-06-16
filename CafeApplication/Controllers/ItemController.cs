using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using CafeApplication.Models;
using System.Web.Script.Serialization;
using System.Diagnostics;

namespace CafeApplication.Controllers
{
    public class ItemController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static ItemController()
        {
          client = new HttpClient();
          client.BaseAddress = new Uri("https://localhost:44327/api/");
        }
        // GET: Item
        public ActionResult List()
        {
          string url = "itemdata/listitems";

          HttpResponseMessage response = client.GetAsync(url).Result;
          IEnumerable<ItemDto> items = response.Content.ReadAsAsync<IEnumerable<ItemDto>>().Result;

          return View(items);
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {

          string url = "itemdata/finditem/"+id;

          HttpResponseMessage response = client.GetAsync(url).Result;
          ItemDto item = response.Content.ReadAsAsync<ItemDto>().Result;

          return View(item);
        }
        public ActionResult Error()
        {

          return View();
        }

    // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(Item item)
        {
          string url = "itemdata/additem/";

          string jsonpayload = jss.Serialize(item);
          HttpContent content = new StringContent(jsonpayload);
      Debug.WriteLine(jsonpayload);
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

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
          string url = "itemdata/finditem/" + id;
          HttpResponseMessage response = client.GetAsync(url).Result;
          ItemDto SelectedItem = response.Content.ReadAsAsync<ItemDto>().Result;
          return View(SelectedItem);
        }

        // POST: Item/Update/5
        [HttpPost]
        public ActionResult Update(int id, Item item)
        {
          string url = "itemdata/updateitem/" + id;
          string jsonpayload = jss.Serialize(item);
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

        // GET: Item/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
          string url = "itemdata/finditem/" + id;
          HttpResponseMessage response = client.GetAsync(url).Result;
          ItemDto selectedItem = response.Content.ReadAsAsync<ItemDto>().Result;
          return View(selectedItem);
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
          string url = "itemdata/deleteitem/" + id;
          HttpContent content = new StringContent("");
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
    }
}
