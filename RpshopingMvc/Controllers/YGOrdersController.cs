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
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;

namespace RpshopingMvc.Controllers
{
    public class YGOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: YGOrders
        public ActionResult Index()
        {
            return View(db.YGOrder.ToList());
        }

        // GET: YGOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGOrder yGOrder = db.YGOrder.Find(id);
            if (yGOrder == null)
            {
                return HttpNotFound();
            }
            return View(yGOrder);
        }

        // GET: YGOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: YGOrders/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,IssueID,OrderTime,LockCode,BuyNumber,Paytype")] YGOrder yGOrder)
        {
            if (ModelState.IsValid)
            {
                db.YGOrder.Add(yGOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yGOrder);
        }

        // GET: YGOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGOrder yGOrder = db.YGOrder.Find(id);
            if (yGOrder == null)
            {
                return HttpNotFound();
            }
            return View(yGOrder);
        }

        // POST: YGOrders/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,IssueID,OrderTime,LockCode,BuyNumber,Paytype")] YGOrder yGOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yGOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yGOrder);
        }

        // GET: YGOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGOrder yGOrder = db.YGOrder.Find(id);
            if (yGOrder == null)
            {
                return HttpNotFound();
            }
            return View(yGOrder);
        }

        // POST: YGOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YGOrder yGOrder = db.YGOrder.Find(id);
            db.YGOrder.Remove(yGOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 云购商品购买
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="buynumber">购买数量</param>
        /// <param name="issueid">云购商品期数id</param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult Buy(string uid, int buynumber, int issueid)
        {
            try
            {
                if (buynumber>1000)
                {
                    return Json(Comm.ToJsonResult("OverstepNumber", "单次购买数量必须小于1000"), JsonRequestBehavior.AllowGet);
                }
                if (buynumber <= 0)
                {
                    return Json(Comm.ToJsonResult("BuyNumberLow", "购买数量必须大于零"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var usmodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == uid);
                    if (usmodel == null)
                    {
                        return Json(Comm.ToJsonResult("NotFind", "用户不存在"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        decimal balan = usmodel.Balance;
                        var issuemodel = db.YGoodsIssue.FirstOrDefault(s => s.ID == issueid);
                        if (issuemodel == null)
                        {
                            return Json(Comm.ToJsonResult("NotFindIssue", "商品不存在"), JsonRequestBehavior.AllowGet);
                        }
                        else if (issuemodel.IsLock==1)
                        {
                            return Json(Comm.ToJsonResult("IsEnd", "已经结束"), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (balan <= 0)
                            {
                                return Json(Comm.ToJsonResult("BalanceLow", "积分余额不足"), JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                int tempissusenumber = issuemodel.SurplusNumber;//剩余人次
                                var goodmodel = db.YGoods.FirstOrDefault(s => s.ID == issuemodel.YGoodsId);
                                int usid = usmodel.ID;
                                string tempcode = string.Empty;
                                int goodmark = goodmodel.Mark.GetHashCode();
                                int markvaule = 0;//要扣除的积分类型
                                if (goodmark == 0)
                                {
                                    markvaule = 1;
                                }
                                else if (goodmark == 1)
                                {
                                    markvaule = 5;
                                }
                                else if (goodmark == 2)
                                {
                                    markvaule = 10;
                                }
                                else if (goodmark == 3)
                                {
                                    markvaule = 100;
                                }
                                int sumnumber = markvaule * buynumber;//计算出要扣除的积分总额
                                if (sumnumber <= usmodel.Balance)
                                {
                                    string timecode = DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                                    string stringcode = (usid + 1).ToString() + timecode;
                                    long time = long.Parse(stringcode);
                                    for (int i = 0; i < buynumber; i++)
                                    {
                                        if (string.IsNullOrWhiteSpace(tempcode))
                                        {
                                            tempcode = time.ToString();
                                        }
                                        else
                                        {
                                            tempcode = tempcode + "," + time + i;
                                        }
                                    }
                                    //保存订单信息
                                    YGOrder ygordermodel = new YGOrder();
                                    ygordermodel.BuyNumber = buynumber;
                                    ygordermodel.IssueID = issueid;
                                    ygordermodel.OrderTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                                    ygordermodel.UserID = usid;
                                    ygordermodel.Paytype = PayType.yy;
                                    ygordermodel.LockCode = tempcode;
                                    db.YGOrder.Add(ygordermodel);
                                    //db.SaveChanges();
                                    //记录扣除用户积分明细
                                    IntegralDetails integraldetailmodel = new IntegralDetails();
                                    integraldetailmodel.AddOrReduce = 1;
                                    integraldetailmodel.AddTime = DateTime.Now.ToString();
                                    integraldetailmodel.AddType = "云购商品消耗";
                                    integraldetailmodel.IntegralNumber = sumnumber;
                                    db.IntegralDetails.Add(integraldetailmodel);
                                    //修改用户积分
                                    usmodel.Integral = usmodel.Integral - sumnumber;
                                    //如果购买数量等于剩余数量则锁住云购商品期数
                                    if (buynumber == issuemodel.SurplusNumber)
                                    {
                                        issuemodel.IsLock = 1;
                                        issuemodel.State = "已揭晓";
                                        issuemodel.AnnounceTime = DateTime.Now.AddMinutes(3).ToString();
                                    }
                                    issuemodel.SurplusNumber = issuemodel.SurplusNumber - buynumber;
                                    issuemodel.AlreadyNumber = issuemodel.AlreadyNumber + buynumber;
                                    db.SaveChanges();
                                    if (tempissusenumber== buynumber)
                                    {
                                        SetLuckCode(issueid);
                                    }
                                        //Task t = new Task(SetLuckCode);
                                        //Task t1 = Task.Factory.StartNew(() => SetLuckCode(issueid));//开启一个线程执行中奖结果
                                        //System.Threading.Thread t = new System.Threading.Thread(() =>
                                        //{
                                        //    SetLuckCode(issueid);
                                        //});
                                        //t.IsBackground = true;
                                        //t.Start();
                                        return Json(Comm.ToJsonResult("Success", "成功", balan), JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    return Json(Comm.ToJsonResult("BalanceLow", "积分余额不足"), JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        //计算中奖
        public void SetLuckCode(int issueid)
        {
            try
            {
                //拼接参数
                SqlParameter[] parameters = {
                                        new SqlParameter("@id", SqlDbType.Int)
                                    };
                parameters[0].Value = issueid;
                string sqlstr = string.Empty;
                string sql = string.Format(@"SELECT LockCode FROM dbo.YGOrders WHERE IssueID=@id");
                List<Codeclass> codelist = db.Database.SqlQuery<Codeclass>(sql, parameters).ToList();

                string tempcodestr = string.Empty;
                for (int i = 0; i < codelist.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(tempcodestr))
                    {
                        tempcodestr = codelist[i].LockCode;
                    }
                    else
                    {
                        tempcodestr = tempcodestr + "," + codelist[i].LockCode;
                    }
                }
                Random r = new Random(Guid.NewGuid().GetHashCode());
                string[] liststr = tempcodestr.Split(',');
                int luckynumber = r.Next(0, liststr.Length);
                string luckycode = liststr[luckynumber].ToString();
                var issuemodel = db.YGoodsIssue.FirstOrDefault(s => s.ID == issueid);
                string luckussql = string.Format("SELECT * FROM dbo.YGOrders WHERE IssueID={0} AND LockCode LIKE '%{1}%'", issueid, luckycode);
                List<YGOrder> luckuser = db.Database.SqlQuery<YGOrder>(luckussql).ToList();
                issuemodel.LuckUserID = luckuser[0].UserID;
                issuemodel.LuckCode = luckycode;
                db.SaveChanges();
                CreateNext(issueid);
                //Thread.Sleep(2000);
            }
            catch (Exception)
            {

            }
        }
        private void CreateNext(int issueid)
        {
            YGoodsIssue ygi = db.YGoodsIssue.FirstOrDefault(s => s.ID == issueid);
            //如果存在则返回已存在通知
            if (ygi != null)
            {
                    var model = new YGoodsIssue
                    {
                        YGoodsId = ygi.YGoodsId,
                        AlreadyNumber = 0,
                        AnnounceTime = "",
                        IssueNumber = ygi.IssueNumber+1,
                        LuckCode = "",
                        State = "进行中",
                        SumNumber = ygi.SumNumber,
                        SurplusNumber = ygi.SurplusNumber
                    };
                    db.YGoodsIssue.Add(model);
                    db.SaveChanges();
            }
        }
        private class Codeclass {
            public string LockCode { get; set; }
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
