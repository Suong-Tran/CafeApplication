using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeApplication.Models
{
  public class Customer
  {
    [Key]
    public int CustomerID { get; set; }
    public string CustomerFName { get; set; }
    public string CustomerLName { get; set; }
    //option between Male,Female or Other
    public string CustomerGender { get; set; }
    public int CustomerAge { get; set; }
    public bool isRegular { get; set; }
    [ForeignKey("Items")]
    public int ItemID {  get;  set;  }
    public virtual Item Items { get; set; }
  }

  public class CustomerDto
  {
    public int CustomerID { get; set; }
    public string CustomerFName { get; set; }
    public string CustomerLName { get; set; }
    //option between Male,Female or Other
    public string CustomerGender { get; set; }
    public int CustomerAge { get; set; }
    public bool isRegular { get; set; }
    public int ItemID { get; set; }
    public string ItemName { get; set; }
  }
}