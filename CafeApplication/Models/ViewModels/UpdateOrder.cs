using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeApplication.Models.ViewModels
{
  public class UpdateOrder
  {
    public OrderDto SelectedOrder { get; set; }
    public IEnumerable<CustomerDto> CustomerOptions { get; set; }
  }
}