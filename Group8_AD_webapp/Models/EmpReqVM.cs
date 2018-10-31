using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class EmpReqVM
    {
        public int ReqId { get; set; }
        public string EmpName { get; set; }
        public int EmpId { get; set; }
        public int ApproverId { get; set; }
        public string ApproverComment { get; set; }
        public DateTime ReqDateTime { get; set; }
        public DateTime ApprovedDateTime { get; set; }
        public DateTime CancelledDateTime { get; set; }
        public DateTime FulfilledDateTime { get; set; }
        public string Status { get; set; }
        public EmpReqVM()
        {

        }
    }
}