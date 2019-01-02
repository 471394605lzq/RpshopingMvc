
using RpshopingMvc.App_Start.Common;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aop.Api.Util;

namespace RpshopingMvc
{
    public partial class AliNotifyUrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //Log.WriteLog("支付宝回调", "支付宝回调", "sdf");
            string charset = "utf-8";
            IDictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;
            String[] requestItem = coll.AllKeys;
            for (int i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            string out_trade_no = Request.Form["out_trade_no"];//订单号 
            string strPrice = Request.Form["receipt_amount"];//金额 receipt_amount
            string trade_status = Request.Form["trade_status"];//交易状态
            string gmt_payment = Request.Form["gmt_payment"];//交易付款时间

            //验证
            bool flag = AlipaySignature.RSACheckV1(sArray, AliPayConfig.alipaypublickey, charset, "RSA2", false);
            //验证成功
            if (flag && (trade_status.Equals("TRADE_FINISHED") || trade_status.Equals("TRADE_SUCCESS")))
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    PayOrder order = db.PayOrders.FirstOrDefault(p => p.out_trade_no == out_trade_no && p.OrderState == Enums.Enums.OrderState.UnHandle);
                    if (order != null)
                    {
                        order.cash_fee = Decimal.Parse(strPrice);
                        order.transaction_id = string.Empty;
                        order.PayResult = string.Empty;
                        order.OrderState = trade_status == "TRADE_FINISHED" ? Enums.Enums.OrderState.Success : trade_status == "TRADE_SUCCESS" ? Enums.Enums.OrderState.Success : Enums.Enums.OrderState.Failed;
                        order.PayTime = gmt_payment;
                        int row = db.SaveChanges();
                        if (row > 0 && order.OrderState == Enums.Enums.OrderState.Success && order.OrderType == Enums.Enums.OrderType.Recharge)
                        {
                            //进行充值数据保存
                            tb_userinfo user = db.tb_userinfos.FirstOrDefault(s => s.UserID == order.User_ID);
                            var tempisfirstcharge = user.FirstCharge;
                            var tempbalance = user.Balance;
                            var addmodel = new tb_Recharge
                            {
                                CreateDateTime = DateTime.Now,
                                give = 0,
                                paytype = Enums.Enums.PayType.ali,
                                R_Money = order.cash_fee,
                                U_ID = user.ID,
                                RechargeType = ((Enums.Enums.RechargeType)order.cash_fee),
                                UserID = user.UserID,
                                PayOrderID = order.ID
                            };
                            user.Balance = tempbalance + order.cash_fee;
                            db.tb_Recharges.Add(addmodel);
                            db.SaveChanges();
                            Response.Write("success");
                            Response.End();
                        }
                    }
                }
            }
            else
            {
                Response.Write("fail");
                Response.End();
            }
            //}
            //catch (Exception ex)
            //{
            //    Log.WriteLog("支付宝支付回调错误", "支付宝回调", ex.Message);
            //}
        }
    }
}