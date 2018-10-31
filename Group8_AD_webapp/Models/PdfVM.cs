using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class PdfVM
    {
        public string DeptCode { get; set; }
        public string ItemCode { get; set; }
        public string Cat { get; set; }
        public string Desc { get; set; }

        public int Request_Qty { get; set; }

        public int Fulfilled_Qty { get; set; }
    }
}