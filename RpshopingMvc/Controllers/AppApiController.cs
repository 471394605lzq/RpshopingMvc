using RpshopingMvc.App_Start;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RpshopingMvc.Controllers
{
    public class AppApiController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AppApi
        /// <summary>
        /// 获取app首页数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetIndexData()
        {
            try
            {
                //获取轮播图
                string sql = string.Format(@"SELECT * FROM Selides");
                List<SelideShow> data = db.Database.SqlQuery<SelideShow>(sql).ToList();

                //获取推荐商品
                string goodssql = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.* FROM dbo.goods g {0}
                                        GROUP BY g.GoodsName,g.Code,g.Price,g.Price,g.zkprice,g.ImagePath,g.SmallImages,g.DetailPath,g.SalesVolume,g.IncomeRatio,g.Brokerage,
                                        g.BrokerageExplain,g.Postage,g.Stock,g.ByIndex,g.GoodsState,g.ID,g.IsRecommend,g.GetPath,g.Brand,g.Specs,g.SendAddress
                                        ) t WHERE GoodsState=0 AND t.Ornumber > {1} AND t.Ornumber<={2} ORDER BY ByIndex DESC", "", 0, 20);
                List<goodsshow> gooddata = db.Database.SqlQuery<goodsshow>(goodssql).ToList();

                //获取积分夺宝最新揭晓
                string ygsql = string.Format(@"SELECT top 2  i.ID,i.State,CONVERT(varchar(100), i.AnnounceTime, 20) AS AnnounceTime,i.IssueNumber,g.MainImage,g.GoodsName,u.UserName,u.UserImage FROM dbo.YGoodsIssues i 
                                                INNER JOIN dbo.YGoods g ON g.ID=i.YGoodsId
                                                INNER JOIN dbo.tb_userinfo u ON u.ID=i.LuckUserID WHERE i.State='已揭晓' ORDER BY i.AnnounceTime DESC");
                List<yginfo> ygdata = db.Database.SqlQuery<yginfo>(ygsql).ToList();
                var returndata = new
                {
                    selidedata = data,
                    goodsdata = gooddata,
                    ygsdata = ygdata
                };
                return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
    public class yginfo
    {
        public int ID { get; set; }
        public string State { get; set; }
        public string AnnounceTime { get; set; }
        public int IssueNumber { get; set; }
        public string MainImage { get; set; }
        public string GoodsName { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
    }
}