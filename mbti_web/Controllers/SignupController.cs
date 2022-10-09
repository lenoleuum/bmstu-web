using Microsoft.AspNetCore.Mvc;

namespace mbti_web.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
