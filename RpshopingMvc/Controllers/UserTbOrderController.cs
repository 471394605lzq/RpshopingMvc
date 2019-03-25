using RpshopingMvc.App_Start;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Controllers
{
    public class UserTbOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// 新增用户订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddUserOrder(Tborder model)
        {
            try
            {
                var goods = db.tb_goods.Find(model.GoodsID);
                var us = db.tb_userinfos.FirstOrDefault(s => s.UserID == model.YUserID);
                var usgrade = us.UserGrade;
                decimal temprebatemoney = 0;
                //初级会员
                if (usgrade == UserGrade.Primary)
                {
                    temprebatemoney = decimal.Round((goods.Brokerage * (decimal)0.1), 2);
                }
                //高级会员、运营商、合伙人
                else
                {
                    temprebatemoney = decimal.Round((goods.Brokerage * (decimal)0.5), 2);
                }

                model.BalanceTime = Convert.ToDateTime("1990-01-01");
                model.GoodsImage = goods.ImagePath;
                model.GoodsName = goods.GoodsName;
                model.OrderPrice = goods.Qhprice;
                model.OrderState = TbOrderState.NoBalance;
                model.UserID = us.ID;
                model.RebateMoney = temprebatemoney;
                model.OrderTime = DateTime.Now;
                db.Tborder.Add(model);
                db.SaveChanges();
                return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 搜索结果新增用户订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddUserOrderFromSearch(Tborder model)
        {
            try
            {
                //var goods = db.tb_goods.Find(model.GoodsID);
                var us = db.tb_userinfos.FirstOrDefault(s => s.UserID == model.YUserID);
                var usgrade = us.UserGrade;
                decimal temprebatemoney = 0;
                //初级会员
                if (usgrade == UserGrade.Primary)
                {
                    temprebatemoney = decimal.Round((model.RebateMoney * (decimal)0.1), 2);
                }
                //高级会员、运营商、合伙人
                else
                {
                    temprebatemoney = decimal.Round((model.RebateMoney * (decimal)0.5), 2);
                }
                model.BalanceTime = Convert.ToDateTime("1990-01-01");
                model.OrderState = TbOrderState.NoBalance;
                model.UserID = us.ID;
                model.OrderTime = DateTime.Now;
                model.RebateMoney = temprebatemoney;
                db.Tborder.Add(model);
                db.SaveChanges();
                return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}