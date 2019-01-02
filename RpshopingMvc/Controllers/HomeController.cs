using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using RpshopingMvc.Models;

namespace RpshopingMvc.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SynchronizationUser()
        {
            try
            {
                //var path = $@"e:\user.json";
                //var sourceContent = System.IO.File.ReadAllText(path);
                //var sourceobjects = JArray.Parse(sourceContent);
                //foreach (var item in sourceobjects)
                //{
                //    tb_userinfo umodel = new tb_userinfo();
                //    umodel.UserID= item["_id"].ToString();
                //    umodel.Integral = 0;
                //    umodel.RewardMoney = 0;
                //    umodel.FirstCharge = item["firstcharge"].ToString() == "是" ? Enums.Enums.YesOrNo.Yes : Enums.Enums.YesOrNo.No;
                //    db.tb_userinfos.Add(umodel);
                //    db.SaveChanges();
                //}
            }
            catch (Exception ex)
            {

            }
            return View("Index");
        }
    }
}