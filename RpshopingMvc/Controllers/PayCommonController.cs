using RpshopingMvc.App_Start;
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
        /// 微信统计下单
        /// </summary>
        /// <param name="body">描述</param>
        /// <param name="type">充值类型</param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult UnifiedOrder(string body, RechargeType type)
        {
            WxPayData parmdata = new WxPayData();
            string out_trade_no = WxPayApi.GenerateOutTradeNo();
            parmdata.SetValue("body", body);//商品描述
            parmdata.SetValue("attach", "逸趣网络科技有限公司");//附加数据
            parmdata.SetValue("out_trade_no", out_trade_no);//商户订单号
            parmdata.SetValue("total_fee", type.GetHashCode());//总金额
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
                }
                var returndata = new
                {
                    result = resultcode,
                    prepay_id = prepay_id,
                    noncestr = noncestr
                };
                PayOrder model = new PayOrder();
                model.OrderState = PayOrderState.wait;
                model.out_trade_no = out_trade_no;
                model.Paynoncestr = noncestr;
                model.PayPrepay_id = prepay_id;
                model.PayTime = "";
                model.settlement_total_fee = type.GetHashCode();
                model.CreateTime = DateTime.Now;
                model.Sign = signstr;
                model.total_fee = type.GetHashCode();
                model.User_ID = 0;
                db.PayOrders.Add(model);
                db.SaveChanges();
                return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(Comm.ToJsonResult("Error", "下单失败"), JsonRequestBehavior.AllowGet);
            }

            
        }
    }
}