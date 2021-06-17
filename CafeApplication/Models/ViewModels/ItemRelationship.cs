using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeApplication.Models.ViewModels
{
  public class ItemRelationship
  {
    public ItemDto SelectedItem { get; set; }
    public IEnumerable<CustomerDto> RelatedCustomers { get; set; }
    public IEnumerable<OrderDto> RelatedOrders { get; set; }
  }
}