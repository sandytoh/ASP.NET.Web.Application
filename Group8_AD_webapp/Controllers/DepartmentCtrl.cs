using Group8_AD_webapp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.BusinessLogic;

/* 
* Class Name       :       DepartmentCtrl
* Created by       :       Noel Noel Han
* Created date     :       13/Jul/2018
* Student No.      :       A0180529B
*/
namespace Group8_AD_webapp.Controllers
{
    public class DepartmentCtrl
    {
        private static readonly string API_Url = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"];
        public static DepartmentVM GetDept(int empId)
        {
            return DepartmentBL.GetDept(empId);
        }

        public static List<DepartmentVM> GetAllDept()
        {
                List<DepartmentVM> departments = DepartmentBL.GetAllDept();
                return departments.Where(x => x.DeptName != "Store Department").ToList();
        }


        public static bool RemoveDelegate(string deptCode)
        {
            return DepartmentBL.removeDelegate(deptCode);

        }
        public static bool SetRep(string deptCode, int empId)
        {
            return DepartmentBL.setRep(deptCode, empId);
          
        }

        public static bool SetDelegate(string deptCode, DateTime fromDate, DateTime toDate, int empId)
        {

            return DepartmentBL.setDelegate(deptCode, fromDate, toDate, empId);
           
        }

    }
}