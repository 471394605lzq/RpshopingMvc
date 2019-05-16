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
    public class UserInvitationAwardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserInvitationAwards
        public ActionResult Index()
        {
            return View(db.UserInvitationAward.ToList());
        }

        // GET: UserInvitationAwards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInvitationAward userInvitationAward = db.UserInvitationAward.Find(id);
            if (userInvitationAward == null)
            {
                return HttpNotFound();
            }
            return View(userInvitationAward);
        }

        // GET: UserInvitationAwards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInvitationAwards/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,AwardMoney,CreateTime,FromUserID,States")] UserInvitationAward userInvitationAward)
        {
            if (ModelState.IsValid)
            {
                db.UserInvitationAward.Add(userInvitationAward);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userInvitationAward);
        }

        // GET: UserInvitationAwards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInvitationAward userInvitationAward = db.UserInvitationAward.Find(id);
            if (userInvitationAward == null)
            {
                return HttpNotFound();
            }
            return View(userInvitationAward);
        }

        // POST: UserInvitationAwards/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,AwardMoney,CreateTime,FromUserID,States")] UserInvitationAward userInvitationAward)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInvitationAward).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInvitationAward);
        }

        // GET: UserInvitationAwards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInvitationAward userInvitationAward = db.UserInvitationAward.Find(id);
            if (userInvitationAward == null)
            {
                return HttpNotFound();
            }
            return View(userInvitationAward);
        }

        // POST: UserInvitationAwards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInvitationAward userInvitationAward = db.UserInvitationAward.Find(id);
            db.UserInvitationAward.Remove(userInvitationAward);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //获取我的邀请
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetMyInvitation(string userid, int? page = 1, int? pageSize = 20)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;
                var usermodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == userid);
                if (usermodel != null)
                {


                    //拼接参数
                    SqlParameter[] parameters = {
                                new SqlParameter("@userid", SqlDbType.NVarChar),
                                new SqlParameter("@starpagesize", SqlDbType.Int),
                                new SqlParameter("@endpagesize", SqlDbType.Int)
                             };
                    parameters[0].Value = userid;
                    parameters[1].Value = starpagesize;
                    parameters[2].Value = endpagesize;
                    string sqlstr = string.Empty;
                    sqlstr = string.Format(@"SELECT * FROM (SELECT  CAST(ROW_NUMBER() over(order by COUNT(a.ID) DESC) AS INTEGER) AS Ornumber, a.ID,b.UserName,b.Phone,a.CreateTime,b.UserImage,COUNT(a.ID) AS total,COUNT(a.ID)*3 AS totalmoney
                                            FROM dbo.UserInvitationAwards a INNER JOIN
                                            dbo.tb_userinfo b ON a.FromUserID=b.UserID 
                                            WHERE a.UserID='' GROUP BY a.ID,b.UserName,b.Phone,a.CreateTime,b.UserImage) t 
                                                WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize");
                    List<invitationusinfo> data = db.Database.SqlQuery<invitationusinfo>(sqlstr, parameters).ToList();
                    //string gettotalsql = string.Format(@"SELECT COUNT(*) FROM dbo.UserInvitationAwards WHERE UserID='{0}'", userid);

                    return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Comm.ToJsonResult("NotFindUser", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        public class invitationusinfo {
            public int ID { get; set; }
            public string UserName { get; set; }//用户姓名
            public string Phone { get; set; }//手机号
            public string CreateTime { get; set; }//邀请时间
            public string UserImage { get; set; }
            public int total { get; set; }//总数量
            public decimal totalmoney { get; set; }

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
