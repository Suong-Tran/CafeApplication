using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeApplication.Models
{
  public class Order
  {
    [Key]
    public int OrderID { get; set; }
    [ForeignKey("Customers")]
    public int CustomerID { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual Customer Customers { get; set; }
    public ICollection<Item> Items { get; set; }
  }

  public class OrderDto
  {
    public int OrderID { get; set; }
    public int CustomerID { get; set; }
    public string CustomerFName { get; set; }
    public string CustomerLName { get; set; }
    public DateTime OrderDate { get; set; }
  }
}