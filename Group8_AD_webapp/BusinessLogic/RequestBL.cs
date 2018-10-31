using Group8_AD_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Author: Tang Shenqi: A0114523U

namespace Group8AD_WebAPI.BusinessLogic
{
    public class RequestBL
    {
        // get a list of request by empId and status
        // done
        public static List<RequestVM> GetReq(int empId, string status)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    List<RequestVM> reqlist = new List<RequestVM>();
                    List<Request> lst = new List<Request>();
                    Employee employee = new Employee();
                    employee = entities.Employees.Where(e => e.EmpId == empId).FirstOrDefault();
                    string role = employee.Role;
                    string deptCode = employee.DeptCode;
                    Department dept = entities.Departments.Where(x => x.DeptCode == deptCode).FirstOrDefault();
                    if (empId == dept.DeptHeadId || empId == dept.DelegateApproverId)
                    {
                        reqlist = GetReq(deptCode, status);
                    }
                    else
                    {
                        if (status == "All")
                        {
                            lst = entities.Requests.Where(r => r.EmpId == empId && (r.Status == "Submitted" ||
                            r.Status == "Approved" || r.Status == "Rejected" || r.Status == "Cancelled" || r.Status == "Fulfilled")).ToList();
                            for (int i = 0; i < lst.Count; i++)
                            {
                                RequestVM req = new RequestVM();
                                req.ReqId = lst[i].ReqId;
                                req.EmpId = lst[i].EmpId;
                                if (lst[i].ApproverId != null)
                                    req.ApproverId = (int)lst[i].ApproverId;
                                else
                                    req.ApproverId = 0;
                                req.ApproverComment = lst[i].ApproverComment;
                                if (lst[i].ReqDateTime != null)
                                    req.ReqDateTime = (DateTime)lst[i].ReqDateTime;
                                if (lst[i].ApprovedDateTime != null)
                                    req.ApprovedDateTime = (DateTime)lst[i].ApprovedDateTime;
                                if (lst[i].CancelledDateTime != null)
                                    req.CancelledDateTime = (DateTime)lst[i].CancelledDateTime;
                                if (lst[i].FulfilledDateTime != null)
                                    req.FulfilledDateTime = (DateTime)lst[i].FulfilledDateTime;
                                req.Status = lst[i].Status;
                                reqlist.Add(req);
                            }
                        }
                        else
                        {
                            lst = entities.Requests.Where(r => r.EmpId == empId && r.Status == status).ToList();
                            for (int i = 0; i < lst.Count; i++)
                            {
                                RequestVM req = new RequestVM();
                                req.ReqId = lst[i].ReqId;
                                req.EmpId = lst[i].EmpId;
                                if (lst[i].ApproverId != null)
                                    req.ApproverId = (int)lst[i].ApproverId;
                                else
                                    req.ApproverId = 0;
                                req.ApproverComment = lst[i].ApproverComment;
                                if (lst[i].ReqDateTime != null)
                                    req.ReqDateTime = (DateTime)lst[i].ReqDateTime;
                                if (lst[i].ApprovedDateTime != null)
                                    req.ApprovedDateTime = (DateTime)lst[i].ApprovedDateTime;
                                if (lst[i].CancelledDateTime != null)
                                    req.CancelledDateTime = (DateTime)lst[i].CancelledDateTime;
                                if (lst[i].FulfilledDateTime != null)
                                    req.FulfilledDateTime = (DateTime)lst[i].FulfilledDateTime;
                                req.Status = lst[i].Status;
                                reqlist.Add(req);
                            }
                        }
                    }
                    return reqlist;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // get request by empId, status, fromDate and toDate
        // done
        public static List<RequestVM> GetReq(int empId, string status, DateTime fromDate, DateTime toDate)
        {
            try
            {
                List<RequestVM> reqlist = GetReq(empId, status);
                List<RequestVM> list = new List<RequestVM>();
                for (int i = 0; i < reqlist.Count; i++)
                {
                    DateTime reqDate = reqlist[i].ReqDateTime;
                    int result1 = DateTime.Compare(reqDate, fromDate);
                    int result2 = DateTime.Compare(reqDate, toDate);
                    if (result1 >= 0 && result2 <= 0)
                        list.Add(reqlist[i]);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // get a list of request by status
        // done
        public static List<RequestVM> GetReq(string status)
        {
            try
            {
                List<RequestVM> reqlist = new List<RequestVM>();
                List<Request> lst = new List<Request>();
                if (status == "All")
                {
                    using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                    {
                        lst = entities.Requests.Where(r => r.Status == "Submitted" || r.Status == "Approved" ||
                        r.Status == "Rejected" || r.Status == "Cancelled" || r.Status == "Fulfilled").ToList();
                    }
                    for (int i = 0; i < lst.Count; i++)
                    {
                        RequestVM req = new RequestVM();
                        req.ReqId = lst[i].ReqId;
                        req.EmpId = lst[i].EmpId;
                        if (lst[i].ApproverId != null)
                            req.ApproverId = (int)lst[i].ApproverId;
                        else
                            req.ApproverId = 0;
                        req.ApproverComment = lst[i].ApproverComment;
                        if (lst[i].ReqDateTime != null)
                            req.ReqDateTime = (DateTime)lst[i].ReqDateTime;
                        if (lst[i].ApprovedDateTime != null)
                            req.ApprovedDateTime = (DateTime)lst[i].ApprovedDateTime;
                        if (lst[i].CancelledDateTime != null)
                            req.CancelledDateTime = (DateTime)lst[i].CancelledDateTime;
                        if (lst[i].FulfilledDateTime != null)
                            req.FulfilledDateTime = (DateTime)lst[i].FulfilledDateTime;
                        req.Status = lst[i].Status;
                        reqlist.Add(req);
                    }
                }
                else
                {
                    using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                    {
                        lst = entities.Requests.Where(r => r.Status == status).ToList();
                    }
                    for (int i = 0; i < lst.Count; i++)
                    {
                        RequestVM req = new RequestVM();
                        req.ReqId = lst[i].ReqId;
                        req.EmpId = lst[i].EmpId;
                        if (lst[i].ApproverId != null)
                            req.ApproverId = (int)lst[i].ApproverId;
                        else
                            req.ApproverId = 0;
                        req.ApproverComment = lst[i].ApproverComment;
                        if (lst[i].ReqDateTime != null)
                            req.ReqDateTime = (DateTime)lst[i].ReqDateTime;
                        if (lst[i].ApprovedDateTime != null)
                            req.ApprovedDateTime = (DateTime)lst[i].ApprovedDateTime;
                        if (lst[i].CancelledDateTime != null)
                            req.CancelledDateTime = (DateTime)lst[i].CancelledDateTime;
                        if (lst[i].FulfilledDateTime != null)
                            req.FulfilledDateTime = (DateTime)lst[i].FulfilledDateTime;
                        req.Status = lst[i].Status;
                        reqlist.Add(req);
                    }
                }
                return reqlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // get a list of request by deptCode and status
        // done
        public static List<RequestVM> GetReq(string deptCode, string status)
        {
            try
            {
                List<RequestVM> reqlist = new List<RequestVM>();
                using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                {
                    List<Employee> emplist = entities.Employees.Where(e => e.DeptCode == deptCode).ToList();
                    List<Request> templist = new List<Request>();
                    if (status == "All")
                    {
                        templist = entities.Requests.Where(r => r.Status == "Submitted" || r.Status == "Approved" ||
                        r.Status == "Rejected" || r.Status == "Cancelled" || r.Status == "Fulfilled").ToList();
                        for (int i = 0; i < templist.Count; i++)
                        {
                            for (int j = 0; j < emplist.Count; j++)
                            {
                                if (templist[i].EmpId == emplist[j].EmpId)
                                {
                                    RequestVM req = new RequestVM();
                                    req.ReqId = templist[i].ReqId;
                                    req.EmpId = templist[i].EmpId;
                                    if (templist[i].ApproverId != null)
                                        req.ApproverId = (int)templist[i].ApproverId;
                                    else
                                        req.ApproverId = 0;
                                    req.ApproverComment = templist[i].ApproverComment;
                                    if (templist[i].ReqDateTime != null)
                                        req.ReqDateTime = (DateTime)templist[i].ReqDateTime;
                                    if (templist[i].ApprovedDateTime != null)
                                        req.ApprovedDateTime = (DateTime)templist[i].ApprovedDateTime;
                                    if (templist[i].CancelledDateTime != null)
                                        req.CancelledDateTime = (DateTime)templist[i].CancelledDateTime;
                                    if (templist[i].FulfilledDateTime != null)
                                        req.FulfilledDateTime = (DateTime)templist[i].FulfilledDateTime;
                                    req.Status = templist[i].Status;
                                    reqlist.Add(req);
                                }
                            }
                        }
                    }
                    else
                    {
                        templist = entities.Requests.Where(r => r.Status == status).ToList();
                        for (int i = 0; i < templist.Count; i++)
                        {
                            for (int j = 0; j < emplist.Count; j++)
                            {
                                if (templist[i].EmpId == emplist[j].EmpId)
                                {
                                    RequestVM req = new RequestVM();
                                    req.ReqId = templist[i].ReqId;
                                    req.EmpId = templist[i].EmpId;
                                    if (templist[i].ApproverId != null)
                                        req.ApproverId = (int)templist[i].ApproverId;
                                    else
                                        req.ApproverId = 0;
                                    req.ApproverComment = templist[i].ApproverComment;
                                    if (templist[i].ReqDateTime != null)
                                        req.ReqDateTime = (DateTime)templist[i].ReqDateTime;
                                    if (templist[i].ApprovedDateTime != null)
                                        req.ApprovedDateTime = (DateTime)templist[i].ApprovedDateTime;
                                    if (templist[i].CancelledDateTime != null)
                                        req.CancelledDateTime = (DateTime)templist[i].CancelledDateTime;
                                    if (templist[i].FulfilledDateTime != null)
                                        req.FulfilledDateTime = (DateTime)templist[i].FulfilledDateTime;
                                    req.Status = templist[i].Status;
                                    reqlist.Add(req);
                                }
                            }
                        }
                    }
                }
                return reqlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // get request by reqId
        // done
        public static RequestVM GetReq(int reqId)
        {
            try
            {
                RequestVM request = new RequestVM();
                Request req = new Request();
                using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                {
                    req = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                }
                request.ReqId = req.ReqId;
                request.EmpId = req.EmpId;
                if (req.ApproverId != null)
                    request.ApproverId = (int)req.ApproverId;
                else
                    request.ApproverId = 0;
                request.ApproverComment = req.ApproverComment;
                if (req.ReqDateTime != null)
                    request.ReqDateTime = (DateTime)req.ReqDateTime;
                if (req.ApprovedDateTime != null)
                    request.ApprovedDateTime = (DateTime)req.ApprovedDateTime;
                if (req.CancelledDateTime != null)
                    request.CancelledDateTime = (DateTime)req.CancelledDateTime;
                if (req.FulfilledDateTime != null)
                    request.FulfilledDateTime = (DateTime)req.FulfilledDateTime;
                request.Status = req.Status;

                return request;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // add request
        // done
        public static RequestVM AddReq(int empId, string status)
        {
            try
            {
                RequestVM request = new RequestVM();
                using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                {
                    Request req = new Request();
                    req.EmpId = empId;
                    req.Status = status;
                    req.ApproverComment = "";
                    entities.Requests.Add(req);
                    entities.SaveChanges();

                    List<Request> lst = entities.Requests.ToList();
                    Request r = lst[lst.Count - 1];
                    request.ReqId = r.ReqId;
                    request.EmpId = r.EmpId;
                    if (r.ApproverId != null)
                        request.ApproverId = (int)r.ApproverId;
                    else
                        request.ApproverId = 0;
                    request.ApproverComment = r.ApproverComment;
                    if (r.ReqDateTime != null)
                        request.ReqDateTime = (DateTime)r.ReqDateTime;
                    if (r.ApprovedDateTime != null)
                        request.ApprovedDateTime = (DateTime)r.ApprovedDateTime;
                    if (r.CancelledDateTime != null)
                        request.CancelledDateTime = (DateTime)r.CancelledDateTime;
                    if (r.FulfilledDateTime != null)
                        request.FulfilledDateTime = (DateTime)r.FulfilledDateTime;
                    request.Status = r.Status;
                }
                return request;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // remove request by empId and status
        // done
        public static bool RemoveReq(int empId, string status)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    List<Request> reqlist = entities.Requests.Where(r => r.EmpId == empId && r.Status == status).ToList();
                    if (reqlist.Count > 0)
                    {
                        for (int i = 0; i < reqlist.Count; i++)
                        {
                            if (reqlist[i].Status.Equals("Unsubmitted") || reqlist[i].Status.Equals("Bookmarked"))
                            {
                                RequestDetailBL.removeAllReqDet(reqlist[i].ReqId);
                            }
                            else
                            {
                                reqlist[i].Status = "Cancelled";
                                reqlist[i].CancelledDateTime = DateTime.Now;
                                entities.SaveChanges();
                            }
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // remove request by reqId
        // done
        public static bool RemoveReq(int reqId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    Request request = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                    if (request != null)
                    {
                        if (request.Status.Equals("Unsubmitted") || request.Status.Equals("Bookmarked"))
                        {
                            RequestDetailBL.removeAllReqDet(reqId);
                        }
                        else
                        {
                            request.Status = "Cancelled";
                            request.CancelledDateTime = DateTime.Now;
                            entities.SaveChanges();
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // submit request
        // done
        public static RequestVM SubmitReq(int reqId, List<RequestDetailVM> reqDetList)
        {
            // make requestId in reqDetList is the same as reqId
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    RequestVM req = GetReq(reqId);
                    for (int i = 0; i < reqDetList.Count; i++)
                    {
                        if (reqDetList[i].ReqId == reqId)
                        {
                            List<RequestDetail> rdlist = entities.RequestDetails.ToList();
                            for (int j = 0; j < rdlist.Count; j++)
                            {
                                if (reqDetList[i].ReqId == rdlist[j].ReqId && reqDetList[i].ReqLineNo == rdlist[j].ReqLineNo)
                                {
                                    rdlist[j].ReqQty = reqDetList[i].ReqQty;
                                    rdlist[j].AwaitQty = reqDetList[i].AwaitQty;
                                    rdlist[j].FulfilledQty = reqDetList[i].FulfilledQty;
                                    entities.SaveChanges();
                                }
                            }
                        }
                    }
                    req.ReqDateTime = DateTime.Now;
                    req.Status = "Submitted";
                    req = UpdateReq(req);

                    int empId = req.EmpId;
                    Employee emp = entities.Employees.Where(x => x.EmpId == empId).FirstOrDefault();
                    string deptCode = emp.DeptCode;
                    Department dept = entities.Departments.Where(x => x.DeptCode == deptCode).FirstOrDefault();

                    int fromEmpId = req.EmpId;
                    int toEmpId;
                    //if (dept.DelegateApproverId != null && DateTime.Compare(DateTime.Now, (DateTime)dept.DelegateFromDate) >= 0 &&
                    //    DateTime.Compare(DateTime.Now, (DateTime)dept.DelegateToDate) >= 0)
                    if (dept.DelegateApproverId != null && DateTime.Now >= dept.DelegateFromDate &&
                        DateTime.Now <= dept.DelegateToDate)
                        toEmpId = (int)dept.DelegateApproverId;
                    else
                        toEmpId = (int)dept.DeptHeadId;
                    string type = "Stationery Request";
                    string content = "A new stationery request has been submitted";
                    NotificationBL.AddNewNotification(fromEmpId, toEmpId, type, content);

                    // for email
                    EmailBL.SendNewReqEmail(empId, toEmpId);

                    return req;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // update request
        // done
        public static RequestVM UpdateReq(RequestVM req)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                int reqId = req.ReqId;
                Request request = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                request.ApproverComment = req.ApproverComment;
                if (req.ReqDateTime != null && DateTime.Compare(req.ReqDateTime, new DateTime(1800, 01, 01)) > 0)
                    request.ReqDateTime = req.ReqDateTime;
                if (req.ApprovedDateTime != null && DateTime.Compare(req.ApprovedDateTime, new DateTime(1800, 01, 01)) > 0)
                    request.ApprovedDateTime = req.ApprovedDateTime;
                if (req.CancelledDateTime != null && DateTime.Compare(req.CancelledDateTime, new DateTime(1800, 01, 01)) > 0)
                    request.CancelledDateTime = req.CancelledDateTime;
                if (req.FulfilledDateTime != null && DateTime.Compare(req.FulfilledDateTime, new DateTime(1800, 01, 01)) > 0)
                    request.FulfilledDateTime = req.FulfilledDateTime;
                request.Status = req.Status;
                entities.SaveChanges();
            }
            return req;
        }

        // accept request
        // done
        public static bool AcceptRequest(int reqId, int empId, string cmt)
        {
            try
            {
                List<RequestVM> reqlist = GetReq(empId, "Submitted");
                // for email
                RequestVM reqVM = new RequestVM();

                int toId = 0;
                for (int i = 0; i < reqlist.Count; i++)
                {
                    if (reqlist[i].ReqId == reqId)
                    {
                        using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                        {
                            Request req = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                            toId = req.EmpId;
                            req.ApproverId = empId;
                            req.ApproverComment = cmt;
                            req.ApprovedDateTime = DateTime.Now;
                            req.Status = "Approved";
                            entities.SaveChanges();
                            // for email
                            reqVM.ReqId = req.ReqId;
                            reqVM.EmpId = req.EmpId;
                            reqVM.ApproverId = (int)req.ApproverId;
                            reqVM.ApproverComment = req.ApproverComment;
                            reqVM.ReqDateTime = (DateTime)req.ReqDateTime;
                            reqVM.ApprovedDateTime = (DateTime)req.ApprovedDateTime;
                            //reqVM.CancelledDateTime = (DateTime)req.CancelledDateTime;
                            reqVM.Status = req.Status;
                            //reqVM.FulfilledDateTime = (DateTime)req.FulfilledDateTime;
                        }
                    }
                }
                int fromEmpId = empId;
                int toEmpId = toId;
                string type = "Stationery Request";
                string content = "Your stationery request has been approved : No comment";
                NotificationBL.AddNewNotification(fromEmpId, toEmpId, type, content);
                // for email
                EmailBL.SendReqApprEmail(toId, reqVM);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // reject request
        // done
        public static bool RejectRequest(int reqId, int empId,string cmt)
        {
            try
            {
                List<RequestVM> reqlist = GetReq(empId, "Submitted");
                // for email
                RequestVM reqVM = new RequestVM();

                int toId = 0;
                for (int i = 0; i < reqlist.Count; i++)
                {
                    if (reqlist[i].ReqId == reqId)
                    {
                        using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                        {
                            Request req = entities.Requests.Where(r => r.ReqId == reqId).FirstOrDefault();
                            toId = req.EmpId;
                            req.ApproverId = empId;
                            req.ApproverComment = cmt;
                            req.ApprovedDateTime = DateTime.Now;
                            req.Status = "Rejected";
                            entities.SaveChanges();
                            // for email
                            reqVM.ReqId = req.ReqId;
                            reqVM.EmpId = req.EmpId;
                            reqVM.ApproverId = (int)req.ApproverId;
                            reqVM.ApproverComment = req.ApproverComment;
                            reqVM.ReqDateTime = (DateTime)req.ReqDateTime;
                            reqVM.ApprovedDateTime = (DateTime)req.ApprovedDateTime;
                            //reqVM.CancelledDateTime = (DateTime)req.CancelledDateTime;
                            reqVM.Status = req.Status;
                            //reqVM.FulfilledDateTime = (DateTime)req.FulfilledDateTime;
                        }
                    }
                }
                int fromEmpId = empId;
                int toEmpId = toId;
                string type = "Stationery Request";
                string content = "Your stationery request has been rejected : Please review quantities";
                NotificationBL.AddNewNotification(fromEmpId, toEmpId, type, content);
                // for email
                EmailBL.SendReqApprEmail(toId, reqVM);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}