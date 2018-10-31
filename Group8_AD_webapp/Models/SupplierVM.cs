using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class SupplierVM
    {
        private string suppCode;
        private string suppName;

        public string SuppCode { get => suppCode; set => suppCode = value; }
        public string SuppName { get => suppName; set => suppName = value; }

        public string SuppCtcName { get; set; }
        public string SuppCtcNo { get; set; }
        public string SuppFaxNo { get; set; }
        public string SuppAddr { get; set; }
    }
}