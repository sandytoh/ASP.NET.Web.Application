using Group8_AD_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.Utility
{
    public class EmployeeUtility
    {
        public static List<EmployeeVM> Convert_Employee_To_EmployeeVM(List<Employee> empslist)
        {

            List<EmployeeVM> empvmList = new List<EmployeeVM>();
            foreach (Employee emp in empslist)
            {
                EmployeeVM e = new EmployeeVM();
                e.EmpId = emp.EmpId;
                e.EmpName = emp.EmpName;
                e.DeptCode = emp.DeptCode;
                e.EmpAddr = emp.EmpAddr;
                e.EmpEmail = emp.EmpEmail;
                e.EmpCtcNo = emp.EmpCtcNo;
                e.Role = emp.Role;

                empvmList.Add(e);
            }

            return empvmList;
        }


        public static List<Employee> Convert_EmployeeVM_To_Employee(List<EmployeeVM> empslist)
        {

            List<Employee> empvmList = new List<Employee>();
            foreach (EmployeeVM emp in empslist)
            {
                Employee e = new Employee();
                e.EmpId = emp.EmpId;
                e.EmpName = emp.EmpName;
                e.DeptCode = emp.DeptCode;
                e.EmpAddr = emp.EmpAddr;
                e.EmpEmail = emp.EmpEmail;
                e.EmpCtcNo = emp.EmpCtcNo;
                e.Role = emp.Role;

                empvmList.Add(e);
            }

            return empvmList;
        }
    }
}