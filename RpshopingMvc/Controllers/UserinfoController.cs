using APICloud.Rest;
using PagedList;
using PagedList.Mvc;
using RpshopingMvc.App_Start;
using RpshopingMvc.App_Start.Common;
using RpshopingMvc.App_Start.Extensions;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
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
                    
                    
                    model.Balance = 1;
                    model.FirstCharge = Enums.Enums.YesOrNo.No;
                    model.RewardMoney = 0;
                    model.Integral = 0;
                    model.UserGrade = UserGrade.Primary;
                    model.ParentID = 0;
                    model.Siteid = AliPayConfig.MediaID;
                    model.Memberid = AliPayConfig.Memberid;
                    
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
                        string uscode= GetCreateUserCode(model.Phone,model.ID);
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
        public string GetCreateUserCode(string phone,int id)
        {
            string tempphonestr = phone.Substring(7, 4);
            string tempstr = "rp" + tempphonestr + id.ToString();
            string returnstr = "";
            if (db.tb_userinfos.Any(s => s.UserCode == tempstr))
            {
                GetCreateUserCode(phone, id + 1);
            }
            else {
                returnstr = tempstr;
            }
            return returnstr;
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
                    return Json(Comm.ToJsonResult("Error", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var returndata = new
                    {
                        Memberid = user.Memberid,
                        Siteid = user.Siteid,
                        UsPID=user.PID,
                        Adzoneid = user.Adzoneid
                    };
                    return Json(Comm.ToJsonResult("Success", "成功", returndata), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [AllowCrossSiteJson]
        public void getinfo(string cmd,string data)
        {
            try {
                if (cmd.Equals("adzone_add"))
                {
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
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}