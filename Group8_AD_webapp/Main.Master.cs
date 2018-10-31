using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group8_AD_webapp.Models;
using System.Web.UI.HtmlControls;
using Group8AD_WebAPI.BusinessLogic;
using System.Web.Security;
using Newtonsoft.Json;

namespace Group8_AD_webapp
{
    // Authors: Han Myint Tun , A0180555A and Toh Shu Hui Sandy, A0180548Y
    // Version 1.0 Initial Release
    public partial class Main : System.Web.UI.MasterPage
    {
        public static List<RequestDetailVM> cartDetailList = new List<RequestDetailVM>();
        public static List<NotificationVM> notifList = new List<NotificationVM>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetProfile();
                FillCart();
                FillNotifications();
                PopulateMenuItems();
            }
        }

        // Sets up profile badge
        protected void SetProfile()
        {
            if(Session["empId"] != null)
            {
                int empId = (int)Session["empId"];

                if (File.Exists(Server.MapPath("~/img/employee/" + empId + ".png")))
                {
                    imgProfile.Src = "~/img/employee/" + empId + ".png";
                }
                else
                {
                    imgProfile.Src = "~/img/employee/profile_default.png";
                }

            lblName.Text = (string)Session["empName"];
                lblRole.Text = (string)Session["role"];
            }
            else
            {
                Response.Redirect("~/Home.aspx");
            }
        }

        // Sets up Sidebar menu based on role
        protected void PopulateMenuItems()
        {
            List<HtmlGenericControl> deptHeadList = new List<HtmlGenericControl>() { menuDeptHeadDash, menuDeptHeadRequest };
            List<HtmlGenericControl> employeeList = new List<HtmlGenericControl>() { menuCatalogueDash, menuEmployeeRequest};
            List<HtmlGenericControl> storeList = new List<HtmlGenericControl>() { menuManagerDash, menuProductVol, menuRestock, menuSuppliers,menuReports, menuReports2 };
            List<HtmlGenericControl> managerList = new List<HtmlGenericControl>() { menuManagerDash, menuProductVol, menuRestock, menuSuppliers, menuAdjustment, menuReports, menuReports2 };
            List<HtmlGenericControl> allMenu = new List<HtmlGenericControl>();
            allMenu.AddRange(deptHeadList);
            allMenu.AddRange(employeeList);
            allMenu.AddRange(storeList);
            allMenu.AddRange(managerList);
            foreach (HtmlGenericControl m in allMenu)
            {
                m.Visible = false;
            }
            btnCartLi.Visible = false;

            switch (Session["role"])
            {
                case "Department Head":
                        foreach (HtmlGenericControl m in deptHeadList)  m.Visible = true; break; 
                case "Delegate":
                    foreach (HtmlGenericControl m in deptHeadList) m.Visible = true; break;
                case "Representative":
                    {
                        foreach (HtmlGenericControl m in employeeList) m.Visible = true;
                        btnCartLi.Visible = true;
                        break;
                    }
                case "Employee":
                    {
                        foreach (HtmlGenericControl m in employeeList) m.Visible = true;
                        btnCartLi.Visible = true;
                        break;
                    }
                case "Store Manager":
                    foreach (HtmlGenericControl m in managerList) m.Visible = true; break;
                case "Store Supervisor":
                    foreach (HtmlGenericControl m in managerList) m.Visible = true; break;
                case "Store Clerk":
                    foreach (HtmlGenericControl m in storeList) m.Visible = true; break;
                default:  break;
            }
        }

        // Sets up Cart dropdown
        public void FillCart()
        {
            int empId = (int)Session["empId"];
            RequestVM cart = Controllers.RequestCtrl.GetReq(empId, "Unsubmitted").FirstOrDefault();
            
            if (cart != null)
            {
                int reqId = cart.ReqId;
                List<RequestDetailVM> reqDetails = Controllers.RequestDetailCtrl.GetReqDetList(reqId);
                reqDetails = BusinessLogic.AddItemDescToReqDet(reqDetails);
                cartDetailList = reqDetails;
                lstCart.DataSource = cartDetailList;
                lstCart.DataBind();
                UpdateCartCount();

            }
            else {
                cartDetailList = new List<RequestDetailVM>();
                lstCart.DataSource = cartDetailList;
                lstCart.DataBind();
            }
        }

        // Sets up Cart count dropdown
        public void UpdateCartCount()
        {
            int cartCount = 0;
            foreach (RequestDetailVM item in cartDetailList)
            {
                cartCount += item.ReqQty;
            }
            lblCartCount.Text = cartCount.ToString();
        }

        // Sets up Notification dropdown
        public void FillNotifications()
        {
            int empId = (int)Session["empId"];
            notifList = NotificationBL.GetNotifications(empId);
            lblNotifCount.Text = notifList.Where(x => x.IsRead == false).Count().ToString();

            notifList = notifList.OrderByDescending(x => x.NotificationDateTime).Take(10).ToList();

            lstNotifications.DataSource = notifList;
            lstNotifications.DataBind();
        }

        // Rebind cart
        protected void LstCart_PagePropertiesChanged(object sender, EventArgs e)
        {
            lstCart.DataSource = cartDetailList;
            lstCart.DataBind();
        }

        // Rebind notifications
        protected void LstNotif_PagePropertiesChanged(object sender, EventArgs e)
        {
            FillNotifications();
        }

        // Logout of site
        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }

        // GO to cart button
        protected void BtnCart_Click(object sender, EventArgs e)
        {
            int empId = (int)Session["empId"];
            RequestVM unsubRequest = Controllers.RequestCtrl.GetReq(empId, "Unsubmitted").FirstOrDefault();
            Main master = this;
            if (unsubRequest != null)
            {
                if (cartDetailList.Count != 0)
                {
                    Response.Redirect("~/Employee/RequestList.aspx");
                }
            }
        }

        // Toggles read status of notifications
        protected void BtnOnNotif_Click(object sender, EventArgs e)
        {
            var lbl = (LinkButton)sender;
            var item = (ListViewItem)lbl.NamingContainer;
            int id = Convert.ToInt32(((Label)item.FindControl("lblID")).Text);
            NotificationBL.ToggleReadNotification(id);
            FillNotifications();
        }

        // Marksall visible notifications as read
        protected void BtnMarkRead_Click(object sender, EventArgs e)
        {
            NotificationBL.MarkAllAsRead(notifList);
            FillNotifications();
        }

        // Service Method for showing Toast
        public void ShowToastr(object sender, string message, string title, string type)
        {
            ScriptManager.RegisterStartupScript((Page)sender, sender.GetType(), "toastr_message",
            String.Format("toastr.{0}('{1}', '{2}', {{positionClass: 'toast-bottom-right'}});", type.ToLower(), message, title), true);
        }

        // Service method to add active class to sidebar menu item
        public void ActiveMenu(string page)
        {
            List<HtmlGenericControl> allMenu = new List<HtmlGenericControl>() { menuDeptHeadDash, menuDeptHeadRequest, menuDeptHeadSubmitted, menuDeptHeadApproved, menuDeptHeadRejected,
                menuDeptHeadSubmitCancelled, menuDeptHeadFulfilled, menuCatalogueDash, menuEmployeeRequest, menuManagerDash, menuProductVol, menuRestock, menuSuppliers, menuReports, menuReports2, menuAdjustment };
            foreach(HtmlGenericControl menu in allMenu)
            { 
                menu.Attributes.Remove("class");
            }

            switch (page)
            {
                case "catalogue": menuCatalogueDash.Attributes.Add("class", "menuactive"); break;
                case "reqhistory": menuEmployeeRequest.Attributes.Add("class", "menuactive"); break;
                case "storedash": menuManagerDash.Attributes.Add("class", "menuactive"); break;
                case "productrank": menuProductVol.Attributes.Add("class", "menuactive"); break;
                case "updatesupp": menuSuppliers.Attributes.Add("class", "menuactive"); break;
                case "reports": menuReports.Attributes.Add("class", "menuactive"); break;
                case "reports2": menuReports2.Attributes.Add("class", "menuactive"); break;

                case "dhdash": menuDeptHeadDash.Attributes.Add("class", "menuactive"); break;
                case "dhrequest": menuDeptHeadRequest.Attributes.Add("class", "menuactive"); break;
                case "dhsubmit": menuDeptHeadSubmitted.Attributes.Add("class", "menuactive"); break;
                case "dhapprove": menuDeptHeadApproved.Attributes.Add("class", "menuactive"); break;
                case "dhcancel": menuDeptHeadSubmitCancelled.Attributes.Add("class", "menuactive"); break;
                case "dhreject": menuDeptHeadRejected.Attributes.Add("class", "menuactive"); break;
                case "dhfulfill": menuDeptHeadFulfilled.Attributes.Add("class", "menuactive"); break;
                case "storerestock": menuRestock.Attributes.Add("class", "menuactive"); break;
                case "storeadjustment": menuAdjustment.Attributes.Add("class", "menuactive"); break;
                case "none": break;
                default: break;
            }                
        }
    }
}