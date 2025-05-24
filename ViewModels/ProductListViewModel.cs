using System.Collections.Generic;
using ProductionManagementSystem.Models;

namespace ProductionManagementSystem.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product>? Products { get; set; }
        public List<string>? Categories { get; set; }
        public string? SelectedCategory { get; set; }
        public string? SearchTerm { get; set; }  //Добавлено для поиска
    }
}