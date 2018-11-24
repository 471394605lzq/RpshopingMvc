using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpshopingMvc.App_Start.Common
{
    public class PayResultNotify: Notify
    {
        public PayResultNotify(System.Web.UI.Page page) : base(page)
        {
            base.page = page;
        }

        /// <summary>
        /// 接收返回的结果，根据结果处理订单状态
        /// </summary>
        public override void ProcessNotify()
        {

            WxPayData notifyData = GetNotifyData();
            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {

                Log.Debug("notifyData", $"notifyData:完成");
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                Log.Error(this.GetType().ToString(), "The Pay result is error : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }

            string transaction_id = notifyData.GetValue("transaction_id").ToString();
            string out_trade_no = notifyData.GetValue("out_trade_no").ToString();
            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                page.Response.Write(res.ToXml());
                page.Response.End();
            }
            //查询订单成功
            else
            {
                int row = 0;
                #region 更改订单状态，保存支付结果
                if (!string.IsNullOrEmpty(out_trade_no))
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        PayOrder order = db.PayOrders.FirstOrDefault(p => p.out_trade_no == out_trade_no && p.OrderState == Enums.Enums.OrderState.UnHandle);
                        if (order != null)
                        {
                            order.cash_fee = Convert.ToDecimal(notifyData.GetValue("total_fee").ToString()) / 100m;
                            order.transaction_id = transaction_id;
                            order.PayResult = notifyData.ToJson();
                            order.OrderState = notifyData.GetValue("result_code").ToString() == "SUCCESS" ? Enums.Enums.OrderState.Success : Enums.Enums.OrderState.Failed;
                            order.PayTime = DateTime.Now.ToString();
                            row = db.SaveChanges();
                            if (row > 0 && order.OrderState == Enums.Enums.OrderState.Success)
                            {
                                //进行充值数据保存
                            }
                        }
                    }
                }
                #endregion
                if (row > 0)
                {
                    WxPayData res = new WxPayData();
                    res.SetValue("return_code", "SUCCESS");
                    res.SetValue("return_msg", "OK");
                    Log.Info(this.GetType().ToString(), "order query success : " + res.ToXml());
                    page.Response.Write(res.ToXml());
                    page.Response.End();
                }
                else
                {//如果没更新成功，就继续接收通知
                    WxPayData res = new WxPayData();
                    res.SetValue("return_code", "FAIL");
                    res.SetValue("return_msg", "未接收成功");
                    Log.Error(this.GetType().ToString(), "The recieve result is error : " + res.ToXml());
                    page.Response.Write(res.ToXml());
                    page.Response.End();
                }

            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}