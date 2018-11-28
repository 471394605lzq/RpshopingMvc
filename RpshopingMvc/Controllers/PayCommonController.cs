using RpshopingMvc.App_Start;
using RpshopingMvc.App_Start.Common;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Controllers
{
    /// <summary>
    /// 付款公用控制器
    /// </summary>
    public class PayCommonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// 微信统一下单
        /// </summary>
        /// <param name="body">描述</param>
        /// <param name="type">充值类型</param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult ToOrder(Enums.Enums.OrderType body, RechargeType type, string userID)
        {
            if (!db.tb_userinfos.Any(s => s.UserID == userID))
            {
                return Json(Comm.ToJsonResult("Error", "用户不存在"), JsonRequestBehavior.AllowGet);
            }
            WxPayData parmdata = new WxPayData();
            string out_trade_no = WxPayApi.GenerateOutTradeNo();
            parmdata.SetValue("body", ((Enums.Enums.OrderType)body).GetDisplayName());//商品描述
            parmdata.SetValue("attach", "逸趣网络科技有限公司");//附加数据
            parmdata.SetValue("out_trade_no", out_trade_no);//商户订单号
            parmdata.SetValue("total_fee", Convert.ToInt32(type.GetDisplayName()) * 100);//总金额
            parmdata.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
            parmdata.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
            parmdata.SetValue("goods_tag", "");//商品标记
            parmdata.SetValue("trade_type", "APP");//交易类型
            //parmdata.SetValue("product_id", productid);//商品ID

            WxPayData resultdata = WxPayApi.UnifiedOrder(parmdata);
            string resultcode = resultdata.GetValue("return_code").ToString();
            if (resultcode.Equals("SUCCESS"))
            {
                string signstr = resultdata.GetValue("sign").ToString();
                string noncestr = WxPayApi.GenerateNonceStr();
                string result_code = resultdata.GetValue("result_code").ToString();
                string prepay_id = string.Empty;
                if (result_code.Equals("SUCCESS"))
                {
                    prepay_id = resultdata.GetValue("prepay_id").ToString();
                    var stringA = $"appid={WxPayConfig.APPID}&noncestr={noncestr}&package=Sign=WXPay&partnerid={WxPayConfig.MCHID}&prepayid={prepay_id}&timestamp={Unite.GenerateTimeStamp(DateTime.Now)}&key={WxPayConfig.KEY}";
                    var sign = Unite.ToMD5New(stringA);

                    //保存下单信息到数据库
                    PayOrder model = new PayOrder();
                    model.OrderState = Enums.Enums.OrderState.UnHandle;
                    model.out_trade_no = out_trade_no;
                    model.Paynoncestr = noncestr;
                    model.PayPrepay_id = prepay_id;
                    model.settlement_total_fee = Convert.ToInt32(type.GetDisplayName()) * 100;
                    model.CreateTime = DateTime.Now;
                    model.Sign = signstr;
                    model.total_fee = Convert.ToInt32(type.GetDisplayName()) * 100;
                    model.User_ID = userID;
                    model.OrderType = body;
                    db.PayOrders.Add(model);
                    int resultrow = db.SaveChanges();
                    //保存订单数据结果
                    if (resultrow > 0)
                    {
                        var returndata = new
                        {
                            result = resultcode,
                            prepay_id = prepay_id,
                            noncestr = noncestr,
                            sign = sign
                        };
                        return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(Comm.ToJsonResult("Error", "下单失败"), JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(Comm.ToJsonResult("Error", "下单失败"), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(Comm.ToJsonResult("Error", "下单失败"), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult Test(int? ss)
        {
            return Json(Comm.ToJsonResult("Success", "1", ss), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询返回支付结果
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        [AllowCrossSiteJson]
        public ActionResult QueryOrder(string ordercode, string userid)
        {
            try
            {
                string resultcode = "";
                string resultmessage = "";
                if (!db.tb_userinfos.Any(s => s.UserID == userid))
                {
                    return Json(Comm.ToJsonResult("Error", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                var query = db.PayOrders.FirstOrDefault(s => s.PayPrepay_id == ordercode && s.User_ID == userid);
                if (query == null)
                {
                    resultcode = "NotFind";
                    resultmessage = "订单不存在";
                }
                if (query.OrderState == Enums.Enums.OrderState.UnHandle)
                {
                    resultcode = "Doing";
                    resultmessage = "订单正在处理中";
                }
                if (query.OrderState == Enums.Enums.OrderState.Failed)
                {
                    resultcode = "Fail";
                    resultmessage = "支付失败";
                }
                if (query.OrderState == Enums.Enums.OrderState.Success)
                {
                    resultcode = "Success";
                    resultmessage = "支付成功";
                }
                if (query.OrderState == Enums.Enums.OrderState.Canceled)
                {
                    resultcode = "Cance";
                    resultmessage = "订单已取消";
                }
                return Json(Comm.ToJsonResult(resultcode, resultmessage), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}