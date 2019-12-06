using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RpshopingMvc.Models;
using RpshopingMvc.App_Start;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Controllers
{
    public class zyordersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: zyorders
        public ActionResult Index()
        {
            return View(db.zyorder.ToList());
        }

        // GET: zyorders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zyorder zyorder = db.zyorder.Find(id);
            if (zyorder == null)
            {
                return HttpNotFound();
            }
            return View(zyorder);
        }

        // GET: zyorders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: zyorders/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,User_ID,OrderCode,CreateTime,PayTime,total_fee,OrderState,GoodsID,PayType")] zyorder zyorder)
        {
            if (ModelState.IsValid)
            {
                db.zyorder.Add(zyorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zyorder);
        }

        // GET: zyorders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zyorder zyorder = db.zyorder.Find(id);
            if (zyorder == null)
            {
                return HttpNotFound();
            }
            return View(zyorder);
        }

        // POST: zyorders/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,User_ID,OrderCode,CreateTime,PayTime,total_fee,OrderState,GoodsID,PayType")] zyorder zyorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zyorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zyorder);
        }

        // GET: zyorders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            zyorder zyorder = db.zyorder.Find(id);
            if (zyorder == null)
            {
                return HttpNotFound();
            }
            return View(zyorder);
        }

        // POST: zyorders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            zyorder zyorder = db.zyorder.Find(id);
            db.zyorder.Remove(zyorder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddOrder(zyorder model,string usid)
        {
            try
            {
                var usmodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == usid);
                var goodsmodel = db.goods.FirstOrDefault(s => s.ID == model.GoodsID);
                var redmodel = db.RedPpacket.FirstOrDefault(s => s.ID == model.RedID);
                if (usmodel == null)
                {
                    return Json(Comm.ToJsonResult("UserIdIsNull", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else if (goodsmodel == null)
                {
                    return Json(Comm.ToJsonResult("GoodsIsNull", "商品不存在"), JsonRequestBehavior.AllowGet);
                }
                else if (redmodel==null)
                {
                    return Json(Comm.ToJsonResult("RedIsNull", "红包不可用"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ordercode = WxPayApi.GenerateOutTradeNo();
                    model.User_ID = usmodel.ID;
                    model.OrderCode = ordercode;
                    model.OrderState = GoodsOrderState.StayPay;
                    model.CreateTime = DateTime.Now;
                    model.PayTime = "";
                    model.ExpressCode = "";
                    model.total_fee = (goodsmodel.zkprice * model.GoodsNumber) + goodsmodel.Postage;
                    db.zyorder.Add(model);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "添加成功",model.OrderCode), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "获取失败", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 获取待付款订单信息
        /// </summary>
        /// <param name="ordercode">待付款订单号</param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult GetStayPayOrder(string ordercode)
        {
            try
            {
                var returndata = from a in db.zyorder
                                 join b in db.goods on a.GoodsID equals b.ID
                                 join c in db.DeliveryAddress on a.DeliveryAddressID equals c.ID
                                 where a.OrderCode==ordercode
                                 select new ordermodel
                                 {
                                     CreateTime = a.CreateTime,
                                     DA_DetailedAddress = c.DA_Province + c.DA_City + c.DA_Town + c.DA_DetailedAddress,
                                     DA_Name = c.DA_Name,
                                     DA_Phone = c.DA_Phone,
                                     GoodsName = b.GoodsName,
                                     GoodsNumber = a.GoodsNumber,
                                     ImagePath = b.ImagePath,
                                     OrderCode = a.OrderCode,
                                     Postage = a.Postage,
                                     Specs = b.Specs,
                                     total_fee = a.total_fee,
                                     zkprice = b.zkprice,
                                     DeliveryAddressID = c.ID
                                 };
                return Json(Comm.ToJsonResult("Success", "获取成功", returndata), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "操作失败", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //取消订单
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult CancelZYOrder(string ordercode)
        {
            try
            {
                var zymodel = db.zyorder.FirstOrDefault(s => s.OrderCode == ordercode);
                if (zymodel == null)
                {
                    return Json(Comm.ToJsonResult("NotFind", "订单不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    zymodel.OrderState = GoodsOrderState.Cancel;
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "取消成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "操作失败", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //删除订单
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult DeleteZYOrder(string ordercode)
        {
            try
            {
                var zymodel = db.zyorder.FirstOrDefault(s => s.OrderCode == ordercode);
                if (zymodel == null)
                {
                    return Json(Comm.ToJsonResult("NotFind", "订单不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var usmodel = db.tb_userinfos.FirstOrDefault(s => s.ID == zymodel.User_ID);
                    var payorder = db.PayOrders.FirstOrDefault(s => s.RelationID == zymodel.ID && s.User_ID == usmodel.UserID);
                    db.zyorder.Remove(zymodel);
                    db.PayOrders.Remove(payorder);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "取消成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "操作失败", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        private class ordermodel {
            public string OrderCode { get; set; }
            public DateTime CreateTime { get; set; }
            public decimal total_fee { get; set; }
            public int GoodsNumber { get; set; }
            public int Postage { get; set; }
            public string DA_Name { get; set; }
            public string DA_Phone { get; set; }
            public string DA_DetailedAddress { get; set; }
            public string GoodsName { get; set; }
            public decimal zkprice { get; set; }
            public string ImagePath { get; set; }
            public string Specs { get; set; }
            public int DeliveryAddressID { get; set; }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
