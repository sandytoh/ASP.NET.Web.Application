using Group8_AD_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Group8AD_WebAPI.BusinessLogic
{
    public static class EmployeeBL
    {

        //get employee by employeeid
        public static EmployeeVM GetEmp(int empId)
        {
            EmployeeVM employee = new EmployeeVM();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {

                employee = entities.Employees.Where(e => e.EmpId == empId).Select(e => new EmployeeVM()
                {
                    EmpId = e.EmpId,
                    EmpName = e.EmpName,
                    DeptCode = e.DeptCode,
                    EmpAddr = e.EmpAddr,
                    EmpCtcNo = e.EmpCtcNo,
                    EmpEmail = e.EmpEmail,
                    Role = e.Role
                }).First<EmployeeVM>();
            }
            return employee;
        }


        //get department code by employeeid 
        public static string GetDeptCode(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string deptcode = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.DeptCode).First();
                return deptcode;
            }
        }

        //need to clearify return type and model
        public static string GetHeadId(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                string deptCode = GetDeptCode(empId);
                string DeptHeadId = entities.Departments.Where(x => x.DeptCode == deptCode).Select(x => x.DeptHeadId).First().ToString();
                return DeptHeadId;
            }
        }



        //get employee role by employeeid
        public static string GetRole(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                var role = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.Role).First();
                return role;
            }
        }

        //get all employee list
        public static List<EmployeeVM> GetAllEmp()
        {
            List<EmployeeVM> emplists = new List<EmployeeVM>();

            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {

                emplists = entities.Employees.Select(e => new EmployeeVM()
                {
                    EmpId = e.EmpId,
                    EmpName = e.EmpName,
                    DeptCode = e.DeptCode,
                    EmpAddr = e.EmpAddr,
                    EmpCtcNo = e.EmpCtcNo,
                    EmpEmail = e.EmpEmail,
                    Role = e.Role
                }).ToList<EmployeeVM>();

            }
            return emplists;
        }

        //Get Employee 
        public static List<EmployeeVM> GetEmplistsbyDeptCode(string deptCode)
        {
            List<EmployeeVM> empvmList = new List<EmployeeVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<Employee> empList = entities.Employees.Where(e => e.DeptCode.Equals(deptCode)).ToList();

                empvmList = Utility.EmployeeUtility.Convert_Employee_To_EmployeeVM(empList);
            }

            return empvmList;
        }


        public static List<EmployeeVM> GetEmp(string dCode, string name)
        {
            List<EmployeeVM> empvmList = new List<EmployeeVM>();
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                if (dCode != null && name == null)
                {

                    empvmList = entities.Departments.Where(d => d.DeptCode.Equals(dCode))
                                                .Join(entities.Employees, d => d.DeptCode, e => e.DeptCode, (d, e) => new { d, e })
                                                .Join(entities.Requests, r => r.e.EmpId, req => req.EmpId, (r, req) => new { r, req })
                                                .Where(requ => requ.req.Status.Equals("Approved"))
                                                .Select(result => new EmployeeVM
                                                {
                                                    EmpId = result.r.e.EmpId,
                                                    DeptCode = result.r.e.DeptCode,
                                                    EmpName = result.r.e.EmpName,
                                                    EmpAddr = result.r.e.EmpAddr,
                                                    EmpEmail = result.r.e.EmpEmail,
                                                    EmpCtcNo = result.r.e.EmpCtcNo
                                                }).ToList();
                }
                else if (name != null && dCode == null)
                {
                    empvmList = entities.Employees.Where(e => e.EmpName.Contains(name))
                                .Join(entities.Requests, r => r.EmpId, req => req.EmpId, (r, req) => new { r, req })
                                                .Where(requ => requ.req.Status.Equals("Approved"))
                                                .Select(result => new EmployeeVM
                                                {
                                                    EmpId = result.r.EmpId,
                                                    DeptCode = result.r.DeptCode,
                                                    EmpName = result.r.EmpName,
                                                    EmpAddr = result.r.EmpAddr,
                                                    EmpEmail = result.r.EmpEmail,
                                                    EmpCtcNo = result.r.EmpCtcNo
                                                }).ToList();
                }
                else if (name != null && dCode != null)
                {
                    empvmList = entities.Departments.Where(d => d.DeptCode.Equals(dCode))
                                               .Join(entities.Employees, d => d.DeptCode, e => e.DeptCode, (d, e) => new { d, e })
                                               .Join(entities.Requests, r => r.e.EmpId, req => req.EmpId, (r, req) => new { r, req })
                                               .Where(requ => requ.req.Status.Equals("Approved") && requ.r.e.EmpName.Contains(name))
                                               .Select(result => new EmployeeVM
                                               {
                                                   EmpId = result.r.e.EmpId,
                                                   DeptCode = result.r.e.DeptCode,
                                                   EmpName = result.r.e.EmpName,
                                                   EmpAddr = result.r.e.EmpAddr,
                                                   EmpEmail = result.r.e.EmpEmail,
                                                   EmpCtcNo = result.r.e.EmpCtcNo
                                               }).ToList();
                }
                else
                    empvmList = entities.Employees
                                .Join(entities.Requests, r => r.EmpId, req => req.EmpId, (r, req) => new { r, req })
                                                .Where(requ => requ.req.Status.Equals("Approved"))
                                                .Select(result => new EmployeeVM
                                                {
                                                    EmpId = result.r.EmpId,
                                                    DeptCode = result.r.DeptCode,
                                                    EmpName = result.r.EmpName,
                                                    EmpAddr = result.r.EmpAddr,
                                                    EmpEmail = result.r.EmpEmail,
                                                    EmpCtcNo = result.r.EmpCtcNo
                                                }).ToList();

            }
            return empvmList;
        }
    }
}