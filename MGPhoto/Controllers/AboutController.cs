using Microsoft.AspNetCore.Mvc;

namespace BarberBooking.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
