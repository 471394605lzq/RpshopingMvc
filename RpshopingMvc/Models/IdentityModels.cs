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
        //商品分类等级
        public DbSet<GoodsSortGrade> GoodsSortGrade { get; set; }
        //用户订单表
        public DbSet<Tborder> Tborder { get; set; }
        //用户收入明细
        public DbSet<UserSettlement> UserSettlement { get; set; }
        //新手学院
        public DbSet<CollegeInfo> CollegeInfo { get; set; }
        //用户反馈
        public DbSet<Feedback> Feedback { get; set; }
        //提现记录
        public DbSet<Withdrawcash> Withdrawcash { get; set; }
        //邀请奖励记录
        public DbSet<UserInvitationAward> UserInvitationAward { get; set; }
        //积分云购商品类别
        public DbSet<YGoodsType> YGoodsType { get; set; }
        //积分云购商品期数
        public DbSet<YGoodsIssue> YGoodsIssue { get; set; }
        //积分云购商品
        public DbSet<YGoods> YGoods { get; set; }
        //云购订单
        public DbSet<YGOrder> YGOrder { get; set; }
        //用户积分明细
        public DbSet<IntegralDetails> IntegralDetails { get; set; }
        //自营商品分类
        public DbSet<goodstype> goodstype { get; set; }
        //自营商品
        public DbSet<goods> goods { get; set; }
        //品牌
        public DbSet<Brand> Brand { get; set; }
        //自营商品分类中间表
        public DbSet<goodstypetemp> goodstypetemp { get; set; }
        //收货地址
        public DbSet<DeliveryAddress> DeliveryAddress { get; set; }
        //自营商品订单
        public DbSet<zyorder> zyorder { get; set; }
        //用户余额明细
        public DbSet<BalanceDetail> BalanceDetail { get; set; }
        //自营产品服务中间表
        public DbSet<zygoodservicetemp> zygoodservicetemp { get; set; }
        //自营产品服务
        public DbSet<zygoodservice> zygoodservice { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}