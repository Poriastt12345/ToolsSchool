using Microsoft.AspNetCore.Mvc;
using ToolsSchool.DB;
using ToolsSchool.Entities;
using ToolsSchool.Models;

namespace ToolsSchool.Controllers
{
    public class UsersController : Controller
    {
        private readonly SchoolDB _schooldb;
        public UsersController(SchoolDB schoolDB)
        {
            _schooldb = schoolDB;
        }
        public IActionResult RegisterUser()
        {

            return View("../Users/RegisterUser");
        }
        public IActionResult LoginUser()
        {
            return View("../Users/LoginUser");
        }
        public IActionResult SubmitReqisterUser(SubmitRegisterUserDto submitRegister)
        {
            if (string.IsNullOrEmpty(submitRegister.Name))
            {
                TempData["Error"] = "نام را وارد کنید.";
                return RedirectToAction("RegisterUser");
            }
            if (string.IsNullOrEmpty(submitRegister.LastName))
            {
                TempData["Error"] = "نام خانوادگی را وارد کنید.";
                return RedirectToAction("RegisterUser");

            }
            if (string.IsNullOrEmpty(submitRegister.Email))
            {
                TempData["Error"] = "ایمیل خود را وارد کنید.";
                return RedirectToAction("RegisterUser");

            }
            if (string.IsNullOrEmpty(submitRegister.PhoneNumber))
            {
                TempData["Error"] = "شماره همراره خود را وارد کنید.";
                return RedirectToAction("RegisterUser");

            }
            if (string.IsNullOrEmpty(submitRegister.Password)||submitRegister.Password.Length<8)
            {
                TempData["Error"] = " رمز عبور نامعتبر است .";
                return RedirectToAction("RegisterUser");

            }
            if (submitRegister.Password!=submitRegister.Password2)
            {

                TempData["Error"] = "رمز عبور باهم برابر نیست.";
                return RedirectToAction("RegisterUser");
            }
            var user = new User();
            user.Name = submitRegister.Name;
            user.LastName = submitRegister.LastName;
            user.Email = submitRegister.Email;
            user.PhoneNumber = submitRegister.PhoneNumber;
            user.Password = submitRegister.Password;
            _schooldb.Users.Add(user);
            _schooldb.SaveChanges();
            return View("../Home/Index");
        }
        public IActionResult SubmintLoginUser(SubmitLoginUser loginUser)
        {

            if (string.IsNullOrEmpty(loginUser.PhoneNum))
            {
                TempData["Error"] = "شماره همراه را وارد کنید.";
                return RedirectToAction("LoginUser");
            }
            if (string.IsNullOrEmpty(loginUser.Password))
            {
                TempData["Error"] = "رمز عبور را وارد کنید.";
                return RedirectToAction("LoginUser");
            }
            var user = _schooldb.Users.Where(x => x.PhoneNumber == loginUser.PhoneNum && x.Password == loginUser.Password);
            if (user==null)
            {
                TempData["Error"] = "نام کاربری یا رمز عبور اشتباه است.";
                return RedirectToAction("LoginUser");

            }

            return View("../Home/Index");
        }
    }
}
