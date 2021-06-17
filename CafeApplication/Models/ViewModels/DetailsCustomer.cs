﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeApplication.Models.ViewModels
{
  public class DetailsCustomer
  {
    public ItemDto SelectedItem { get; set; }
    public IEnumerable<CustomerDto> RelatedCustomers { get; set; }
  }
}