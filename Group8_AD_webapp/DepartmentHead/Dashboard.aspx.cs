using Group8_AD_webapp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group8AD_WebAPI.BusinessLogic;

namespace Group8_AD_webapp
{
    // Author: Han Myint Tun , A0180555A
    // Version 1.0 Initial Release

    public partial class Dashboard : System.Web.UI.Page
    {
        DepartmentVM dept = new DepartmentVM();
        int empId;
        static string deptCode;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Adds active class to menu Item (sidebar)
            Main master = (Main)this.Master;
            master.ActiveMenu("dhdash");

            Service.UtilityService.CheckRoles("DeptHead");
            empId = Convert.ToInt32(Session["empId"]);
            dept = Controllers.DepartmentCtrl.GetDept(empId);
            deptCode = dept.DeptCode;

            //hide for delegate 
            if (empId == dept.DelegateApproverId)
            {
                btnRemoveDelegate.Disabled = true;
                ddlDelegate.Enabled = false;
                btnAddDelegate.Disabled = true;
                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
                
            }
            
            //get past 6 months
            var lastSixMonths = Enumerable.Range(0, 7).Select(i => DateTime.Now.AddMonths(i - 6).ToString("MMMM" + " yyyy", CultureInfo.InvariantCulture)).Reverse();
            List<string> monthslist = lastSixMonths.ToList();

            if (!IsPostBack)
            {
                BindEmpDDL();
                ddlMonth.DataSource = monthslist;
                ddlMonth.DataBind();
                GetDeleRep();
                
            }

        }

        //get current delegate and rep 
        protected void GetDeleRep()
        {
            int delId;
            int repId;

            if (dept.DelegateApproverId == null)
            {
                txtCurDelegate.Text = "-";

            }
            else
            {
                delId = Convert.ToInt32(dept.DelegateApproverId);
                EmployeeVM empDel = Controllers.EmployeeCtrl.getEmployeebyId(delId);
                txtCurDelegate.Text = empDel.EmpName;
                lblCurrentDel.Text = txtCurDelegate.Text.ToString();
            }

            repId = Convert.ToInt32(dept.DeptRepId);
            EmployeeVM empRep = Controllers.EmployeeCtrl.getEmployeebyId(repId);
            txtRep.Text = empRep.EmpName;

        }

        //bind dropdown employee dropdown list
        protected void BindEmpDDL()
        {
            DepartmentVM dp = Controllers.DepartmentCtrl.GetDept(empId);
            List<EmployeeVM> replist = Controllers.EmployeeCtrl.getEmployeeList().Where(x => x.DeptCode == deptCode && x.Role != "Department Head" && x.EmpId != dp.DelegateApproverId).ToList();
            List<EmployeeVM> delist = Controllers.EmployeeCtrl.getEmployeeList().Where(x => x.DeptCode == deptCode && x.Role != "Department Head" && x.EmpId != dp.DeptRepId).ToList();
            ddlRep.DataSource = replist;
            ddlRep.DataTextField = "empName";
            ddlRep.DataValueField = "empId";
            ddlRep.DataBind();
            ddlDelegate.DataSource = delist;
            ddlDelegate.DataTextField = "empName";
            ddlDelegate.DataValueField = "empId";
            ddlDelegate.DataBind();
        }

        //remove delegate
        protected void RemoveDelegate(object sender, EventArgs e)
        {
            GetDeleRep();
            if (txtCurDelegate.Text == "-")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "removeemptydelwarning();", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlDeleRemove').modal();", true);//modal popup
            }

        }

        //add delegate
        protected void AddDelegate(object sender, EventArgs e)
        {
           

            if (ddlDelegate.SelectedItem.Text != "Select Employee" && txtFromDate.Text != "" && txtToDate.Text != "")
            {

                DateTime startDate = DateTime.Parse(txtFromDate.Text.ToString());
                DateTime endDate = DateTime.Parse(txtToDate.Text.ToString());

                if ( startDate > endDate)
                {
                    lblDateCompare.Text = "Start date must be earlier than end date";
                }
                else
                {
                    lblDateCompare.Visible = false;
                    lblSelectedDel.Text = ddlDelegate.SelectedItem.Text.ToString();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlDeleSet').modal();", true);//modal popup
                }

            }
        }

        protected void btnSetDelYes_Click(object sender, EventArgs e)
        {
            int delId = Convert.ToInt32(ddlDelegate.SelectedValue);
            DateTime fromdate = DateTime.Parse(txtFromDate.Text);
            DateTime todate = DateTime.Parse(txtToDate.Text);

            bool success = Controllers.DepartmentCtrl.SetDelegate(deptCode, fromdate, todate, delId);

            if (success)
            {
                
                ClearText();
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Delegate has been successfully added!"), "Successfully Added!", "success");
                EmployeeVM empRep = Controllers.EmployeeCtrl.getEmployeebyId(delId);
                txtCurDelegate.Text = empRep.EmpName;
            }
            else
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Current delegate changes not Submitted"), "Something Went Wrong!", "error");
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlRepSet').modal('toggle');", true);//modal popup
        }
        protected void btnSetDelNo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlRepSet').modal('toggle');", true);//modal popup
        }

        //add rep
        protected void AddRep(object sender, EventArgs e)
        {

            if (ddlRep.SelectedItem.Text != "Select Employee")
            {

                lblSelectedRep.Text = ddlRep.SelectedItem.Text.ToString();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlRepSet').modal();", true);
            }

        }

        protected void btnSetRepYes_Click(object sender, EventArgs e)
        {
            int repId = Convert.ToInt32(ddlRep.SelectedValue);

            bool success = Controllers.DepartmentCtrl.SetRep(deptCode, repId);

            if (success)
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Representative has been successfully added!"), "Successfully added!", "success");
                EmployeeVM empRep = Controllers.EmployeeCtrl.getEmployeebyId(repId);
                txtRep.Text = empRep.EmpName;
            }
            else
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Representative changes not Submitted"), "Something Went Wrong!", "error");
            }
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlDeleSet').modal('toggle');", true);//modal popup
        }
        protected void btnSetRepNo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlDeleSet').modal('toggle');", true);//modal popup
        }


        // remove delegate 
        protected void btnRemovDelYes_Click(object sender, EventArgs e)
        {


            bool success = Controllers.DepartmentCtrl.RemoveDelegate(deptCode);

            if (success)
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Current delegate has been successfully removed!"), "Successfully Removed!", "success");
              
            }
            else
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Current delegate changes not Submitted"), "Something Went Wrong!", "error");
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlDeleRemove').modal('toggle');", true);//modal popup
        }

        protected void btnRemovDelNo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlConfirm').modal('toggle');", true);//modal popup
        }

        
        //clear text box and dropdown list
        protected void ClearText()
        {
            ddlDelegate.SelectedIndex = 0;
            txtFromDate.Text = "";
            txtToDate.Text = "";
        }
        
        // Dynamic Chart
        [WebMethod]
        public static string GetChart(DateTime month)
        {
            DateTime fromDate = month;
            DateTime toDate = fromDate.AddMonths(-2);

            List<ReportItemVM> resultlists = Controllers.TransactionController.GetCBByMth(deptCode, fromDate, toDate);
            List<ReportItemVM> datatodisplay = new List<ReportItemVM>();
            foreach (ReportItemVM r in resultlists)
            {
                var random = new Random();
                var color = String.Format("#{0:X6}", random.Next(0x1000000));

                ReportItemVM reportItemVM = new ReportItemVM();
                reportItemVM.Period = r.Period;
                reportItemVM.Label = r.Label;
                reportItemVM.Val1 = r.Val1;
                reportItemVM.Val2 = r.Val2;
                reportItemVM.color = color;
                datatodisplay.Add(reportItemVM);
            }

            var jsondata = JsonConvert.SerializeObject(datatodisplay); //Converting the Object Lists to json format

            return jsondata;
        }


    }
}