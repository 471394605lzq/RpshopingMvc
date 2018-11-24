using RpshopingMvc.App_Start;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RpshopingMvc.Controllers
{
    public class RechargeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Recharge
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddRecharge(tb_Recharge model)
        {
            try
            {
                //检查数据库中是否存在该用户信息
                var thisuser = db.tb_userinfos.Where(s => s.UserID == model.UserID).FirstOrDefault();
                //检验话术分类是否存在 
                if (thisuser == null)
                {
                    return Json(Comm.ToJsonResult("Error", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var addmodel = new tb_Recharge
                    {
                        CreateDateTime = DateTime.Now,
                        give = 0,
                        paytype = model.paytype,
                        R_Money = model.RechargeType.GetHashCode(),
                        U_ID = thisuser.ID,
                        RechargeType = model.RechargeType,
                        UserID = model.UserID
                    };
                    db.tb_Recharges.Add(addmodel);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "新增成功"), JsonRequestBehavior.AllowGet);
            }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}