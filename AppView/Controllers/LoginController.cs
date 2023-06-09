using AppData.Models;
using AppView.Models;
using Microsoft.AspNetCore.Mvc;

namespace App_Ban_Quan_Ao_Thoi_Trang_Nam.Controllers
{
    public class LoginController : Controller
    {
        private AssignmentDBContext context = new AssignmentDBContext();
        public LoginController()
        {
            context = new AssignmentDBContext();
        }
        [HttpGet]
        public IActionResult Login()
        {
            var response = new UserViewModel();
            return View(response);
        }
        [HttpPost]
        public IActionResult Login(UserViewModel user)
        {
            if (!ModelState.IsValid) return View(user);

            var ngdung = context.NguoiDungs.Where(c => c.Email == user.Email).FirstOrDefault();
            if (ngdung != null)
            {
                if (user.Password.Equals(ngdung.Password))
                {
                    HttpContext.Session.SetString("IdUser", ngdung.IDNguoiDung.ToString());
                    HttpContext.Session.SetString("UserName", ngdung.Ten);
                    var quyen = context.VaiTros.Where(c => c.Ten == "Admin").First();
                    if (ngdung.IDVaiTro == quyen.ID)
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (!ModelState.IsValid) return View(user);
                }
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Register(UserViewModel user)
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
