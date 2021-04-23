using Inventory.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategoryDropDown();
    }
}
