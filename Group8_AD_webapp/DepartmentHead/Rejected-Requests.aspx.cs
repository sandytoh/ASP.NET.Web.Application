using Group8_AD_webapp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group8AD_WebAPI.BusinessLogic;
namespace Group8_AD_webapp
{
    public partial class Rejected_Requests : System.Web.UI.Page
    {
        // Author: Han Myint Tun , A0180555A
        // Version 1.0 Initial Release
        static int rid;
        int empId;
        string status = "Rejected";
        protected void Page_Load(object sender, EventArgs e)
        {
            Service.UtilityService.CheckRoles("DeptHead");
            empId = Convert.ToInt32(Session["empId"]);
            if (!IsPostBack)
            {
                Main master = (Main)this.Master;
                master.ActiveMenu("dhreject");
                BindGrid();
            }
        }

        //bind rejected request list
        protected void BindGrid()
        {
            List<EmpReqVM> requestlists = BusinessLogic.GetEmpReqList(empId, status);
            lstCancel.DataSource = requestlists;
            lstCancel.DataBind();
        }

        // populate rejected detail in modal
        protected void PopulateDetailList(int rid)
        {
            RequestVM req = Controllers.RequestCtrl.GetRequestByReqId(rid);
            EmployeeVM emp = Controllers.EmployeeCtrl.getEmployeebyId(req.EmpId);

            List<RequestDetailVM> showList = BusinessLogic.GetItemDetailList(rid);
            lblReqid.Text = req.ReqId.ToString();
            lblEmpName.Text = emp.EmpName.ToString();
            lblSubmitteddate.Text = req.ReqDateTime.ToString("dd'-'MMM'-'yyyy");
            lblReject.Text = req.ApprovedDateTime.ToString("dd'-'MMM'-'yyyy");
            if (req.ApproverComment == null)
            {
                txtComments.Text = "";
            }
            else
            {
                txtComments.Text = req.ApproverComment.ToString();
            }
           
            lstShow.DataSource = showList;
            lstShow.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);//modal popup
        }

        //detail buttom action 
        protected void lstOrder_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "ReqDetail")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    rid = Convert.ToInt32(e.CommandArgument);
                    PopulateDetailList(rid);
                }
            }
        }

        protected void DataPagerProducts_PreRender(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}