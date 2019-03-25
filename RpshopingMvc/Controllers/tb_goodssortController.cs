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
    public class tb_goodssortController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Sidebar(string name = "商品分类管理")
        {
            ViewBag.Sidebar = name;

        }
        // GET: tb_goodssort
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.tb_goodssort
                    select new tb_goodssortshow
                    {
                        ID = e.ID,
                        Grade = e.Grade,
                        ImagePath = e.ImagePath,
                        ParentID = e.ParentID,
                        SortName = e.SortName,
                        SortIndex = e.SortIndex
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.SortName.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
        }

        // GET: tb_goodssort/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_goodssort tb_goodssort = db.tb_goodssort.Find(id);
            if (tb_goodssort == null)
            {
                return HttpNotFound();
            }
            return View(tb_goodssort);
        }

        // GET: tb_goodssort/Create
        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.tb_goodssort.Where(s => s.Grade == 1 && s.ParentID != s.ID), "ID", "SortName");
            ViewBag.Grade = new SelectList(db.GoodsSortGrade.OrderBy(s => s.Index), "ID", "Name");
            Sidebar();
            var model = new tb_goodssortview();
            return View(model);
        }

        // POST: tb_goodssort/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tb_goodssortview tb_goodssort)
        {
            if (ModelState.IsValid)
            {

                var model = new tb_goodssort
                {
                    SortName = tb_goodssort.SortName,
                    Grade = tb_goodssort.Grade,
                    ImagePath = string.Join(",", tb_goodssort.ImagePath.Images),
                    ParentID = tb_goodssort.Grade == 1 ? 0 : tb_goodssort.ParentID,
                    SortIndex = tb_goodssort.SortIndex
                };
                db.tb_goodssort.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_goodssort);
        }

        // GET: tb_goodssort/Edit/5
        public ActionResult Edit(int? id)
        {
            Sidebar();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_goodssort model = db.tb_goodssort.Find(id);
            var models = new tb_goodssortview
            {
                ID = model.ID,
                SortName = model.SortName,
                Grade = model.Grade,
                ParentID = model.ParentID,
                SortIndex = model.SortIndex,
            };
            models.ImagePath.Images = model.ImagePath?.Split(',') ?? new string[0];
            ViewBag.ParentID = new SelectList(db.tb_goodssort.Where(s => s.Grade == 1 && s.ParentID != s.ID), "ID", "SortName", models.ParentID);
            ViewBag.Grade = new SelectList(db.GoodsSortGrade.OrderBy(s => s.Index), "ID", "Name", models.Grade);

            if (models == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: tb_goodssort/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_goodssortview tb_goodssort)
        {
            if (ModelState.IsValid)
            {
                var t = db.tb_goodssort.FirstOrDefault(s => s.ID == tb_goodssort.ID);
                t.ID = tb_goodssort.ID;
                t.SortName = tb_goodssort.SortName;
                t.Grade = tb_goodssort.Grade;
                //t.EnterpriseID = AccontData.EnterpriseID;
                t.ParentID = tb_goodssort.Grade == 1 ? 0 : tb_goodssort.ParentID;
                t.SortIndex = tb_goodssort.SortIndex;
                t.ImagePath = string.Join(",", tb_goodssort.ImagePath.Images);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(tb_goodssort);
        }

        // GET: tb_goodssort/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_goodssort tb_goodssort = db.tb_goodssort.Find(id);
            if (tb_goodssort == null)
            {
                return HttpNotFound();
            }
            return View(tb_goodssort);
        }

        // POST: tb_goodssort/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_goodssort tb_goodssort = db.tb_goodssort.Find(id);
            db.tb_goodssort.Remove(tb_goodssort);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //同步分类选品库ID
        public ActionResult SetFavoritesIDToGoodsSort()
        {
            SqlParameter[] parameters = {
                    };
            string sql = @"SELECT fa.FavoritesID,s.ID,s.fname as Name FROM (SELECT a.SortName+t.SortName AS fname,t.ID FROM (SELECT * FROM dbo.tb_goodssort WHERE Grade=2) t
                            JOIN (SELECT *FROM dbo.tb_goodssort WHERE Grade=1) a ON a.ID=t.ParentID) s
                            JOIN dbo.tb_Favorites fa ON fa.Name=s.fname ";
            List<SortModel> data = db.Database.SqlQuery<SortModel>(sql, parameters).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                int trempid = data[i].ID;
                var t = db.tb_goodssort.FirstOrDefault(s => s.ID == trempid);
                t.FID = data[i].FavoritesID;
                db.SaveChanges();
            }
            return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
        }
        public class SortModel {
            public int ID { get; set; }
            public string Name { get; set; }
            public string FavoritesID { get; set; }

        }
        //获取选品库列表
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetGoodsSortList(int parentid, bool isparentid, int page = 1, int pageSize = 20)
        {
            var query = from a in db.tb_goodssort
                        select new
                        {
                            ID = a.ID,
                            Name = a.SortName,
                            Index = a.SortIndex,
                            Image = a.ImagePath,
                            Parentid = a.ParentID,
                            Grade=a.Grade,
                            Fid=a.FID
                        };
            if (isparentid)
            {
                query = query.Where(s => s.Parentid == parentid);
            }
            else {
                query = query.Where(s => s.Grade == 1);
            }
            var paged = query.OrderBy(s => s.Index).ToPagedList(page, pageSize);
            return Json(Comm.ToJsonResultForPagedList(paged, paged), JsonRequestBehavior.AllowGet);
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
