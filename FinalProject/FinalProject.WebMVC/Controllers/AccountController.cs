using Application.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.WebMVC.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountController(
			UserManager<IdentityUser> userManager,
			RoleManager<IdentityRole> roleManager,
			SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}

		[AllowAnonymous]
		public IActionResult Login(string ReturnUrl = "")
		{
			TempData["ReturnUrl"] = ReturnUrl;
			return View();
		}

		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user != null)
			{
				var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
				if (result.Succeeded)
				{
					var returnUrl = TempData["ReturnUrl"] as string;
					if (!string.IsNullOrEmpty(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return Redirect("/");
					}

				}
				ModelState.AddModelError(string.Empty, "password is not correct!");
				return View(model);
			}
			ModelState.AddModelError(string.Empty, "email is not correct");
			return View(model);
		}
		public IActionResult Logout()
		{
			_signInManager.SignOutAsync();
			return Redirect("/account/login");
		}
		public IActionResult AccessDenied()
		{
			return View();
		}

		[AllowAnonymous]
		public IActionResult Register(string ReturnUrl = "")
		{
			TempData["ReturnUrl"] = ReturnUrl;
			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var user = new IdentityUser
			{
				UserName = model.UserName,
				Email = model.Email,
			};
			var userResult = await _userManager.CreateAsync(user, model.Password);
			if (userResult.Succeeded)
			{
				var roleResult = await _userManager.AddToRoleAsync(user, "user");
				if (roleResult.Succeeded)
				{
					await _signInManager.SignInAsync(user, false);
					return Redirect("/");
				}
				else
				{
					await _userManager.DeleteAsync(user);
					ModelState.AddModelError(string.Empty, GetErrorMessage(roleResult));
					return View(model);
				}
			}

			ModelState.AddModelError(string.Empty, GetErrorMessage(userResult));
			return View(model);
		}

		public ActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}

			var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, isPersistent: false);
				return RedirectToAction("Index", "Home");
			}

			return View(model);
		}

		#region Role

		[Authorize(Roles = "admin")]
		public IActionResult Roles()
		{
			var roles = _roleManager.Roles.ToList();
			return View(roles);
		}

		[Authorize(Roles = "admin")]
		public IActionResult CreateRole()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var role = new IdentityRole
			{
				Name = model.RoleName
			};
			var result = await _roleManager.CreateAsync(role);
			if (result.Succeeded)
			{
				return Redirect("/Account/Roles");
			}
			ModelState.AddModelError(string.Empty, GetErrorMessage(result));
			return View(model);
		}
		#endregion


		#region User

		[Authorize(Roles = "admin")]
		public IActionResult Users()
		{
			var users = _userManager.Users.ToList();
			return View(users);
		}

		[Authorize(Roles = "admin")]
		public IActionResult CreateUser()
		{
			var roles = _roleManager.Roles.ToList();
			ViewBag.Roles = roles;
			return View();
		}

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateUser(CreateUserViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var user = new IdentityUser
			{
				UserName = model.UserName,
				Email = model.Email,
			};
			var userResult = await _userManager.CreateAsync(user, model.Password);
			if (userResult.Succeeded)
			{
				var role = await _roleManager.FindByIdAsync(model.RoleId);
				var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
				if (roleResult.Succeeded)
				{
					return Redirect("/Account/users");
				}
				else
				{
					await _userManager.DeleteAsync(user);
					ModelState.AddModelError(string.Empty, GetErrorMessage(roleResult));
					return View(model);
				}
			}

			ModelState.AddModelError(string.Empty, GetErrorMessage(userResult));
			return View(model);
		}

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)	
            {
				var editUserModel = new EditUserViewModel
				{
					Id = user.Id,
					UserName = user.UserName,
					Email = user.Email,
				};
                return View(editUserModel);
            }
            return View(null);
        }

		[HttpPost]
		public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
		{
			var user = await _userManager.FindByIdAsync(editUserViewModel.Id);
			if (user != null)
			{
				user.UserName = editUserViewModel.UserName;
				user.Email = editUserViewModel.Email;

				var userResult = await _userManager.UpdateAsync(user);
				if (userResult.Succeeded)
				{
					return Redirect("/Account/users");
				}
			}
			return View(null);
		}

		public async Task<IActionResult> DeleteUser(string Id)
        {
			var user = await _userManager.FindByIdAsync(Id);
			if(user!= null)
			{
				var userResult = await _userManager.DeleteAsync(user);
				if(userResult.Succeeded)
				{
                    return Redirect("/Account/users");
                }
            }
			return View();
        }

        #endregion
        private string GetErrorMessage(IdentityResult result)
		{
			if (result.Errors.Any())
			{
				var errorMessage = string.Join(" ", result.Errors.Select(x => x.Description).ToList());
				return errorMessage;
			}
			return string.Empty;
		}
	}


}