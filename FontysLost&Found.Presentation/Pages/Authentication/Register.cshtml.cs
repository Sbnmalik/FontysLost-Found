using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer.Abstractions;

namespace FontysLost_Found.Presentation.Pages.Authentication
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthenticationService _authService;
        public RegisterModel(IAuthenticationService authService)
        {
            _authService = authService;
        }
        [BindProperty]
        public Register Input { get; set; } = new Register();

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (await _authService.UserNameExistsAsync(Input.UserName))
            {
                ModelState.AddModelError(string.Empty, "Username already exists.");
                return Page();
                
            }
            await _authService.RegisterAsync(Input);
            return RedirectToPage("./Login");

        }
    }
}
