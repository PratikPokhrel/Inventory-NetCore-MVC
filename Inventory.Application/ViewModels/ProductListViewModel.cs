using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModels
{
    public class ProductListViewModel:PageBaseViewModel
    {
        public ProductListViewModel()
        {
            Products = new List<ProductViewModel>();
            CategoryDropDown = new List<CategoryViewModel>();
        }
        public Guid? CategoryId { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public List<CategoryViewModel> CategoryDropDown { get; set; }
    }
}
