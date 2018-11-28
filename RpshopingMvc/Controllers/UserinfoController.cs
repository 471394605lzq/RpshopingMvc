using RpshopingMvc.App_Start;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RpshopingMvc.Controllers
{
    public class UserinfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddUserInfo(tb_userinfo model)
        {
            try
            {
                if (db.tb_userinfos.Any(s => s.UserID == model.UserID))
                {
                    return Json(Comm.ToJsonResult("Error", "用户已存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.Balance = 0;
                    model.FirstCharge = Enums.Enums.YesOrNo.No;
                    model.RewardMoney = 0;
                    model.Integral = 0;
                    db.tb_userinfos.Add(model);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}