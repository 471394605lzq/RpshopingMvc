using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public static Wininet http = new Wininet();
        //获取选品库列表
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetFavoritesList()
        {
            ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
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
        #region 淘宝客邀请码生成
        /// <summary>
        /// 淘宝客邀请码生成
        /// </summary>
        /// <param name="relation_id">渠道关系ID 为0时表示不传</param>
        /// <param name="code_type">邀请码类型，1 - 渠道邀请，2 - 渠道裂变，3 -会员邀请/param>
        /// <returns>返回生成的邀请码</returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetInvitecode(long relation_id, long code_type)
        {
            try
            {
                GetAuthoCode();
                ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                TbkScInvitecodeGetRequest req = new TbkScInvitecodeGetRequest();
                if (relation_id != 0)
                {
                    req.RelationId = relation_id;
                }
                req.RelationApp = "common";
                req.CodeType = code_type;
                TbkScInvitecodeGetResponse rsp = client.Execute(req);
                var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                string s = jsondataformain.SelectToken("tbk_sc_invitecode_get_response").ToString();
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;//转json格式
                string returndata = data.SelectToken("data").ToString();
                return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Fail", "获取失败", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 淘宝客渠道信息备案
        /// <summary>
        /// 淘宝客渠道信息备案
        /// </summary>
        /// <param name="inviter_code">淘宝客邀请渠道的邀请码</param>
        /// <param name="code_type">邀请码类型，1 - 渠道邀请，2 - 渠道裂变，3 -会员邀请/param>
        /// <returns>返回备案成功的信息</returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult PublisherInfo(string inviter_code)
        {
            try
            {
                ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                TbkScPublisherInfoSaveRequest req = new TbkScPublisherInfoSaveRequest();
                //req.RelationFrom = "1";
                //req.OfflineScene = "1";
                //req.OnlineScene = "1";
                req.InviterCode = inviter_code;
                req.InfoType = 1L;
                //req.Note = "小蜜蜂";
                TbkScPublisherInfoSaveResponse rsp = client.Execute(req);
                var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                string s = jsondataformain.SelectToken("tbk_sc_publisher_info_save_response").ToString();
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;//转json格式
                string returndata = data.SelectToken("data").ToString();
                return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Fail", "获取失败", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //获取授权码
        private string GetAuthoCode()
        {
            string url = "http://container.open.taobao.com/container?authcode={"+ AliPayConfig .AuthoCode+ "}";
            string JsonData = http.Get(AliPayConfig.authourl);
            string adzoneId =AlmmCommon.Get_Middle_Text(JsonData, ",\"top_session\":", "},\"info\"");
            return adzoneId;
        }
    }
}