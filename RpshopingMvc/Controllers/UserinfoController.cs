using APICloud.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;
using PagedList.Mvc;
using RpshopingMvc.App_Start;
using RpshopingMvc.App_Start.Common;
using RpshopingMvc.App_Start.Extensions;
using RpshopingMvc.App_Start.Qiniu;
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
    public class UserinfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public void Sidebar(string name = "用户管理")
        {
            ViewBag.Sidebar = name;

        }
        //列表页
        //[Authorize(Roles = SysRole.EnterpriseManageRead + "," + SysRole.EEnterpriseManageRead)]
        public ActionResult Index(string filter, bool? enable = null, int page = 1)
        {
            Sidebar();
            //int usertype = (int)this.GetAccountData().UserType;//从cookie中读取用户类型
            //string userID = this.GetAccountData().UserID;//从cookie中读取userid
            //var user = db.Users.FirstOrDefault(s => s.Id == userID);
            var m = from e in db.tb_userinfos
                    select new userinfoview
                    {
                        ID=e.ID,
                        UserID = e.UserID,
                        Balance = e.Balance,
                        RewardMoney = e.RewardMoney,
                        Integral = e.Integral,
                        FirstCharge = e.FirstCharge
                    };

            //if (!string.IsNullOrWhiteSpace(filter))
            //{
            //    m = m.Where(s => s.Name.Contains(filter));
            //}
            var paged = m.OrderByDescending(s => s.ID).ToPagedList(page);
            return View(paged);
        }
        public ActionResult SetCode()
        {
            string temppath = "/Upload/";
            string st = DateTime.Now.ToString("yyyyMMddHHmmss");
            string filename = temppath + st.Trim() + ".jpg";
            string ststr=Comm.GenerateQRCode("123", filename, "");
            QinQiuApi qiniu = new QinQiuApi();
            qiniu.UploadFile(ststr,true);
            return View();
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddUserInfo(tb_userinfo model)
        {
            try
            {

                if (db.tb_userinfos.Any(s => s.UserID == model.UserID))
                {
                    return Json(Comm.ToJsonResult("Error", "用户已存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    model.Balance = 0;
                    model.FirstCharge = Enums.Enums.YesOrNo.No;
                    model.RewardMoney = 0;
                    model.Integral = 0;
                    model.UserGrade = UserGrade.Primary;
                    model.ParentID = 0;
                    model.Siteid = AliPayConfig.MediaID;
                    model.Memberid = AliPayConfig.Memberid;
                    model.ThisMonthSettlementMoney = 0;
                    model.ThisMonthEstimateIncome = 0;
                    model.LastMonthEstimateIncome = 0;
                    model.LastMonthSettlementMoney = 0;
                    model.AliAccount = string.Empty;
                    model.AliUserName = string.Empty;
                    
                    db.tb_userinfos.Add(model);
                    int row=db.SaveChanges();
                    if (row>0)
                    {
                        string tempazoneid = "";
                        tb_TKInfo tk = db.tb_TKInfo.FirstOrDefault(s=>s.PIDState== YesOrNo.No);
                        if (tk!=null)
                        {
                            tempazoneid = tk.Adzoneid;
                        }
                        tk.PIDState = YesOrNo.Yes;
                        tk.UID = model.ID;
                        string uscode=Comm.GetCreateUserCode(model.Phone,model.ID);
                        var usmodel = db.tb_userinfos.Find(model.ID);
                        usmodel.UserCode = uscode;
                        usmodel.Adzoneid = tempazoneid;
                        usmodel.PID = "mm_" + AliPayConfig.Memberid + "_" + AliPayConfig.MediaID + "_" + tempazoneid;
                        db.SaveChanges();
                    }
                    return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //设置用户关联
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult SetUserParent(string parnetusid, string childuserid)
        {
            try
            {
                var parnetuser = db.tb_userinfos.FirstOrDefault(s => s.UserID == parnetusid);
                var childuser = db.tb_userinfos.FirstOrDefault(s => s.UserID == childuserid);
                var parentuserparent = db.tb_userinfos.FirstOrDefault(s=>s.ID==parnetuser.ParentID);
                ////查询被拉起用户是否有下级
                //var childuserlist = from e in db.tb_userinfos
                //                     where e.ParentID == childuser.ID
                //                     select new tb_userinfo
                //                     {
                //                         ID = e.ID
                //                     };
                //List<tb_userinfo> childlist = childuserlist.ToList<tb_userinfo>();


                if (parnetuser == null || childuser == null)
                {
                    return Json(Comm.ToJsonResult("nofind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                //else if (childlist.Count>=5)
                //{
                //    return Json(Comm.ToJsonResult("exist", "您已绑定上级,不可修改"), JsonRequestBehavior.AllowGet);
                //}
                else if (!string.IsNullOrWhiteSpace(childuser.ParentID.ToString()))
                {
                    return Json(Comm.ToJsonResult("exist", "您已绑定上级,不可修改"), JsonRequestBehavior.AllowGet);
                }
                //如果扫码用户是被扫码用户的上级时不能绑定
                else if (childuser.ID.ToString() == parnetuser.ParentID.ToString())
                {
                    return Json(Comm.ToJsonResult("exist", "该用户不能相互绑定"), JsonRequestBehavior.AllowGet);
                }
                //a拉起b b再拉起c c再拉起a时不能绑定
                else if (parentuserparent.ParentID == childuser.ID)
                {
                    return Json(Comm.ToJsonResult("exist", "该用户不能相互绑定"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    childuser.ParentID = parnetuser.ID;
                    int row = db.SaveChanges();
                    var parnetuserlist = from e in db.tb_userinfos
                                         where e.ParentID == parnetuser.ID
                                         select new tb_userinfo
                                         {
                                             ID = e.ID
                                         };
                    List<tb_userinfo> listt = parnetuserlist.ToList<tb_userinfo>();
                    //如果当前用户等级为初级会员并且拉起人数达到5人则自动升级为高级会员
                    if (listt.Count >= 5 && parnetuser.UserGrade == UserGrade.Primary)
                    {
                        parnetuser.UserGrade = UserGrade.Senior;
                        db.SaveChanges();
                    }
                    //如果当前用户等级为高级会员并且拉起人数达到50人则自动升级为运营商
                    else if (listt.Count >= 50 && parnetuser.UserGrade == UserGrade.Senior)
                    {
                        parnetuser.UserGrade = UserGrade.Operator;
                        db.SaveChanges();
                    }
                    //如果当前用户等级为运营商并且拉起人数达到50人则自动升级为合伙人
                    else if (listt.Count >= 200 && parnetuser.UserGrade == UserGrade.Operator)
                    {
                        parnetuser.UserGrade = UserGrade.Partner;
                        db.SaveChanges();
                    }
                    //拉新人奖励
                    UserInvitationAward useraward = new UserInvitationAward();
                    useraward.AwardMoney = 3;
                    useraward.CreateTime = DateTime.Now.ToString();
                    useraward.UserID = parnetusid;
                    useraward.FromUserID = childuserid;
                    useraward.States = YesOrNo.No;
                    db.UserInvitationAward.Add(useraward);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetUserInfo(string userid)
        {
            try
            {
                var user = db.tb_userinfos.FirstOrDefault(s => s.UserID == userid);
                if (user == null)
                {
                    return Json(Comm.ToJsonResult("nofind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var returndata = new
                    {
                        Memberid = user.Memberid,
                        Siteid = user.Siteid,
                        UsPID=user.PID,
                        Adzoneid = user.Adzoneid,
                        ThisMonthEstimateIncome = user.ThisMonthEstimateIncome,
                        ThisMonthSettlementMoney = user.ThisMonthSettlementMoney,
                        LastMonthEstimateIncome = user.LastMonthEstimateIncome,
                        LastMonthSettlementMoney = user.LastMonthSettlementMoney,
                        Balance = user.Balance,
                        aliaccount=user.AliAccount,
                        aliusname=user.AliUserName,
                        username=user.UserName,
                        handimg=user.UserImage,
                        phonestr=user.Phone,
                        userpath=user.UserPath

                    };
                    return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        //设置用户支付宝
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult SetUserAliAccountInfo(string userid,string account,string name)
        {
            try
            {
                var user = db.tb_userinfos.FirstOrDefault(s => s.UserID == userid);
                if (user == null)
                {
                    return Json(Comm.ToJsonResult("nofind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    user.AliAccount = account;
                    user.AliUserName = name;
                    db.SaveChanges();
                    var tempus = db.tb_userinfos.FirstOrDefault(s => s.UserID == userid);
                    if (!string.IsNullOrWhiteSpace(tempus.AliAccount))
                    {
                        return Json(Comm.ToJsonResult("Success", "设置成功"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(Comm.ToJsonResult("Fail", "设置失败"), JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        //获取我的团队
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetSubordinateUsers(string userids, int? page = 1, int? pageSize = 20)
        {
            try
            {
                int starpagesize = page.Value * pageSize.Value - pageSize.Value;
                int endpagesize = page.Value * pageSize.Value;
                var usermodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == userids);
                //拼接参数
                SqlParameter[] param = {
                        new SqlParameter("@userid", SqlDbType.Int),
                         };
                param[0].Value = usermodel.ID;
                string tempsql = string.Format(@"SELECT DISTINCT b.ID FROM dbo.tb_userinfo a  CROSS JOIN dbo.tb_userinfo b
                                                        WHERE b.ParentID = @userid");
                List<tempuserid> useriddata = db.Database.SqlQuery<tempuserid>(tempsql, param).ToList();
                string tempuserstr = "";
                if (useriddata.Count > 0)
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
                sqlstr = string.Format(@"SELECT * FROM (SELECT CAST(ROW_NUMBER() over(order by COUNT(g.ID) DESC) AS INTEGER) AS Ornumber,g.UserName,g.UserImage,
                                            CASE WHEN g.UserGrade=0 THEN '初级会员' WHEN g.UserGrade=1 THEN'高级会员' WHEN g.UserGrade=2 THEN '运营商' ELSE '合伙人' END AS Grade
                                             FROM dbo.tb_userinfo g 
                                             WHERE g.ID in(@useridstr) AND g.UserGrade IS NOT NULL GROUP BY g.UserName,g.UserImage,g.UserGrade) t 
                                                WHERE t.Ornumber > @starpagesize AND t.Ornumber<=@endpagesize");
                List<TempUserInfo> data = db.Database.SqlQuery<TempUserInfo>(sqlstr, parameters).ToList();
                return Json(Comm.ToJsonResult("Success", "成功", data), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AllowCrossSiteJson]
        public void getinfo(string cmd, string data)
        {
            try
            {
                bool isload = false;
                //创建广告位
                if (cmd.Equals("adzone_add")&& !isload)
                {
                    isload = true;
                    Newtonsoft.Json.Linq.JObject datas = JsonHelper.DeserializeObject(data);
                    string memberID = datas.SelectToken("memberID").ToString();
                    string mediaID = datas.SelectToken("mediaID").ToString();
                    string adzoneID = datas.SelectToken("adzoneID").ToString();
                    string temppid = "mm_" + memberID + "_" + mediaID + "_" + adzoneID;
                    tb_TKInfo model = new tb_TKInfo();
                    model.Adzoneid = adzoneID;
                    model.Memberid = memberID;
                    model.PID = temppid;
                    model.PIDState = YesOrNo.No;
                    model.Siteid = mediaID;
                    db.tb_TKInfo.Add(model);
                    db.SaveChanges();
                    isload = false;
                }
                //付款订单
                else if (cmd.Equals("payorderorder") && !isload)
                {
                    isload = true;
                    var datas = Newtonsoft.Json.JsonConvert.DeserializeObject(data) as JContainer;//转json格式 JsonHelper.DeserializeObject(data);
                    string ss2 = datas.ToString();
                    JsonSerializer serializer1 = new JsonSerializer();
                    StringReader sr1 = new StringReader(ss2);
                    object o1 = serializer1.Deserialize(new JsonTextReader(sr1), typeof(List<OrderModel>));
                    List<OrderModel> list = o1 as List<OrderModel>;
                    if (list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {


                            string tempgoodsid = list[i].商品ID.ToString();// datas.SelectToken("商品ID").ToString();
                            string ordercode = list[i].订单编号.ToString(); //datas.SelectToken("订单编号").ToString();
                            string ggid = list[i].广告位ID.ToString(); //datas.SelectToken("广告位ID").ToString();
                            string goodimg = "";
                            decimal ygsr = decimal.Round(decimal.Parse(list[i].预估收入.ToString() == "" ? "0" : list[i].预估收入.ToString()), 2);
                            decimal jsje = decimal.Round(decimal.Parse(list[i].结算金额.ToString() == "" ? "0" : list[i].结算金额.ToString()), 2);
                            decimal xgyg = decimal.Round(decimal.Parse(list[i].效果预估.ToString()), 2);
                            string ordertime = list[i].创建时间.ToString();
                            string ratestr = list[i].收入比率.ToString();
                            string[] tempstr = ratestr.Split('%');

                            //var tborder = db.Tborder.FirstOrDefault(s => s.OrderCode == ordercode);
                            //如果还没同步过订单则进行数据保存
                            if (!db.Tborder.Any(s => s.OrderCode == ordercode))
                            {
                                //根据商品id获取商品图片
                                //ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                                //TbkItemInfoGetRequest req = new TbkItemInfoGetRequest();
                                //req.NumIids = tempgoodsid;
                                //req.Platform = 2L;
                                ////req.Ip = "11.22.33.43";
                                //TbkItemInfoGetResponse rsp = client.Execute(req);
                                //var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                                //string s = jsondataformain.SelectToken("tbk_item_info_get_response").ToString();
                                //var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                                //string s1 = js.SelectToken("results").ToString();
                                //var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
                                //string s2 = js1.SelectToken("n_tbk_item").ToString();
                                //JsonSerializer serializer = new JsonSerializer();
                                //StringReader sr = new StringReader(s2);
                                //object o = serializer.Deserialize(new JsonTextReader(sr), typeof(stroemodel));
                                //List<stroemodel> modellist = o as List<stroemodel>;
                                //if (modellist.Count > 0)
                                //{
                                //    goodimg = modellist[0].pict_url;
                                //}
                                Tborder m = new Tborder();
                                m.AdsenseID = ggid;
                                m.EffectIncome = xgyg;
                                m.EstimateIncome = xgyg;
                                m.GoodsID = tempgoodsid;
                                m.GoodsImage = goodimg;
                                m.GoodsName = list[i].商品信息.ToString();  //datas.SelectToken("商品信息").ToString();
                                m.GoodsPrice = decimal.Round(decimal.Parse(list[i].商品单价.ToString()), 2);
                                m.IncomeRate = decimal.Round(decimal.Parse(tempstr[0]), 2);
                                m.OrderCode = ordercode;
                                m.OrderPrice = decimal.Round(decimal.Parse(list[i].付款金额.ToString()), 2);
                                m.OrderState = TbOrderState.NoBalance;
                                m.OrderTime = Convert.ToDateTime(ordertime);
                                m.OrderType = list[i].订单类型.ToString();//datas.SelectToken("订单类型").ToString();
                                m.SettlementMoney = jsje;
                                m.SettlementTime = Convert.ToDateTime("1990-01-01");
                                m.StoreName = list[i].所属店铺.ToString(); //datas.SelectToken("所属店铺").ToString();
                                db.Tborder.Add(m);
                                int row = db.SaveChanges();
                                if (row > 0)
                                {
                                    //计算用户结算佣金
                                    OrderCommon oc = new OrderCommon();
                                    oc.SetUserGradeRate(ggid, xgyg, "pay", ordertime, ordercode, "1990-01-01");
                                    isload = false;
                                }
                            }

                        }
                    }
                }
                //结算订单
                else if (cmd.Equals("Settlementorder") && !isload)
                {
                    var datas = Newtonsoft.Json.JsonConvert.DeserializeObject(data) as JContainer;//转json格式 JsonHelper.DeserializeObject(data);
                    string ss2 = datas.ToString();
                    JsonSerializer serializer1 = new JsonSerializer();
                    StringReader sr1 = new StringReader(ss2);
                    object o1 = serializer1.Deserialize(new JsonTextReader(sr1), typeof(List<OrderModel>));
                    List<OrderModel> list = o1 as List<OrderModel>;

                    if (list.Count > 0)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {


                            string tempgoodsid = list[i].商品ID.ToString(); 
                            string ordercode = list[i].订单编号.ToString();
                            string ggid = list[i].广告位ID.ToString();
                            string goodimg = "";
                            decimal ygsr = decimal.Round(decimal.Parse(list[i].预估收入.ToString() == "" ? "0" : list[i].预估收入.ToString()), 2);
                            decimal jsje = decimal.Round(decimal.Parse(list[i].结算金额.ToString() == "" ? "0" : list[i].结算金额.ToString()), 2);
                            string ordertime = list[i].创建时间.ToString();
                            string jstime = list[i].结算时间.ToString();
                            string ratestr = list[i].收入比率.ToString();
                            string[] tempstr = ratestr.Split('%');

                            //如果还没同步过订单则进行数据保存
                            if (!db.Tborder.Any(s => s.OrderCode == ordercode))
                            {
                                //根据商品id获取商品图片
                                //ITopClient client = new DefaultTopClient(AliPayConfig.tkapp_url, AliPayConfig.tkapp_key, AliPayConfig.tkapp_secret, "json");
                                //TbkItemInfoGetRequest req = new TbkItemInfoGetRequest();
                                //req.NumIids = tempgoodsid;
                                //req.Platform = 2L;
                                ////req.Ip = "11.22.33.43";
                                //TbkItemInfoGetResponse rsp = client.Execute(req);
                                //var jsondataformain = Newtonsoft.Json.JsonConvert.DeserializeObject(rsp.Body) as JContainer;//转json格式
                                //string s = jsondataformain.SelectToken("tbk_item_info_get_response").ToString();
                                //var js = Newtonsoft.Json.JsonConvert.DeserializeObject(s) as JContainer;
                                //string s1 = js.SelectToken("results").ToString();
                                //var js1 = Newtonsoft.Json.JsonConvert.DeserializeObject(s1) as JContainer;
                                //string s2 = js1.SelectToken("n_tbk_item").ToString();
                                //JsonSerializer serializer = new JsonSerializer();
                                //StringReader sr = new StringReader(s2);
                                //object o = serializer.Deserialize(new JsonTextReader(sr), typeof(stroemodel));
                                //List<stroemodel> modellist = o as List<stroemodel>;
                                //if (modellist.Count > 0)
                                //{
                                //    goodimg = modellist[0].pict_url;
                                //}
                                //[{"创建时间":"2019-03-21 09:25:25","点击时间":"2019-03-21 09:24:14","商品信息":"4条装日系内裤女纯棉高腰大码女士三角裤全棉质面料少女蕾丝短裤",
                                //"商品ID":"541009215405","掌柜旺旺":"wisdomfeng","所属店铺":"美多拉内衣","商品数":"1","商品单价":"76.00","订单状态":"订单付款","订单类型":"淘宝","收入比率":"9.00 %",
                                //"分成比率":"100.00 %","付款金额":"37.80","效果预估":"3.40","结算金额":"0.00","预估收入":"0.00","结算时间":"","佣金比率":"9.00 %","佣金金额":"0.00","技术服务费比率":"",
                                //"补贴比率":"0.00 %","补贴金额":"0.00","补贴类型":"-","成交平台":"无线","第三方服务来源":"--","订单编号":"384487585332201957","类目名称":"内衣/家居服","来源媒体ID":"283700162","来源媒体名称":"rp云购",
                                //"广告位ID":"96815000311","广告位名称":"001"}]

                                Tborder m = new Tborder();
                                m.AdsenseID = ggid;
                                m.EffectIncome = decimal.Round(decimal.Parse(list[i].效果预估.ToString()), 2);
                                m.EstimateIncome = ygsr;
                                m.GoodsID = tempgoodsid;
                                m.GoodsImage = goodimg;
                                m.GoodsName = list[i].商品信息.ToString();//datas.SelectToken("商品信息").ToString();
                                m.GoodsPrice = decimal.Round(decimal.Parse(list[i].商品单价.ToString()), 2);
                                m.IncomeRate = decimal.Round(decimal.Parse(tempstr[0]), 2);
                                m.OrderCode = list[i].订单编号.ToString();//datas.SelectToken("订单编号").ToString();
                                m.OrderPrice = decimal.Round(decimal.Parse(list[i].付款金额.ToString()), 2);
                                m.OrderState = TbOrderState.IsBalance;
                                m.OrderTime = Convert.ToDateTime(list[i].创建时间.ToString());
                                m.OrderType = list[i].订单类型.ToString();
                                m.SettlementMoney = jsje;
                                m.SettlementTime = Convert.ToDateTime(jstime);
                                m.StoreName = list[i].所属店铺.ToString();// datas.SelectToken("所属店铺").ToString();
                                db.Tborder.Add(m);
                                int row = db.SaveChanges();
                                if (row > 0)
                                {
                                    //计算用户结算佣金
                                    OrderCommon oc = new OrderCommon();
                                    oc.SetUserGradeRate(ggid, ygsr, "Settlement", ordertime, ordercode, jstime);
                                    isload = true;
                                }
                            }
                            else
                            {
                                var tborder = db.Tborder.FirstOrDefault(s => s.OrderCode == ordercode);
                                tborder.SettlementMoney = jsje;
                                tborder.EstimateIncome = ygsr;
                                tborder.OrderState = TbOrderState.IsBalance;
                                tborder.SettlementTime =Convert.ToDateTime(jstime);
                                int row = db.SaveChanges();
                                //if (row > 0)
                                //{
                                    //计算用户结算佣金
                                    OrderCommon oc = new OrderCommon();
                                    oc.SetUserGradeRate(ggid, ygsr, "Settlement", ordertime, ordercode, jstime);
                                    isload = true;
                                //}
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //结算佣金
        public ActionResult SetSettlement()
        {
            int daystr = DateTime.Now.Day;
            if (daystr == 20)
            {
                string sqlstr = "UPDATE dbo.tb_userinfo SET LastMonthEstimateIncome=ThisMonthEstimateIncome,LastMonthSettlementMoney=ThisMonthSettlementMoney,ThisMonthEstimateIncome=0,ThisMonthSettlementMoney=0";
                int rows = db.Database.ExecuteSqlCommand(sqlstr);
                return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(Comm.ToJsonResult("Error", "还没到结算时间"), JsonRequestBehavior.AllowGet);
            }
        }
        //[{"创建时间":"2019-03-21 09:25:25","点击时间":"2019-03-21 09:24:14","商品信息":"4条装日系内裤女纯棉高腰大码女士三角裤全棉质面料少女蕾丝短裤",
        //"商品ID":"541009215405","掌柜旺旺":"wisdomfeng","所属店铺":"美多拉内衣","商品数":"1","商品单价":"76.00","订单状态":"订单付款","订单类型":"淘宝","收入比率":"9.00 %",
        //"分成比率":"100.00 %","付款金额":"37.80","效果预估":"3.40","结算金额":"0.00","预估收入":"0.00","结算时间":"","佣金比率":"9.00 %","佣金金额":"0.00","技术服务费比率":"",
        //"补贴比率":"0.00 %","补贴金额":"0.00","补贴类型":"-","成交平台":"无线","第三方服务来源":"--","订单编号":"384487585332201957","类目名称":"内衣/家居服","来源媒体ID":"283700162","来源媒体名称":"rp云购",
        //"广告位ID":"96815000311","广告位名称":"001"}]
        public class OrderModel {
            public string 创建时间 { get; set; }
            public string 商品信息 { get; set; }
            public string 商品ID { get; set; }
            public string 所属店铺 { get; set; }
            public string 商品单价 { get; set; }

            public string 订单状态 { get; set; }
            public string 订单类型 { get; set; }
            public string 收入比率 { get; set; }
            public string 付款金额 { get; set; }
            public string 效果预估 { get; set; }
            public string 结算金额 { get; set; }
            public string 预估收入 { get; set; }
            public string 结算时间 { get; set; }
            public string 订单编号 { get; set; }
            public string 广告位ID { get; set; }

        }
        public class tempuserid
        {
            public int ID { get; set; }
        }
        public class TempUserInfo
        {
            public string UserName { get; set; }
            public string UserImage { get; set; }
            public string Grade { get; set; }
        }
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