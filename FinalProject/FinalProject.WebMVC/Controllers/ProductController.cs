using FinalProject.Application.Services;
using FinalProject.Domain.Enums;
using FinalProject.Domain.Helpers;
using FinalProject.Domain.Models.Products;
using FinalProject.Domain.Services;
using FinalProject.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.WebMVC.Controllers
{
	public class ProductController : Controller

	{

		private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
		{
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var model = new ProductListingPageModel();
            model.Categories = _categoryService.GetCategories();
            model.SelectPageSize = new List<int> { 8, 16, 32 };
            model.OrderBys = EnumHelper.GetList(typeof(SortEnum));
            return View(model);
        }

        public IActionResult Detail()
		{
			return View();
		}

		public async Task<IActionResult> ProductListPartial([FromBody] ProductPage model)
		{
			var result = await _productService.GetProducts(model);
			return PartialView(result);
		}
	}
}
