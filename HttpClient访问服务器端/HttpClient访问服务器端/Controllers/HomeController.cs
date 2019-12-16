using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HttpClient请求的服务器.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            if (userName == "admin" && password == "123")
            {
                return Json("ok");
            }
            else
            {
                return Json("error");
            }
        }

        [HttpPost]
        public ActionResult Login2(Logion2Request data)
        {
           // dynamic data = JsonConvert.DeserizlizeObject<dynamic>(Content);
            string userName = data.userName;
            string password = data.passowrd;
            if (userName == "admin" && password == "123")
            {
                return Json(data);
            }
            else
            {
                return Json(data);
            }
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFile file)
        {
            // dynamic data = JsonConvert.DeserizlizeObject<dynamic>(Content);
            string userName = Request.Headers["UaerName"];
            string password = Request.Headers["Password"];
            if (userName == "admin" && password == "123")
            {
                file.SaveAs(Server.MapPath("~/" + file.FileName));
                return Json("ok");
            }
            else
            {
                return Json("error");
            }
        }

    }


    public class Logion2Request
    {
        public string userName { get; set; }
        public string passowrd { get; set; }
    }
}