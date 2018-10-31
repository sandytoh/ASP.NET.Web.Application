using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8_AD_webapp.Models;
using Newtonsoft.Json;
using RestSharp;
using Group8AD_WebAPI.BusinessLogic;

/* 
* Class Name       :       RequestDetailCtrl
* Created by       :       Noel Noel Han
* Created date     :       13/Jul/2018
* Student No.      :       A0180529B
*/

namespace Group8_AD_webapp.Controllers
{
    public class RequestDetailCtrl
    {

        public static List<RequestDetailVM> GetReqDetList(int reqId)
        {
            List<RequestDetailVM> reqDetails = RequestDetailBL.GetReqDetList(reqId);
            return BusinessLogic.AddItemDescToReqDet(reqDetails);
        }

        public static bool AddBookmark(int empId, string itemCode)
        {
            RequestDetailVM req = RequestDetailBL.AddReqDet(empId, itemCode, 1, "Bookmarked");
            if(req != null)
            {
                return true;
            }
            return false;
        }

        public static bool AddToCart(int empId, string itemCode, int reqQty)
        {
            RequestDetailVM req = RequestDetailBL.AddReqDet(empId, itemCode, reqQty, "Unsubmitted");
            if (req != null)
            {
                return true;
            }
            return false;
        }
        
        public static bool RemoveReqDet(int reqId, string itemCode)
        {
            return RequestDetailBL.removeReqDet(reqId, itemCode);
        }

    }
}