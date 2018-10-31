using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Group8_AD_webapp.Models;


namespace Group8AD_WebAPI.BusinessLogic
{
    /* 
    * Class Name       :       EmailBL
    * Created by       :       Noel Noel Han
    * Created date     :       25/Jul/2018
    * Student No.      :       A0180529B
    */
    public static class EmailBL
    {
        //addNewEmail
        public static bool AddNewEmail(int fromEmpId, int toEmpId, string type, string content)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    var from_email = entities.Employees.Where(e => e.EmpId == fromEmpId).Select(e => e.EmpEmail).First();
                    var to_email = entities.Employees.Where(e => e.EmpId == toEmpId).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == toEmpId).Select(e => e.EmpName).First();

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);
                    msg.To.Add(to_email);
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + Environment.NewLine + Environment.NewLine + "Thank you.";

                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        //SendNewReqEmail
        public static bool SendNewReqEmail(int fromempId, int toempId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                   // int fromEmpid = fromempId;
                    // int toEmpid = (int)currReq.ApproverId;
                   // int toEmpid = fromempId;
                    var from_email = entities.Employees.Where(e => e.EmpId == fromempId).Select(e => e.EmpEmail).First();
                    var to_email = entities.Employees.Where(e => e.EmpId == toempId).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == toempId).Select(e => e.EmpName).First();

                    string type = "Stationery Request";
                    string content = "A new stationery request has been submitted";

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);
                    msg.To.Add(to_email);
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + Environment.NewLine + Environment.NewLine + "Thank you.";

                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        //SendReqApprEmail(int empId, Request currReq)
        public static bool SendReqApprEmail(int empId, RequestVM currReq)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    int fromEmpid = (int)currReq.ApproverId;
                    int toEmpid = empId;

                    var to_email = entities.Employees.Where(e => e.EmpId == toEmpid).Select(e => e.EmpEmail).First();
                    var from_email = entities.Employees.Where(e => e.EmpId == fromEmpid).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == toEmpid).Select(e => e.EmpName).First();

                    var status = entities.Requests.Where(r => r.Status == currReq.Status).Select(r => r.Status).First();
                    var approver_cmd = entities.Requests.Where(r => r.ApproverComment == currReq.ApproverComment).Select(r => r.ApproverComment).First();

                    string type = "Stationery Request";
                    string content = "Your stationery request has been";

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);
                    msg.To.Add(to_email);
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + " " + status + " " + ":" + " " + approver_cmd + Environment.NewLine + Environment.NewLine + "Thank you.";

                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        //SendDisbEmailForClerk
        //with attach
        public static bool SendDisbEmailForClerk(int empId, string attachfile1, string attachfile2)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    //var to_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpName).First();
                    var from_email = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpEmail).First();
                    var _from = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpName).First();

                    var to_email_101 = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpEmail).First();
                    var to_email_102 = entities.Employees.Where(e => e.EmpId == 102).Select(e => e.EmpEmail).First();
                    var to_email_103 = entities.Employees.Where(e => e.EmpId == 103).Select(e => e.EmpEmail).First();

                    string type = "Disbursement For Clerk";
                    string content = "You have recently requested for Disbursement email for clerk the Logic University on ";
                    string filePath = HttpContext.Current.Server.MapPath("~/PDF/");

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);//101's email
                    msg.To.Add(to_email_101);//storeclerk1
                    msg.To.Add(to_email_102);//storeclerk2
                    msg.To.Add(to_email_103);//storeclerk3
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + " " + System.DateTime.Now.ToString("dd MMMM yyyy h:mm tt") + Environment.NewLine + Environment.NewLine +
                                "Kindly refer to the attachment." + Environment.NewLine + Environment.NewLine + "Thank you.";

                    Attachment at = new Attachment(filePath + attachfile1);
                    Attachment at1 = new Attachment(filePath + attachfile2);
                    msg.Attachments.Add(at);
                    msg.Attachments.Add(at1);

                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static bool SendDisbEmailForClerk(int empId, string attachfile)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    //var to_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpName).First();
                    var from_email = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpEmail).First();
                    var _from = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpName).First();

                    var to_email_101 = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpEmail).First();
                    var to_email_102 = entities.Employees.Where(e => e.EmpId == 102).Select(e => e.EmpEmail).First();
                    var to_email_103 = entities.Employees.Where(e => e.EmpId == 103).Select(e => e.EmpEmail).First();

                    string type = "Disbursement For Clerk";
                    string content = "You have recently requested for Disbursement email for clerk the Logic University on ";
                    string filePath = HttpContext.Current.Server.MapPath("~/PDF/");

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);//101's email
                    msg.To.Add(to_email_101);//storeclerk1
                    msg.To.Add(to_email_102);//storeclerk2
                    msg.To.Add(to_email_103);//storeclerk3
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + " " + System.DateTime.Now.ToString("dd MMMM yyyy h:mm tt") + Environment.NewLine + Environment.NewLine +
                                "Kindly refer to the attachment." + Environment.NewLine + Environment.NewLine + "Thank you.";

                    Attachment at = new Attachment(filePath + attachfile);
                    msg.Attachments.Add(at);

                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //SendDisbEmailForRep
        //with attach
        public static bool SendDisbEmailForRep(int empId, string deptCode)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    var to_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpName).First();
                    var from_email = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpEmail).First();
                    var colpt = entities.Departments.Where(d => d.DeptCode == deptCode).Select(d => d.ColPtId).First();
                    var location = entities.CollectionPoints.Where(c => c.ColPtId == colpt).Select(c => c.Location).First();
                    var time = entities.CollectionPoints.Where(c => c.ColPtId == colpt).Select(c => c.Time).First();


                    string type = "Disbursement For Rep";
                    string content = "You have recently requested for Disbursement email for rep the Logic University on";
                    string filePath = HttpContext.Current.Server.MapPath("~/PDF/");

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);//101's email
                    msg.To.Add(to_email);
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + " " + System.DateTime.Now.ToString("dd MMMM yyyy h:mm tt") +
                                Environment.NewLine + Environment.NewLine + "Thank you.";


                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //SendLowStockEmail
        //with attach
        public static bool SendLowStockEmail(int empId, string attachfile)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    var from_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var to_email = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpName).First();
                    var date = DateTime.Now.ToString("dd MMMM yyyy h:mm tt");
                    string type = "Low Stock";
                    string content = "You have recently requested for a low stock item report for the Logic University on";

                    string filePath = HttpContext.Current.Server.MapPath("~/PDF/");
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);
                    msg.To.Add(to_email);//101's email
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + " " + date + Environment.NewLine + Environment.NewLine + "Kindly refer to the attachment." + Environment.NewLine + Environment.NewLine + "Thank you.";
                    Attachment at = new Attachment(filePath + attachfile);
                    msg.Attachments.Add(at);
                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //MyMessage.Bcc.Add(addressBCC);
        //SendRcvEmail
        public static bool SendRcvEmail(int empId)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    var from_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpName).First();
                    var _from = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpName).First();
                    var deptCode = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.DeptCode).First();
                    var dept = entities.Departments.Where(d => d.DeptCode == deptCode).Select(d => d.DeptName).First();
                    var depthead_id = entities.Departments.Where(d => d.DeptCode == deptCode).Select(d => d.DeptHeadId).First();
                    var dept_head_email = entities.Employees.Where(d => d.EmpId == depthead_id).Select(d => d.EmpEmail).First();
                    var dept_head_name = entities.Employees.Where(d => d.EmpId == depthead_id).Select(d => d.EmpName).First();

                    var to_email_101 = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpEmail).First();
                    var to_email_102 = entities.Employees.Where(e => e.EmpId == 102).Select(e => e.EmpEmail).First();
                    var to_email_103 = entities.Employees.Where(e => e.EmpId == 103).Select(e => e.EmpEmail).First();

                    string type = "Weekly Disbursement";
                    string content = "has collected stationery for ";

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);
                    msg.To.Add(dept_head_email);//dept_head_email
                    msg.To.Add(to_email_101);//101
                    msg.To.Add(to_email_102);//102
                    msg.To.Add(to_email_103);//103
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + dept_head_name + "," + Environment.NewLine + Environment.NewLine + _from + " " +
                                content + " " + dept + Environment.NewLine + Environment.NewLine + "Thank you.";

                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //SendPOEmail
        //with attach
        public static bool SendPOEmail(int empId, DateTime targetDate, string attachfile)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    var to_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var deptCode = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.DeptCode).First();
                    //var head_id = entities.Departments.Where(d => d.DeptCode == deptCode).Select(d => d.DeptHeadId).First();
                    var clerk_email = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpEmail).First();
                    //var dept_head_email = entities.Employees.Where(e => e.EmpId == head_id).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpName).First();

                    string type = "Purchase Order";
                    string content = "You have recently requested to generate a Purchase Order for some items for the Logic University on";
                    //string content2 = "If you require any other information in relationship to the above, please do not hesitate to contact us/me.";
                    //string content3 = "Your early response will be highly appreciable.";

                    string filePath = HttpContext.Current.Server.MapPath("~/PDF/");
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(clerk_email);
                    msg.To.Add(to_email);
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + " " + targetDate + Environment.NewLine + Environment.NewLine +
                                "Kindly refer to the attachment." + Environment.NewLine + Environment.NewLine +
                                "Thank you.";
                    Attachment at = new Attachment(filePath + attachfile);
                    msg.Attachments.Add(at);
                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //SendAdjReqEmail(int empId, List<Adjustment> adjList)
        public static bool SendAdjReqEmail(int empId, List<AdjustmentVM> adjList)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    var from_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var to_email = entities.Employees.Where(e => e.EmpId == 105).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == 105).Select(e => e.EmpName).First();
                    string voucherNo = adjList.Select(a => a.VoucherNo).First().ToString();

                    string type = "Adjustment Request";
                    string content = "has been raised.";

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);
                    msg.To.Add(to_email);
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                voucherNo + " " + content + Environment.NewLine + Environment.NewLine + "Thank you.";

                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        //SendAdjApprEmail(int empId, Adjustment adj)
        public static bool SendAdjApprEmail(int empId, List<AdjustmentVM> adjList)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    var from_email = entities.Employees.Where(e => e.EmpId == 105).Select(e => e.EmpEmail).First();
                    var to_email = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == 105).Select(e => e.EmpName).First();
                    //var voucherNo = entities.Adjustments.Where(a => a.VoucherNo == adj.VoucherNo).Select(a => a.VoucherNo).First();
                    //var status = entities.Adjustments.Where(a => a.Status == adj.Status).Select(a => a.Status).First();
                    //var approverComment = entities.Adjustments.Where(a => a.ApproverComment == adj.ApproverComment).Select(a => a.ApproverComment).First();

                    string voucherNo = adjList.Select(a => a.VoucherNo).First().ToString();
                    string status = adjList.Select(a => a.Status).First().ToString();
                    string approverComment = adjList.Select(a => a.ApproverComment).First().ToString();

                    string type = "Adjustment Request";
                    string content = "has been ";

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);//105's email
                    msg.To.Add(to_email);//101's email
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine + voucherNo + " " +
                                content + " " + ":" + " " + status + " " + approverComment + Environment.NewLine + Environment.NewLine + "Thank you.";

                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        //SendReportEmail(int empId, List<ReportItem> reportItemList)
        public static bool SendReportEmail(int empId, List<ReportItemVM> reportItemList)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    var to_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpName).First();
                    var deptCode = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.DeptCode).First();
                    var head_id = entities.Departments.Where(d => d.DeptCode == deptCode).Select(d => d.DeptHeadId).First();
                    var dept_head_email = entities.Employees.Where(e => e.EmpId == head_id).Select(e => e.EmpEmail).First();

                    string type = "Representative Assignment";
                    string content = "You have been assigned as representative for your department by your department head";

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(dept_head_email);
                    msg.To.Add(to_email);
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + Environment.NewLine + Environment.NewLine + "Thank you.";

                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        //SendInvListEmail(int empId, List<Item> items)
        public static bool SendInvListEmail(int empId, string attachfile)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    var from_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var to_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == 101).Select(e => e.EmpName).First();

                    var deptCode = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.DeptCode).First();
                    var dept = entities.Departments.Where(d => d.DeptCode == deptCode).Select(d => d.DeptName).First();
                    //var collpt = entities.CollectionPoints.Where(d => d. == deptCode).Select(d => d.DeptName).First();

                    string type = "Inventory List";
                    string content = "You have recently requested for a list of inventory item for the Logic University on ";
                    // CollectionPoint.Location +  + CollectionPoint.Location;
                    string filePath = HttpContext.Current.Server.MapPath("~/PDF/");
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);
                    msg.To.Add(to_email);
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + " " + System.DateTime.Now.ToString("dd MMMM yyyy h:mm tt") + Environment.NewLine + Environment.NewLine + "Kindly refer to the attachment." + Environment.NewLine + Environment.NewLine + "Thank you.";
                    Attachment at = new Attachment(filePath + attachfile);
                    //Attachment at1 = new Attachment(filePath+attachfile);
                    msg.Attachments.Add(at);
                    //msg.Attachments.Add(at1);
                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public static bool AddNewEmailToEmp(int empId, string type, string content)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    var from_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var to_email = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpEmail).First();
                    var _to = entities.Employees.Where(e => e.EmpId == empId).Select(e => e.EmpName).First();

                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(from_email);
                    msg.To.Add(to_email);
                    msg.Subject = type;
                    msg.IsBodyHtml = false;
                    msg.Body = "Hi" + " " + _to + "," + Environment.NewLine + Environment.NewLine +
                                content + Environment.NewLine + Environment.NewLine + "Thank you.";

                    msg.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Send(msg);

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}