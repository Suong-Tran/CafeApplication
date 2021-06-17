using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeApplication.Models.ViewModels
{
  public class DetailsOrder
  {
    public CustomerDto SelectedCustomer { get; set; }
    public IEnumerable<OrderDto> RelatedOrders { get; set; }
  }
}