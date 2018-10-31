using Group8_AD_webapp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.BusinessLogic;

namespace Group8_AD_webapp.Controllers
{
    public class TransactionCtrl
    {
        public static List<ItemVM> GetVolume(DateTime fromDate, DateTime toDate)
        {
            return ReportItemBL.GetVolume(fromDate, toDate);
        }
    }
}