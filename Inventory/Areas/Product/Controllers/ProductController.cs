using Inventory.Application.Models;
using Inventory.Application.Services;
using Inventory.Application.ViewModels;
using Inventory.Web.Constants;
using Inventory.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.Areas.Product.Controllers
{
    [Area(AreaConstants.Product)]
    [Authorize]
    public class ProductController : Controller
    {
        protected readonly IProductService _productService;
        protected readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        #region "Constructor Injection"
        public ProductController(IProductService productService, 
                                 ICategoryService categoryService,
                                 IWebHostEnvironment webHostEnvironment) 
        {
            _categoryService = categoryService;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ProductListViewModel productList = await GetIndexListAsync();
            productList.CategoryDropDown = await _categoryService.GetCategoryDropDown();
            return View(productList);
        }


        [HttpPost]
        public async Task<IActionResult> Search(ProductListViewModel model)
        {
            ProductListViewModel returnResult = await _productService.ListAsync(model);
            return PartialView("_Search", returnResult);

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.CategoryDropDown = (await _categoryService.GetCategoryDropDown()).OrderBy(e=>e.Name);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ProductViewModel product = await _productService.GetAsync(id);
            ViewBag.CategoryDropDown = (await _categoryService.GetCategoryDropDown()).OrderBy(e => e.Name);
            return View("Add",product);
        }

        [HttpPost]
        [ModelStateValidationActionFilter]
        public async Task<IActionResult> AddOrEdit(ProductViewModel model)
        {
            try
            {
              
                    if (model.ImageFile!=null)
                        model.ImageUrl = await UploadedFile(model);

                    if (model.Id.HasValue)
                        await _productService.UpdateAsync(model);
                    else
                        await _productService.AddAsync(model);

                    return Redirect(nameof(Card_Index));
            }
            catch (Exception ex)
            {
                var exx = ex;
                return View();
            }
        }

        private async Task<ProductListViewModel> GetIndexListAsync()
        {
            ProductListViewModel leaveListVm = new ProductListViewModel
            {
                Page = 1,
                RowsPerPage = 10,
                SortColumn = "CreatedOn",
                SortOrder = "Desc"
            };
            return await _productService.ListAsync(leaveListVm);
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return Json(new ReturnObject<ProductViewModel>(true, "Product deleted Successfully"));
            }
            catch (Exception)
            {
                return Json(new ReturnObject<ProductViewModel>(false, "Oops.. something went wrong please try again"));
            }
        }



        //note:this methd is same as Index but UI Look is different.
        //Ui in Cards
        [HttpGet]
        public async Task<IActionResult> Card_Index()
        {
            ProductListViewModel productList = await GetIndexListAsync();
            productList.CategoryDropDown = await _categoryService.GetCategoryDropDown();
            return View(productList);
        }

        [HttpPost]
        public async Task<IActionResult> Search_Card(ProductListViewModel model)
        {
            ProductListViewModel returnResult = await _productService.ListAsync(model);
            return PartialView("_Search_Card", returnResult);

        }
        [NonAction]
        private async Task<string> UploadedFile(ProductViewModel model)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            string extension = Path.GetExtension(model.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Image/", fileName);
            string imageUrl = "/Image/" + fileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }
            return imageUrl;
        }
    }
}
