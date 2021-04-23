using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModels
{
   public class CategoryViewModel
    {

        public CategoryViewModel()
        {
            Product = new List<ProductViewModel>();
        }
        public Guid? Id { get; set; }
        public string Name { get; set; }

        public List<ProductViewModel> Product  { get; set; }
    }
}
