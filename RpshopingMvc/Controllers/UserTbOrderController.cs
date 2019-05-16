using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RpshopingMvc.App_Start;
using RpshopingMvc.App_Start.Common;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Controllers
{
    public class UserTbOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// 新增用户订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddUserOrder(Tborder model)
        {
            try
            {
                //var goods = db.tb_goods.Find(model.GoodsID);
                //var us = db.tb_userinfos.FirstOrDefault(s => s.UserID == model.YUserID);
                //var usgrade = us.UserGrade;
                //decimal temprebatemoney = 0;
                ////初级会员
                //if (usgrade == UserGrade.Primary)
                //{
                //    temprebatemoney = decimal.Round((goods.Brokerage * (decimal)0.1), 2);
                //}
                ////高级会员、运营商、合伙人
                //else
                //{
                //    temprebatemoney = decimal.Round((goods.Brokerage * (decimal)0.5), 2);
                //}

                //model.BalanceTime = Convert.ToDateTime("1990-01-01");
                //model.GoodsImage = goods.ImagePath;
                //model.GoodsName = goods.GoodsName;
                //model.OrderPrice = goods.Qhprice;
                //model.OrderState = TbOrderState.NoBalance;
                //model.UserID = us.ID;
                //model.RebateMoney = temprebatemoney;
                //model.OrderTime = DateTime.Now;
                //db.Tborder.Add(model);
                //db.SaveChanges();
                return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 搜索结果新增用户订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddUserOrderFromSearch(Tborder model)
        {
            try
            {
                ////var goods = db.tb_goods.Find(model.GoodsID);
                //var us = db.tb_userinfos.FirstOrDefault(s => s.UserID == model.YUserID);
                //var usgrade = us.UserGrade;
                //decimal temprebatemoney = 0;
                ////初级会员
                //if (usgrade == UserGrade.Primary)
                //{
                //    temprebatemoney = decimal.Round((model.RebateMoney * (decimal)0.1), 2);
                //}
                ////高级会员、运营商、合伙人
                //else
                //{
                //    temprebatemoney = decimal.Round((model.RebateMoney * (decimal)0.5), 2);
                //}
                //model.BalanceTime = Convert.ToDateTime("1990-01-01");
                //model.OrderState = TbOrderState.NoBalance;
                //model.UserID = us.ID;
                //model.OrderTime = DateTime.Now;
                //model.RebateMoney = temprebatemoney;
                //db.Tborder.Add(model);
                //db.SaveChanges();
                return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取用户收入明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetUserSettlement(string userids, int? page = 1, int? pageSize = 20)
        {
            try
            {
                var usermodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == userids);
                if (usermodel == null)
                {
                    return Json(Comm.ToJsonResult("nofind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int uid = usermodel.ID;
                    int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                    int endpagesize = page.Value * pageSize.Value;
                    //拼接参数
                    SqlParameter[] parameters = {
                        new SqlParameter("@userid", SqlDbType.Int),
                        new SqlParameter("@starpagesize", SqlDbType.Int),
                        new SqlParameter("@endpagesize", SqlDbType.Int)
                    };
                    parameters[0].Value = uid;
                    parameters[1].Value = starpagesize;
                    parameters[2].Value = endpagesize;
                    string sqlstr = string.Empty;
                    sqlstr = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.SettlementMoney,g.SettlementTime FROM dbo.UserSettlements g
                                                WHERE g.SettlementMoney>0 and g.UserID=@userid
                                                GROUP BY g.SettlementMoney,g.SettlementTime) t 
                                                WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize");
                    List<tempsettlement> data = db.Database.SqlQuery<tempsettlement>(sqlstr, parameters).ToList();
                    List<tempsettlement> datalist = new List<tempsettlement>();
                    if (data.Count>0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            tempsettlement tempdata = new tempsettlement();
                            tempdata.SettlementMoney = data[i].SettlementMoney;
                            tempdata.SettlementTimestr = data[i].SettlementTime.ToString();
                            datalist.Add(tempdata);
                        }
                    }
                    return Json(Comm.ToJsonResult("Success", "成功", datalist), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取用户订单明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetUserOrder(string userids,string typecode, int? page = 1, int? pageSize = 20)
        {
            try
            {
                var usermodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == userids);
                if (usermodel == null)
                {
                    return Json(Comm.ToJsonResult("nofind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int userid = usermodel.ID;
                    int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                    int endpagesize = page.Value * pageSize.Value;
                    List<temptborder> data = new List<temptborder>();
                    //查询所有订单
                    if (typecode == "all")
                    {
                        //拼接参数
                        SqlParameter[] parameters = {
                        new SqlParameter("@userid", SqlDbType.Int),
                        new SqlParameter("@Adzoneid",SqlDbType.NVarChar),
                        new SqlParameter("@starpagesize", SqlDbType.Int),
                        new SqlParameter("@endpagesize", SqlDbType.Int)
                         };
                        parameters[0].Value = userid;
                        parameters[1].Value = usermodel.Adzoneid;
                        parameters[2].Value = starpagesize;
                        parameters[3].Value = endpagesize;
                        string sqlstr = string.Empty;
                        sqlstr = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.GoodsName,g.OrderCode,g.OrderPrice,g.GoodsImage,
                                            g.OrderTime,g.SettlementTime,us.EstimateIncome,g.GoodsID,g.ID,g.OrderState
                                             FROM dbo.Tborders g 
                                             INNER JOIN dbo.UserSettlements us ON us.OrderCode=g.OrderCode
                                             WHERE us.UserID=@userid AND us.FromUserAdzoneid=@Adzoneid and us.EstimateIncome>0 GROUP BY g.GoodsName,g.OrderCode,g.OrderPrice,g.GoodsImage,
                                            g.OrderTime,g.SettlementTime,us.EstimateIncome,g.GoodsID,g.ID,g.OrderState) t 
                                                WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize");
                        data = db.Database.SqlQuery<temptborder>(sqlstr, parameters).ToList();
                    }
                    //查询待结算和已结算订单
                    else if (typecode == "settlementstr"|| typecode== "alreadysettledstr")
                    {
                        int state = 0;
                        if (typecode == "settlementstr")
                        {
                            state = 0;
                        }
                        else if (typecode == "alreadysettledstr") {
                            state = 1;
                        }
                        //拼接参数
                        SqlParameter[] parameters = {
                        new SqlParameter("@userid", SqlDbType.Int),
                        new SqlParameter("@state", SqlDbType.Int),
                        new SqlParameter("@starpagesize", SqlDbType.Int),
                        new SqlParameter("@endpagesize", SqlDbType.Int),
                        new SqlParameter("@Adzoneid",SqlDbType.NVarChar),

                         };
                        parameters[0].Value = userid;
                        parameters[1].Value = state;
                        parameters[2].Value = starpagesize;
                        parameters[3].Value = endpagesize;
                        parameters[4].Value = usermodel.Adzoneid;
                        string sqlstr = string.Empty;
                        sqlstr = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.GoodsName,g.OrderCode,g.OrderPrice,g.GoodsImage,
                                            g.OrderTime,g.SettlementTime,us.EstimateIncome,g.GoodsID,g.ID,g.OrderState
                                             FROM dbo.Tborders g 
                                             INNER JOIN dbo.UserSettlements us ON us.OrderCode=g.OrderCode
                                             WHERE us.UserID=@userid and us.SettlementState=@state AND us.FromUserAdzoneid=@Adzoneid  AND us.EstimateIncome>0 GROUP BY g.GoodsName,g.OrderCode,g.OrderPrice,g.GoodsImage,
                                            g.OrderTime,g.SettlementTime,us.EstimateIncome,g.GoodsID,g.ID,g.OrderState) t 
                                                WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize");
                        data = db.Database.SqlQuery<temptborder>(sqlstr, parameters).ToList();
                    }
                    else if (typecode== "subordinatestr")
                    {
                        //拼接参数
                        SqlParameter[] param = {
                        new SqlParameter("@userid", SqlDbType.Int),
                         };
                        param[0].Value = userid;
                        string tempsql =string.Format(@"SELECT DISTINCT b.ID FROM dbo.tb_userinfo a  CROSS JOIN dbo.tb_userinfo b
                                                        WHERE b.ParentID = @userid");
                        List<tempuserid> useriddata = db.Database.SqlQuery<tempuserid>(tempsql, param).ToList();
                        string tempuserstr = "";
                        if (useriddata.Count>0)
                        {
                            for (int i = 0; i < useriddata.Count; i++)
                            {
                                if (tempuserstr == "")
                                {
                                    tempuserstr = useriddata[i].ID.ToString();
                                }
                                else
                                {
                                    tempuserstr = tempuserstr + "," + useriddata[i].ID.ToString();
                                }
                            }
                            //拼接参数
                            SqlParameter[] parameters = {
                                new SqlParameter("@useridstr", SqlDbType.NVarChar),
                                new SqlParameter("@starpagesize", SqlDbType.Int),
                                new SqlParameter("@endpagesize", SqlDbType.Int)
                             };
                            parameters[0].Value = tempuserstr;
                            parameters[1].Value = starpagesize;
                            parameters[2].Value = endpagesize;
                            string sqlstr = string.Empty;
                            sqlstr = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.GoodsName,g.OrderCode,g.OrderPrice,g.GoodsImage,
                                            g.OrderTime,g.SettlementTime,us.EstimateIncome,g.GoodsID,g.ID,g.OrderState
                                             FROM dbo.Tborders g 
                                             INNER JOIN dbo.UserSettlements us ON us.OrderCode=g.OrderCode
                                             WHERE us.UserID in(@useridstr) AND us.EstimateIncome>0 GROUP BY g.GoodsName,g.OrderCode,g.OrderPrice,g.GoodsImage,
                                            g.OrderTime,g.SettlementTime,us.EstimateIncome,g.GoodsID,g.ID,g.OrderState) t 
                                                WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize");
                            data = db.Database.SqlQuery<temptborder>(sqlstr, parameters).ToList();
                        }
                    }
                    List<temptborder> tempdata = new List<temptborder>();
                    if (data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            temptborder model = new temptborder();
                            string tempimg = data[i].GoodsImage;
                            if (string.IsNullOrWhiteSpace(tempimg))
                            {
                                //根据商品id获取商品图片
                                ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                                TbkItemInfoGetRequest req = new TbkItemInfoGetRequest();
                                req.NumIids = data[i].GoodsID;
                                req.Platform = 2L;
                                //req.Ip = "11.22.33.43";
                                TbkItemInfoGetResponse rsp = client.Execute(req);
                                var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                                string s = jsondataformain.SelectToken("tbk_item_info_get_response").ToString();
                                var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                                string s1 = js.SelectToken("results").ToString();
                                var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
                                string s2 = js1.SelectToken("n_tbk_item").ToString();
                                s2 = s2.TrimEnd(']');
                                s2 = s2.TrimStart('[');
                                Newtonsoft.Json.Linq.JObject datas = JsonHelper.DeserializeObject(s2.Trim());
                                if (datas != null)
                                {
                                    int tempid = data[i].ID;
                                    string tempstr = datas.SelectToken("pict_url").ToString();
                                    Tborder ordermodel = db.Tborder.Find(tempid);
                                    ordermodel.GoodsImage = tempstr;
                                    db.SaveChanges();
                                    model.GoodsImage = tempstr;
                                }
                            }
                            else
                            {
                                model.GoodsImage = data[i].GoodsImage;
                            }
                            model.GoodsName = data[i].GoodsName;
                            model.OrderCode = data[i].OrderCode;
                            model.OrderPrice = data[i].OrderPrice;
                            model.OrderTimestr = data[i].OrderTime.ToString();
                            model.EstimateIncome = data[i].EstimateIncome;
                            model.SettlementTimestr = data[i].SettlementTime.ToString();
                            tempdata.Add(model);
                        }
                        return Json(Comm.ToJsonResult("Success", "成功", tempdata), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(Comm.ToJsonResult("Success", "成功", tempdata), JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult TX(string userids)
        {
            var usermodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == userids);
            int thisdaystr = DateTime.Now.Day;

            if (usermodel == null)
            {
                return Json(Comm.ToJsonResult("nofind", "用户不存在"), JsonRequestBehavior.AllowGet);
            }
            else if (thisdaystr!=10)
            {
                return Json(Comm.ToJsonResult("noday", "每月10号为提现日"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                string appid = AliPayConfig.appid;//appid
                string app_private_key = AliPayConfig.app_private_key;//私钥
                string alipay_public_key = AliPayConfig.app_public_key;//公钥
                string charset = "utf-8";
                string out_biz_no = AliPayConfig.GenerateOutTradeNo();//订单号
                IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", appid, app_private_key.Trim(), "json", "1.0", "RSA2", alipay_public_key.Trim(), charset, false);
                AlipayFundTransToaccountTransferRequest request = new AlipayFundTransToaccountTransferRequest();
                string timestr = DateTime.Now.AddMonths(-1).Month.ToString();
                decimal txamount = usermodel.Balance;
                string alipayaccount = usermodel.AliAccount;
                string aliusername = usermodel.AliUserName;
                //如果余额不足
                if (txamount <= 0)
                {
                    return Json(Comm.ToJsonResult("nobalance", "余额不足"), JsonRequestBehavior.AllowGet);
                }
                //如果没绑定支付宝账号
                else if (string.IsNullOrWhiteSpace(alipayaccount))
                {
                    return Json(Comm.ToJsonResult("noaccount", "未绑定支付宝"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string remarkstr = timestr + "月佣金提现";
                    request.BizContent = "{" +
                    "\"out_biz_no\":\"" + out_biz_no + "\"," +
                    "\"payee_type\":\"ALIPAY_LOGONID\"," +
                    "\"payee_account\":\"" + alipayaccount + "\"," +
                    "\"amount\":\"" + txamount.ToString() + "\"," +
                    "\"payer_show_name\":\"RP云购佣金提现\"," +
                    "\"payee_real_name\":\"" + aliusername + "\"," +
                    "\"remark\":\"" + remarkstr + "\"" +
                    "  }";
                    AlipayFundTransToaccountTransferResponse response = client.Execute(request);
                    var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Body) as JContainer;//转json格式
                    string signstr = jsondataformain.SelectToken("sign").ToString();
                    string s = jsondataformain.SelectToken("alipay_fund_trans_toaccount_transfer_response").ToString();
                    var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                    string msg = js.SelectToken("msg").ToString();
                    if (msg.Equals("Success"))
                    {
                        string rout_biz_no = js.SelectToken("out_biz_no").ToString();
                        string order_id = js.SelectToken("order_id").ToString();
                        string pay_date = js.SelectToken("pay_date").ToString();
                        //保存提现记录
                        Withdrawcash wmodel = new Withdrawcash();
                        wmodel.AliAccount = alipayaccount;
                        wmodel.order_id = order_id;
                        wmodel.out_biz_no = rout_biz_no;
                        wmodel.pay_date = pay_date;
                        wmodel.signstr = signstr;
                        wmodel.txamount = txamount;
                        wmodel.txmonth = timestr;
                        wmodel.Userid = usermodel.ID;
                        wmodel.UserName = usermodel.UserName;
                        db.Withdrawcash.Add(wmodel);
                        usermodel.Balance = 0;
                        db.SaveChanges();
                        return Json(Comm.ToJsonResult("Success", "提现成功"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(Comm.ToJsonResult("txfail", "提现失败"), JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }
        public class tempuserid{
            public int ID { get; set; }
        }
        public class tempsettlement {
            public decimal SettlementMoney { get; set; }
            public DateTime SettlementTime { get; set; }
            public string SettlementTimestr { get; set; }
        }
        public class temptborder {
            public string GoodsName { get; set; }
            public string OrderCode { get; set; }
            public decimal OrderPrice { get; set; }
            public string GoodsImage { get; set; }
            public DateTime OrderTime { get; set; }
            public DateTime SettlementTime { get; set; }
            public string OrderTimestr { get; set; }
            public string SettlementTimestr { get; set; }
            public decimal EstimateIncome { get; set; }
            public string GoodsID { get; set; }
            public int ID { get; set; }
            public int OrderState { get; set; }
        }
        public class stroemodel
        {
            //public string cat_leaf_name { get; set; }
            //public string cat_name { get; set; }
            //public string item_url { get; set; }
            public string pict_url { get; set; }
            //public string num_iid { get; set; }
            //public string title { get; set; }
            //public string user_type { get; set; }
            //public int material_lib_type { get; set; }
            //public string nick { get; set; }
            //public string provcity { get; set; }
            //public decimal reserve_price { get; set; }
            //public long seller_id { get; set; }
            //public object small_images { get; set; }
            //public int volume { get; set; }
            //public decimal zk_final_price { get; set; }
        }

    //        "n_tbk_item": [
    //  {
    //    "cat_leaf_name": "健身套装",
    //    "cat_name": "运动服/休闲服装",
    //    "item_url": "https://detail.m.tmall.com/item.htm?id=574776744816",
    //    "material_lib_type": "1",
    //    "nick": "百斯邦旗舰店",
    //    "num_iid": 574776744816,
    //    "pict_url": "https://img.alicdn.com/bao/uploaded/i2/3569753962/O1CN01mMBtPb1f8dj1WoycZ_!!0-item_pic.jpg",
    //    "provcity": "广东 东莞",
    //    "reserve_price": "199",
    //    "seller_id": 3569753962,
    //    "small_images": {
    //      "string": [
    //        "https://img.alicdn.com/i2/3569753962/O1CN01XqFxha1f8dj2fKJH8_!!3569753962.jpg",
    //        "https://img.alicdn.com/i3/3569753962/O1CN01foU7fu1f8diooTZl6_!!3569753962.jpg",
    //        "https://img.alicdn.com/i3/3569753962/O1CN011f8dg27BlBQvgmo_!!3569753962.jpg",
    //        "https://img.alicdn.com/i4/3569753962/O1CN01Za61Q61f8dhd5fMAi_!!3569753962.jpg"
    //      ]
    //},
    //    "title": "健身服套装男跑步运动速干衣紧身衣训练服篮球晨跑夏季健身房装备",
    //    "user_type": 1,
    //    "volume": 6052,
    //    "zk_final_price": "49"
    //  }
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