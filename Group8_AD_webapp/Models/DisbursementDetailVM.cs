using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class DisbursementDetailVM
    {
        public string DeptCode { get; set; }
        public string ItemCode { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int ReqQty { get; set; }
        public int AwaitQty { get; set; }
        public int FulfilledQty { get; set; }
        public int EmpId { get; set; }
        public int ReqId { get; set; }
    }
}