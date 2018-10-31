using Group8_AD_webapp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.BusinessLogic;
namespace Group8_AD_webapp.Controllers
{
    public class TransactionController
    {
        private static readonly string API_Url = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"];
        public static List<ReportItemVM> GetCBByMth(string deptCode, DateTime fromDate, DateTime toDate)
        {
            return ReportItemBL.GetCBByMth(deptCode, fromDate, toDate);
        }
    }
}