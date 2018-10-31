using Group8_AD_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group8AD_WebAPI.BusinessLogic;
namespace Group8_AD_webapp.Manager
{
    public partial class AdjRequestHistory : System.Web.UI.Page
    {
        // Author: Han Myint Tun , A0180555A
        // Version 1.0 Initial Release
        static List<AdjustmentVM> adj = new List<AdjustmentVM>();
        string status = "All";
        static string voucherno;
        static int empid;
        static string cmt;
        protected void Page_Load(object sender, EventArgs e)
        {
            Service.UtilityService.CheckRoles("Manager");
            empid = Convert.ToInt32(Session["empId"]);
            if (!IsPostBack)
            {
                // Adds active class to menu Item (sidebar)
                Main master = (Main)this.Master;
                master.ActiveMenu("storeadjustment");

                List<string> statuses = new List<string> { "Submitted", "Approved", "Rejected" };
                ddlStatus.DataSource = statuses;
                ddlStatus.DataBind();
            
            }
        }

        //bind request lists 
        protected void BindGrid()
        {
            status = ddlStatus.SelectedItem.Text;
            adj = Controllers.AdjustmentCtrl.GetAdjListByStatusApproverId(status, empid);
            List<AdjustmentVM> adj2 = new List<AdjustmentVM>();
            List<string> voucherno = adj.Select(a => a.VoucherNo).Distinct().ToList();

            foreach (string vnum in voucherno)
            {
                AdjustmentVM adjj = adj.Where(a => a.VoucherNo.Equals(vnum)).FirstOrDefault();
                adj2.Add(adjj);
            }
            lstRequests.DataSource = adj2.OrderByDescending(x => x.DateTimeIssued).ToList();
            lstRequests.DataBind();
        }

        //dropdown selected changed
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();

        }

        //detail button action in grid view 
        protected void lstRequests_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            if (e.CommandName == "Detail")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    voucherno = e.CommandArgument.ToString();

                    PopulateDetailList(voucherno, empid);
                }

            }


        }

        //accept
        protected void btnAccept_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlConfirm').modal();", true);//modal popup
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            cmt = txtComments.Text.ToString();
            bool success = Controllers.AdjustmentCtrl.AcceptRequest(voucherno, empid, cmt);
            if (success)
            {
                BindGrid();
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Request has been accepted!"), "Successfully approved!", "success");
               
            }
            else
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Changes not Submitted"), "Something Went Wrong!", "error");
            }

        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlConfirm').modal('toggle');", true);//modal popup
        }

        //reject
        protected void btnReject_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlRejConfirm').modal();", true);//modal popup
        }

        protected void btnRejConfirm_Click(object sender, EventArgs e)
        {

            cmt = txtComments.Text.ToString();
            bool success = Controllers.AdjustmentCtrl.RejectRequest(voucherno, empid, cmt);
            if (success)
            {
                BindGrid();
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Request has been rejected!"), "Successfully rejected!", "success");
                
            }
            else
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Changes not Submitted"), "Something Went Wrong!", "error");
            }
        }

        protected void btnRejNo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlRejConfirm').modal('toggle');", true);//modal popup
        }

        //populate request details list 
        protected void PopulateDetailList(string voucherno, int empid)
        {
            // List<AdjustmentVM> adj = AdjustmentBL.GetAdj(voucherno);
            List<AdjustmentVM> showList = BusinessLogic.GetItemAdjustList(voucherno, empid);
            foreach (AdjustmentVM aj in showList)
            {
                cmt = txtComments.Text.ToString();

                lblvnum.Text = voucherno.ToString();
                lblstatus.Text = aj.Status;
                if (aj.Status == "Submitted")
                {
                    txtComments.Visible = true;
                    btnAccept.Visible = true;
                    btnReject.Visible = true;
                    txtComments.ReadOnly = false;
                }
                else if (aj.Status == "Rejected")
                {
                    if (aj.ApproverComment == null)
                    {
                        txtComments.Text = " ";
                    }
                    else
                    {
                        txtComments.Visible = true;
                        txtComments.ReadOnly = true;
                        txtComments.Text = aj.ApproverComment.ToString();
                    }

                    btnAccept.Visible = false;
                    btnReject.Visible = false;
                    
                }
                else if (aj.Status == "Approved")
                {
                    if (aj.ApproverComment == null)
                    {
                        txtComments.Text = " ";
                    }
                    else
                    {
                        txtComments.Visible = true;
                        txtComments.ReadOnly = true;
                        txtComments.Text = aj.ApproverComment.ToString();
                    }

                    btnAccept.Visible = false;
                    btnReject.Visible = false;
                    
                }
                else
                {
                    btnAccept.Visible = false;
                    btnReject.Visible = false;
                }


                if (aj.ApproverComment == null)
                {
                    txtComments.Text = " ";
                }
                else
                {
                    txtComments.Text = aj.ApproverComment.ToString();
                }
            }
            lstShow.DataSource = showList;
            lstShow.DataBind();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);//modal popup
        }

        protected void DataPagerProducts_PreRender(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}