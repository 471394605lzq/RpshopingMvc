using Qiniu.RS;
using Qiniu.Util;
using RpshopingMvc.App_Start;
using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RpshopingMvc.Controllers
{
    public class CommonController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// 删除七牛云图片
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowCrossSiteJson]
        public ActionResult DeleteQiNiuImage2(string key) {
            string[] liststr = key.Split('/');
            string resultstr = "";
            string tempkey = liststr[3].Trim();

            try
            {
                Mac mac = new Mac("AwGglhX2wy5BX36zbHL_5YfC--EiQFWPdE44oblq", "X7nOg3cLkpVff_ZSor2zTUTmbYeMaJWaujsXX_Yd");
                string bucket = "test";
                BucketManager bm = new BucketManager(mac);
                var result = bm.Delete(bucket, tempkey);
                resultstr = result.Code.ToString();

            }
            catch (Exception ex)
            {
                resultstr = ex.ToString();
            }
            return Json(Comm.ToJsonResult("Success", "成功"), JsonRequestBehavior.AllowGet);

        }
    }
}