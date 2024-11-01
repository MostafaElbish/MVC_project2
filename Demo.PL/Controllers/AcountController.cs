using Demo.DAL.Models;
using Demo.PL.Healper;
using Demo.PL.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AcountController : Controller
    {
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly SignInManager<ApplicationUser> _SignInManager;

		public AcountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager) {
			_UserManager = userManager;
			_SignInManager = signInManager;
		}

		public UserManager<ApplicationUser> UserManager { get; }
		public SignInManager<ApplicationUser> SignInManager { get; }
		#region singup
		[HttpGet]
		public IActionResult singup() 
        { 
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Signup(SinupViewmodel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _UserManager.FindByNameAsync(model.UserName);
				if (user == null)
				{
					user = await _UserManager.FindByEmailAsync(model.Email);
					if (user == null)
					{
						user = new ApplicationUser
						{
							UserName = model.UserName,
							Email = model.Email,
							firstname = model.FirstName,
							Lastname = model.LastName,
							Isagree = model.IsAgree
						};
						var result = await _UserManager.CreateAsync(user, model.Password);

						if (result.Succeeded)
						
							return RedirectToAction(nameof(SignIn));
						

						foreach (var error in result.Errors)
						{
							ModelState.AddModelError(string.Empty, error.Description);
						}



						ModelState.AddModelError(string.Empty, "Email is already taken.");
					}
				}
				
			}
			return View(model);
		}
		#endregion
		[HttpGet]
		public IActionResult Signin()
		{
			return View();
		}
		[HttpPost]
		public async Task< IActionResult> Signin(SigninViewmodel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _UserManager.FindByEmailAsync(model.Email);
			if (result is not null)
				{
				var flag=await	_UserManager.CheckPasswordAsync(result, model.PassWord);
					if (flag)
					{ 
					var Result= await _SignInManager.PasswordSignInAsync(result, model.PassWord, model.Rememberme,false);
                     if(Result.Succeeded)
						{
							return RedirectToAction(nameof(HomeController.Index), controllerName: "Home");
						}
				
					} 
				}
				ModelState.AddModelError(string.Empty, errorMessage: "Invalied Logen");
			}
			return View(model);
		}
		
		public new async Task< IActionResult> SignOut()
		{
		await	_SignInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

		public IActionResult ForgetPassword()
		{
			return View();
		}
		public async Task< IActionResult> SendResetPasswordUrl(ForgetViewMOdel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _UserManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					//create token
					var token = await _UserManager.GeneratePasswordResetTokenAsync(user);
					//create Url
					var url = Url.Action(action:"ResetPassword","Acount",new {email=model.Email,token = token},Request.Scheme);

					var email = new Email()
					{
						Subject = "Reset Your Password",
					Reseption = model.Email,
					Bodey = url
					};
					//send Email
					EmailSetings.SendEmail(email);

					return RedirectToAction(nameof(cheackYourEmail));
				}
				ModelState.AddModelError(string.Empty, errorMessage: "Email not Found");
			}
			return View(nameof(ForgetPassword),model);
		}
		public IActionResult cheackYourEmail()
		{
			return View();
		}
		#region #REset password
		[HttpGet]		
		public IActionResult ResetPassword(string email,string token)
		{
			TempData[key: "email"] = email; 
			TempData[key:"token"]=token;
			return View();
		}
		[HttpPost]
        public async Task< IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

			if (ModelState.IsValid)
			{
				var email = TempData[key: "email"] as string;
				var token = TempData[key: "token"] as string;
				var user =await  _UserManager.FindByEmailAsync(email);
				if (user != null)
				{
					var result = await _UserManager.ResetPasswordAsync(user,token,model.NewPassword);
					if (result.Succeeded)
					
						return RedirectToAction(nameof(Signin));
					foreach(var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty,error.Description);
					}
					
				}
				ModelState.AddModelError(string.Empty, errorMessage: "invalid rest password");
            }


            return View();
        }

        #endregion
    }
}
