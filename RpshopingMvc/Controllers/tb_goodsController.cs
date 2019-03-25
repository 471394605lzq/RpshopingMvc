using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RpshopingMvc.Models;
using Top.Api;
using RpshopingMvc.App_Start.Common;
using Top.Api.Request;
using Top.Api.Response;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using static RpshopingMvc.Enums.Enums;
using RpshopingMvc.App_Start;
using System.Data.SqlClient;

namespace RpshopingMvc.Controllers
{
    public class tb_goodsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Sidebar(string name = "商品管理")
        {
            ViewBag.Sidebar = name;
        }
        // GET: tb_goods
        public ActionResult Index(string filter, int page = 1)
        {
            Sidebar();
            var m = from e in db.tb_goods
                    select new tb_goodsview
                    {
                        ID = e.ID,
                        ImagePath = e.ImagePath,
                        Brokerage = e.Brokerage,
                        CouponCount = e.CouponCount,
                        CouponDenomination = e.CouponDenomination,
                        CouponEndTime = e.CouponEndTime,
                        CouponStartTime = e.CouponStartTime,
                        CouponSurplus = e.CouponSurplus,
                        GoodsName = e.GoodsName,
                        GoodsSort = e.GoodsSort,
                        IncomeRatio = e.IncomeRatio,
                        PlatformType = e.PlatformType,
                        Price = e.Price,
                        SalesVolume = e.SalesVolume,
                        SmallImages = e.SmallImages,
                        StoreName = e.StoreName,
                    };

            if (!string.IsNullOrWhiteSpace(filter))
            {
                m = m.Where(s => s.GoodsName.Contains(filter));
            }
            var paged = m.OrderBy(s => s.ID).ToPagedList(page);
            return View(paged);
        }

        // GET: tb_goods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_goods tb_goods = db.tb_goods.Find(id);
            if (tb_goods == null)
            {
                return HttpNotFound();
            }
            return View(tb_goods);
        }

        // GET: tb_goods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tb_goods/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GoodsName,Code,Price,ImagePath,SmallImages,DetailPath,GoodsSort,TkPath,SalesVolume,IncomeRatio,Brokerage,SellerID,StoreName,StoreImage,PlatformType,CouponID,CouponCount,CouponSurplus,CouponDenomination,CouponStartTime,CouponEndTime,CouponPath,CouponSpreadPath")] tb_goods tb_goods)
        {
            if (ModelState.IsValid)
            {
                db.tb_goods.Add(tb_goods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_goods);
        }

        // GET: tb_goods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_goods tb_goods = db.tb_goods.Find(id);
            if (tb_goods == null)
            {
                return HttpNotFound();
            }
            return View(tb_goods);
        }

        // POST: tb_goods/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GoodsName,Code,Price,ImagePath,SmallImages,DetailPath,GoodsSort,TkPath,SalesVolume,IncomeRatio,Brokerage,SellerID,StoreName,StoreImage,PlatformType,CouponID,CouponCount,CouponSurplus,CouponDenomination,CouponStartTime,CouponEndTime,CouponPath,CouponSpreadPath")] tb_goods tb_goods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_goods).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_goods);
        }

        // GET: tb_goods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_goods tb_goods = db.tb_goods.Find(id);
            if (tb_goods == null)
            {
                return HttpNotFound();
            }
            return View(tb_goods);
        }

        // POST: tb_goods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_goods tb_goods = db.tb_goods.Find(id);
            db.tb_goods.Remove(tb_goods);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //废弃
        //[HttpGet]
        //[AllowCrossSiteJson]
        //public ActionResult GetGoodsList(int FavoritesID, int? page = 1, int? pageSize = 20)
        //{
        //    try
        //    {
        //        int starpagesize = page.Value * pageSize.Value - pageSize.Value;
        //        int endpagesize = page.Value * pageSize.Value;
        //        //拼接参数
        //        SqlParameter[] parameters = {
        //                new SqlParameter("@FavoritesID", SqlDbType.Int),
        //                new SqlParameter("@starpagesize", SqlDbType.Int),
        //                new SqlParameter("@endpagesize", SqlDbType.Int)
        //            };
        //        parameters[0].Value = FavoritesID;
        //        parameters[1].Value = starpagesize;
        //        parameters[2].Value = endpagesize;
        //        string sqlstr = string.Empty;
        //        sqlstr = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.Code,g.ID,g.GoodsName,g.ImagePath,g.SmallImages,g.DetailPath,g.TkPath,g.SalesVolume,g.Brokerage,g.StoreName,g.PlatformType,g.CouponDenomination,g.wxzkprice,g.wxzkprice-g.CouponDenomination as qhprice,g.StoreNick FROM dbo.tb_goods g
        //                                            WHERE g.FavoritesID=@FavoritesID
        //                                            GROUP BY g.Code,g.ID,g.GoodsName,g.ImagePath,g.SmallImages,g.DetailPath,g.TkPath,g.SalesVolume,g.Brokerage,g.StoreName,g.PlatformType,g.CouponDenomination,g.wxzkprice,g.StoreNick) t WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize");
        //        List<GoodsResultModel> data = db.Database.SqlQuery<GoodsResultModel>(sqlstr, parameters).ToList();
        //        return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
        //    }
        //}
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetGoodsList(string userid, int FavoritesID, int? page = 1, long? pageSize = 200,int? israndom=0)
        {
            try
            {
                long tempfid = FavoritesID;
                if (israndom == 1)
                {
                    SqlParameter[] parameters = {
                    };
                    string sqlstr = string.Empty;
                    sqlstr = string.Format(@"SELECT * FROM dbo.tb_Favorites");
                    List<tb_Favorites> data = db.Database.SqlQuery<tb_Favorites>(sqlstr, parameters).ToList();
                    int resultrow = data.Count;
                    Random rd = new Random();
                    int rs = rd.Next(1, resultrow);
                    tempfid = long.Parse(data[rs].FavoritesID);
                }
                var usmodel = db.tb_userinfos.FirstOrDefault(a => a.UserID == userid);
                long tempadzoneid = 0;
                if (usmodel != null)
                {
                    tempadzoneid = long.Parse(usmodel.Adzoneid);
                }
                else
                {
                    tempadzoneid = 96815000311L;
                }
                ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                TbkUatmFavoritesItemGetRequest req = new TbkUatmFavoritesItemGetRequest();
                req.Platform = 2;
                req.PageSize = pageSize;
                req.AdzoneId = tempadzoneid;
                //req.Unid = "3456";
                req.FavoritesId = tempfid;
                req.PageNo = page;
                req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick,shop_title,zk_final_price_wap,event_start_time,event_end_time,tk_rate,status,type,coupon_total_count,coupon_info,coupon_end_time,coupon_click_url,coupon_start_time,coupon_remain_count,category,click_url";
                TbkUatmFavoritesItemGetResponse rsp = client.Execute(req);

                List<searchresultmodel> list2 = new List<searchresultmodel>();
                if (rsp.ErrCode != "15")
                {
                    var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                    string s = jsondataformain.SelectToken("tbk_uatm_favorites_item_get_response").ToString();
                    var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                    string s1 = js.SelectToken("results").ToString();
                    int total = Convert.ToInt32(js.SelectToken("total_results"));//总数

                    var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
                    string s2 = js1.SelectToken("uatm_tbk_item").ToString();
                    JsonSerializer serializer = new JsonSerializer();
                    StringReader sr = new StringReader(s2);
                    object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<tbgoods>));
                    List<tbgoods> list = o as List<tbgoods>;
                    
                    if (list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            string tempid = list[i].num_iid.ToString();//.SelectToken("num_iid").ToString();
                            string status = list[i].status.ToString();
                            if (status == "1")
                            {
                                string smallimg = list[i].small_images == null ? "" : list[i].small_images.ToString();
                                string str = "";
                                if (!string.IsNullOrWhiteSpace(smallimg) && !smallimg.Equals("{}"))
                                {
                                    var smallimgjson = Newtonsoft.Json.JsonConvert.DeserializeObject(smallimg) as JContainer;
                                    var tempsmallimg = smallimgjson.SelectToken("string").ToList();
                                    str = string.Join(",", tempsmallimg);//主图集合
                                }
                                string couponinfo = string.IsNullOrWhiteSpace(list[i].coupon_info) ? "" : list[i].coupon_info;
                                string[] splitcoupon = string.IsNullOrWhiteSpace(couponinfo) ? new string[] { } : couponinfo.Split('减');
                                string[] splitcoupon2 = string.IsNullOrWhiteSpace(couponinfo) ? new string[] { } : splitcoupon[1].Split('元');
                                int tCouponDenomination = string.IsNullOrWhiteSpace(couponinfo) ? 0 : Convert.ToInt32(splitcoupon2[0]);
                                decimal tqhprice = decimal.Round((decimal)list[i].zk_final_price_wap - tCouponDenomination, 2);
                                decimal tBrokerage = decimal.Round(decimal.Parse(((decimal)list[i].zk_final_price_wap * ((decimal)list[i].tk_rate / 100)).ToString()), 2);

                                int tCouponCount = string.IsNullOrWhiteSpace(couponinfo) ? 0 : Convert.ToInt32(list[i].coupon_total_count);
                                
                                string tCouponEndTime = string.IsNullOrWhiteSpace(couponinfo) ? "" : list[i].coupon_end_time.ToString();
                                string tCouponSpreadPath = string.IsNullOrWhiteSpace(couponinfo) ? "" : list[i].coupon_click_url.ToString();
                                string tCouponStartTime = string.IsNullOrWhiteSpace(couponinfo) ? "" : list[i].coupon_start_time.ToString();
                                int tCouponSurplus = string.IsNullOrWhiteSpace(couponinfo) ? 0 : Convert.ToInt32(list[i].coupon_remain_count);
                                string tDetailPath = list[i].item_url.ToString();
                                string tGoodsName = list[i].title.ToString();
                                int tGoodsSort = (int)list[i].category;
                                string tImagePath = list[i].pict_url.ToString();
                                decimal tIncomeRatio = (decimal)list[i].tk_rate;
                                string tPlatformType = list[i].user_type.ToString() == "1" ? "天猫" : "淘宝";
                                decimal tPrice = (decimal)list[i].reserve_price;
                                int tSalesVolume = Convert.ToInt32(list[i].volume);
                                long tSellerID = list[i].seller_id;
                                string tStoreName = list[i].shop_title.ToString();
                                string tStoreNick = list[i].nick.ToString();
                                string tTkPath = string.IsNullOrWhiteSpace(list[i].click_url) ? "" : list[i].click_url;
                                decimal wxzkprice = (decimal)list[i].zk_final_price_wap;
                                decimal zkprice = (decimal)list[i].zk_final_price;
                                string tprovcity = string.IsNullOrWhiteSpace(list[i].provcity) ? "" : list[i].provcity;


                                searchresultmodel tempgoods = new searchresultmodel();
                                tempgoods.commission_rate = tIncomeRatio;
                                tempgoods.coupon_amount = tCouponDenomination;
                                tempgoods.coupon_end_time = tCouponEndTime;
                                tempgoods.coupon_info = couponinfo;
                                tempgoods.coupon_share_url = tCouponSpreadPath;
                                tempgoods.coupon_start_time= tCouponStartTime;
                                tempgoods.qhprice = tqhprice;
                                tempgoods.item_id = tempid;
                                tempgoods.item_url = tTkPath;
                                tempgoods.nick = tStoreNick;
                                tempgoods.pict_url = tImagePath;
                                tempgoods.seller_id = tSellerID;
                                tempgoods.shop_title = tStoreName;
                                tempgoods.small_images = str;
                                tempgoods.title = tGoodsName;
                                tempgoods.user_type = tPlatformType;
                                tempgoods.volume = tSalesVolume;
                                tempgoods.yjamount = tBrokerage;
                                tempgoods.zk_final_price = wxzkprice;
                                list2.Add(tempgoods);
                            }
                        }
                    }
                }
                return Json(Comm.ToJsonResult("Success", "成功", list2), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetGoodsListByName(string goodsname, int? page = 1, int? pageSize = 20)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;
                //拼接参数
                SqlParameter[] parameters = {
                        new SqlParameter("@goodsname", SqlDbType.NVarChar),
                        new SqlParameter("@starpagesize", SqlDbType.Int),
                        new SqlParameter("@endpagesize", SqlDbType.Int)
                    };
                parameters[0].Value = "%"+goodsname+"%";
                parameters[1].Value = starpagesize;
                parameters[2].Value = endpagesize;
                string sqlstr = string.Empty;
                sqlstr = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.Code,g.ID,g.GoodsName,g.ImagePath,g.SmallImages,g.DetailPath,g.TkPath,g.SalesVolume,g.Brokerage,g.StoreName,g.PlatformType,g.CouponDenomination,g.wxzkprice,g.wxzkprice-g.CouponDenomination as qhprice,g.StoreNick FROM dbo.tb_goods g
                                                    WHERE g.GoodsName like @goodsname
                                                    GROUP BY g.Code,g.ID,g.GoodsName,g.ImagePath,g.SmallImages,g.DetailPath,g.TkPath,g.SalesVolume,g.Brokerage,g.StoreName,g.PlatformType,g.CouponDenomination,g.wxzkprice,g.StoreNick) t WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize");
                List<GoodsResultModel> data = db.Database.SqlQuery<GoodsResultModel>(sqlstr, parameters).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }


        //同步选品库中的商品
        public ActionResult SynchronizationGoods()
        {
        string sqlstr = string.Format(@"SELECT * FROM dbo.tb_Favorites");
        List<tb_Favorites> data = db.Database.SqlQuery<tb_Favorites>(sqlstr).ToList();
            if (data.Count>0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    int fid =Convert.ToInt32(data[i].FavoritesID);
                    setgoods(fid);
                }

            }
            return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
        }
        //检查失效商品并删除
        public ActionResult CheckGoods()
        {
            string sqlstr = string.Format(@"SELECT * FROM dbo.tb_Favorites");
            List<tb_Favorites> data = db.Database.SqlQuery<tb_Favorites>(sqlstr).ToList();
            if (data.Count > 0)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    int fid = Convert.ToInt32(data[i].FavoritesID);
                    Check(fid);
                }

            }
            return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
        }
        private void Check(int fid, long? pageno = 1L) {
            try
            {
                ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                TbkUatmFavoritesItemGetRequest req = new TbkUatmFavoritesItemGetRequest();
                req.Platform = 2;
                req.PageSize = 200L;
                req.AdzoneId = 96815000311L;
                req.Unid = "3456";
                req.FavoritesId = fid;
                req.PageNo = pageno;
                req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick,shop_title,zk_final_price_wap,event_start_time,event_end_time,tk_rate,status,type,coupon_total_count,coupon_info,coupon_end_time,coupon_click_url,coupon_start_time,coupon_remain_count,category,click_url";
                TbkUatmFavoritesItemGetResponse rsp = client.Execute(req);
                if (rsp.ErrCode != "15")
                {
                    var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                    string s = jsondataformain.SelectToken("tbk_uatm_favorites_item_get_response").ToString();
                    var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                    string s1 = js.SelectToken("results").ToString();
                    int total = Convert.ToInt32(js.SelectToken("total_results"));//总数

                    var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
                    string s2 = js1.SelectToken("uatm_tbk_item").ToString();
                    JsonSerializer serializer = new JsonSerializer();
                    StringReader sr = new StringReader(s2);
                    object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<tbgoods>));
                    List<tbgoods> list = o as List<tbgoods>;
                    if (list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            string tempid = list[i].num_iid.ToString();//.SelectToken("num_iid").ToString();
                            string status = list[i].status.ToString();
                            if (status == "0")
                            {
                                var query = from a in db.tb_goods
                                            where a.Code == tempid
                                            select new tb_goodsview
                                            {
                                                 ID=a.ID
                                            };
                                var data = query.ToList();
                                if (data.Count > 0)
                                {
                                    tb_goods tb_goods = db.tb_goods.Find(data[0].ID);
                                    db.tb_goods.Remove(tb_goods);
                                    db.SaveChanges();
                                }
                            }
                            if (i == list.Count - 1 && (total - 100) > 0 && pageno < 2)
                            {
                                Check(fid, 2L);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void setgoods(int fid, long? pageno = 1L)
        {
            try
            {
                ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                TbkUatmFavoritesItemGetRequest req = new TbkUatmFavoritesItemGetRequest();
                req.Platform = 2;
                req.PageSize = 200L;
                req.AdzoneId = 96815000311L;
                req.Unid = "3456";
                req.FavoritesId = fid;
                req.PageNo = pageno;
                req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick,shop_title,zk_final_price_wap,event_start_time,event_end_time,tk_rate,status,type,coupon_total_count,coupon_info,coupon_end_time,coupon_click_url,coupon_start_time,coupon_remain_count,category,click_url";
                TbkUatmFavoritesItemGetResponse rsp = client.Execute(req);
                if (rsp.ErrCode != "15")
                {
                    var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                    string s = jsondataformain.SelectToken("tbk_uatm_favorites_item_get_response").ToString();
                    var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                    string s1 = js.SelectToken("results").ToString();
                    int total = Convert.ToInt32(js.SelectToken("total_results"));//总数

                    var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
                    string s2 = js1.SelectToken("uatm_tbk_item").ToString();
                    JsonSerializer serializer = new JsonSerializer();
                    StringReader sr = new StringReader(s2);
                    object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<tbgoods>));
                    List<tbgoods> list = o as List<tbgoods>;

                    if (list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            string tempid = list[i].num_iid.ToString();//.SelectToken("num_iid").ToString();
                            string status = list[i].status.ToString();
                            if (!db.tb_goods.Any(d => d.Code == tempid)&& status=="1")
                            {
                                string smallimg = list[i].small_images==null ? "" : list[i].small_images.ToString();
                                string str = "";
                                if (!string.IsNullOrWhiteSpace(smallimg)&& !smallimg.Equals("{}"))
                                {
                                    var smallimgjson = Newtonsoft.Json.JsonConvert.DeserializeObject(smallimg) as JContainer;
                                    var tempsmallimg = smallimgjson.SelectToken("string").ToList();
                                    str = string.Join(",", tempsmallimg);//主图集合
                                }
                                string couponinfo = string.IsNullOrWhiteSpace(list[i].coupon_info) ? "" : list[i].coupon_info;
                                string[] splitcoupon = string.IsNullOrWhiteSpace(couponinfo) ? new string[] { } : couponinfo.Split('减');
                                string[] splitcoupon2 = string.IsNullOrWhiteSpace(couponinfo) ? new string[] { } : splitcoupon[1].Split('元');

                                decimal tBrokerage = decimal.Round(decimal.Parse(((decimal)list[i].zk_final_price_wap * ((decimal)list[i].tk_rate / 100)).ToString()), 2);
                                
                                int tCouponCount = string.IsNullOrWhiteSpace(couponinfo) ? 0 : Convert.ToInt32(list[i].coupon_total_count);
                                int tCouponDenomination = string.IsNullOrWhiteSpace(couponinfo) ? 0 : Convert.ToInt32(splitcoupon2[0]);
                                decimal tqhprice = decimal.Round((decimal)list[i].zk_final_price_wap - tCouponDenomination, 2);
                                string tCouponEndTime = string.IsNullOrWhiteSpace(couponinfo) ? "" : list[i].coupon_end_time.ToString();
                                string tCouponSpreadPath = string.IsNullOrWhiteSpace(couponinfo) ? "" : list[i].coupon_click_url.ToString();
                                string tCouponStartTime = string.IsNullOrWhiteSpace(couponinfo) ? "" : list[i].coupon_start_time.ToString();
                                int tCouponSurplus = string.IsNullOrWhiteSpace(couponinfo) ? 0 : Convert.ToInt32(list[i].coupon_remain_count);
                                string tDetailPath = list[i].item_url.ToString();
                                string tGoodsName = list[i].title.ToString();
                                int tGoodsSort = (int)list[i].category;
                                string tImagePath = list[i].pict_url.ToString();
                                decimal tIncomeRatio = (decimal)list[i].tk_rate;
                                string tPlatformType = list[i].user_type.ToString() == "1" ? "天猫" : "淘宝";
                                decimal tPrice = (decimal)list[i].reserve_price;
                                int tSalesVolume = Convert.ToInt32(list[i].volume);
                                string tSellerID = list[i].seller_id.ToString();
                                string tStoreName = list[i].shop_title.ToString();
                                string tStoreNick = list[i].nick.ToString();
                                string tTkPath =string.IsNullOrWhiteSpace(list[i].click_url)?"": list[i].click_url;
                                decimal wxzkprice = (decimal)list[i].zk_final_price_wap;
                                decimal zkprice = (decimal)list[i].zk_final_price;
                                string tprovcity = string.IsNullOrWhiteSpace(list[i].provcity) ? "" : list[i].provcity;

                                var model = new tb_goods
                                {
                                    Code = tempid,
                                    Brokerage = tBrokerage,
                                    BrokerageExplain = couponinfo,
                                    CouponCount = tCouponCount,
                                    CouponDenomination = tCouponDenomination,
                                    CouponEndTime = tCouponEndTime,
                                    CouponID = "",
                                    CouponPath = string.Empty,
                                    CouponSpreadPath = tCouponSpreadPath,
                                    CouponStartTime = tCouponStartTime,
                                    CouponSurplus = tCouponSurplus,
                                    DetailPath = tDetailPath,
                                    GoodsName = tGoodsName,
                                    GoodsSort = tGoodsSort,
                                    ImagePath = tImagePath,
                                    IncomeRatio = tIncomeRatio,
                                    PlatformType = tPlatformType,
                                    Price = tPrice,
                                    SalesVolume = tSalesVolume,
                                    SellerID = tSellerID,
                                    SmallImages = str,
                                    StoreImage = "",
                                    StoreName = tStoreName,
                                    StoreNick = tStoreNick,
                                    TkPath = tTkPath,
                                    wxzkprice = wxzkprice,
                                    zkprice = zkprice,
                                    FavoritesID = fid,
                                    provcity = tprovcity,
                                    Qhprice = tqhprice,
                                    GoodsType = GoodsType.Synchronous,//同步类型
                                    CategoryName = tGoodsName.Substring(0,5)
                                };
                                db.tb_goods.Add(model);
                                db.SaveChanges();
                            }
                            if (i == list.Count - 1 && (total - 100) > 0 && pageno < 2)
                            {
                                setgoods(fid,2L);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //根据商品ID查询商品信息
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetGoodsDetailByID(int id)
        {
            tb_goods tb_goods = db.tb_goods.Find(id);
            string storeimg = tb_goods.StoreImage;
            long storeid =long.Parse(tb_goods.SellerID);
            string temostoreimg = "";
            if (string.IsNullOrWhiteSpace(storeimg))
            {
                //ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                //TbkShopRecommendGetRequest req = new TbkShopRecommendGetRequest();
                //req.Fields = "user_id,shop_title,shop_type,seller_nick,pict_url,shop_url";
                //req.UserId = storeid;
                //req.Count = 20L;
                //req.Platform = 1L;
                //TbkShopRecommendGetResponse rsp = client.Execute(req);


                ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                TbkShopGetRequest req = new TbkShopGetRequest();
                req.Fields = "user_id,shop_title,shop_type,seller_nick,pict_url,shop_url";
                req.Q = tb_goods.StoreName;
                req.Platform = 2L;
                req.PageNo = 1L;
                req.PageSize = 20L;
                TbkShopGetResponse rsp = client.Execute(req);

                var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                string s = jsondataformain.SelectToken("tbk_shop_get_response").ToString();
                var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                string s1 = js.SelectToken("results").ToString();

                var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
                string s2 = js1.SelectToken("n_tbk_shop").ToString();
                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = new StringReader(s2);
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<storeinfo>));
                List<storeinfo> list = o as List<storeinfo>;
                if (list.Count > 0)
                {
                    temostoreimg = list[0].pict_url;
                    tb_goods.StoreImage = temostoreimg;
                    db.SaveChanges();
                }
            }
            else
            {
                temostoreimg = storeimg;
            }
            var resultdata = new
            {
                GoodsName = tb_goods.GoodsName,
                Code = tb_goods.Code,
                wxzkprice = tb_goods.wxzkprice,
                qhprice = tb_goods.Qhprice,
                ImagePath = tb_goods.ImagePath,
                SmallImages = tb_goods.SmallImages,
                CouponDenomination = tb_goods.CouponDenomination,
                Brokerage = tb_goods.Brokerage,
                CouponStartTime = tb_goods.CouponStartTime,
                CouponEndTime = tb_goods.CouponEndTime,
                StoreName = tb_goods.StoreName,
                StoreNick = tb_goods.StoreNick,
                PlatformType = tb_goods.PlatformType,
                StoreImage = temostoreimg,
                SalesVolume = tb_goods.SalesVolume

            };
            return Json(Comm.ToJsonResult("Success", "成功", tb_goods), JsonRequestBehavior.AllowGet);
        }
        //根据店铺名称查询店铺信息
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetStoreByName(string StoreName) {
            ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
            TbkShopGetRequest req = new TbkShopGetRequest();
            req.Fields = "user_id,shop_title,shop_type,seller_nick,pict_url,shop_url";
            req.Q = StoreName;
            req.Platform = 2L;
            req.PageNo = 1L;
            req.PageSize = 20L;
            TbkShopGetResponse rsp = client.Execute(req);

            var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
            string s = jsondataformain.SelectToken("tbk_shop_get_response").ToString();
            var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
            string s1 = js.SelectToken("results").ToString();

            var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
            string s2 = js1.SelectToken("n_tbk_shop").ToString();
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(s2);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<storeinfo>));
            List<storeinfo> list = o as List<storeinfo>;
            var resultdata = new
            {
                StoreImage = list[0].pict_url

            };
            return Json(Comm.ToJsonResult("Success", "成功", resultdata), JsonRequestBehavior.AllowGet);
        }
        //public ActionResult sss (){
        //    ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
        //    TbkAdzoneCreateRequest req = new TbkAdzoneCreateRequest();
        //    req.SiteId = 123456L;
        //    req.AdzoneName = "广告位";
        //    TbkAdzoneCreateResponse rsp = client.Execute(req);
        //    Console.WriteLine(rsp.Body);
        //}
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult SearchGoodsByKey(string userid,string key,long? PageNo=1)
        {
            List<searchresultmodel> list2 = new List<searchresultmodel>();
            var usmodel = db.tb_userinfos.FirstOrDefault(a => a.UserID == userid);
            long tempadzoneid = 0;
            if (usmodel != null)
            {
                tempadzoneid = long.Parse(usmodel.Adzoneid);
            }
            else
            {
                tempadzoneid= 96815000311L;
            }

                ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                TbkDgMaterialOptionalRequest req = new TbkDgMaterialOptionalRequest();
                //req.StartDsr = 10L;
                req.PageSize = 20L;
                req.PageNo = PageNo;
                req.Platform = 2L;
                //req.EndTkRate = 1234L;
                //req.StartTkRate = 1234L;
                //req.EndPrice = 10L;
                //req.StartPrice = 10L;
                req.IsOverseas = false;
                req.IsTmall = false;
                req.Sort = "total_sales_des";
                //req.Itemloc = "杭州";
                //req.Cat = "16,18";
                req.Q = key;
                req.MaterialId = 6707L;
                req.HasCoupon = false;
                //req.Ip = "13.2.33.4";
                req.AdzoneId = tempadzoneid;
                //req.NeedFreeShipment = true;
                //req.NeedPrepay = true;
                //req.IncludePayRate30 = true;
                //req.IncludeGoodRate = true;
                //req.IncludeRfdRate = true;
                req.NpxLevel = 2L;
                //req.EndKaTkRate = 1234L;
                //req.StartKaTkRate = 1234L;
                //req.DeviceEncrypt = "MD5";
                //req.DeviceValue = "xxx";
                //req.DeviceType = "IMEI";
                TbkDgMaterialOptionalResponse rsp = client.Execute(req);
                var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                string s = jsondataformain.SelectToken("tbk_dg_material_optional_response").ToString();
                var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                string s1 = js.SelectToken("result_list").ToString();

                var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
                string s2 = js1.SelectToken("map_data").ToString();
                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = new StringReader(s2);
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<searchresultmodel>));
                List<searchresultmodel> list = o as List<searchresultmodel>;

            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    searchresultmodel m = new searchresultmodel();
                    m.commission_rate = list[i].commission_rate;
                    m.coupon_amount = list[i].coupon_amount;
                    m.coupon_end_time = list[i].coupon_end_time;
                    m.coupon_info = list[i].coupon_info;
                    string tempshareurl = list[i].coupon_share_url;
                    if (string.IsNullOrWhiteSpace(tempshareurl))
                    {
                        m.coupon_share_url = list[i].item_url;
                    }
                    else if (!string.IsNullOrWhiteSpace(tempshareurl) && tempshareurl.Contains("https:"))
                    {
                        m.coupon_share_url = tempshareurl;
                    }
                    else
                    {
                        m.coupon_share_url = "https:" + tempshareurl;
                    }

                    m.coupon_start_time = list[i].coupon_start_time;
                    m.item_id = list[i].item_id;
                    m.item_url = list[i].item_url;
                    m.nick = list[i].nick;
                    m.pict_url = list[i].pict_url;
                    m.seller_id = list[i].seller_id;
                    m.shop_title = list[i].shop_title;
                    m.category_name = list[i].category_name;

                    string smallimg = list[i].small_images == null ? "" : list[i].small_images.ToString();
                    string str = "";
                    if (!string.IsNullOrWhiteSpace(smallimg) && !smallimg.Equals("{}"))
                    {
                        var smallimgjson = Newtonsoft.Json.JsonConvert.DeserializeObject(smallimg) as JContainer;
                        var tempsmallimg = smallimgjson.SelectToken("string").ToList();
                        str = string.Join(",", tempsmallimg);//主图集合
                    }

                    m.small_images = str;
                    m.title = list[i].title;
                    m.user_type = list[i].user_type == "1" ? "天猫" : "淘宝";
                    m.volume = list[i].volume;
                    m.zk_final_price = list[i].zk_final_price;
                    decimal tempqhprice = decimal.Round((list[i].zk_final_price - list[i].coupon_amount), 2);
                    decimal temprate1 = decimal.Round((list[i].commission_rate / 100), 4);
                    decimal temprate = decimal.Round((temprate1 / 100), 4);
                    m.yjamount = decimal.Round(decimal.Parse((tempqhprice * temprate).ToString()), 2);
                    m.qhprice = tempqhprice;
                    list2.Add(m);
                }

            }
            return Json(Comm.ToJsonResult("Success", "成功", list2), JsonRequestBehavior.AllowGet);
        }

        //关联推荐商品(废弃)
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetRelationGoods(long goodsid)
        {
            List<searchresultmodel> list2 = new List<searchresultmodel>();
            try
            {
                ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                TbkItemRecommendGetRequest req = new TbkItemRecommendGetRequest();
                req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick,shop_title";
                req.NumIid = goodsid;
                req.Count = 6L;
                req.Platform = 2L;
                TbkItemRecommendGetResponse rsp = client.Execute(req);

                var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                string s = jsondataformain.SelectToken("tbk_item_recommend_get_response").ToString();
                var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                string s1 = js.SelectToken("results").ToString();
                var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
                string s2 = js1.SelectToken("n_tbk_item").ToString();
                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = new StringReader(s2);
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<searchresultmodel>));
                List<searchresultmodel> list = o as List<searchresultmodel>;
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string smallimg = list[i].small_images == null ? "" : list[i].small_images.ToString();
                        string str = "";
                        if (!string.IsNullOrWhiteSpace(smallimg) && !smallimg.Equals("{}"))
                        {
                            var smallimgjson = Newtonsoft.Json.JsonConvert.DeserializeObject(smallimg) as JContainer;
                            var tempsmallimg = smallimgjson.SelectToken("string").ToList();
                            str = string.Join(",", tempsmallimg);//主图集合
                        }
                        searchresultmodel tempgoods = new searchresultmodel();
                        tempgoods.small_images = str;
                        tempgoods.pict_url = list[i].pict_url;
                        tempgoods.item_id = list[i].item_id;
                        tempgoods.item_url = list[i].item_url;
                        tempgoods.nick = list[i].nick;
                        tempgoods.seller_id = list[i].seller_id;
                        tempgoods.shop_title = list[i].shop_title;
                        tempgoods.title = list[i].title;
                        tempgoods.user_type = list[i].user_type;
                        tempgoods.volume = list[i].volume;
                        tempgoods.zk_final_price = list[i].zk_final_price;

                        tempgoods.qhprice = 0;// list[i].nick;
                        tempgoods.yjamount = 0;// list[i].nick;
                        tempgoods.coupon_start_time = "";
                        list2.Add(tempgoods);
                    }
                }
                return Json(Comm.ToJsonResult("Success", "成功", list2), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Success", "成功", list2), JsonRequestBehavior.AllowGet);
            }
        }
        private class searchresultmodel {
            public string item_id { get; set; }
            public string nick { get; set; }
            public decimal coupon_amount { get; set; }
            public string shop_title { get; set; }
            public string coupon_info { get; set; }
            public long seller_id { get; set; }
            public long volume { get; set; }
            public decimal commission_rate { get; set; }
            public string item_url { get; set; }
            public string user_type { get; set; }
            public decimal zk_final_price { get; set; }
            public object small_images { get; set; }
            public string pict_url { get; set; }
            public string title { get; set; }
            public string coupon_end_time { get; set; }
            public string coupon_start_time { get; set; }
            public string coupon_share_url { get; set; }
            public decimal yjamount { get; set; }
            public decimal qhprice { get; set; }
            public string category_name { get; set; }//分类名称
        }
        private class storeinfo {
            public long user_id { get; set; }
            public string shop_title { get; set; }
            public string shop_type { get; set; }
            public string seller_nick { get; set; }
            public string pict_url { get; set; }
            public string shop_url { get; set; }
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
