using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using CafeApplication.Models;
using System.Web.Script.Serialization;
using CafeApplication.Models.ViewModels;

namespace CafeApplication.Controllers
{
    public class CustomerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
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
            DetailsOrder ViewModel = new DetailsOrder();
            
            string url = "customerdata/findcustomer/"+id;

            HttpResponseMessage response = client.GetAsync(url).Result;
            CustomerDto SelectedCustomer = response.Content.ReadAsAsync<CustomerDto>().Result;
            ViewModel.SelectedCustomer = SelectedCustomer;

            url = "orderdata/listordersforcustomer/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<OrderDto> RelatedOrders = response.Content.ReadAsAsync<IEnumerable<OrderDto>>().Result;
            ViewModel.RelatedOrders = RelatedOrders;



            return View(ViewModel);
        }
        
        //GET: Customer/Error
        public ActionResult Error()
        {

          return View();
        }

      // GET: Customer/Create
      public ActionResult Create()
        {
          string url = "itemdata/listitems";
          HttpResponseMessage response = client.GetAsync(url).Result;
          IEnumerable<ItemDto> ItemOptions = response.Content.ReadAsAsync<IEnumerable<ItemDto>>().Result;

          return View(ItemOptions);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            string url = "customerdata/addcustomer/";
      
            string jsonpayload = jss.Serialize(customer);
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
          UpdateCustomer ViewModel = new UpdateCustomer();

          string url = "customerdata/findcustomer/" + id;
          HttpResponseMessage response = client.GetAsync(url).Result;
          CustomerDto SelectedCustomer= response.Content.ReadAsAsync<CustomerDto>().Result;
          ViewModel.SelectedCustomer = SelectedCustomer;

          url = "itemdata/listitems";
          response = client.GetAsync(url).Result;
          IEnumerable<ItemDto> ItemOptions = response.Content.ReadAsAsync<IEnumerable<ItemDto>>().Result;
          ViewModel.ItemOptions = ItemOptions;

          return View(ViewModel);
        }

        // POST: Customer/Update/5
        [HttpPost]
        public ActionResult Update(int id, Customer customer)
        {
          string url = "customerdata/updatecustomer/" + id;
          string jsonpayload = jss.Serialize(customer);
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

        // GET: Customer/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
      string url = "customerdata/findcustomer/" + id;
      HttpResponseMessage response = client.GetAsync(url).Result;
          CustomerDto selectedcustomer = response.Content.ReadAsAsync<CustomerDto>().Result;
          return View(selectedcustomer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Customer customer)
        {

          string url = "customerdata/deletecustomer/" + id;
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
