using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SaloonApp.Models;
using SaloonApp.Services;

namespace SaloonApp.Pages.Search
{
    public class SearchModel : PageModel
    {
        private readonly SalonService _service;

        public SearchModel(SalonService service)
        {
            _service = service;
        }

        [BindProperty]
        public string PinCode { get; set; }

        public List<Salon> Salons { get; set; }

        public void OnPost()
        {
            Salons = _service.GetSalonsByPin(PinCode);
        }
        //public void OnGet()
        //{
        //}
    }
}
