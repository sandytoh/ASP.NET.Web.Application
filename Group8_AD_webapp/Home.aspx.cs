using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group8_AD_webapp
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //IIdentity id = User.Identity;
            //Label1.Text = String.Format("IsAuthenticated:{0},Name:{1},Type:{2}",
            //                            id.IsAuthenticated, id.Name, id.AuthenticationType);

            if (Session["empId"] != null && Session["role"] != null)
            {
                GoToDash();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            bool IsValid = Int32.TryParse(txtID.Text, out int empId);
            if (!IsValid)
            {
                Label1.Text = "Please Type a Number";
            }
            else
            {
                bool success = Service.UtilityService.Authenticate(empId);
                if (!success)
                {
                    Label1.Text = "Invalid Number";
                }
                else
                {
                    GoToDash();
                }
            }
        }

        protected void GoToDash()
        {
            switch (Session["role"])
            {
                case "Department Head": Response.Redirect("~/DepartmentHead/Dashboard.aspx"); break;  
                case "Delegate": Response.Redirect("~/DepartmentHead/Dashboard.aspx"); break;          
                case "Representative": Response.Redirect("~/Employee/CatalogueDash.aspx"); break;
                case "Employee": Response.Redirect("~/Employee/CatalogueDash.aspx"); break;
                case "Store Manager": Response.Redirect("~/Manager/StoreDashboard.aspx"); break;
                case "Store Supervisor": Response.Redirect("~/Manager/StoreDashboard.aspx"); break;
                case "Store Clerk": Response.Redirect("~/Manager/StoreDashboard.aspx"); break;
                default: Response.Redirect("Login.aspx"); break;       // Redirect to exception?
            }
        }
    }
}