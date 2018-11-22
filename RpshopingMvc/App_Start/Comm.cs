using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpshopingMvc
{
    public class Comm
    {
        public static Dictionary<string, object> ToJsonResult(string state, string message, object data = null)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("State", state);
            result.Add("Message", message);
            result.Add("Result", data);
            //if (data != null)
            //{
            //    foreach (var item in data.GetType().GetProperties())
            //    {
            //        result.Add(item.Name, item.GetValue(data));
            //    }
            //}

            return result;
        }
    }
}