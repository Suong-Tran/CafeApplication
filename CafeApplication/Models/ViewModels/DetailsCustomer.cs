using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeApplication.Models.ViewModels
{
  public class DetailsCustomer
  {
    public CustomerDto SelectedCustomer { get; set; }
    public IEnumerable<ItemDto> ItemOptions { get; set; }
    public ItemDto Item { get; set; }
  }
}