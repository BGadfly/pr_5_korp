using System;
using System.Collections.Generic;
using ProductionManagementSystem.Models;

namespace ProductionManagementSystem.ViewModels
{
    public class OrderListViewModel
    {
        public List<WorkOrder>? Orders { get; set; }
    }
}