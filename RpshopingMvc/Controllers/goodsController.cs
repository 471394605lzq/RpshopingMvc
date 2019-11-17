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
    public class goodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Sidebar(string name = "自营商品")
        {
            ViewBag.Sidebar = name;
        }
        // GET: goods
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.goods
                    select new goodsshow
                    {
                        ID = e.ID,
                        ImagePath = e.ImagePath,
                        Brokerage = e.Brokerage,
                        GoodsName = e.GoodsName,
                        IncomeRatio = e.IncomeRatio,
                        Price = e.Price,
                        SalesVolume = e.SalesVolume,
                        BrokerageExplain = e.BrokerageExplain,
                        Code = e.Code,
                        Postage = e.Postage,
                        zkprice = e.zkprice,
                        Specs=e.Specs
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.GoodsName.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
        }

        // GET: goods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goods goods = db.goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        // GET: goods/Create
        public ActionResult Create()
        {
            ViewBag.Brand = new SelectList(db.Brand, "ID", "Name");
            var model = new goodsview();
            return View(model);
        }

        // POST: goods/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(goodsview goods)
        {
            if (ModelState.IsValid)
            {
                var model = new goods
                {
                    GoodsName = goods.GoodsName,
                    Brokerage = goods.Brokerage,
                    zkprice = goods.zkprice,
                    BrokerageExplain = goods.BrokerageExplain,
                    Code = goods.Code,
                    DetailPath = goods.DetailPath,
                    ImagePath = string.Join(",", goods.ImagePath.Images),
                    IncomeRatio = goods.IncomeRatio,
                    Postage = goods.Postage,
                    SalesVolume = goods.SalesVolume,
                    SmallImages = string.Join(",", goods.SamllImages.Images),
                    Price = goods.Price,
                    ByIndex = goods.ByIndex,
                    GoodsState = goods.GoodsState,
                    Stock = goods.Stock,
                    Brand = goods.Brand,
                    GetPath = goods.GetPath,
                    IsRecommend = goods.IsRecommend,
                    Specs = goods.Specs
                };
                db.goods.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goods);
        }

        // GET: goods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goods model = db.goods.Find(id);
            var models = new goodsview
            {
                ID = model.ID,
                GoodsName = model.GoodsName,
                Price = model.Price,
                Stock = model.Stock,
                Brokerage = model.Brokerage,
                BrokerageExplain = model.BrokerageExplain,
                Code = model.Code,
                zkprice = model.zkprice,
                DetailPath = model.DetailPath,
                IncomeRatio = model.IncomeRatio,
                Postage = model.Postage,
                SalesVolume = model.SalesVolume,
                ByIndex = model.ByIndex,
                GoodsState = model.GoodsState,
                GetPath = model.GetPath,
                IsRecommend = model.IsRecommend,
                Brand = model.Brand,
                Specs = model.Specs
            };
            models.ImagePath.Images = model.ImagePath?.Split(',') ?? new string[0];
            models.SamllImages.Images = model.SmallImages?.Split(',') ?? new string[0];
            ViewBag.GoodsType = new SelectList(db.goodstype.OrderBy(s => s.ID), "ID", "Name", models.GoodsType);
            ViewBag.Brand= new SelectList(db.Brand.OrderBy(s => s.ID), "ID", "Name", models.Brand);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: goods/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(goodsview goods)
        {
            if (ModelState.IsValid)
            {
                var t = db.goods.FirstOrDefault(s => s.ID == goods.ID);
                t.ID = goods.ID;
                t.GoodsName = goods.GoodsName;
                t.ImagePath = string.Join(",", goods.ImagePath.Images);
                t.SmallImages = string.Join(",", goods.SamllImages.Images);
                t.Stock = goods.Stock;
                t.Brokerage = goods.Brokerage;
                t.BrokerageExplain = goods.BrokerageExplain;
                t.Code = goods.Code;
                t.DetailPath = goods.DetailPath;
                t.IncomeRatio = goods.IncomeRatio;
                t.Postage = goods.Postage;
                t.Price = goods.Price;
                t.SalesVolume = goods.SalesVolume;
                t.zkprice = goods.zkprice;
                t.ByIndex = goods.ByIndex;
                t.GoodsState = goods.GoodsState;
                t.Brand = goods.Brand;
                t.Specs = goods.Specs;
                t.IsRecommend = goods.IsRecommend;
                t.GetPath = goods.GetPath;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(goods);
        }

        // GET: goods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goods goods = db.goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        // POST: goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            goods goods = db.goods.Find(id);
            db.goods.Remove(goods);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        //获取自营商品
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetGoodsList(string file, int type, int? page = 1, int? pageSize = 20)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;
                string wherestr = " ";
                if (type != 0 && !string.IsNullOrWhiteSpace(file))
                {
                    wherestr = "where g.GoodsName='" + file + "' and g.GoodsType=" + type;
                }
                else if (type != 0 && string.IsNullOrWhiteSpace(file))
                {
                    wherestr = "where g.GoodsType=" + type;
                }
                else if (type == 0 && !string.IsNullOrWhiteSpace(file))
                {
                    wherestr = "where g.GoodsName='" + file + "' ";
                }
                string sql = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.* FROM dbo.goods g {0}
                                        GROUP BY g.GoodsName,g.Code,g.Price,g.Price,g.zkprice,g.ImagePath,g.SmallImages,g.DetailPath,g.SalesVolume,g.IncomeRatio,g.Brokerage,
                                        g.BrokerageExplain,g.Postage,g.Stock,g.ByIndex,g.GoodsState,g.ID,g.IsRecommend,g.GetPath,g.Brand,g.Specs
                                        ) t WHERE GoodsState=0 AND t.Ornumber > {1} AND t.Ornumber<={2} ORDER BY ByIndex DESC", wherestr, starpagesize, endpagesize);
                List<goodsshow> data = db.Database.SqlQuery<goodsshow>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //获取自营推荐商品
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetTJGoodsList(int? page = 1, int? pageSize = 20)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;
                string sql = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.* FROM dbo.goods g where  IsRecommend=1
                                        GROUP BY g.GoodsName,g.Code,g.Price,g.Price,g.zkprice,g.ImagePath,g.SmallImages,g.DetailPath,g.SalesVolume,g.IncomeRatio,g.Brokerage,
                                        g.BrokerageExplain,g.Postage,g.Stock,g.ByIndex,g.GoodsState,g.ID,g.IsRecommend,g.GetPath,g.Brand,g.Specs
                                        ) t WHERE t.Ornumber > {0} AND t.Ornumber<={1} ORDER BY ByIndex DESC", starpagesize, endpagesize);
                List<goodsshow> data = db.Database.SqlQuery<goodsshow>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //获取商品分类
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult getgoodstype()
        {
            try
            {
                string sql = string.Format(@"SELECT ID,Name FROM dbo.goodstypes");
                List<goodstypeshow> data = db.Database.SqlQuery<goodstypeshow>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //获取商品当前分类
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult getgoodsthistype(int id) {
            try
            {
                string sql = string.Format(@"SELECT gt.ID,gs.Name FROM dbo.goodstypetemps gt
                                            INNER JOIN dbo.goodstypes gs ON gt.goodstypeid=gs.ID
                                            WHERE gt.goodsid={0}", id);
                List<goodstypeshow> data = db.Database.SqlQuery<goodstypeshow>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //获取商品当前分类
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult addgoodstype(int goodsid, int goodstypeid)
        {
            try
            {
                goodstypetemp model = db.goodstypetemp.FirstOrDefault(s => s.goodsid == goodsid && s.goodstypeid == goodstypeid);
                if (model != null)
                {
                    return Json(Comm.ToJsonResult("Exist", "该商品已经添加了此分类"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    goodstypetemp tempmodel = new goodstypetemp();
                    tempmodel.goodstypeid = goodstypeid;
                    tempmodel.goodsid = goodsid;
                    db.goodstypetemp.Add(tempmodel);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //删除商品分类
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult deletegoodstype(int id)
        {
            try
            {
                goodstypetemp model = db.goodstypetemp.FirstOrDefault(s => s.ID == id);
                if (model == null)
                {
                    return Json(Comm.ToJsonResult("NotFind", "商品分类不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.goodstypetemp.Remove(model);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "删除成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //获取商品服务
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult getgoodservice()
        {
            try
            {
                string sql = string.Format(@"SELECT ID,Name FROM dbo.zygoodservices");
                List<goodstypeshow> data = db.Database.SqlQuery<goodstypeshow>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //获取商品当前服务
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult getgoodsthiservice(int id)
        {
            try
            {
                string sql = string.Format(@"SELECT gt.ID,gs.Name FROM dbo.zygoodservicetemps gt
                                            INNER JOIN dbo.zygoodservices gs ON gt.serviceid=gs.ID
                                            WHERE gt.goodsid={0}", id);
                List<goodstypeshow> data = db.Database.SqlQuery<goodstypeshow>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //添加商品当前服务
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult addgoodservice(int goodsid, int goodserviceid)
        {
            try
            {
                zygoodservicetemp model = db.zygoodservicetemp.FirstOrDefault(s => s.goodsid == goodsid && s.serviceid == goodserviceid);
                if (model != null)
                {
                    return Json(Comm.ToJsonResult("Exist", "该商品已经添加了此服务"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    zygoodservicetemp tempmodel = new zygoodservicetemp();
                    tempmodel.serviceid = goodserviceid;
                    tempmodel.goodsid = goodsid;
                    db.zygoodservicetemp.Add(tempmodel);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //删除商品服务
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult deletegoodservice(int id)
        {
            try
            {
                zygoodservicetemp model = db.zygoodservicetemp.FirstOrDefault(s => s.ID == id);
                if (model == null)
                {
                    return Json(Comm.ToJsonResult("NotFind", "商品服务不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.zygoodservicetemp.Remove(model);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "删除成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }


        //获取商品活动
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult getgoodactivity()
        {
            try
            {
                string sql = string.Format(@"SELECT ID,Name FROM dbo.zyactivities");
                List<goodstypeshow> data = db.Database.SqlQuery<goodstypeshow>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //获取商品当前参加的活动
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult getgoodsthisactivity(int id)
        {
            try
            {
                string sql = string.Format(@"SELECT gt.ID,gs.Name FROM dbo.zyactivitygoods gt
                                            INNER JOIN dbo.zyactivities gs ON gt.activityid=gs.ID
                                            WHERE gt.goodsid={0}", id);
                List<goodstypeshow> data = db.Database.SqlQuery<goodstypeshow>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //添加商品当前活动
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult addgoodactivity(int goodsid, int goodactivityid, int postage, decimal acctivityprice, string remark)
        {
            try
            {
                zyactivitygoods model = db.zyactivitygoods.FirstOrDefault(s => s.goodsid == goodsid && s.activityid == goodactivityid);
                if (model != null)
                {
                    return Json(Comm.ToJsonResult("Exist", "该商品已经添加了此服务"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    zyactivitygoods models = new zyactivitygoods();
                    models.activityid = goodactivityid;
                    models.goodsid = goodsid;
                    models.acrivityprice = acctivityprice;
                    models.Postage = postage;
                    models.remark = remark;
                    db.zyactivitygoods.Add(models);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //删除商品活动
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult deletegoodactivity(int id)
        {
            try
            {
                zyactivitygoods model = db.zyactivitygoods.FirstOrDefault(s => s.ID == id);
                if (model == null)
                {
                    return Json(Comm.ToJsonResult("NotFind", "商品服务不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.zyactivitygoods.Remove(model);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "删除成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取自营商品详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult GetGoodsDetail(int id)
        {
            try
            {
                goods g = db.goods.FirstOrDefault(s => s.ID == id);
                if (g != null)
                {
                    return Json(Comm.ToJsonResult("Success", "获取成功", g), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Comm.ToJsonResult("NotFind", "商品不存在"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "获取失败"), JsonRequestBehavior.AllowGet);
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
