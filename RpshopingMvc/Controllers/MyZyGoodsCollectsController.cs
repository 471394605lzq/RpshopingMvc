using RpshopingMvc.App_Start;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RpshopingMvc.Controllers
{
    public class MyZyGoodsCollectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// 添加商品收藏
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="goodsid"></param>
        /// <param name="goodsid"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult AddMyZYgoodsCollect(string userid, int goodsid, int isactive, int iscollect)
        {
            try
            {
                var usmodel = db.tb_userinfos.FirstOrDefault(s => s.UserID == userid);
                //var goodsmodel = db.goods.FirstOrDefault(s => s.ID == goodsid);
                goods goodsmodel = new goods();
                //如果不是活动商品
                if (isactive == 0)
                {
                    goodsmodel = db.goods.FirstOrDefault(s => s.ID == goodsid);
                }
                else
                {
                    var activegood = db.zyactivitygoods.FirstOrDefault(s => s.ID == goodsid);
                    goodsmodel = db.goods.FirstOrDefault(s => s.ID == activegood.goodsid);
                }
                if (usmodel == null)
                {
                    return Json(Comm.ToJsonResult("UserIdIsNull", "用户不存在"), JsonRequestBehavior.AllowGet);
                }
                else if (goodsmodel == null)
                {
                    return Json(Comm.ToJsonResult("GoodsIsNull", "商品不存在"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //如果没收藏就添加收藏
                    if (iscollect == 0)
                    {
                        var tempmodel = db.MyZyGoodsCollect.FirstOrDefault(s => s.goodid == goodsmodel.ID && s.userid == usmodel.ID);
                        if (tempmodel != null)
                        {
                            return Json(Comm.ToJsonResult("IsCollect", "添加成功"), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            MyZyGoodsCollect collectmodel = new MyZyGoodsCollect();
                            collectmodel.goodid = goodsid;
                            collectmodel.userid = usmodel.ID;
                            db.MyZyGoodsCollect.Add(collectmodel);
                            db.SaveChanges();
                            return Json(Comm.ToJsonResult("Success", "添加成功", collectmodel.ID), JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        var collectmodel = db.MyZyGoodsCollect.FirstOrDefault(s => s.goodid == goodsmodel.ID && s.userid == usmodel.ID);
                        db.MyZyGoodsCollect.Remove(collectmodel);
                        db.SaveChanges();
                        return Json(Comm.ToJsonResult("Success", "添加成功"), JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception ex)
            {
                return Json(Comm.ToJsonResult("Error", "收藏失败", ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}