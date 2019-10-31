using RpshopingMvc.App_Start;
using RpshopingMvc.App_Start.Common;
using RpshopingMvc.App_Start.Qiniu;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.Controllers
{
    public class MemberController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult Register(tb_userinfo model)
        {
            try
            {

                if (db.tb_userinfos.Any(s => s.Phone == model.Phone))
                {
                    return Json(Comm.ToJsonResult("Exist", "手机号已被注册"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string guidstr = Guid.NewGuid().ToString();
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
                    model.UsPwd = "";// Unite.ToMD5New(model.UsPwd);
                    model.UserID = guidstr;
                    model.createtime = DateTime.Now.ToString();

                    db.tb_userinfos.Add(model);
                    int row = db.SaveChanges();
                    if (row > 0)
                    {
                        string temppath = "/Upload/";
                        string st = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string filename = temppath + st.Trim() + ".jpg";
                        string ststr = Comm.GenerateQRCode(guidstr, filename, "");
                        QinQiuApi qiniu = new QinQiuApi();
                        string qiniupath = qiniu.UploadFile(ststr, true);

                        string tempazoneid = "";
                        tb_TKInfo tk = db.tb_TKInfo.FirstOrDefault(s => s.PIDState == YesOrNo.No);
                        if (tk != null)
                        {
                            tempazoneid = tk.Adzoneid;
                        }
                        tk.PIDState = YesOrNo.Yes;
                        tk.UID = model.ID;
                        string uscode = Comm.GetCreateUserCode(model.Phone, model.ID);
                        var usmodel = db.tb_userinfos.Find(model.ID);
                        usmodel.UserCode = uscode;
                        usmodel.Adzoneid = tempazoneid;
                        usmodel.UserPath = qiniupath;
                        usmodel.PID = "mm_" + AliPayConfig.Memberid + "_" + AliPayConfig.MediaID + "_" + tempazoneid;
                        db.SaveChanges();
                    }
                    var returnstr = new
                    {
                        usid = guidstr
                    };
                    return Json(Comm.ToJsonResult("Success", "成功", returnstr), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult Login(string phone, string pwd)
        {
            try
            {
                string temppwd = Unite.ToMD5New(pwd);
                var user = db.tb_userinfos.FirstOrDefault(s => s.Phone == phone && s.UsPwd == temppwd);
                if (user == null)
                {
                    return Json(Comm.ToJsonResult("Fail", "账号或密码错误"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var returndata = new
                    {
                        guid = user.UserID,
                        openid = user.OpenID,
                        UsPID = user.PID,
                        tbuserid = user.tbuserid
                    };
                    return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetMemberInfo(string openid)
        {
            try
            {
                var user = db.tb_userinfos.FirstOrDefault(s => s.OpenID == openid);
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
                        UsPID = user.PID,
                        Adzoneid = user.Adzoneid,
                        ThisMonthEstimateIncome = user.ThisMonthEstimateIncome,
                        ThisMonthSettlementMoney = user.ThisMonthSettlementMoney,
                        LastMonthEstimateIncome = user.LastMonthEstimateIncome,
                        LastMonthSettlementMoney = user.LastMonthSettlementMoney,
                        Balance = user.Balance,
                        aliaccount = user.AliAccount,
                        aliusname = user.AliUserName,
                        guid = user.UserID
                    };
                    return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 个人中心获取用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetMemberInfos(string usid)
        {
            try
            {
                var user = db.tb_userinfos.FirstOrDefault(s => s.UserID == usid);
                if (user == null)
                {
                    return Json(Comm.ToJsonResult("nofind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var returndata = new
                    {

                        usimage = user.UserImage,
                        sex = user.sex,
                        birthday = user.birthday,
                        hometown = user.hometown,
                        residence = user.residence,
                        signature = user.signature,
                        nickname = user.UserName,
                        leve = user.UserGrade.GetDisplayName(),
                        guid = user.UserID,
                        tbuserid = user.tbuserid
                    };
                    return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult UpdatePwd(string phone, string newpwd)
        {
            try
            {
                tb_userinfo model = db.tb_userinfos.FirstOrDefault(s => s.Phone == phone);
                if (model != null)
                {
                    model.UsPwd = Unite.ToMD5New(newpwd);
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Comm.ToJsonResult("notfind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult UpdateInfo(string fields, string values, string usid)
        {
            try
            {
                tb_userinfo model = db.tb_userinfos.FirstOrDefault(s => s.UserID == usid);
                if (model != null)
                {
                    //修改用户名
                    if (fields.Equals("1"))
                    {
                        model.UserName = values;
                    }
                    //性别
                    else if (fields.Equals("2"))
                    {
                        model.sex = values;
                    }
                    //生日
                    else if (fields.Equals("3"))
                    {
                        model.birthday = values;
                    }
                    //居住地
                    else if (fields.Equals("4"))
                    {
                        model.residence = values;
                    }
                    //家乡
                    else if (fields.Equals("5"))
                    {
                        model.hometown = values;
                    }
                    //签名
                    else if (fields.Equals("6"))
                    {
                        model.signature = values;
                    }
                    //签名
                    else if (fields.Equals("7"))
                    {
                        string temppath = "/Upload/";
                        string st = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string filename = temppath + st.Trim() + ".jpg";
                        string ststr = Comm.GenerateQRCode(model.UserID, filename, model.UserImage);
                        QinQiuApi qiniu = new QinQiuApi();
                        string qiniupath = qiniu.UploadFile(ststr, true);
                        model.UserPath = qiniupath;
                    }
                    var returndata = new
                    {
                        uspath = model.UserPath
                    };
                    db.SaveChanges();
                    return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Comm.ToJsonResult("notfind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 获取用户余额
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetUserB(string uid)
        {
            try
            {
                var usmodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == uid);
                if (usmodel != null)
                {
                    var balan = usmodel.Balance;
                    return Json(Comm.ToJsonResult("Success", "成功", balan), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Comm.ToJsonResult("NotFind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowCrossSiteJson]
        public ActionResult GetExist(string uid)
        {
            try
            {
                var usmodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == uid);
                if (usmodel != null)
                {
                    return Json(Comm.ToJsonResult("Success", "成功", "1"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Comm.ToJsonResult("NotFind", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "操作失败"), JsonRequestBehavior.AllowGet);
            }
        }
    }
}