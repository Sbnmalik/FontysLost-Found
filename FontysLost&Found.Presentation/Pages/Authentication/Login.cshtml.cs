using BusinessLogicLayer.Models;
using BusinessLogicLayer.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using IAuthenticationService = BusinessLogicLayer.Abstractions.IAuthenticationService;


namespace FontysLost_Found.Presentation.Pages.Authentication
{
    public class LoginModel : PageModel
    {
        private readonly IAuthenticationService _authService;
        public LoginModel(IAuthenticationService authService)
        {
            _authService = authService;
        }
        [BindProperty]
        public Login Input { get; set; } = new Login();
        public void OnGet()
        {
        }
        public async  Task<IActionResult> OnPostAsync (string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _authService.ValidateUserAsync(Input);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),

                new Claim(ClaimTypes.Role, user.UserName.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                    ? "Admin" :
                    "User")

            };
            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            
            return Redirect(returnUrl);
            
            return RedirectToPage("/Posts/View");


        }
    }
}
