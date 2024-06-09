using FinalProject.Domain.Models.Products;
using FinalProject.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.WebMVC.Controllers
{
	public class ProductController : Controller

	{

		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
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
