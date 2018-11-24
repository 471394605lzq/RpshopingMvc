using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpshopingMvc.App_Start.Common
{
    public class WxPayException: Exception
    {
        public WxPayException(string msg) : base(msg)
        {

        }
    }
}