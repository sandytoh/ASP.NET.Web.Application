using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8_AD_webapp.Models
{
    public class EmployeeVM
    {
        int empId;
        string deptCode;
        string empName;
        string empAddr;
        string empEmail;
        string empCtcNo;
        string role;
        public int EmpId { get => empId; set => empId = value; }
        public string DeptCode { get => deptCode; set => deptCode = value; }
        public string EmpName { get => empName; set => empName = value; }
        public string EmpAddr { get => empAddr; set => empAddr = value; }
        public string EmpEmail { get => empEmail; set => empEmail = value; }
        public string EmpCtcNo { get => empCtcNo; set => empCtcNo = value; }
        public string Role { get => role; set => role = value; }

        public EmployeeVM(int empId, string deptCode, string empName, string empAddr, string empEmail, string empCtcNo, string role)
        {
            
            EmpId = empId;
            DeptCode = deptCode;
            EmpName = empName;
            EmpAddr = empAddr;
            EmpEmail = empEmail;
            EmpCtcNo = empCtcNo;
            Role = role;
        }
        public EmployeeVM()
        {

        }
    }
}