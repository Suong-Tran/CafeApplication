using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CafeApplication.Models
{
  public class Item
  {
    public int ItemID { get; set; }
    public string ItemName { get; set; }
    //unit is either in slice or whole cake
    public string ItemUnit { get; set; }
    public int ItemCalories { get; set; }
    public decimal ItemPrice { get; set; }
    public ICollection<Order> Orders { get; set; }
  }
}