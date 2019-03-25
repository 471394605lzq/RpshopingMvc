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
using Top.Api;
using RpshopingMvc.App_Start.Common;
using Top.Api.Request;
using Top.Api.Response;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace RpshopingMvc.Controllers
{
    public class tb_FavoritesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Sidebar(string name = "选品库管理")
        {
            ViewBag.Sidebar = name;
        }
        // GET: tb_Favorites
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.tb_Favorites
                    select new tb_Favoritesshow
                    {
                        ID = e.ID,
                        Explain = e.Explain,
                        FavoritesID = e.FavoritesID,
                        ImagePath = e.ImagePath,
                        Name = e.Name,
                        Type = e.Type,
                        ICO=e.ICO
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.Name.Contains(filter));
            }
            var paged = m.OrderBy(s => s.FavoritesID).ToPagedList(page);
            return View(paged);
        }

        // GET: tb_Favorites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Favorites tb_Favorites = db.tb_Favorites.Find(id);
            if (tb_Favorites == null)
            {
                return HttpNotFound();
            }
            return View(tb_Favorites);
        }

        // GET: tb_Favorites/Create
        public ActionResult Create()
        {
            Sidebar();
            var model = new tb_Favoritesview();
            return View(model);
        }

        // POST: tb_Favorites/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tb_Favoritesview tb_Favorites)
        {
            if (ModelState.IsValid)
            {
                var model = new tb_Favorites
                {
                    FavoritesID = "",
                    Type = tb_Favorites.Type,
                    Name = tb_Favorites.Name,
                    Explain = tb_Favorites.Explain,
                    ImagePath = string.Join(",", tb_Favorites.ImagePath.Images),
                    ICO=tb_Favorites.ICO
                };
                db.tb_Favorites.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_Favorites);
        }

        // GET: tb_Favorites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Favorites model = db.tb_Favorites.Find(id);
            var models = new tb_Favoritesview
            {
                ID = model.ID,
                Explain = model.Explain,
                Name = model.Name,
                Type = model.Type,
                ICO=model.ICO
            };
            models.ImagePath.Images = model.ImagePath?.Split(',') ?? new string[0];
            if (models == null)
            {
                return HttpNotFound();
            }
            return View(models);
        }

        // POST: tb_Favorites/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_Favoritesview tb_Favorites)
        {
            if (ModelState.IsValid)
            {
                var t = db.tb_Favorites.FirstOrDefault(s => s.ID == tb_Favorites.ID);
                t.ID = tb_Favorites.ID;
                t.ImagePath = string.Join(",", tb_Favorites.ImagePath.Images);
                t.Explain = tb_Favorites.Explain;
                t.Name = tb_Favorites.Name;
                t.Type = tb_Favorites.Type;
                t.ICO = tb_Favorites.ICO;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_Favorites);
        }

        // GET: tb_Favorites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Favorites tb_Favorites = db.tb_Favorites.Find(id);
            if (tb_Favorites == null)
            {
                return HttpNotFound();
            }
            return View(tb_Favorites);
        }

        // POST: tb_Favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_Favorites tb_Favorites = db.tb_Favorites.Find(id);
            db.tb_Favorites.Remove(tb_Favorites);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //获取选品库列表
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetFavoritesList(int page = 1, int pageSize = 20)
        {
            var query = from a in db.tb_Favorites
                        where a.Type == FavoritesType.Recommend
                        select new
                        {
                            ID = a.ID,
                            Name = a.Name,
                            FavoritesID = a.FavoritesID,
                            Image = a.ImagePath,
                            Explain = a.Explain
                        };
            var paged = query.OrderBy(s => s.ID).ToPagedList(page, pageSize);
            return Json(Comm.ToJsonResultForPagedList(paged, paged), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SynchronizationFavorites(long? pageno=1L)
        {
            try
            {
                ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                TbkUatmFavoritesGetRequest req = new TbkUatmFavoritesGetRequest();
                req.PageNo = pageno;
                req.PageSize = 100L;
                req.Fields = "favorites_title,favorites_id,type";
                req.Type = -1L;
                TbkUatmFavoritesGetResponse rsp = client.Execute(req);
                //Console.WriteLine(rsp.Body);
                var bodys = rsp.Body;

                //List<tempfavorites> sdudentList4 = Comm.DeserializeJsonToList<tempfavorites>("[{\"ID\":\"112\",\"Name\":\"石子儿\"}]");

                var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(bodys) as JContainer;//转json格式
                string s = jsondataformain.SelectToken("tbk_uatm_favorites_get_response").ToString();
                var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                string s1 = js.SelectToken("results").ToString();
                int total=Convert.ToInt32(js.SelectToken("total_results"));//总数

                var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
                string s2 = js1.SelectToken("tbk_favorites").ToString();
                var js2 = Newtonsoft.Json.JsonConvert.DeserializeObject(s2) as JContainer;
                if (js2.Count > 0)
                {
                    for (int j = 0; j < js2.Count; j++)
                    {
                        string tempid = js2[j].SelectToken("favorites_id").ToString();
                        if (!db.tb_Favorites.Any(d =>d.FavoritesID == tempid))
                        {
                            var model = new tb_Favorites
                            {
                                FavoritesID = tempid,
                                Type = FavoritesType.Ordinary,
                                Name = js2[j].SelectToken("favorites_title").ToString(),
                                Explain = "淘宝同步选品库",
                                ImagePath = ""
                            };
                            db.tb_Favorites.Add(model);
                            db.SaveChanges();
                        }
                        if (j==js2.Count-1&&(total-100)>0&& pageno<2)
                        {
                            SynchronizationFavorites(2L);
                        }
                    }
                }
                return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View();
            }
            
        }
        private class tempfavorites {

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
