using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecureLoginWebApp.Controllers
{
    public class RegistrationController : Controller
    {
        SecureLoginService.SecureLoginServiceClient client = new SecureLoginService.SecureLoginServiceClient();
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.RegistrationUser model)
        {
            try
            {
                if (model.Login != null && model.Password != null && model.RePassword != null)
                {
                    if (model.Password != model.RePassword) return View();
                    SecureLoginService.User user = new SecureLoginService.User
                    {
                        Login = Server.HtmlEncode(model.Login),
                        Password = Server.HtmlEncode(model.Password)
                    };
                    SecureLoginService.Response response = client.AddUser(user);
                    if (response.Type == SecureLoginService.Types.AccountCreated)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                }
            }
            catch(Exception ex)
            {
                return View();
            }
            return View();
        }
    }
}