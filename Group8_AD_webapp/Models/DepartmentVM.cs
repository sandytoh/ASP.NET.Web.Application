using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class DepartmentVM
    {

        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string DeptCtcNo { get; set; }
        public string DeptFaxNo { get; set; }
        public Nullable<int> ColPtId { get; set; }
        public Nullable<int> DeptHeadId { get; set; }
        public Nullable<int> DeptRepId { get; set; }
        public Nullable<int> DelegateApproverId { get; set; }
        public Nullable<System.DateTime> DelegateFromDate { get; set; }
        public Nullable<System.DateTime> DelegateToDate { get; set; }
        public int FulfilledQty { get; set; }
        public int EmpId { get; set; }

    }
}