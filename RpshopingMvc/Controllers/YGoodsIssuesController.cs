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
using System.Data.SqlClient;

namespace RpshopingMvc.Controllers
{
    public class YGoodsIssuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: YGoodsIssues
        public ActionResult Index()
        {
            return View(db.YGoodsIssue.ToList());
        }

        // GET: YGoodsIssues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoodsIssue yGoodsIssue = db.YGoodsIssue.Find(id);
            if (yGoodsIssue == null)
            {
                return HttpNotFound();
            }
            return View(yGoodsIssue);
        }

        // GET: YGoodsIssues/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YGoodsIssues/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IssueNumber,State,AnnounceTime,AlreadyNumber,SurplusNumber,SumNumber,LuckCode")] YGoodsIssue yGoodsIssue)
        {
            if (ModelState.IsValid)
            {
                db.YGoodsIssue.Add(yGoodsIssue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yGoodsIssue);
        }

        // GET: YGoodsIssues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoodsIssue yGoodsIssue = db.YGoodsIssue.Find(id);
            if (yGoodsIssue == null)
            {
                return HttpNotFound();
            }
            return View(yGoodsIssue);
        }

        // POST: YGoodsIssues/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IssueNumber,State,AnnounceTime,AlreadyNumber,SurplusNumber,SumNumber,LuckCode")] YGoodsIssue yGoodsIssue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yGoodsIssue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yGoodsIssue);
        }

        // GET: YGoodsIssues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoodsIssue yGoodsIssue = db.YGoodsIssue.Find(id);
            if (yGoodsIssue == null)
            {
                return HttpNotFound();
            }
            return View(yGoodsIssue);
        }

        // POST: YGoodsIssues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YGoodsIssue yGoodsIssue = db.YGoodsIssue.Find(id);
            db.YGoodsIssue.Remove(yGoodsIssue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //获取云购产品期数
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetYGoodsList(string state,int type, int? page = 1, int? pageSize = 20)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;
                string wherestr = " ";
                if (type != 0 && !string.IsNullOrWhiteSpace(state))
                {
                    wherestr = "where yi.State='" + state + "' and yg.type=" + type;
                }
                else if (type != 0 && string.IsNullOrWhiteSpace(state))
                {
                    wherestr = "where yg.type=" + type;
                }
                else if (type == 0 && !string.IsNullOrWhiteSpace(state))
                {
                    wherestr = "where yi.State='" + state + "' ";
                }
                //拼接参数
//                SqlParameter[] parameters = {
//                        new SqlParameter("@where", SqlDbType.NVarChar),
//                        new SqlParameter("@starpagesize", SqlDbType.Int),
//                        new SqlParameter("@endpagesize", SqlDbType.Int)
//                    };
//                parameters[0].Value = wherestr;
//                parameters[1].Value = starpagesize;
//                parameters[2].Value = endpagesize;
//                string sqlstr = string.Empty;
//                sqlstr = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(yi.ID) DESC) AS INTEGER) AS Ornumber,yi.*,yg.GoodsName,yg.MainImage,yg.Price,CASE WHEN yg.Mark=0 THEN '一元商品' WHEN yg.Mark=1 THEN '五元商品' WHEN
//yg.Mark=2 THEN '十元商品' ELSE '百元商品' END AS Mark FROM dbo.YGoodsIssues yi
//                                        INNER JOIN dbo.YGoods yg ON yg.ID=yi.YGoodsId
//                                         @where
//                                        GROUP BY yi.ID,yi.IssueNumber,yi.State,yi.AnnounceTime,yi.AlreadyNumber,yi.SurplusNumber,yi.SumNumber,yi.LuckCode,
//                                        yi.YGoodsId,yg.GoodsName,yg.MainImage,yg.Mark,yg.Price) t WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize");


                string sql= string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(yi.ID) DESC) AS INTEGER) AS Ornumber,yi.*,yg.GoodsName,yg.MainImage,yg.Price,CASE WHEN yg.Mark=0 THEN '一元商品' WHEN yg.Mark=1 THEN '五元商品' WHEN
yg.Mark=2 THEN '十元商品' ELSE '百元商品' END AS Markstr FROM dbo.YGoodsIssues yi
                                        INNER JOIN dbo.YGoods yg ON yg.ID=yi.YGoodsId
                                         {0}
                                        GROUP BY yi.ID,yi.IssueNumber,yi.State,yi.AnnounceTime,yi.AlreadyNumber,yi.SurplusNumber,yi.SumNumber,yi.LuckCode,yi.IsLock,yi.LuckUserID,
                                        yi.YGoodsId,yg.GoodsName,yg.MainImage,yg.Mark,yg.Price) t WHERE t.Ornumber > {1} AND t.Ornumber<={2}", wherestr, starpagesize, endpagesize);
                List<YGoodsModel> data = db.Database.SqlQuery<YGoodsModel>(sql).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //获取云购产品期数详情
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetYGoodsDetail(int id)
        {
            try
            {
                //拼接参数
                SqlParameter[] parameters = {
                                        new SqlParameter("@id", SqlDbType.Int)
                                    };
                parameters[0].Value = id;
                string sqlstr = string.Empty;
                string sql = string.Format(@"SELECT yi.*,yg.GoodsName,yg.MainImage,yg.Price,
                                            CASE WHEN yg.Mark=0 THEN 1 WHEN yg.Mark=1 THEN 5 WHEN
yg.Mark=2 THEN 10 ELSE 10 END AS Mark,yg.SamllImage,yg.Info 
                                            FROM dbo.YGoodsIssues yi
                                                                                    INNER JOIN dbo.YGoods yg ON yg.ID=yi.YGoodsId
                                                                                     WHERE yi.ID=@id");
                List<YGoodsModel> data = db.Database.SqlQuery<YGoodsModel>(sql, parameters).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //获取云购产品期数详情
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetNextID(int id)
        {
            try
            {
                var idata = db.YGoodsIssue.FirstOrDefault(s => s.ID == id);
                var tempnextid = 0;
                if (idata != null && idata.State == "已揭晓")
                {
                    var tmodel = db.YGoodsIssue.FirstOrDefault(s => s.YGoodsId == idata.YGoodsId);
                    tempnextid = tmodel.ID;
                }
                else
                {
                    tempnextid = 0;
                }
                return Json(Comm.ToJsonResult("Success", "成功", tempnextid), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //云购商品
        public class YGoodsModel {
            public int ID { get; set; }
            public int IssueNumber { get; set; }
            public string State { get; set; }
            public string AnnounceTime { get; set; }
            public int AlreadyNumber { get; set; }
            public int SurplusNumber { get; set; }
            public int SumNumber { get; set; }
            public string LuckCode { get; set; }
            public int YGoodsId { get; set; }
            public string GoodsName { get; set; }
            public string MainImage { get; set; }
            public decimal Price { get; set; }
            public string Markstr { get; set; }
            public int Mark { get; set; }
            public string SamllImage { get; set; }
            public string Info { get; set; }
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
