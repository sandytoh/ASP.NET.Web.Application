using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class RequestVM
    {
        int reqId;
        int empId;
        int approverId;
        string approverComment;
        DateTime reqDateTime;
        DateTime approvedDateTime;
        DateTime cancelledDateTime;
        DateTime fulfilledDateTime;
        string status;

        public int ReqId { get => reqId; set => reqId = value; }
        public int EmpId { get => empId; set => empId = value; }
        public int ApproverId { get => approverId; set => approverId = value; }
        public string ApproverComment { get => approverComment; set => approverComment = value; }
        public DateTime ReqDateTime { get => reqDateTime; set => reqDateTime = value; }
        public DateTime ApprovedDateTime { get => approvedDateTime; set => approvedDateTime = value; }
        public DateTime CancelledDateTime { get => cancelledDateTime; set => cancelledDateTime = value; }
        public DateTime FulfilledDateTime { get => fulfilledDateTime; set => fulfilledDateTime = value; }
        public string Status { get => status; set => status = value; }
       
        public RequestVM(int reqId, int empId, int approverId, string approverComment, DateTime reqDateTime, DateTime approvedDateTime, DateTime cancelledDateTime, DateTime fulfilledDateTime, string status)
        {
            ReqId = reqId;
            EmpId = empId;
            ApproverId = approverId;
            ApproverComment = approverComment;
            ReqDateTime = reqDateTime;
            ApprovedDateTime = approvedDateTime;
            CancelledDateTime = cancelledDateTime;
            FulfilledDateTime = fulfilledDateTime;
            Status = status;
            
        }
        public RequestVM() { }

    }
}