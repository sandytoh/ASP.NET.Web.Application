using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group8_AD_webapp.Models;
using Newtonsoft.Json;

namespace Group8_AD_webapp
{
    // Author: Toh Shu Hui Sandy, A0180548Y
    // Version 1.0 Initial Release
    public partial class RequestList : System.Web.UI.Page
    {
        List<RequestVM> requests = new List<RequestVM>();
        static List<RequestDetailVM> showList = new List<RequestDetailVM>();
        static List<RequestDetailVM> bookmarkList = new List<RequestDetailVM>();
        static List<RequestDetailVM> submitList = new List<RequestDetailVM>();
        
        static int reqid;
        static int bmkid;
        static string status = "";
        static public bool IsEditable = false;
        static public bool IsNotSubmitted = false;
        static public bool IsApproved = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Service.UtilityService.CheckRoles("Employee");
            int empId = Convert.ToInt32(Session["empId"]);

            if (!IsPostBack)
            {
                // Adds active class to menu Item (sidebar)
                Main master = (Main)this.Master;
                master.ActiveMenu("none");

                reqid = 0;
                bmkid = 0;
                // If a request ID is present
                if (Request.QueryString["reqid"] != null)
                {
                    RequestVM request = new RequestVM();
                    bool success = Int32.TryParse(Request.QueryString["reqid"], out reqid);
                    if (success)
                    {
                        request = Controllers.RequestCtrl.GetRequestByReqId(reqid);
                    }
                    else
                    {
                        Session["Message"] = "That is not a valid Request ID";
                        Response.Redirect("~/Employee/RequestHistory.aspx");
                    }

                    if (request != null)
                    {
                        reqid = request.ReqId;
                        status = request.Status;
                        if (request.ApproverComment != null)
                        {
                            lblCommentContent.Text = request.ApproverComment;
                        }
                        PopulateList(reqid);
                        BindGrids();
                    }
                    else
                    {
                        Session["Message"] = "That is not a valid Request ID";
                        Response.Redirect("~/Employee/RequestHistory.aspx");
                    }
                }

                // Default: Show Unsubmitted List and Bookmarks
                else
                {
                    RequestVM unsubRequest = Controllers.RequestCtrl.GetReq(empId, "Unsubmitted").FirstOrDefault();
                    RequestVM bookmarks = Controllers.RequestCtrl.GetReq(empId, "Bookmarked").FirstOrDefault();
                    if (unsubRequest != null)
                    {
                        reqid = unsubRequest.ReqId;
                        status = unsubRequest.Status;
                        PopulateList(reqid);
                        BindGrids();
                    }
                    else
                    {
                        Response.Redirect("CatalogueDash.aspx");
                    }
                    if (bookmarks != null)
                    {
                        bmkid = bookmarks.ReqId;
                        PopulateBookmarks(bmkid);
                    }
                    else
                    {
                        bookmarkList = new List<RequestDetailVM>();
                    }
                    BindGrids();
                }

                lblStatus.Text = status.ToUpper(); 

                // Sets visible columns/buttons based on Status
                if (status == "Unsubmitted")
                {
                    IsApproved = false;
                    commentarea.Visible = false;
                    UnsubSettings();
                }

                if (status == "Submitted")
                {
                    IsEditable = true;
                    IsNotSubmitted = false;
                    IsApproved = false;
                    btnSubmit.Text = "Update";
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                    btnCatalogue.Visible = false;
                    commentarea.Visible = false;
                    lstShow.FindControl("thdBookmark").Visible = false;
                }

                if (status == "Approved" || status == "Fulfilled" || status == "Cancelled" || status == "Rejected")
                {
                    IsEditable = false;
                    IsApproved = true;
                    IsNotSubmitted = false;
                    lstShow.FindControl("thdBookmark").Visible = false;
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    btnCatalogue.Visible = false;
                    lstShow.FindControl("thdRemove").Visible = false;
                    commentarea.Visible = true;
                }

                if (status == "Cancelled" || status == "Rejected")
                {
                    IsApproved = false;
                }

                if (!IsApproved)
                {
                    HideHeaders();
                }
            }

        }

        // Sets visible columns/buttons for Unsubmitted status
        protected void UnsubSettings()
        {
            IsEditable = true;
            IsNotSubmitted = true;
            btnCancel.Visible = true;
            btnSubmit.Visible = true;
            btnCatalogue.Visible = true;
            btnReqList.Visible = false;
        }

        // Sets visible columns/buttons for not-yet-approved status
        protected void HideHeaders()
        {
            lstShow.FindControl("thdFulfQty").Visible = false;
            lstShow.FindControl("thdBalQty").Visible = false;
            lstShow.FindControl("thdFulf").Visible = false;
        }

        // Displays list of request details
        protected void PopulateList(int reqid)
        {
            List<RequestDetailVM> reqDetails = Controllers.RequestDetailCtrl.GetReqDetList(reqid);
            
            showList = reqDetails;
        }

        // Displays bookmark list
        protected void PopulateBookmarks(int reqid)
        {
            List<RequestDetailVM> reqDetails = Controllers.RequestDetailCtrl.GetReqDetList(reqid);
            bookmarkList = reqDetails;
            bookmarkList = bookmarkList.OrderByDescending(x => x.ReqLineNo).ToList();
        }

        // Binds list to listview
        protected void BindGrids()
        {
            lstShow.DataSource = showList;
            lstShow.DataBind();

            lstBookmark.DataSource = bookmarkList;
            lstBookmark.DataBind();
        }

        // Adds item from cart to bookmarks
        protected void BtnBookmark_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (ListViewItem)btn.NamingContainer;
            Label lblItemCode = (Label)item.FindControl("lblItemCode");
            Label lblDescription = (Label)item.FindControl("lblDescription");
            string itemCode = lblItemCode.Text;
            string description = lblDescription.Text;

            int empId = (int)Session["empId"];
            bool success = Controllers.RequestDetailCtrl.AddBookmark(empId, itemCode);
            Main master = (Main)this.Master;
            if (success)
            {
                RequestVM bookmarks = Controllers.RequestCtrl.GetReq(empId, "Bookmarked").FirstOrDefault();
                PopulateBookmarks(bookmarks.ReqId);
                lstBookmark.DataSource = bookmarkList;
                lstBookmark.DataBind();
                
                master.ShowToastr(this, String.Format("{0} Added to Bookmarks", description), "Item Added Successfully", "success");
            }
            else
            {
                master.ShowToastr(this, String.Format("{0} Not Added to Bookmarks", description), "Something Went Wrong!", "error");
            }
        }

        // Removes item from list
        protected void BtnRemove_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (ListViewItem)btn.NamingContainer;
            Label lblList = (Label)item.FindControl("lblList");
            Label lblReqId = (Label)item.FindControl("lblReqId");
            Label lblItemCode = (Label)item.FindControl("lblItemCode");
            Label lblDescription = (Label)item.FindControl("lblDescription");
            string list = lblList.Text;
            string description = lblDescription.Text;
            string itemCode = lblItemCode.Text;
            int reqId = Convert.ToInt32(lblReqId.Text);

            Main master = (Main)this.Master;
            bool success = Controllers.RequestDetailCtrl.RemoveReqDet(reqId, itemCode);
            if (success)
            {
                if (list == "Cart") { PopulateList(reqId); }
                else if (list == "Bookmark") { PopulateBookmarks(reqId); }
                BindGrids();
                
                master.FillCart();
                master.UpdateCartCount();

                master.ShowToastr(this, String.Format("{0}", description), "Item Removed", "success");
            }
            else
            {
                master.ShowToastr(this, String.Format("{0} Not Removed", description), "Something Went Wrong!", "error");
            }
        }

        // Back to catalogue button
        protected void BtnCatalogue_Click(object sender, EventArgs e)
        {
            Response.Redirect("CatalogueDash.aspx");
        }

        // Back to Request History button
        protected void BtnReqList_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestHistory.aspx");
        }
        
        // Adds item from Bookmarks to Cart
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var item = (ListViewItem)btn.NamingContainer;
            Label lblItemCode = (Label)item.FindControl("lblItemCode");
            Label lblDescription = (Label)item.FindControl("lblDescription");
            string itemCode = lblItemCode.Text;
            int reqQty = 1;
            string description = lblDescription.Text;

            int empId = (int)Session["empId"];
            bool success = Controllers.RequestDetailCtrl.AddToCart(empId, itemCode, reqQty);
            Main master = (Main)this.Master;

            if (success)
            {
                RequestVM unsubRequest = Controllers.RequestCtrl.GetReq(empId, "Unsubmitted").FirstOrDefault();
                PopulateList(unsubRequest.ReqId);
                lstShow.DataSource = showList;
                lstShow.DataBind();
                UnsubSettings();
                HideHeaders();
                
                master.FillCart();
                master.UpdateCartCount();

                master.ShowToastr(this, String.Format("{0} Qty:{1} Added to Order", description, reqQty), "Item Added Successfully", "success");
            }
            else
            {
                master.ShowToastr(this, String.Format("Item {0} Not Added", description), "Something Went Wrong!", "error");
            }
        }
        
        // Submits request
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            submitList = new List<RequestDetailVM>();
            foreach (ListViewItem item in lstShow.Items)
            {
                RequestDetailVM addItem = new RequestDetailVM();
                Label lblReqId = (Label)item.FindControl("lblReqId");
                addItem.ReqId = Convert.ToInt32(lblReqId.Text);
                Label lblReqLineNo = (Label)item.FindControl("lblReqLineNo");
                addItem.ReqLineNo = Convert.ToInt32(lblReqLineNo.Text);
                Label lblItemCode = (Label)item.FindControl("lblItemCode");
                addItem.ItemCode = lblItemCode.Text;
                TextBox txtQty = (TextBox)item.FindControl("spnQty");
                addItem.ReqQty = Convert.ToInt32(txtQty.Text);
                Label lblDesc = (Label)item.FindControl("lblDescription");
                addItem.Desc = lblDesc.Text;
                addItem.AwaitQty = 0;
                addItem.FulfilledQty = 0;

                submitList.Add(addItem);
            }
            
            lstConfirm.DataSource = submitList;
            lstConfirm.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }


        // Confirms submission of request
        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            bool success = Controllers.RequestCtrl.SubmitRequest(reqid, submitList);
            if (success && status == "Unsubmitted")
            {
                Session["Message"] = "Request Submitted Successfully";
                Response.Redirect("RequestHistory.aspx");
            }
            else if (success)
            {
                Session["Message"] = "Request Updated Successfully";
                Response.Redirect("RequestHistory.aspx");
            }
            else
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Request not Submitted"), "Something Went Wrong!", "error");
            }
        }

        // Cancels request
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openCancelModal();", true);
        }

        // Confirm cancellation from modal
        protected void BtnConfirmCancel_Click(object sender, EventArgs e)
        {
            bool success = Controllers.RequestCtrl.CancelRequest(reqid);
            if (success)
            {
                Session["Message"] = "Request Cancelled Successfully";
                Response.Redirect("RequestHistory.aspx");
            }
            else
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Request not Cancelled"), "Something Went Wrong!", "error");
            }
        }
    }
}