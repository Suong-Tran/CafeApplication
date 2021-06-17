using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeApplication.Models.ViewModels
{
  public class DetailsItem
  {
    public OrderDto SelectedOrder { get; set; }
    public IEnumerable<ItemDto> RelatedItems { get; set; }
    public IEnumerable<ItemDto> ItemNotOrdered { get; set; }
  }
}