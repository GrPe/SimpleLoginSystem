using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecureLoginWebApp.Controllers
{
    public class HomeController : Controller
    {

        SecureLoginService.SecureLoginServiceClient client = new SecureLoginService.SecureLoginServiceClient();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(SecureLoginService.User model)
        {
            if (model.Login != null && model.Password != null)
            {
                try
                {
                    //XSS
                    model.Login = Server.HtmlEncode(model.Login);
                    model.Password = Server.HtmlEncode(model.Password);

                    SecureLoginService.Response response =  client.Login(model);
                    if(response.Type == SecureLoginService.Types.CorrectLogin)
                    {
                        return RedirectToAction("HomePage", model);
                    }
                }
                catch(Exception ex)
                {
                    return View();
                }
            }
            return View();
        }
        

        public ActionResult HomePage(SecureLoginService.User user)
        {
            return View(user);
        }
    }
}