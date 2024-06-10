using FinalProject.Domain.Helpers;
using FinalProject.Domain.Services;
using FinalProject.WebMVC.Models;
using FinalProject.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalProject.WebMVC.Controllers
{
	public class ShopController : Controller
	{
		private readonly ILogger<ShopController> _logger;
		private readonly ICategoryService _categoryService;
		private readonly IProductService _productService;

		public ShopController(ILogger<ShopController> logger, IProductService productService, ICategoryService categoryService)
		{
			_logger = logger;
			_categoryService = categoryService;
			_productService = productService;
		}

		public IActionResult Index()
		{
			var model = new ProductListingPageModel();
			model.Categories = _categoryService.GetCategories();
			model.SelectPageSize = new List<int> { 8, 16, 32 };
			model.OrderBys = EnumHelper.GetList(typeof(SortEnum));
			return View(model);
		}

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
