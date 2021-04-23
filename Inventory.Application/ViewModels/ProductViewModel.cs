using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventory.Application.ViewModels
{
    public class ProductViewModel
    {

        public ProductViewModel()
        {
            CategoryDropDown = new List<CategoryViewModel>();
        }
        public Guid? Id { get; set; }
        [DisplayName("Category")]
        public Guid? CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
        public string Description { get; set; }
        public CategoryViewModel Category { get; set; }

        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<CategoryViewModel> CategoryDropDown  { get; set; }
    }
}
