using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaloonApp.Services;
using System.Security.Claims;

namespace SaloonApp.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly UserService _service;

        public LoginModel(UserService service)
        {
            _service = service;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Error { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _service.GetUser(Username);

            if (user == null || user.PasswordHash != Password)
            {
                Error = "Invalid username or password";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return RedirectToPage("/Index");
        }

        public void OnGet()
        {
        }
    }
}
