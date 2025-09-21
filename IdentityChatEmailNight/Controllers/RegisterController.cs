using IdentityChatEmailNight.Entities;
using IdentityChatEmailNight.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmailNight.Controllers
{
	public class RegisterController : Controller
	{
		//dependency Injection
		private readonly UserManager<AppUser> _userManager;

		public RegisterController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}
		// end of dependency Injection

		[HttpGet]
		public IActionResult CreateUser()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateUser(RegisterViewModel model)
		{
			AppUser appUser = new AppUser()
			{ 
				Name = model.Name,
				Surname = model.Surname,
				Email = model.Email,
				UserName = model.UserName,
			};

			var result =await _userManager.CreateAsync(appUser,model.Password);

			if (result.Succeeded)
			{
				return RedirectToAction("UserLogin","Login");
			}
			else
			{
				foreach(var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
					
				}
				return View();
			}
		}
	}
}
