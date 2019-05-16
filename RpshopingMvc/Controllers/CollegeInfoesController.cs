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
    public class CollegeInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Sidebar(string name = "学院管理")
        {
            ViewBag.Sidebar = name;

        }
        // GET: CollegeInfoes
        public ActionResult Index(string filter, int page = 1)
        {


            Sidebar();
            var m = from e in db.CollegeInfo
                    select new CollegeInfoshow
                    {
                        ID = e.ID,
                        Number = e.Number,
                        Title = e.Title
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.Title.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
        }

        // GET: CollegeInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeInfo collegeInfo = db.CollegeInfo.Find(id);
            if (collegeInfo == null)
            {
                return HttpNotFound();
            }
            return View(collegeInfo);
        }

        // GET: CollegeInfoes/Create
        public ActionResult Create()
        {
            var model = new CollegeInfoshow();
            return View(model);
        }

        // POST: CollegeInfoes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CollegeInfoshow collegeInfo)
        {
            if (ModelState.IsValid)
            {
                var model = new CollegeInfo
                {
                    Title = collegeInfo.Title,
                    Number = collegeInfo.Number,
                    Info = collegeInfo.Info
                };
                db.CollegeInfo.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collegeInfo);
        }

        // GET: CollegeInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeInfo model = db.CollegeInfo.Find(id);
            var models = new CollegeInfoshow
            {
                ID = model.ID,
                Info = model.Info,
                Number = model.Number,
                Title = model.Title
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: CollegeInfoes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关   HttpUtility.HtmlEncode(
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CollegeInfoshow collegeInfo)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(collegeInfo).State = EntityState.Modified;
                var t = db.CollegeInfo.FirstOrDefault(s => s.ID == collegeInfo.ID);
                t.Info = collegeInfo.Info;
                t.Number = collegeInfo.Number;
                t.Title = collegeInfo.Title;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collegeInfo);
        }

        // GET: CollegeInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeInfo collegeInfo = db.CollegeInfo.Find(id);
            if (collegeInfo == null)
            {
                return HttpNotFound();
            }
            return View(collegeInfo);
        }

        // POST: CollegeInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CollegeInfo collegeInfo = db.CollegeInfo.Find(id);
            db.CollegeInfo.Remove(collegeInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //获取学院管理信息
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetCollegeInfoesList(int? page = 1, int? pageSize = 20)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;


                //拼接参数
                SqlParameter[] parameters = {
                                new SqlParameter("@starpagesize", SqlDbType.Int),
                                new SqlParameter("@endpagesize", SqlDbType.Int)
                             };
                parameters[0].Value = starpagesize;
                parameters[1].Value = endpagesize;
                string sqlstr = string.Empty;
                sqlstr = string.Format(@"SELECT * FROM (SELECT  CAST(ROW_NUMBER() over(order by COUNT(a.ID) DESC) AS INTEGER) AS Ornumber, a.ID,a.Number,a.Title,a.Code
                                            FROM dbo.CollegeInfoes a 
                                            GROUP BY a.ID,a.Number,a.Title,a.Code) t 
                                                WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize ORDER BY t.Number");
                List<CollegeInfoshow> data = db.Database.SqlQuery<CollegeInfoshow>(sqlstr, parameters).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //获取详情
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetCollegeInfoByID(string filestr, string value)
        {
            try
            {
                string sql = "";
                if (filestr == "0")
                {
                    sql= string.Format(@"select * from CollegeInfoes where ID=@value");
                }
                else if (filestr == "1")
                {
                    sql = string.Format(@"select * from CollegeInfoes where Code=@value");
                }
                //拼接参数
                SqlParameter[] parameters = {
                                new SqlParameter("@value", SqlDbType.NVarChar)
                             };
                parameters[0].Value = value;
                List<tempcollegeinfo> data = db.Database.SqlQuery<tempcollegeinfo>(sql, parameters).ToList();
                if (data.Count>0)
                {
                    string tempinfocontent = HttpUtility.UrlEncode(data[0].info); //Server.HtmlEncode(data[0].info);
                    var returns = new {
                        info= tempinfocontent
                    };
                    return Json(Comm.ToJsonResult("Success", "成功", returns), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Comm.ToJsonResult("Fail","失败"), JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        public class tempcollegeinfo {
            public string info { get; set; }//详情内容
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
