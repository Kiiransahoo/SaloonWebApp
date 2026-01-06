using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaloonApp.Services;

namespace SaloonApp.Pages.Register
{
    //[Authorize(Roles = "Saloon")]
    public class RegisterModel : PageModel
    {
        private readonly UserService _service;

        public RegisterModel(UserService service)
        {
            _service = service;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Role { get; set; }

        public string Message { get; set; }

        public void OnPost()
        {
            _service.Register(Username, Password, Role);
            Message = "User Created";
        }

      
    }
}
