using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8_AD_webapp.Models;
using Newtonsoft.Json;
using RestSharp;
using Group8AD_WebAPI.BusinessLogic;

/* 
* Class Name       :       SupplierCtrl
* Created by       :       Noel Noel Han
* Created date     :       13/Jul/2018
* Student No.      :       A0180529B
*/
namespace Group8_AD_webapp.Controllers
{
    public class SupplierCtrl
    {
        public static List<string> GetSupplierCodes()
        {

            List<SupplierVM> suppList =  SupplierBL.GetAllSupp();
            List<string> suppCodeList = new List<string>();
            foreach (SupplierVM s in suppList)
            {
                suppCodeList.Add(s.SuppCode);
            }
            return suppCodeList;
        }

        public static List<SupplierVM> GetAllSupp()
        {
            return SupplierBL.GetAllSupp();
        }
    }
}