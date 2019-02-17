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
            ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest", AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret,"json");
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
    }
}