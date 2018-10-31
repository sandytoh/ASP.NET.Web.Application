using Group8_AD_webapp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8AD_WebAPI.BusinessLogic;

namespace Group8_AD_webapp.Controllers
{
    public class EmployeeCtrl
    {
        private static readonly string API_Url = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"];
        public static List<EmployeeVM> getEmployeeList()
        {
            return EmployeeBL.GetAllEmp();

        }

        public static EmployeeVM getEmployeebyId(int id)
        {
            return EmployeeBL.GetEmp(id);
        }
    }
}