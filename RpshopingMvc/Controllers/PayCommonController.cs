using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
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
            if (body != Enums.Enums.OrderType.Recharge && body != Enums.Enums.OrderType.OrderPay)
            {
                return Json(Comm.ToJsonResult("Error", "请求参数错误"), JsonRequestBehavior.AllowGet);
            }
            if (type != Enums.Enums.RechargeType.Fifty && type != Enums.Enums.RechargeType.FiveHundred && type != Enums.Enums.RechargeType.Hundred && type != Enums.Enums.RechargeType.Ten && type != Enums.Enums.RechargeType.Thirty && type != Enums.Enums.RechargeType.TwoHundred)
            {
                return Json(Comm.ToJsonResult("Error", "请求参数错误"), JsonRequestBehavior.AllowGet);
            }
            WxPayData parmdata = new WxPayData();
            string out_trade_no = WxPayApi.GenerateOutTradeNo();
            parmdata.SetValue("body", ((Enums.Enums.OrderType)body).GetDisplayName());//商品描述
            parmdata.SetValue("attach", "逸趣网络科技有限公司");//附加数据
            parmdata.SetValue("out_trade_no", out_trade_no);//商户订单号
            parmdata.SetValue("total_fee", Convert.ToInt32(type.GetDisplayName()));//总金额 * 100
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
                    //var stringA = $"appid={WxPayConfig.APPID}&noncestr={noncestr}&package=Sign=WXPay&partnerid={WxPayConfig.MCHID}&prepayid={prepay_id}&timestamp={Unite.GenerateTimeStamp(DateTime.Now)}&key={WxPayConfig.KEY}";
                    //var sign = Unite.ToMD5New(stringA).ToUpper();

                    //保存下单信息到数据库
                    PayOrder model = new PayOrder();
                    model.OrderState = Enums.Enums.OrderState.UnHandle;
                    model.out_trade_no = out_trade_no;
                    model.Paynoncestr = noncestr;
                    model.PayPrepay_id = prepay_id;
                    model.settlement_total_fee = Convert.ToInt32(type.GetDisplayName());
                    model.CreateTime = DateTime.Now;
                    model.Sign = signstr;
                    model.total_fee = Convert.ToInt32(type.GetDisplayName());
                    model.User_ID = userID;
                    model.OrderType = body;
                    db.PayOrders.Add(model);
                    int resultrow = db.SaveChanges();
                    //保存订单数据结果
                    if (resultrow > 0)
                    {

                        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
                        long ts = (long)(DateTime.Now - startTime).TotalSeconds; // 相差秒数

                        System.Text.StringBuilder paySignpar = new System.Text.StringBuilder();
                        paySignpar.Append($"appid={resultdata.GetValue("appid")?.ToString()}");
                        paySignpar.Append($"&noncestr={noncestr}");
                        paySignpar.Append($"&package=Sign=WXPay&partnerid={WxPayConfig.MCHID}");
                        paySignpar.Append($"&prepayid={resultdata.GetValue("prepay_id")?.ToString()}");
                        //paySignpar.Append($"&signType=MD5");
                        paySignpar.Append($"&timestamp={ts.ToString()}");
                        paySignpar.Append($"&key={WxPayConfig.KEY ?? string.Empty}");
                        string strPaySignpar = paySignpar.ToString();

                        var sign = Unite.GetMd5Hash(strPaySignpar).ToUpper();
                        //dynamic retModel = new
                        //{
                        //    timeStamp = ts.ToString(),
                        //    nonceStr = resultdata.GetValue("nonce_str")?.ToString(),
                        //    package = "prepay_id=" + resultdata.GetValue("prepay_id")?.ToString(),
                        //    signType = "MD5",
                        //    paySign = sign,
                        //    total_fee = model.total_fee / 100m,
                        //};

                        var returndata = new
                        {
                            result = resultcode,
                            timestamp = ts.ToString(),
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


        /// <summary>
        /// 支付宝支付生成签名字符串
        /// </summary>
        /// <param name="body"></param>
        /// <param name="type"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AliPayCreateSignStr(Enums.Enums.OrderType body, RechargeType type, string userID)
        {
            try
            {
                if (!db.tb_userinfos.Any(s => s.UserID == userID))
                {
                    return Json(Comm.ToJsonResult("Error", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                if (body!=Enums.Enums.OrderType.Recharge&&body!=Enums.Enums.OrderType.OrderPay)
                {
                    return Json(Comm.ToJsonResult("Error", "请求参数错误"), JsonRequestBehavior.AllowGet);
                }
                if (type != Enums.Enums.RechargeType.Fifty && type != Enums.Enums.RechargeType.FiveHundred && type != Enums.Enums.RechargeType.Hundred && type != Enums.Enums.RechargeType.Ten && type != Enums.Enums.RechargeType.Thirty && type != Enums.Enums.RechargeType.TwoHundred)
                {
                    return Json(Comm.ToJsonResult("Error", "请求参数错误"), JsonRequestBehavior.AllowGet);
                }
                string appid = AliPayConfig.appid;//appid
                string app_private_key = AliPayConfig.app_private_key;//私钥
                string alipay_public_key = AliPayConfig.app_public_key;//公钥
                string charset = "utf-8";
                string outtradeno = AliPayConfig.GenerateOutTradeNo();//订单号
                string notifyurl = AliPayConfig.notifyurl;//回调通知页面地址
                IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", appid, app_private_key.Trim(), "json", "1.0", "RSA2", alipay_public_key.Trim(), charset, false);
                //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
                AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
                //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
                AlipayTradeAppPayModel model = new AlipayTradeAppPayModel();
                model.Body = ((Enums.Enums.OrderType)body).GetDisplayName();
                model.Subject = "充值";
                model.TotalAmount = "0.1"; //type.GetDisplayName();
                model.ProductCode = AliPayConfig.productcode;
                model.OutTradeNo = outtradeno;
                model.TimeoutExpress = "30m";
                request.SetBizModel(model);
                request.SetNotifyUrl(notifyurl);
                //这里和普通的接口调用不同，使用的是sdkExecute
                AlipayTradeAppPayResponse response = client.SdkExecute(request);
                //HttpUtility.HtmlEncode是为了输出到页面时防止被浏览器将关键参数html转义，实际打印到日志以及http传输不会有这个问题
                //Response.Write(HttpUtility.HtmlEncode(response.Body));
                string resultcode = HttpUtility.HtmlEncode(response.Body);
                if (!string.IsNullOrWhiteSpace(resultcode))
                {
                    //保存下单信息到数据库
                    PayOrder paymodel = new PayOrder();
                    paymodel.OrderState = Enums.Enums.OrderState.UnHandle;
                    paymodel.out_trade_no = outtradeno;
                    paymodel.Paynoncestr = string.Empty;
                    paymodel.PayPrepay_id = outtradeno;
                    paymodel.settlement_total_fee = Convert.ToInt32(type.GetDisplayName());
                    paymodel.CreateTime = DateTime.Now;
                    paymodel.Sign = resultcode;
                    paymodel.total_fee = Convert.ToInt32(type.GetDisplayName());
                    paymodel.User_ID = userID;
                    paymodel.OrderType = body;
                    db.PayOrders.Add(paymodel);
                    int resultrow = db.SaveChanges();
                    if (resultrow > 0)
                    {
                        string tempresult = resultcode.Replace("amp;", "");
                        var returndata = new
                        {
                            result = tempresult,
                            prepay_id = outtradeno
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
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}