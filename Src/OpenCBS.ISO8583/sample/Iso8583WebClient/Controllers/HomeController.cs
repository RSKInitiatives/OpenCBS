using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Free.iso8583.example.model;

namespace Iso8583WebClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["requestParams"] != null)
            {
                Dictionary<String, Object> reqParams = Session["requestParams"] as Dictionary<String, Object>;
                ViewBag.ServerHost = reqParams["ServerHost"];
                ViewBag.ServerPort = reqParams["ServerPort"];
                ViewBag.IsSSL = reqParams["IsSSL"];
                ViewBag.EditedItem = reqParams["EditedItem"];
                Session.Remove("requestParams");
            }
            else
            {
                ViewBag.ServerHost = "localhost";
                ViewBag.ServerPort = "3107";
                ViewBag.IsSSL = true;
                ViewBag.EditedItem = "Transfer Inquiry";
            }
            
            ViewBag.ModelJson = JsonConvert.SerializeObject(Session["model"], Formatting.Indented,
                 new ByteArrayConverter(), new DateTimeConverter());
            
            return View();
        }
    }
}
