using Inventory.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public interface IProductService
    {
        Task<ProductListViewModel> ListAsync(ProductListViewModel model);
        public Task AddAsync(ProductViewModel model);
        Task UpdateAsync(ProductViewModel model);
        public Task<ProductViewModel> GetAsync(Guid id);
        public Task DeleteAsync(Guid id);
    }
}
