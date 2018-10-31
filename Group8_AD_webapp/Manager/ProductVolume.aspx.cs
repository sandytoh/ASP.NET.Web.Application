using Group8_AD_webapp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

namespace Group8_AD_webapp
{
    // Author: Toh Shu Hui Sandy, A0180548Y
    // Version 1.0 Initial Release
    public partial class ProductVolume : System.Web.UI.Page
    {
        static List<ItemVM> staticpList;
        static List<ItemVM> productList;
        static DateTime d1;
        static DateTime d2;

        protected void Page_Load(object sender, EventArgs e)
        {
            Service.UtilityService.CheckRoles("Store");

            if (!IsPostBack)
            {
                // Adds active class to menu Item (sidebar)
                Main master = (Main)this.Master;
                master.ActiveMenu("productrank");

                // Populates Search Bar
                ddlCategory.DataSource = Controllers.ItemCtrl.GetCategory();
                ddlCategory.DataBind();

                // Determines Sort Direction based on button clicked on dashboard
                if (Request.QueryString["sort"] == "asc")
                {
                    IsDesc.Value = "false";
                }
                else
                {
                    IsDesc.Value = "true";
                    ddlSortDirection.SelectedValue = "desc";
                }
                
                // Determines dates based on dates from dashboard
                if (Request.QueryString["d1"] != null && Request.QueryString["d2"] != null)
                {
                    d1 = DateTime.Parse(Request.QueryString["d1"]);
                    d2 = DateTime.Parse(Request.QueryString["d2"]);
                }
                else
                {
                    d1 = DateTime.Today.AddYears(-1);
                    d2 = DateTime.Today;
                }

                // Populates DataTable Product List
                staticpList = Controllers.TransactionCtrl.GetVolume(d1, d2);
                foreach(ItemVM i in staticpList)
                {
                    i.Price1 = Math.Round(i.Price1, 2);
                }
                productList = new List<ItemVM>(staticpList);
                SortAndBindGrid();
            }

        }

        // Populates DataTable Product List
        protected void SortAndBindGrid()
        {
            if (IsDesc.Value == "true")
            {
                lstProductVolume.DataSource = productList;
                lstProductVolume.DataBind();
            }
            else
            {
                lstProductVolume.DataSource = productList;
                lstProductVolume.DataBind();
            }

            int min = (lstProductVolume.PageIndex) * lstProductVolume.PageSize;
            int max = (min + lstProductVolume.PageSize);
            if (productList.Count < max)
            {
                max = productList.Count;
            }
            lblDateRange.Text = "Date Range: " + d1.ToString("dd-MMM-yyyy") + " to " + d2.ToString("dd-MMM-yyyy");
        }

        // Does search in list
        protected void DdlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnSearch_Click(btnSearch, EventArgs.Empty);
        }

        // Does search in list
        protected List<ItemVM> DoSearch()
        {
            d1 = DateTime.ParseExact(txtStartDate.Text, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
            d2 = DateTime.ParseExact(txtEndDate.Text, "dd-MMM-yyyy", CultureInfo.InvariantCulture);

            if (d2.CompareTo(d1) >= 0 )
            {
                return Controllers.TransactionCtrl.GetVolume(d1, d2);
            }
            else
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, "", "End Date must be after Start Date", "error");
                return new List<ItemVM>();
            }

        }

        // Does search in list
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string cat = ddlCategory.Text;
            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                if (cat == "All")
                {
                    productList = DoSearch();
                }
                else
                {
                    List<ItemVM> searchList = DoSearch();
                    productList = searchList.Where(x => x.Cat == cat).ToList();
                }
                SortAndBindGrid();
            }
            else
            {
                if (cat == "All")
                {
                    productList = new List<ItemVM>(staticpList);
                }
                else
                {
                    productList = staticpList.Where(x => x.Cat == cat).ToList();
                }
                SortAndBindGrid();
            }
        }

        // Changes sort direction
        protected void DdlSortDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sort = ddlSortDirection.SelectedValue;
            if (sort == "asc")
            {
                IsDesc.Value = "false";
                SortAndBindGrid();
            }
            else
            {
                IsDesc.Value = "true";
                SortAndBindGrid();
            }
        }
        
        // Back to Dashboard
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["d1"] != null && Request.QueryString["d2"] != null)
            {
                d1 = DateTime.Parse(Request.QueryString["d1"]);
                d2 = DateTime.Parse(Request.QueryString["d2"]);
            }

            Response.Redirect("StoreDashboard.aspx?d=" + d1.ToString("dd-MMM-yyyy") + "&d2=" + d2.ToString("dd-MMM-yyyy"));
        }
    }
}