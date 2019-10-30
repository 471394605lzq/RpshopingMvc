using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RpshopingMvc.Models;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Controllers
{
    public class YGoodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Sidebar(string name = "云购商品")
        {
            ViewBag.Sidebar = name;

        }
        // GET: YGoods
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.YGoods
                    from a in db.YGoodsType
                    where e.Type==a.ID
                    select new YGoodsShow
                    {
                        ID = e.ID,
                        GoodsName = e.GoodsName,
                        MainImage = e.MainImage,
                        Price = e.Price,
                        Stock = e.Stock,
                        TypeName = a.Name
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.GoodsName.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
            //return View(db.YGoods.ToList());
        }

        // GET: YGoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoods yGoods = db.YGoods.Find(id);
            if (yGoods == null)
            {
                return HttpNotFound();
            }
            return View(yGoods);
        }

        // GET: YGoods/Create
        public ActionResult Create()
        {
            ViewBag.Type = new SelectList(db.YGoodsType, "ID", "Name");
            var model = new YGoodsView();
            return View(model);
        }

        // POST: YGoods/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(YGoodsView yGoods)
        {
            if (ModelState.IsValid)
            {
                var model = new YGoods
                {
                    GoodsName = yGoods.GoodsName,
                    Type = yGoods.Type,
                    Stock = yGoods.Stock,
                    SamllImage = string.Join(",", yGoods.SamllImage.Images),
                    Info = yGoods.Info,
                    MainImage = string.Join(",", yGoods.MainImage.Images),
                    Price = yGoods.Price,
                    Mark = yGoods.Mark
                };
                db.YGoods.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yGoods);
        }

        // GET: YGoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoods model = db.YGoods.Find(id);
            var models = new YGoodsView
            {
                ID = model.ID,
                Info = model.Info,
                Type = model.Type,
                GoodsName = model.GoodsName,
                //MainImage = model.MainImage,
                Price = model.Price,
                //SamllImage = model.SamllImage,
                Stock = model.Stock,
                Mark=model.Mark
            };
            models.MainImage.Images = model.MainImage?.Split(',') ?? new string[0];
            models.SamllImage.Images = model.SamllImage?.Split(',') ?? new string[0];
            ViewBag.Type = new SelectList(db.YGoodsType.OrderBy(s => s.ID), "ID", "Name", models.Type);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: YGoods/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(YGoodsView goods)
        {
            if (ModelState.IsValid)
            {
                var t = db.YGoods.FirstOrDefault(s => s.ID == goods.ID);
                t.ID = goods.ID;
                t.GoodsName = goods.GoodsName;
                t.MainImage = string.Join(",", goods.MainImage.Images);
                t.Price = goods.Price;
                t.SamllImage = string.Join(",", goods.SamllImage.Images);
                t.Stock = goods.Stock;
                t.Type = goods.Type;
                t.Info = goods.Info;
                t.Mark = goods.Mark;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(goods);
        }

        // GET: YGoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YGoods yGoods = db.YGoods.Find(id);
            if (yGoods == null)
            {
                return HttpNotFound();
            }
            return View(yGoods);
        }

        // POST: YGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            YGoods yGoods = db.YGoods.Find(id);
            db.YGoods.Remove(yGoods);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 创建云购期数
        /// </summary>
        /// <param name="id">云购商品id</param>
        /// <returns></returns>
        public ActionResult CreateIssues(int id)
        {
            try
            {
                YGoodsIssue ygi = db.YGoodsIssue.FirstOrDefault(s => s.YGoodsId == id);
                YGoods ygoods = db.YGoods.FirstOrDefault(s => s.ID == id);
                //如果存在则返回已存在通知
                if (ygi != null)
                {
                    return RedirectToAction("CreateIssuesResult", new { type = "2" });
                }
                else
                {
                    //如果商品不存在不保存云购商品期数数据
                    if (ygoods != null)
                    {
                        int sum = Convert.ToInt32(ygoods.Price);
                        var mark = ygoods.Mark;
                        if (mark == YGoodsEnumType.One)
                        {
                            sum = Convert.ToInt32(ygoods.Price);
                        }
                        else if (mark == YGoodsEnumType.Five)
                        {
                            sum = Convert.ToInt32(ygoods.Price) / 5;
                        }
                        else if (mark == YGoodsEnumType.Ten)
                        {
                            sum = Convert.ToInt32(ygoods.Price) / 10;
                        }
                        else if (mark == YGoodsEnumType.Hundred)
                        {
                            sum = Convert.ToInt32(ygoods.Price) / 100;
                        }
                        var model = new YGoodsIssue
                        {
                            YGoodsId = id,
                            AlreadyNumber = 0,
                            AnnounceTime = "",
                            IssueNumber = 1,
                            LuckCode = "",
                            State = "进行中",
                            SumNumber = sum,
                            SurplusNumber = Convert.ToInt32(ygoods.Price)
                        };
                        db.YGoodsIssue.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("CreateIssuesResult", new { type = "0" });
                    }
                    else
                    {
                        return RedirectToAction("CreateIssuesResult", new { type = "3" });
                    }
                }
            }
            catch (Exception)
            {
                return RedirectToAction("CreateIssuesResult", new { type = "1" });
            }
        }
        //创建期数结果
        public ActionResult CreateIssuesResult(string type)
        {
            if (type == "0")
            {
                ViewBag.text = "创建成功";
            }
            else if (type=="2")
            {
                ViewBag.text = "该商品已经存在期数";
            }
            else if (type == "3")
            {
                ViewBag.text = "云购商品不存在";
            }
            else
            {
                ViewBag.text = "操作失败";
            }
            return View();
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
