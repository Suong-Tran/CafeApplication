using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using CafeApplication.Models;
using System.Web.Script.Serialization;
using System.Diagnostics;
using CafeApplication.Models.ViewModels;

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
          DetailsItem ViewModel = new DetailsItem();

          string url = "orderdata/findorder/"+id;
          HttpResponseMessage response = client.GetAsync(url).Result;
          OrderDto SelectedOrder = response.Content.ReadAsAsync<OrderDto>().Result;
          ViewModel.SelectedOrder = SelectedOrder;

          url = "itemdata/listitemsfororder/" + id;
          response = client.GetAsync(url).Result;
          IEnumerable<ItemDto> RelatedItems = response.Content.ReadAsAsync<IEnumerable<ItemDto>>().Result;
          ViewModel.RelatedItems = RelatedItems;

          url = "itemdata/listitemsnotinorder/" +id;
          response = client.GetAsync(url).Result;
          IEnumerable<ItemDto> ItemsNotOrdered = response.Content.ReadAsAsync<IEnumerable<ItemDto>>().Result;
          ViewModel.ItemNotOrdered = ItemsNotOrdered;

          return View(ViewModel);
        }
     
        //POST: Order/Associate/1
        [HttpPost]
        public ActionResult Associate(int id, int ItemID)
        {
          string url = "orderdata/addingitemtoorder/" + id + "/" + ItemID;
          HttpContent content = new StringContent("");
          content.Headers.ContentType.MediaType = "application/json";
          HttpResponseMessage response = client.PostAsync(url, content).Result;


          return RedirectToAction("Details/" + id);
        }

        //GET: Order/Unassociate/1?ItemID={ItemID}
        [HttpGet]
        public ActionResult Unassociate(int id, int ItemID)
        {
          string url = "orderdata/removingitemfromorder/" + id + "/" + ItemID;
          HttpContent content = new StringContent("");
          content.Headers.ContentType.MediaType = "application/json";
          HttpResponseMessage response = client.PostAsync(url, content).Result;


          return RedirectToAction("Details/" + id);
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
          UpdateOrder ViewModel = new UpdateOrder();

          string url = "orderdata/findorder/" + id;
          HttpResponseMessage response = client.GetAsync(url).Result;
          OrderDto SelectedOrder = response.Content.ReadAsAsync<OrderDto>().Result;
          ViewModel.SelectedOrder = SelectedOrder;

          url = "customerdata/listcustomers";
          response = client.GetAsync(url).Result;
          IEnumerable<CustomerDto> CustomerOptions = response.Content.ReadAsAsync<IEnumerable<CustomerDto>>().Result;
          ViewModel.CustomerOptions = CustomerOptions;

          return View(ViewModel);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Order order)
        {
          string url = "orderdata/updateorder/" + id;
          string jsonpayload = jss.Serialize(order);
      Debug.WriteLine(jsonpayload);
          HttpContent content = new StringContent(jsonpayload);
      Debug.WriteLine(content);
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

        // GET: Order/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
          string url = "orderdata/findorder/" + id;
          HttpResponseMessage response = client.GetAsync(url).Result;
          OrderDto SelectedOrder = response.Content.ReadAsAsync<OrderDto>().Result;
          return View(SelectedOrder);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Order order)
        {
          string url = "orderdata/deleteorder/" + id;
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
    }
}
