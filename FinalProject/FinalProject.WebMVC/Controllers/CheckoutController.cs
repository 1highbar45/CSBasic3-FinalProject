using Application.Checkouts;
using FinalProject.Domain.Constants;
using FinalProject.Domain.Enums;
using FinalProject.Domain.Helpers;
using FinalProject.Domain.Models.Checkouts;
using FinalProject.Domain.Models;
using FinalProject.Domain.Services;
using FinalProject.WebMVC.Extension;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.WebMVC.Controllers
{
	[Authorize]
	public class CheckoutController : Controller
	{
		private readonly IBillService _billService;
		public CheckoutController(IBillService billService)
		{
			_billService = billService;
		}
		public IActionResult Index()
		{
			var model = new CheckoutViewModel();
			var cart = HttpContext.Session.GetCart(CommonConstants.Cart);
			model.Items = cart;
			model.PaymentMethod = EnumHelper.GetList(typeof(PaymentMethod));
			return View(model);
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<IActionResult> PlaceOrder(CustomerInfoModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var cart = HttpContext.Session.GetCart(CommonConstants.Cart);
			if (cart == null)
			{
				return Json(new ResponseResult(400, "cart is empty"));
			}
			BillCreateViewModel billModel = new BillCreateViewModel();
			billModel.FirstName = model.FirstName;
			billModel.LastName = model.LastName;
			billModel.Email = model.Email;
			billModel.PhoneNumber = model.PhoneNumber;
			billModel.Address = model.Address;
			billModel.PaymentMethod = model.PaymentMethod;
			billModel.BillDetails = cart.Select(s => new BillDetailCreateViewModel
			{
				Price = s.Price,
				ProductName = s.ProductName,
				Quantity = s.Quantity,
			}).ToList();
			try
			{
				await _billService.CreateBill(billModel);
				var response = new ResponseResult(200, "Place order success");
				HttpContext.Session.Remove(CommonConstants.Cart);
				TempData["checkout"] = JsonConvert.SerializeObject(response);
				return RedirectToAction("Index", "Checkout");
			}
			catch (Exception ex)
			{
				var response = new ResponseResult(400, "Some thing went wrong");
				TempData["checkout"] = JsonConvert.SerializeObject(response);
				return RedirectToAction("Index", "Checkout");
			}


		}
	}
}
