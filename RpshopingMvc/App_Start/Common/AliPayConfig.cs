using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RpshopingMvc.App_Start.Common
{
    public static class AliPayConfig
    {
        //支付宝商户id
        public static string appid = "2018041602567852";//appid
        //应用私钥
        public static string app_private_key = "MIIEpQIBAAKCAQEAyUyCGfwC0XFYOMjAvYntfPuaYvCEldZ/5QKJMFaYKj1dSZU8vuam0I6WMntTcR+xT64k8zrq/Tlw5gI+251PXUbabB9QWjZSMeupFjDc2FN6vBYWZvtyieJz7HE6o2Y0sCu02xi6kqhHwq3ySMxoYe+VfQNh35d7rKQzBen9a/0uXrwSbg1M/aQcnBeWOWGnBto+HDyGGEnoJKsxekE26UVls85cDi7wI7kZbOJuVhpYj9odEdZBvn+Jg5SoPc/Z/s0fqWtNpgHNQG5KdwrFu0QQjjIZVDMslMWvYqr2QOj1j5+pD3dJMyKGkkgc3CPzQ6n1tPQQvnSNQt1AzbfSNwIDAQABAoIBAQCN2XIcqW+682o9qYnYhqdp2UrzyZVEmUDKujy+aWcU7OUeAyIpTBPlB3Vj4W/tWW3zPj4fgDczdhTOoGp3C6Vvj4w/gNl4mKrXLr+aOZiGgF0OyWnD7BDMhV03EptFpbIfKs1pT0W6LwdSco03K4Oq78+hpo6DpxWplJO36SmBvEloDgJzZiJLlAkiVFhQxTipDWLl3thEcfdquiUDia5PAszEcoDlnGQdFOY5p2wWbHtbWqVnXBFNnUQTQYsZ1XNN9lL0t2n5bPLcejY5SyS7KaFkh/gdELUtZV8LG9yo3veXsUhxY61OrPpeZLis9JtuRL9PNKMfYkwpuDBZVe1xAoGBAO6vcgcR5B1pKqD500kLwuNBbU1AQkRiiRr7KM4je7A+qd/7Ar4McftrVoUBlryBcC4CI2AKaZ6UTwqxFpA0A6YcenjCGAKYQOZrOGSEIeUP6Nfil/apdy+J9ynwYjt/oKCWfQwb0mgqM5IJYk1I1ZUEkO+K64PUVmAgOPww8k3rAoGBANfmxQk2ZZN7j6nJBGrF7heg5SFx27wN4QUOicMVlvxHr94QVseMK0H8SOKLCdpaXZet+ckoXZEpdzUPdbA0dKr6sXWQBxSMKFDNLYvRWqixCq95Hg0K2bAiVFKZyqeBMXJZVBpQoF/AoA6xA5KGAZdtGE5IWClKXRWCfoSSzZ3lAoGBAKtRwsbQUKvLkI16w+zqRDhZ/do1BVuQXli/bcqILX+TetsJkC5ZQHb11GQjf85OGfbsEfgdgTIRwaoq8ccPjo7sYfvLVPCH2A2LaC69qJaBlN9gBTNG8AVvQbkYkWmjcefSHG9UiPG7WMi5c5WFcchEPsOxMtqszlKwzjY167WBAoGANxnoc590cR153uU0wWNejp07nTuHzwjjwvyg4C8kZ6KMGeqlmywE5kRS/a5qh1XEyS9XrqUkrCWfDOWzLZNVq0VsAQsPI4lZyLV0yFhYAPGePoZ0yvNX94Hrb2FcvT9VtU9jDYxCQe3Ra651sPGOem0XZPNFvNQDybeSPpeQ7pkCgYEAo4JKQ+SWGfrJSOv9BVZ3QWsXv8del0UupM8XJKrtb6gt47HgrTx2FKQDNhn8QqFAVkWHzaeOQODQ8OAeOhwewv/wPxhyxK9D2DsUpYDY6d/azYbYZE+gznekuyuP1BPt0lerJfW52tEeGEOdC9YSWY0fbq9qw7cRFP4k+7ZnLM8=";//私钥
        //应用公钥
        public static string app_public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAyUyCGfwC0XFYOMjAvYntfPuaYvCEldZ/5QKJMFaYKj1dSZU8vuam0I6WMntTcR+xT64k8zrq/Tlw5gI+251PXUbabB9QWjZSMeupFjDc2FN6vBYWZvtyieJz7HE6o2Y0sCu02xi6kqhHwq3ySMxoYe+VfQNh35d7rKQzBen9a/0uXrwSbg1M/aQcnBeWOWGnBto+HDyGGEnoJKsxekE26UVls85cDi7wI7kZbOJuVhpYj9odEdZBvn+Jg5SoPc/Z/s0fqWtNpgHNQG5KdwrFu0QQjjIZVDMslMWvYqr2QOj1j5+pD3dJMyKGkkgc3CPzQ6n1tPQQvnSNQt1AzbfSNwIDAQAB";//公钥
        //支付宝公钥
        public static string alipaypublickey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgbPKjUaaQc0llCwO2gZrRXte1kfK2NTe0qEFuRc2GUpkE4lGFD/CDL6bsyEs7dPBEiZ9eRV69L4lp7hckfSSHK6GgKDYwezDejePT2Zm/P058Ya0PuXv0w6fS7ne8KuxUb+vzHdXPCHm+X0IhppKS4fS0eWn7XJZMHkiw7I3EmHurirzlNNDt1BberBtBWhKCEO9eOOsnvpdH4UeCMNKhvNdS0QBeKu+e1NmkiPlgFc2IJBxHGAdkmL4qFTkQqUWw5DFHdxJY9SflmtoAq6crpB9DmwpRG4H9e8CAVnEi1AtTdgqPJOH/DPiJAtlBJp4xDakkzUD/piN3v9uvo0zKQIDAQAB";//支付宝公钥(用于校验的时候)
        //结果回调通知地址
        public static string notifyurl = "https://www.rpyungou.com/AliNotifyUrl.aspx";
        //商家和支付宝签约的产品码，为固定值
        public static string productcode = "QUICK_MSECURITY_PAY";
        /// <summary>
        /// 根据当前系统时间加随机序列来生成订单号
        /// </summary>
        /// <returns>订单号</returns>
        public static string GenerateOutTradeNo()
        {
            var ran = new Random();
            return string.Format("{0}{1}{2}", appid, DateTime.Now.ToString("yyyyMMddHHmmss"), ran.Next(999));
        }


        //淘宝客appkey
        public static string tkapp_key = "25530839";
        //淘宝客AppSecret
        public static string tkapp_secret = "0e8d5e9032822b0763a4821b3d4a9097";
    }
}