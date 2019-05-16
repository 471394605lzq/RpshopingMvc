using RpshopingMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static RpshopingMvc.Enums.Enums;

namespace RpshopingMvc.App_Start
{
    public class OrderCommon
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //获取用户等级对应的提成金额
        public void SetUserGradeRate(string ggid, decimal money, string type, string ordertime, string ordercode, string SettlementTime)
        {
            try
            {
                //当前用户信息
                var tempuser = db.tb_userinfos.FirstOrDefault(s => s.Adzoneid == ggid);
                UserGrade sdd = tempuser.UserGrade;
                //运营商
                if (sdd == UserGrade.Operator)
                {
                    decimal returnmoney = decimal.Round(decimal.Parse((money * ((decimal)50 / 100)).ToString()), 2);
                    int rows = SaveSettlement(tempuser.ID, ggid, returnmoney, ordercode, ordertime, SettlementTime, 50, money, type);
                    if (tempuser.ParentID > 0 && rows > 0)
                    {
                        //用户上级合伙人用户
                        var Partneruser = db.tb_userinfos.FirstOrDefault(s => s.ID == tempuser.ParentID);
                        if (Partneruser != null)
                        {
                            decimal tempmoney = decimal.Round(decimal.Parse((money * ((decimal)30 / 100)).ToString()), 2);
                            SaveSettlement(Partneruser.ID, ggid, tempmoney, ordercode, ordertime, SettlementTime, 30, money, type);
                        }
                    }
                }
                //合伙人
                else if (sdd == UserGrade.Partner)
                {
                    decimal returnmoney = decimal.Round(decimal.Parse((money * ((decimal)50 / 100)).ToString()), 2);
                    int rows = SaveSettlement(tempuser.ID, ggid, returnmoney, ordercode, ordertime, SettlementTime, 50, money, type);
                }
                //初级会员
                else if (sdd == UserGrade.Primary)
                {
                    decimal returnmoney = decimal.Round(decimal.Parse((money * ((decimal)10 / 100)).ToString()), 2);
                    int rows = SaveSettlement(tempuser.ID, ggid, returnmoney, ordercode, ordertime, SettlementTime, 10, money, type);
                    if (tempuser.ParentID > 0 && rows > 0)
                    {
                        //用户上级运营商用户
                        var Operatoruser = db.tb_userinfos.FirstOrDefault(s => s.ID == tempuser.ParentID);
                        if (Operatoruser != null)
                        {
                            decimal tempmoney = decimal.Round(decimal.Parse((money * ((decimal)30 / 100)).ToString()), 2);
                            int rows2 = SaveSettlement(Operatoruser.ID, ggid, tempmoney, ordercode, ordertime, SettlementTime, 30, money, type);
                            if (Operatoruser.ParentID > 0 && rows2 > 0)
                            {
                                //用户上级合伙人用户
                                var Partneruser = db.tb_userinfos.FirstOrDefault(s => s.ID == tempuser.ParentID);
                                if (Partneruser != null)
                                {
                                    decimal tempmoney1 = decimal.Round(decimal.Parse((money * ((decimal)30 / 100)).ToString()), 2);
                                    SaveSettlement(Partneruser.ID, ggid, tempmoney1, ordercode, ordertime, SettlementTime, 30, money, type);
                                }
                            }
                        }
                    }
                }
                //高级会员
                else if (sdd == UserGrade.Senior)
                {
                    decimal returnmoney = decimal.Round(decimal.Parse((money * ((decimal)50 / 100)).ToString()), 2);
                    int rows = SaveSettlement(tempuser.ID, ggid, returnmoney, ordercode, ordertime, SettlementTime, 50, money, type);
                    if (tempuser.ParentID > 0 && rows > 0)
                    {
                        //用户上级运营商用户
                        var Operatoruser = db.tb_userinfos.FirstOrDefault(s => s.ID == tempuser.ParentID);
                        if (Operatoruser != null)
                        {
                            decimal tempmoney = decimal.Round(decimal.Parse((money * ((decimal)20 / 100)).ToString()), 2);
                            int rows2 = SaveSettlement(Operatoruser.ID, ggid, tempmoney, ordercode, ordertime, SettlementTime, 20, money, type);
                            if (Operatoruser.ParentID > 0 && rows2 > 0)
                            {
                                //用户上级合伙人用户
                                var Partneruser = db.tb_userinfos.FirstOrDefault(s => s.ID == tempuser.ParentID);
                                if (Partneruser != null)
                                {
                                    decimal tempmoney1 = decimal.Round(decimal.Parse((money * ((decimal)20 / 100)).ToString()), 2);
                                    SaveSettlement(Partneruser.ID, ggid, tempmoney1, ordercode, ordertime, SettlementTime, 20, money, type);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SaveSettlement(int userid, string adzoneid, decimal estimateincome, string ordercode, string ordertime, string settlementtime, decimal settlementrate, decimal ordermoney, string type)
        {
            try
            {
                if (!db.UserSettlement.Any(s => s.OrderCode == ordercode&& s.UserID==userid))
                {
                    UserSettlement thisusersettlement = new UserSettlement();
                    thisusersettlement.UserID = userid;
                    thisusersettlement.FromUserAdzoneid = adzoneid;
                    thisusersettlement.EstimateIncome = estimateincome;
                    thisusersettlement.OrderCode = ordercode;
                    thisusersettlement.OrderTime = Convert.ToDateTime(ordertime);
                    if (type == "Settlement")
                    {
                        thisusersettlement.SettlementTime = Convert.ToDateTime(settlementtime);
                        thisusersettlement.SettlementRate = settlementrate;
                        thisusersettlement.SettlementMoney = estimateincome;
                        thisusersettlement.SettlementState = TbOrderState.IsBalance;
                    }
                    else
                    {
                        thisusersettlement.SettlementTime = Convert.ToDateTime("1990-01-01");
                        thisusersettlement.SettlementRate = 0;
                        thisusersettlement.SettlementMoney = 0;
                        thisusersettlement.SettlementState = TbOrderState.NoBalance;
                    }
                    thisusersettlement.BalanceMoney = ordermoney;
                    db.UserSettlement.Add(thisusersettlement);
                    int rows = db.SaveChanges();
                    if (rows > 0)
                    {
                        var us = db.tb_userinfos.FirstOrDefault(s => s.ID == userid);
                        if (type == "Settlement")
                        {
                            us.ThisMonthEstimateIncome = us.ThisMonthEstimateIncome + estimateincome;
                            us.ThisMonthSettlementMoney = us.ThisMonthSettlementMoney + estimateincome;
                        }
                        else
                        {
                            us.ThisMonthEstimateIncome = us.ThisMonthEstimateIncome + estimateincome;
                        }
                        db.SaveChanges();
                    }
                    return rows;
                }
                else
                {
                    var UserSettlement = db.UserSettlement.FirstOrDefault(s => s.OrderCode == ordercode && s.UserID == userid);
                    if (UserSettlement != null&& UserSettlement.SettlementState== TbOrderState.NoBalance)
                    {
                        UserSettlement.SettlementMoney = estimateincome;
                        UserSettlement.SettlementRate = settlementrate;
                        UserSettlement.SettlementTime = Convert.ToDateTime(settlementtime);
                        UserSettlement.SettlementState = TbOrderState.IsBalance;
                        int row = db.SaveChanges();
                        //if (row > 0)
                        //{
                            var us = db.tb_userinfos.FirstOrDefault(s => s.ID == userid);
                            us.ThisMonthSettlementMoney = us.ThisMonthSettlementMoney + estimateincome;
                            db.SaveChanges();
                        //}
                        return 1;
                    }
                    else {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}