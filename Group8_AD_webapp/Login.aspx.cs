using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Group8_AD_webapp
{
    // Author: Han Myint Tun , A0180555A and Toh Shu Hui Sandy, A0180548Y
    // Version 1.0 Initial Release
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["error"] != null){
                if(Request.QueryString["error"] == "noaccess")
                {
                    Label1.Text = "You do not have the rights to access that page.";
                } else if (Request.QueryString["error"] == "notvalid")
                {
                    Label1.Text = "You are not a valid employee.";
                }
            }
            Redirect();

        }

        // Checks for authentication
        protected void Redirect()
        {
            IIdentity id = User.Identity;

            if (id.IsAuthenticated == true)
            {
                if (id.Name != null)
                { 
                    bool IsValid = Int32.TryParse(id.Name, out int empId);
                    if (IsValid)
                    {
                        bool success = Service.UtilityService.Authenticate(empId);
                        if (!success)
                        {
                            Label1.Text = "Not a valid Employee!";
                        }
                        else
                        {
                            GoToDash();
                        }
                    }
                    else
                    {
                        Label1.Text = "Please enter a valid Number";
                    }
                }
            }
        }

        // Redirects User to correct page based on role
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

        // Triggers upon login
        protected void Login1_LoggedIn(object sender, EventArgs e)

        {
            Redirect();

        }
    }
}