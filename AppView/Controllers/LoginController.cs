using Microsoft.AspNetCore.Mvc;

namespace App_Ban_Quan_Ao_Thoi_Trang_Nam.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
        }
        public IActionResult Register()
        {
            return View();   
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
