using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Group8_AD_webapp.Models;
using Group8AD_WebAPI.BusinessLogic;
using System.Security.Principal;

namespace Group8_AD_webapp.Service
{
    public class UtilityService
    {
        // Checks for role and adds to Session variable
        public static bool Authenticate(int empId)
        {
            EmployeeVM emp = EmployeeBL.GetEmp(empId);
            if(emp == null)
            {
                return false;
            }
            else
            {
                HttpContext.Current.Session["empId"] = empId;
                HttpContext.Current.Session["empName"] = emp.EmpName;
                if (emp.Role == "Employee")
                {
                    DepartmentVM dep = DepartmentBL.GetDept(empId);
                    if (dep.DelegateApproverId == empId)
                    {
                        if (DateTime.Now > dep.DelegateFromDate && DateTime.Now < dep.DelegateToDate)
                        {
                            HttpContext.Current.Session["role"] = "Delegate";
                           
                        }
                        else
                        {
                            HttpContext.Current.Session["role"] = "Employee";
                            Controllers.DepartmentCtrl.RemoveDelegate(dep.DeptCode);
                        }
                       
                    }
                    else if (dep.DeptRepId == empId)
                    {
                        HttpContext.Current.Session["role"] = "Representative";
                    }
                    else
                    {
                        HttpContext.Current.Session["role"] = "Employee";
                    }
                }
                else
                {
                    HttpContext.Current.Session["role"] = emp.Role;
                }

                return true;
            }
            
        }

        // Prevents access to pages based on role
        public static void CheckRoles(string role)
        {
            if(HttpContext.Current.Session["empId"] == null && HttpContext.Current.Session["role"] == null && HttpContext.Current.Session["empName"] == null)
            {
                HttpContext.Current.Response.Redirect("~/Login.aspx?error=notvalid");
            }
            else
            {
                string r = (string)HttpContext.Current.Session["role"];
                
                switch (role)
                {
                    case "Store":
                        {
                            if (r != "Store Clerk" && r != "Store Supervisor" && r != "Store Manager")
                            {
                                HttpContext.Current.Response.Redirect("~/Login.aspx?error=noaccess");
                            }
                            break;
                        }
                    case "Manager":
                        {
                            if (r != "Store Supervisor" && r != "Store Manager")
                            {
                                HttpContext.Current.Response.Redirect("~/Login.aspx?error=noaccess");
                            }
                            break;
                        }
                    case "Employee":
                        {
                            if (r != "Employee" && r != "Representative")
                            {
                                HttpContext.Current.Response.Redirect("~/Login.aspx?error=noaccess");
                            }
                            break;
                        }
                    case "DeptHead":
                        {
                            if (r != "Department Head" && r != "Delegate")
                            {
                                HttpContext.Current.Response.Redirect("~/Login.aspx?error=noaccess");
                            }
                            break;
                        }
                    default: HttpContext.Current.Response.Redirect("~/Login.aspx"); break;
                }
            }
        } 
    }
}