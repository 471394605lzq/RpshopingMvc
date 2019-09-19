using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RpshopingMvc
{
    public partial class authoback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string charset = "utf-8";
            IDictionary<string, string> sArray = new Dictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;
            String[] requestItem = coll.AllKeys;
            for (int i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            string out_trade_no = Request.Form["top_session"];//sessionkey
        }
    }
}