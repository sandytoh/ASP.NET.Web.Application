using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8_AD_webapp.Models;
using Group8AD_WebAPI.BusinessLogic;
namespace Group8_AD_webapp
{
    public static class BusinessLogic
    {
        // Adds Item Description and Units to ItemVM
        public static List<RequestDetailVM> AddItemDescToReqDet(List<RequestDetailVM> list)
        {
            List<ItemVM> items = Controllers.ItemCtrl.GetAllItems();
            foreach(RequestDetailVM req in list)
            {
                req.Desc = (items.Where(x => x.ItemCode == req.ItemCode).FirstOrDefault()).Desc;
                req.UOM = (items.Where(x => x.ItemCode == req.ItemCode).FirstOrDefault()).UOM;
            }
            return list;
        }

        //request list for departmenthead 
        public static List<EmpReqVM> GetEmpReqList(int empId, string status)
        {
            List<RequestVM> requests = Controllers.RequestCtrl.GetReq(empId, status);

            List<EmpReqVM> requestlists = new List<EmpReqVM>();

            foreach (RequestVM req in requests)
            {
              EmployeeVM  emp = Controllers.EmployeeCtrl.getEmployeebyId(req.EmpId);

                EmpReqVM empReq = new EmpReqVM();
                empReq.ReqId = req.ReqId;
                empReq.ReqDateTime = req.ReqDateTime;
                empReq.EmpId = req.EmpId;
                empReq.EmpName = emp.EmpName;
                empReq.CancelledDateTime = req.CancelledDateTime;
                empReq.ApprovedDateTime = req.ApprovedDateTime;
                empReq.FulfilledDateTime = req.FulfilledDateTime;
                requestlists.Add(empReq);
            }
            requestlists = requestlists.OrderByDescending(x => x.ReqDateTime).ToList();
            return requestlists;
        }

        public static List<RequestDetailVM> GetItemDetailList(int rid)
        {
            List<RequestDetailVM> showList = new List<RequestDetailVM>();
            List<RequestDetailVM> reqDetails = Controllers.RequestDetailCtrl.GetReqDetList(rid);
            reqDetails = AddItemDescToReqDet(reqDetails);
            showList = reqDetails;
            return showList;
          
        }

        public static List<AdjustmentVM> AddItemDescToAdj(List<AdjustmentVM> list)
        {
            List<ItemVM> items = Controllers.ItemCtrl.GetAllItems();
            foreach (AdjustmentVM adj in list)
            {
                adj.Desc = (items.Where(x => x.ItemCode == adj.ItemCode).FirstOrDefault()).Desc;
                adj.Price1 = Convert.ToDouble((items.Where(x => x.ItemCode == adj.ItemCode).FirstOrDefault()).Price1);
            }
            return list;
        }


        public static List<AdjustmentVM> GetItemAdjustList(string voucherno, int empid)
        {
            List<AdjustmentVM> showlist = new List<AdjustmentVM>();
            List<AdjustmentVM> adj = Controllers.AdjustmentCtrl.GetAdjList(voucherno, empid);

            adj = AddItemDescToAdj(adj);

            foreach (AdjustmentVM aj in adj)
            {
                
                aj.Value = aj.Price1 * aj.QtyChange;
                
                showlist.Add(aj);
            }
            
            return showlist;
        }
        
    }


}