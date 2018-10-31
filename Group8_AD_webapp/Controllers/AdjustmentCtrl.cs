using Group8_AD_webapp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.BusinessLogic;
namespace Group8_AD_webapp.Controllers
{
    public class AdjustmentCtrl
    {
        private static readonly string API_Url = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"];
        public static List<AdjustmentVM> GetAdjustmentList(string status)
        {
            return AdjustmentBL.GetAdjList(status);
           
        }
        public static List<AdjustmentVM> GetAdjByVoucher(string voucherNo)
        {
            return AdjustmentBL.GetAdj(voucherNo);
        }

        public static List<AdjustmentVM> GetAdjListByStatusApproverId(string status, int empid)
        {
            return AdjustmentBL.GetAdjListByStatusApproverId(status, empid);
        }

        public static List<AdjustmentVM> GetAdjList(string voucherNo, int approverId)
        {
            return AdjustmentBL.GetAdjList(voucherNo, approverId);
        }

        public static bool AcceptRequest(string voucherNo, int empId, string cmt)
        {
            return AdjustmentBL.AcceptRequest(voucherNo, empId, cmt);
        }

        public static bool RejectRequest(string voucherNo, int empId, string cmt)
        {
           return AdjustmentBL.RejectRequest(voucherNo, empId, cmt);
        }
    }
}