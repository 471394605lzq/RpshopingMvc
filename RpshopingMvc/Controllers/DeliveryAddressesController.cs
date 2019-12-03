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

namespace RpshopingMvc.Controllers
{
    public class DeliveryAddressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DeliveryAddresses
        public ActionResult Index()
        {
            return View(db.DeliveryAddress.ToList());
        }

        // GET: DeliveryAddresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryAddress deliveryAddress = db.DeliveryAddress.Find(id);
            if (deliveryAddress == null)
            {
                return HttpNotFound();
            }
            return View(deliveryAddress);
        }

        // GET: DeliveryAddresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryAddresses/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DA_Name,DA_Phone,DA_Province,DA_City,DA_Town,DA_DetailedAddress,DA_ZipCode,DA_IsDefault,U_ID")] DeliveryAddress deliveryAddress)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryAddress.Add(deliveryAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deliveryAddress);
        }

        // GET: DeliveryAddresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryAddress deliveryAddress = db.DeliveryAddress.Find(id);
            if (deliveryAddress == null)
            {
                return HttpNotFound();
            }
            return View(deliveryAddress);
        }

        // POST: DeliveryAddresses/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DA_Name,DA_Phone,DA_Province,DA_City,DA_Town,DA_DetailedAddress,DA_ZipCode,DA_IsDefault,U_ID")] DeliveryAddress deliveryAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deliveryAddress);
        }

        // GET: DeliveryAddresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryAddress deliveryAddress = db.DeliveryAddress.Find(id);
            if (deliveryAddress == null)
            {
                return HttpNotFound();
            }
            return View(deliveryAddress);
        }

        // POST: DeliveryAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeliveryAddress deliveryAddress = db.DeliveryAddress.Find(id);
            db.DeliveryAddress.Remove(deliveryAddress);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 增加收货地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddDeliveryAddress(DeliveryAddress model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.U_ID))
                {
                    return Json(Comm.ToJsonResult("UserIdIsNull", "用户登录失效"), JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrWhiteSpace(model.DA_Name))
                {
                    return Json(Comm.ToJsonResult("NameIsNull", "收货人姓名不能为空"), JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrWhiteSpace(model.DA_Phone))
                {
                    return Json(Comm.ToJsonResult("PhoneIsNull", "收货人电话不能为空"), JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrWhiteSpace(model.DA_DetailedAddress))
                {
                    return Json(Comm.ToJsonResult("AddressIsNull", "详细地址不能为空"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.DeliveryAddress.Add(model);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "添加成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "获取失败", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult UpdateDeliveryAddress(DeliveryAddress model) {
            try
            {
                if (string.IsNullOrWhiteSpace(model.U_ID))
                {
                    return Json(Comm.ToJsonResult("UserIdIsNull", "用户登录失效"), JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrWhiteSpace(model.DA_Name))
                {
                    return Json(Comm.ToJsonResult("NameIsNull", "收货人姓名不能为空"), JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrWhiteSpace(model.DA_Phone))
                {
                    return Json(Comm.ToJsonResult("PhoneIsNull", "收货人电话不能为空"), JsonRequestBehavior.AllowGet);
                }
                else if (string.IsNullOrWhiteSpace(model.DA_DetailedAddress))
                {
                    return Json(Comm.ToJsonResult("AddressIsNull", "详细地址不能为空"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var oldmodel = db.DeliveryAddress.FirstOrDefault(s => s.U_ID == model.U_ID && s.DA_IsDefault == "1");
                    if (oldmodel != null&& oldmodel.ID!=model.ID)
                    {
                        oldmodel.DA_IsDefault = "0";
                    }
                    var t = db.DeliveryAddress.FirstOrDefault(s => s.ID == model.ID);
                    t.DA_City = model.DA_City;
                    t.DA_DetailedAddress = model.DA_DetailedAddress;
                    t.DA_IsDefault = model.DA_IsDefault;
                    t.DA_Name = model.DA_Name;
                    t.DA_Phone = model.DA_Phone;
                    t.DA_Province = model.DA_Province;
                    t.DA_Town = model.DA_Town;
                    t.DA_ZipCode = model.DA_ZipCode;
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "修改成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "获取失败", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 获取用户默认地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetDefaultDeliveryAddress(string uid) {
            try
            {
                var usmodel = db.tb_userinfos.FirstOrDefault(s=>s.UserID==uid);
                List<RedPacket> redpacketlist = new List<RedPacket>();
                if (usmodel!=null)
                {
                    string getredpacket = string.Format(@"");
                }
                DeliveryAddress model = db.DeliveryAddress.FirstOrDefault(s => s.U_ID == uid && s.DA_IsDefault == "1");
                if (model != null)
                {
                    return Json(Comm.ToJsonResult("Success", "获取成功", model), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Comm.ToJsonResult("NotFind", "没有默认地址"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "获取失败"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 获取编辑地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult GetEditDeliveryAddress(int id)
        {
            try
            {
                DeliveryAddress model = db.DeliveryAddress.FirstOrDefault(s => s.ID == id);
                if (model != null)
                {
                    return Json(Comm.ToJsonResult("Success", "获取成功", model), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Comm.ToJsonResult("NotFind", "没有默认地址"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "获取失败"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 获取用户地址
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetDeliveryAddressList(string uid, int? page = 1, int? pageSize = 20)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;
                string sql = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(d.ID) DESC) AS INTEGER) AS Ornumber,d.* FROM dbo.DeliveryAddresses d where d.U_ID='{2}'
                                        GROUP BY d.ID,d.DA_Name,d.DA_Phone,d.DA_Province,d.DA_City,d.DA_Town,
                                        d.DA_DetailedAddress,d.DA_ZipCode,d.DA_IsDefault,d.U_ID
                                        ) t WHERE t.Ornumber > {0} AND t.Ornumber<={1}", starpagesize, endpagesize,uid);
                List<DeliveryAddressView> data = db.Database.SqlQuery<DeliveryAddressView>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "获取失败"), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 设置默认收货地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult SetDefault(int id)
        {
            try
            {
                var model = db.DeliveryAddress.FirstOrDefault(s => s.ID == id);
                if (model == null)
                {
                    return Json(Comm.ToJsonResult("Error", "设置失败"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var oldmodel = db.DeliveryAddress.FirstOrDefault(s => s.U_ID == model.U_ID && s.DA_IsDefault == "1");
                    if (oldmodel!=null)
                    {
                        oldmodel.DA_IsDefault = "0";
                    }                    
                    model.DA_IsDefault = "1";
                    db.SaveChanges();
                }
                return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "设置失败"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult DeleteDefault(int id)
        {
            try
            {
                var model = db.DeliveryAddress.FirstOrDefault(s => s.ID == id);
                if (model == null)
                {
                    return Json(Comm.ToJsonResult("Error", "删除失败"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.DeliveryAddress.Remove(model);
                    db.SaveChanges();
                }
                return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "删除失败"), JsonRequestBehavior.AllowGet);
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
