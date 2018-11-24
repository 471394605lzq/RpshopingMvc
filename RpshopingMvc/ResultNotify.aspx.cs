using RpshopingMvc.App_Start.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RpshopingMvc
{
    public partial class ResultNotify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PayResultNotify notify = new PayResultNotify(this.Page);
            notify.ProcessNotify();
        }
    }
}