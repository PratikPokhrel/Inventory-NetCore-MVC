using Inventory.Application.ViewModels;
using Inventory.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<CategoryViewModel>> GetCategoryDropDown()
        {
            return await _context.Category.Select(e => new CategoryViewModel
            {
                Id = e.Id,
                Name = e.Name
            }).ToListAsync();
        }
    }
}
