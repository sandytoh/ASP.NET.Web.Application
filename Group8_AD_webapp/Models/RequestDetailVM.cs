using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class RequestDetailVM
    {
        int reqId;
        int reqLineNo;
        string itemCode;
        int reqQty;
        int awaitQty;
        int fulfilledQty;
        string desc;
        string uom;

        public int ReqId { get => reqId; set => reqId = value; }
        public int ReqLineNo { get => reqLineNo; set => reqLineNo = value; }
        public string ItemCode { get => itemCode; set => itemCode = value; }
        public int ReqQty { get => reqQty; set => reqQty = value; }
        public int AwaitQty { get => awaitQty; set => awaitQty = value; }
        public int FulfilledQty { get => fulfilledQty; set => fulfilledQty = value; }
        public string Desc { get => desc; set => desc = value; }

        public string UOM { get => uom; set => uom = value; }
        public RequestDetailVM()
        {

        }
    }
}