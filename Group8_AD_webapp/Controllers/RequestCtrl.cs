using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8_AD_webapp.Models;
using Newtonsoft.Json;
using Group8AD_WebAPI.BusinessLogic;

namespace Group8_AD_webapp.Controllers
{
    public static class RequestCtrl
    {

        public static List<RequestVM> GetReq(int empId, string status)
        {
            return RequestBL.GetReq(empId, status);
        }

        public static List<RequestVM> GetRequestByDateRange(int empId, string status, DateTime fromDate, DateTime toDate)
        {
            return RequestBL.GetReq(empId, status, fromDate, toDate);
        }


        public static RequestVM GetRequestByReqId(int reqId)
        {
            return RequestBL.GetReq(reqId);
        }

        public static bool SubmitRequest(int reqId, List<RequestDetailVM> reqDetList)
        {
            RequestVM req = RequestBL.SubmitReq(reqId, reqDetList);
            if (req != null)
            {
                return true;
            }
            return false;
        }


        public static bool CancelRequest(int reqId)
        {
            return RequestBL.RemoveReq(reqId);
        }
        public static bool AcceptRequest(int reqId, int empId, string cmt)
        {
            return RequestBL.AcceptRequest(reqId, empId, cmt);
        }
        public static bool RejectRequest(int reqId, int empId, string cmt)
        {
            return RequestBL.RejectRequest(reqId, empId, cmt);
        }


    }
}