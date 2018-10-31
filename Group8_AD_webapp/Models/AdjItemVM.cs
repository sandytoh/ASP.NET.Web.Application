using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class AdjItemVM
    {
        public string ItemCode { get; set; }
        public string Desc { get; set; }
        public double Price1 { get; set; }
        public string VoucherNo { get; set; }
        public int EmpId { get; set; }
        public DateTime DateTimeIssued { get; set; }
        public string Reason { get; set; }
        public int QtyChange { get; set; }
        public string Status { get; set; }
        public int ApproverId { get; set; }
        public string ApproverComment { get; set; }

        public double Value { get; set; }
    }
}