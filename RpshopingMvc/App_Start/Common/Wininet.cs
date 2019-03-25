using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Web;

namespace RpshopingMvc.App_Start.Common
{
    public class Wininet
    {

        #region WininetAPI
        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        private static extern int InternetOpen(string strAppName, int ulAccessType, string strProxy, string strProxyBypass, int ulFlags);
        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        private static extern int InternetConnect(int ulSession, string strServer, int ulPort, string strUser, string strPassword, int ulService, int ulFlags, int ulContext);
        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        private static extern bool InternetCloseHandle(int ulSession);
        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        private static extern bool HttpAddRequestHeaders(int hRequest, string szHeasers, uint headersLen, uint modifiers);
        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        private static extern int HttpOpenRequest(int hConnect, string szVerb, string szURI, string szHttpVersion, string szReferer, string accetpType, long dwflags, int dwcontext);
        [DllImport("wininet.dll")]
        private static extern bool HttpSendRequestA(int hRequest, string szHeaders, int headersLen, string options, int optionsLen);
        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        private static extern bool InternetReadFile(int hRequest, byte[] pByte, int size, out int revSize);
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData, ref System.UInt32 pcchCookieData, int dwFlags, IntPtr lpReserved);
        #endregion

        #region 重载方法

        /// <summary>
        /// 获取页面
        /// </summary>
        /// <param name="Url">URL地址</param>
        /// <returns></returns>
        public string Get(string Url)
        {
            MemoryStream ms = GetHtml(Url, "");
            //无视编码
            System.Text.RegularExpressions.Match meta = Regex.Match(Encoding.Default.GetString(ms.ToArray()), "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
            string c = (meta.Groups.Count > 1) ? meta.Groups[2].Value.ToUpper().Trim() : string.Empty;
            if (c.Length > 2)
            {
                if (c.IndexOf("UTF-8") != -1)
                {
                    return Encoding.GetEncoding("UTF-8").GetString(ms.ToArray());
                }
            }
            return Encoding.GetEncoding("GBK").GetString(ms.ToArray());
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="Url">地址</param>
        /// <param name="pd">提交数据</param>
        /// <returns></returns>
        public string Get(string Url, string pd)
        {
            MemoryStream ms = GetHtml(Url, pd);
            //无视编码
            System.Text.RegularExpressions.Match meta = Regex.Match(Encoding.Default.GetString(ms.ToArray()), "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
            string c = (meta.Groups.Count > 1) ? meta.Groups[2].Value.ToUpper().Trim() : string.Empty;
            if (c.Length > 2)
            {
                if (c.IndexOf("UTF-8") != -1)
                {
                    return Encoding.GetEncoding("UTF-8").GetString(ms.ToArray());
                }
            }
            return Encoding.GetEncoding("GBK").GetString(ms.ToArray());
        }

        /// <summary>
        /// GET（UTF-8）
        /// </summary>
        /// <param name="Url">地址</param>
        /// <returns></returns>
        public string GetU(string Url)
        {
            MemoryStream ms = GetHtml(Url, "");
            return Encoding.GetEncoding("UTF-8").GetString(ms.ToArray());
        }

        /// <summary>
        /// POST（UTF-8）
        /// </summary>
        /// <param name="Url">地址</param>
        /// <param name="pd">提交数据</param>
        /// <returns></returns>
        public string GetU(string Url, string pd)
        {
            MemoryStream ms = GetHtml(Url, pd);
            return Encoding.GetEncoding("UTF-8").GetString(ms.ToArray());
        }

        /// <summary>
        /// 获取网页图片(Image)
        /// </summary>
        /// <param name="Url">图片地址</param>
        /// <returns></returns>
        public Image GetImage(string Url)
        {
            MemoryStream ms = GetHtml(Url, "");
            Image img = Image.FromStream(ms);
            return img;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="Url">远程文件URL</param>
        /// <param name="filepath">保存的路径以及文件名</param>
        /// <param name="cookie">Cookie</param>
        public void GetDownload(string Url, string filepath, string cookie)
        {
            WebClient web = new WebClient();
            web.Headers.Add("Cookie", cookie);
            web.DownloadFile(Url, filepath);
        }

        #endregion

        #region 方法

        private MemoryStream GetHtml(string Url, string Postdata, StringBuilder Header = null)
        {
            try
            {
                //声明部分变量
                Uri uri = new Uri(Url);
                string Method = "GET";
                if (Postdata != "")
                {
                    Method = "POST";
                }
                string UserAgent = "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1; 125LA; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
                int hSession = InternetOpen(UserAgent, 1, "", "", 0);//会话句柄
                if (hSession == 0)
                {
                    InternetCloseHandle(hSession);
                    return null;//Internet句柄获取失败则返回
                }
                int hConnect = InternetConnect(hSession, uri.Host, uri.Port, "", "", 3, 0, 0);//连接句柄
                if (hConnect == 0)
                {
                    InternetCloseHandle(hConnect);
                    InternetCloseHandle(hSession);
                    return null;//Internet连接句柄获取失败则返回
                }
                //请求标记
                long gettype = -2147483632;
                if (Url.Substring(0, 5) == "https")
                {
                    gettype = -2139095024;
                }
                else
                {
                    gettype = -2147467248;
                }
                //取HTTP请求句柄
                int hRequest = HttpOpenRequest(hConnect, Method, uri.PathAndQuery, "HTTP/1.1", "", "", gettype, 0);//请求句柄
                if (hRequest == 0)
                {
                    InternetCloseHandle(hRequest);
                    InternetCloseHandle(hConnect);
                    InternetCloseHandle(hSession);
                    return null;//HTTP请求句柄获取失败则返回
                }
                //添加HTTP头
                StringBuilder sb = new StringBuilder();
                if (Header == null)
                {
                    sb.Append("Accept:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\n");
                    sb.Append("Content-Type:application/x-www-form-urlencoded\r\n");
                    sb.Append("Accept-Language:zh-cn\r\n");
                    sb.Append("Referer:" + Url);
                }
                else
                {
                    sb = Header;
                }
                //获取返回数据
                if (string.Equals(Method, "GET", StringComparison.OrdinalIgnoreCase))
                {
                    HttpSendRequestA(hRequest, sb.ToString(), sb.Length, "", 0);
                }
                else
                {
                    HttpSendRequestA(hRequest, sb.ToString(), sb.Length, Postdata, Postdata.Length);
                }
                //处理返回数据
                int revSize = 0;//计次
                byte[] bytes = new byte[1024];
                MemoryStream ms = new MemoryStream();
                while (true)
                {
                    bool readResult = InternetReadFile(hRequest, bytes, 1024, out revSize);
                    if (readResult && revSize > 0)
                    {
                        ms.Write(bytes, 0, revSize);
                    }
                    else
                    {
                        break;
                    }
                }
                InternetCloseHandle(hRequest);
                InternetCloseHandle(hConnect);
                InternetCloseHandle(hSession);
                return ms;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region 获取WebBrowser的Cookies
        /// <summary>
        /// 取出cookies
        /// </summary>
        /// <param name="url">完整的链接格式</param>
        /// <returns></returns>
        public string GetCookies(string url)
        {
            uint datasize = 512;
            StringBuilder cookieData = new StringBuilder((int)datasize);
            if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x2000, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;

                cookieData = new StringBuilder((int)datasize);
                if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x00002000, IntPtr.Zero))
                    return null;
            }
            return cookieData.ToString();
        }
        #endregion

        #region String与CookieContainer互转
        /// <summary>
        /// 将String转CookieContainer
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static CookieContainer StringToCookie(string url, string cookie)
        {
            string[] arrCookie = cookie.Split(';');
            CookieContainer cookie_container = new CookieContainer();    //加载Cookie
            foreach (string sCookie in arrCookie)
            {
                if (sCookie.IndexOf("expires") > 0)
                    continue;
                cookie_container.SetCookies(new Uri(url), sCookie);
            }
            return cookie_container;
        }

        /// <summary>
        /// 将CookieContainer转换为string类型
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static string CookieToString(CookieContainer cc)
        {
            System.Collections.Generic.List<Cookie> lstCookies = new System.Collections.Generic.List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            StringBuilder sb = new StringBuilder();
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies)
                    {
                        sb.Append(c.Name).Append("=").Append(c.Value).Append(";");
                    }
            }
            return sb.ToString();
        }
        #endregion
    }
}