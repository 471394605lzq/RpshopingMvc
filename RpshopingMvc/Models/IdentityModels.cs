using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RpshopingMvc.Models
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        //用户充值信息表
        public DbSet<tb_Recharge> tb_Recharges { get; set; }
        //用户信息表
        public DbSet<tb_userinfo> tb_userinfos { get; set; }
        //支付订单表
        public DbSet<PayOrder> PayOrders { get; set; }
        //产品分类表
        public DbSet<tb_goodssort> tb_goodssort { get; set; }
        //选品库信息
        public DbSet<tb_Favorites> tb_Favorites { get; set; }
        //商品信息
        public DbSet<tb_goods> tb_goods { get; set; }
        //淘宝客信息
        public DbSet<tb_TKInfo> tb_TKInfo { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}