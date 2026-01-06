using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaloonApp.Models;
using SaloonApp.Services;

namespace SaloonApp.Pages.Salons
{
    public class RegisterModel : PageModel
    {

        private readonly SalonService _service;

        public RegisterModel(SalonService service)
        {
            _service = service;
        }

        [BindProperty]
        public Salon Salon { get; set; }

        public string Message { get; set; }
        public void OnGet()
        {


        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _service.AddSalon(Salon);

            Message = "Salon registered successfully!";

            ModelState.Clear();
            Salon = new Salon();

            return Page();
        }
    }
}
