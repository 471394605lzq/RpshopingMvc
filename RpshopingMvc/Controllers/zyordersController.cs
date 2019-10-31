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
                if (usmodel==null)
                {
                    return Json(Comm.ToJsonResult("UserIdIsNull", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else if (goodsmodel==null)
                {
                    return Json(Comm.ToJsonResult("GoodsIsNull", "商品不存在"), JsonRequestBehavior.AllowGet);
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
