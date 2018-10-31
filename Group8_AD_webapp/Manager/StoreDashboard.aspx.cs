using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group8_AD_webapp.Models;

namespace Group8_AD_webapp
{
    // Author: Toh Shu Hui Sandy, A0180548Y
    // Version 1.0 Initial Release
    public partial class StoreDashboard : System.Web.UI.Page
    {
        static List<ItemVM> volumeList;
        static DateTime d;
        static DateTime d2;
        protected void Page_Load(object sender, EventArgs e)
        {
            Service.UtilityService.CheckRoles("Store");

            if (!IsPostBack)
            {
                Main master = (Main)this.Master;
                master.ActiveMenu("storedash");

                if (Session["Message"] != null)
                {
                    master.ShowToastr(this, String.Format(""), (string)Session["Message"], "success");
                    Session["Message"] = null;
                }
                if (Request.QueryString["d"] != null && Request.QueryString["d2"] != null)
                {
                    d = DateTime.Parse(Request.QueryString["d"]);
                    d2 = DateTime.Parse(Request.QueryString["d2"]);
                }
                else
                {
                    d = DateTime.Today.AddYears(-1);
                    d2 = DateTime.Today;
                }

                volumeList = Controllers.TransactionCtrl.GetVolume(d,d2); 

                SortAndBindGrids();
                lblDateRange.Text = "Date Range: "+d.ToString("dd-MMM-yyyy") + " to " + d2.ToString("dd-MMM-yyyy");
                PopulateCBChart();
            }
        }

        protected void PopulateCBChart()
        {
            List<ReportItemVM> cbList = Controllers.ReportItemCtrl.GetCBMonthly(d, d2);
        }

        protected void txtMonthPick_TextChanged(object sender, EventArgs e)
        {
            string s = txtMonthPick.Text.ToString();
            d = DateTime.ParseExact(s, "MMMM yyyy", CultureInfo.InvariantCulture);
            int n = DateTime.DaysInMonth(d.Year, d.Month);
            d2 = d.AddDays(n - d.Day);
            volumeList = Controllers.TransactionCtrl.GetVolume(d,d2);
            SortAndBindGrids();
            lblDateRange.Text = "Date Range: " + d.ToString("dd-MMM-yyyy") + " to " + d2.ToString("dd-MMM-yyyy");
        }

        protected void SortAndBindGrids()
        {
            grdTopProducts.DataSource = volumeList.OrderByDescending(x => x.TempQtyReq).Take(10); 
            grdBotProducts.DataSource = volumeList.OrderBy(x => x.TempQtyReq).Take(10); 
            grdTopProducts.DataBind();
            grdBotProducts.DataBind();
        }

        protected void btnMore_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductVolume.aspx?sort=desc&d1="+ d.ToString("dd-MMM-yyyy") + "&d2="+ d2.ToString("dd-MMM-yyyy"));
        }

        protected void btnMore2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductVolume.aspx?sort=asc&d1=" + d.ToString("dd-MMM-yyyy") + "&d2=" + d2.ToString("dd-MMM-yyyy"));
        }

        [System.Web.Services.WebMethod]
        public static List<string> getChartData()
        {
            var returnData = new List<string>();

            List<ReportItemVM> cbList = Controllers.ReportItemCtrl.GetCBMonthly(d, d2);

            var chartLabel = new StringBuilder();
            var chartData = new StringBuilder();
            chartLabel.Append("[");
            chartData.Append("[");
            for (int i = 0; i < 10; i++)
            {
                if (i < 9)
                {
                    string s = (cbList[i].Label).Replace("Department", "");
                    chartLabel.Append("'" + s + "', ");
                }
                else
                {
                    string s = (cbList[i].Label).Replace("Department", "");
                    chartLabel.Append("'" + s + "'");
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (i < 10)
                {
                    chartData.Append(cbList[i].Val1 + ", ");
                }
                else
                {
                    chartData.Append(cbList[i].Val1);
                }
            }
            chartData.Append("]");
            chartLabel.Append("]");

            returnData.Add(chartLabel.ToString());
            returnData.Add(chartData.ToString());
            return returnData;
        }
    }
}