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
    public class FeedbacksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Feedbacks
        public ActionResult Index()
        {
            return View(db.Feedback.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feedbacks/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,Titlestr,Contentstr,Contactway")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedback.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Titlestr,Contentstr,Contactway")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedback.Find(id);
            db.Feedback.Remove(feedback);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddFeedbackInfo(Feedback model)
        {
            try
            {
                if (!db.tb_userinfos.Any(s => s.UserID == model.UserID)&& model.UserID!="1")
                {
                    return Json(Comm.ToJsonResult("Error", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    model.BackTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    db.Feedback.Add(model);
                    int row = db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取用户反馈列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetUserFeedBack(string userids, int? page = 1, int? pageSize = 20)
        {
            try
            {
                var usermodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == userids);
                if (usermodel == null)
                {
                    return Json(Comm.ToJsonResult("nofind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                    int endpagesize = page.Value * pageSize.Value;
                    //拼接参数
                    SqlParameter[] parameters = {
                        new SqlParameter("@userid", SqlDbType.NVarChar),
                        new SqlParameter("@starpagesize", SqlDbType.Int),
                        new SqlParameter("@endpagesize", SqlDbType.Int)
                    };
                    parameters[0].Value = userids;
                    parameters[1].Value = starpagesize;
                    parameters[2].Value = endpagesize;
                    string sqlstr = string.Empty;
                    sqlstr = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.Titlestr,g.Contentstr,g.Contactway,g.BackTime,g.ID FROM dbo.Feedbacks g
                                                WHERE g.UserID=@userid
                                                GROUP BY g.ID,g.Titlestr,g.Contentstr,g.Contactway,g.BackTime) t 
                                                WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize ORDER BY t.ID DESC");
                    List<Feedbackshow> data = db.Database.SqlQuery<Feedbackshow>(sqlstr, parameters).ToList();
                    return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        public class Feedbackshow
        {
            public int ID { get; set; }
            public string UserID { get; set; }
            public string Titlestr { get; set; }
            public string Contentstr { get; set; }
            public string Contactway { get; set; }
            public string BackTime { get; set; }
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
