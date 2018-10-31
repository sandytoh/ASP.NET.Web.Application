using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class NotificationVM
    {
        public int NotificationId { get; set; }
        public System.DateTime NotificationDateTime { get; set; }
        public int FromEmp { get; set; }
        public int ToEmp { get; set; }
        public string RouteUri { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        //public Item Item { get; set; }
        public int EmpId { get; set; }
        //public int RepId { get; set; }
        //public Notification notification { get; set; }
        //public Request Request { get; set; }

        public string FromEmpName { get; set; }
    }
}