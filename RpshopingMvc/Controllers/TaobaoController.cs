using Newtonsoft.Json;
using RpshopingMvc.App_Start;
using RpshopingMvc.App_Start.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace RpshopingMvc.Controllers
{
    public class TaobaoController : Controller
    {
        //获取选品库列表
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetFavoritesList()
        {
            ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret,"json");
            TbkUatmFavoritesGetRequest req = new TbkUatmFavoritesGetRequest();
            req.PageNo = 1L;
            req.PageSize = 20L;
            req.Fields = "favorites_title,favorites_id,type";
            req.Type = -1L;
            TbkUatmFavoritesGetResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);
            var returndata = new
            {
                body = rsp.Body
            };
            return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
            //return View();
        }

        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetFavoritesGoodsList()
        {
            ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
            TbkUatmFavoritesItemGetRequest req = new TbkUatmFavoritesItemGetRequest();
            req.Platform = 1L;
            req.PageSize = 20L;
            req.AdzoneId = 96815000311L;
            req.Unid = "3456";
            req.FavoritesId = 10010L;
            req.PageNo = 2L;
            req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick,shop_title,zk_final_price_wap,event_start_time,event_end_time,tk_rate,status,type";
            TbkUatmFavoritesItemGetResponse rsp = client.Execute(req);
            var returndata = new
            {
                body = rsp.Body
            };
            return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
        }
    }
}