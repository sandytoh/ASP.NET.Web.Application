using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class ItemVM
    {
        private string itemCode;
        private string cat;
        private string desc;
        private string location;
        private string uOM;
        private bool isActive;
        private int balance;
        private int reorderLevel;
        private int reorderQty;
        private string suppCode1;
        private double price1;
        private string suppCode2;
        private double price2;
        private string suppCode3;
        private double price3;
        private Nullable<int> tempQtyReq;

        public string ItemCode { get => itemCode; set => itemCode = value; }
        public string Cat { get => cat; set => cat = value; }
        public string Desc { get => desc; set => desc = value; }
        public string Location { get => location; set => location = value; }
        public string UOM { get => uOM; set => uOM = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public int Balance { get => balance; set => balance = value; }
        public int ReorderLevel { get => reorderLevel; set => reorderLevel = value; }
        public int ReorderQty { get => reorderQty; set => reorderQty = value; }
        public string SuppCode1 { get => suppCode1; set => suppCode1 = value; }
        public double Price1 { get => price1; set => price1 = value; }
        public string SuppCode2 { get => suppCode2; set => suppCode2 = value; }
        public double Price2 { get => price2; set => price2 = value; }
        public string SuppCode3 { get => suppCode3; set => suppCode3 = value; }
        public double Price3 { get => price3; set => price3 = value; }
        public int? TempQtyReq { get => tempQtyReq; set => tempQtyReq = value; }




        public Nullable<int> TempQtyDisb { get; set; }
        public Nullable<int> TempQtyCheck { get; set; }

        public int TempQtyAcpt { get; set; }
       

        public string TempReason { get; set; }

        public int TempOrderQty { get; set; }
        public int ReccReorderLvl { get; set; }
        public int ReccReorderQty { get; set; }
        public int NewReorderLvl { get; set; }
        public int NewReorderQty { get; set; }

        public double lvlDiff { get; set; }
        public double qtyDiff { get; set; }
        public ItemVM()
        {

        }
    }
}