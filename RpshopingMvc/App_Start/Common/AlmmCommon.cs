using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace RpshopingMvc.App_Start.Common
{
    public static class AlmmCommon
    {
        public static Wininet http = new Wininet();
        /// <summary>
        /// 创建阿里妈妈推广位
        /// </summary>
        /// <param name="CreateAdzoneName">广告位名称</param>
        /// <param name="CreateAdzoneMediaID">媒体站点ID</param>
        /// <param name="CreateAdzoneChannelID">渠道ID</param>
        /// <returns>创建成功的广告位ID</returns>
        public static string CreateAdzone(string CreateAdzoneName, string CreateAdzoneMediaID, string CreateAdzoneChannelID)
        {
            try
            {
                //{ "invalidKey":null,"ok":true,"data":{ "siteId":283700162,"adzoneId":106053200106},"info":{ "ok":true,"message":null} }  106053200106
                string strul = "https://pub.alimama.com/common/adzone/selfAdzoneCreate.json";
                string postStr = FormatUrl("tag=29&gcid=" + CreateAdzoneChannelID + "&siteid=" + CreateAdzoneMediaID + "&selectact=add&newadzonename=" + CreateAdzoneName + "&t={Time}&_tb_token_={Token}&pvid=10_{IP}_{Rand}_{PvidTime}");
                string JsonData = http.GetU(strul, postStr);
                string adzoneId= Get_Middle_Text(JsonData, ",\"adzoneId\":", "},\"info\"");
                return adzoneId;
            }
            catch (Exception ex)
            {
                Log.WriteLog("创建pid", "创建pid", ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 对URL进行替换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatUrl(string str)
        {
            string temptoken = GetToken();
            str = str.Replace("{IP}",Unite.GetClientIP());
            str = str.Replace("{Token}", temptoken);
            str = str.Replace("{Time}", GetTimeStamp().ToString());
            str = str.Replace("{Rand}", new Random().Next(100, 9999).ToString());
            str = str.Replace("{_Time}", (GetTimeStamp() - new Random().Next(5000, 25555)).ToString());
            str = str.Replace("{PvidTime}", (GetTimeStamp() - new Random().Next(5000, 25555)).ToString());
            return str;
        }

        /// <summary>
        /// 获取Cookie
        /// </summary>
        public static string GetToken()
        {
            try
            {
                Random r = new Random();
                int n = r.Next(1, 5);
                string url = "https://pub.alimama.com/myunion.htm?/#!/myunion/overview"; //联盟首页
                switch (n)
                {
                    case 1:
                        url = "https://pub.alimama.com/myunion.htm?/#!/promo/self/items";//联盟产品
                        break;
                    case 2:
                        url = "https://pub.alimama.com/myunion.htm?/#!/report/site/site"; //效果
                        break;
                    case 3:
                        url = "https://pub.alimama.com/myunion.htm?/#!/report/detail/taoke";//订单
                        break;
                    case 4:
                        url = "https://pub.alimama.com/myunion.htm";
                        break;
                }
                http.GetU(url);//打开下页面 防止被和谐
                string ts = http.GetCookies(url); //获取全部COOKIE给全局
                string returnstr = Get_Middle_Text(ts, "_tb_token_=", ";");
                return returnstr;
            }
            catch (Exception ex)
            {
                Log.WriteLog("获取Cookie", "获取Cookie", ex.Message.ToString());
            }
            return "";
        }

        /// <summary>
        /// 取文本中间
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static string Get_Middle_Text(string str, string str1, string str2)
        {
            try
            {
                Regex regex = new Regex(string.Format("(?<={0}).*?(?={1})", str1, str2), RegexOptions.Compiled);
                Match match = regex.Match(str);
                if (match.Success)
                {
                    return match.Value;
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取时间戳 13位
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            System.DateTime time = DateTime.Now;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }


    }
}