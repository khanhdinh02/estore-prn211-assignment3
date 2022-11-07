using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace eStore.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] string email, [FromForm] string password)
        {
            try
            {
                IMemberRepository memberRepository = new MemberRepository();
                Member loginMember = memberRepository.Login(email, password);

                if (loginMember != null)
                {
                    if(loginMember.MemberId == 0)
                    {
                        HttpContext.Session.SetInt32("RoleID", 0);
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("RoleID", 1);
                    }
                }
                
                return RedirectToAction("Index", "Members");
            }
            catch (Exception ex)
            {
                ViewBag.Email = email;
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public IActionResult UserAccessDenied()
        {
            return View();
        }
    }
}