using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group8_AD_webapp.Models;
using RestSharp;

namespace Group8_AD_webapp
{
    // Author: Toh Shu Hui Sandy, A0180548Y
    // Version 1.0 Initial Release
    public partial class RequestHistory : System.Web.UI.Page
    {
        List<RequestVM> requests = new List<RequestVM>();
        string status = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Adds active class to menu Item (sidebar)
            Service.UtilityService.CheckRoles("Employee");
            int empId = Convert.ToInt32(Session["empId"]);

            // Clears message upon postback
            if (Session["Message"] == null)
            {
                lblMessage.Text = "";
                divAlert.Visible = false;
            }

            if (!IsPostBack)
            {
                // Adds active class to menu Item (sidebar)
                Main master = (Main)this.Master;
                master.ActiveMenu("reqhistory");

                // Populates dropdown list
                List<string> statuses = new List<string> { "Submitted", "Approved", "Fulfilled", "Cancelled", "Rejected"};
                ddlStatus.DataSource = statuses;
                ddlStatus.DataBind();

                // Populates Request List
                requests = Controllers.RequestCtrl.GetReq(empId, "All");
                BindGrid();

                // For showing message if coming from request list page
                if (Session["Message"] != null){
                    lblMessage.Text = Session["Message"].ToString();
                    Session["Message"] = null;
                    divAlert.Visible = true;
                }
                else
                {
                    divAlert.Visible = false;
                }
            }

        }

        // Binds List of Requests
        protected void BindGrid()
        {
            requests = requests.OrderByDescending(x => x.ReqDateTime).ToList();
            lstRequests.DataSource = requests;
            lstRequests.DataBind();
        }

        // Searches request list upon dropdown change
        protected void DdlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int empId = Convert.ToInt32(Session["empId"]);
            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                DoSearch();
            }
            else
            {
                status = ddlStatus.Text;
                requests = Controllers.RequestCtrl.GetReq(empId, status);
                BindGrid();
            }
        }

        // Performs search
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        // Performs search
        protected void DoSearch()
        {
            int empId = Convert.ToInt32(Session["empId"]);
            status = ddlStatus.Text;
            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                DateTime startDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (endDate.CompareTo(startDate) >= 0)
                {
                    requests = Controllers.RequestCtrl.GetRequestByDateRange(empId, status, startDate, endDate);
                    BindGrid();
                }
                else
                {
                    Main master = (Main)this.Master;
                    master.ShowToastr(this, "", "End Date must be after Start Date", "error");
                }
             }
            else
            {
                requests = Controllers.RequestCtrl.GetReq(empId, status);
                BindGrid();
            }
        }
    }
}