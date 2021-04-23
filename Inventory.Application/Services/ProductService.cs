using Inventory.Application.ViewModels;
using Inventory.Common;
using Inventory.DAL;
using Inventory.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task AddAsync(ProductViewModel model)
        {
            var product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CategoryId=model.CategoryId
            };

            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Product product = await _context.Product.FirstOrDefaultAsync(e => e.Id == id);
            if (product != null)
            {
                product.IsDeleted = true;
                product.DeletedOn = DateTime.Now;
                 _context.Product.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ProductViewModel> GetAsync(Guid id)
        {
            return await _context.Product.Include(e => e.Category)
                                         .Select(selectExpression)
                                         .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<ProductListViewModel> ListAsync(ProductListViewModel model)
        {
            IQueryable<ProductViewModel> query = _context.Product
                                                         .Include(e => e.Category)
                                                         .Where(e=>(string.IsNullOrEmpty(model.Search) || e.Name.ToLower().Contains(model.Search.ToLower()) 
                                                                                                       || e.Description.ToLower().Contains(model.Search.ToLower()))
                                                                     && (!model.CategoryId.HasValue ||e.CategoryId.Equals(model.CategoryId)))
                                                          .Select(selectExpression)
                                                          .AsQueryable();


            model.TotalRowCount = await query.CountAsync();
            model.Products = await query.OrderResult(model.SortColumn, model.SortOrder == "Desc" ? true : false)
                                                    .Skip(model.GetStartingRecord)
                                                    .Take(model.GetEndingRecord-model.GetStartingRecord).ToListAsync();
            return model;
        }

        public async Task UpdateAsync(ProductViewModel model)
        {
            Product product = await _context.Product.FirstOrDefaultAsync(e => e.Id == model.Id);
            if (product != null)
            {
                product.Name = model.Name;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.Description = model.Description;
                product.UpdatedOn = DateTime.Now;
                product.CategoryId = model.CategoryId;
                product.ImageUrl = model.ImageUrl;


                 _context.Product.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        #region "Static Select Expression"
        private static readonly Expression<Func<Product, ProductViewModel>> selectExpression = e => new ProductViewModel()
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Price = e.Price,
            Quantity = e.Quantity,
            ImageUrl=e.ImageUrl,
            CategoryId=e.CategoryId,
            CreatedOn=e.CreatedOn,
            Category = e.Category == null ? null : new CategoryViewModel()
            {
                Id = e.Category.Id,
                Name = e.Category.Name,
            }
        };
        #endregion
    }
}
