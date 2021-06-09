using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeApplication.Models
{
  public class Customer
  {
    public int CustomerID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerGender { get; set; }
    public int CustomerAge { get; set; }
    public bool isRegular { get; set; }
    public string favoriteItem {  get;  set;  }
  }
}