using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group8_AD_webapp.Models;
using Group8AD_WebAPI.BusinessLogic;

namespace Group8_AD_webapp.Manager
{

    // Author: Han Myint Tun , A0180555A
    // Co-Author: Toh Shu Hui Sandy, A0180548Y (Only the TrendChart Modal contents)
    // Version 1.0 Initial Release
    public partial class RestockLevel : System.Web.UI.Page
    {
        static List<ItemVM> items = new List<ItemVM>();
        static List<ItemVM> editedItems = new List<ItemVM>();
        static string cat;
        static string desc;
        static double thres;
        static List<ReportItemVM> trendList = new List<ReportItemVM>();
        static string lblTrend;

        protected void Page_Load(object sender, EventArgs e)
        {
            Service.UtilityService.CheckRoles("Store");
            int empid = Convert.ToInt32(Session["empId"]);
            if (!IsPostBack)
            {

                // Adds active class to menu Item (sidebar)
                Main master = (Main)this.Master;
                master.ActiveMenu("storerestock");

                List<String> productList = Controllers.ItemCtrl.GetCategory();
                ddlCategory.DataSource = productList;
                ddlCategory.DataBind();
               // ddlThreshold.SelectedIndex = 4;
                
                BindGrid();
            }
        }
        protected void BindGrid()
        {
            editedItems = Controllers.ItemCtrl.GetAllItemsbyThreshold();
            grdRestockItem.DataSource = editedItems;
            grdRestockItem.DataBind();
            int min = (grdRestockItem.PageIndex) * grdRestockItem.PageSize;
            int max = (min + grdRestockItem.PageSize);
            if (editedItems.Count < max)
            {
                max = editedItems.Count;
            }
            lblPageCount.Text = "Showing " + (min + 1) + " to " + max + " of " + editedItems.Count.ToString();
        }

        protected void SearchList()
        {
            cat = ddlCategory.SelectedValue;
            desc = txtSearch.Text;
            thres = Convert.ToDouble(ddlThreshold.SelectedValue);
            grdRestockItem.DataSource = Controllers.ItemCtrl.GetItems(cat, desc, thres);
            grdRestockItem.DataBind();
        }

        protected void txtSearch_Changed(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchList();
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdRestockItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            saveList();
            grdRestockItem.PageIndex = e.NewPageIndex;
            grdRestockItem.DataSource = editedItems;
            grdRestockItem.DataBind();
        }

        protected void saveList()
        {
            lblPageCount.Text = "";
            foreach (GridViewRow row in grdRestockItem.Rows)
            {
                int pagestart = grdRestockItem.PageIndex * grdRestockItem.PageSize;
                int i = pagestart + row.RowIndex;
                if (editedItems[i].ItemCode == ((Label)row.FindControl("lblItemCode")).Text)
                {
                    editedItems[i].NewReorderLvl = Convert.ToInt32(((TextBox)row.FindControl("txtChangeReLevel")).Text);
                    editedItems[i].NewReorderQty = Convert.ToInt32(((TextBox)row.FindControl("txtChangeRestockQty")).Text);
                }
               
            }
        }

        protected void grdRestockItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int RowIndex = int.Parse(e.CommandArgument.ToString()); // getting row index

            if (e.CommandName == "ReLevel")
            {

                if (e.CommandArgument.ToString() != "")
                {
                    Label lb = (Label)grdRestockItem.Rows[RowIndex].FindControl("lblRecomLevel");
                    string l = lb.Text;
                    TextBox tb = (TextBox)grdRestockItem.Rows[RowIndex].FindControl("txtChangeReLevel");
                    tb.Text = l;

                }

            }

            if (e.CommandName == "ReQty")
            {

                if (e.CommandArgument.ToString() != "")
                {
                    Label lb1 = (Label)grdRestockItem.Rows[RowIndex].FindControl("lblRecomQty");
                    string l1 = lb1.Text;
                    TextBox tb1 = (TextBox)grdRestockItem.Rows[RowIndex].FindControl("txtChangeRestockQty");
                    tb1.Text = l1;

                }

            }

            if (e.CommandName == "Trend")
            {

                if (e.CommandArgument.ToString() != "")
                {
                    Label icode = (Label)grdRestockItem.Rows[RowIndex].FindControl("lblItemCode");
                    Label lblRecomLvl = (Label)grdRestockItem.Rows[RowIndex].FindControl("lblRecomLevel");
                    Label lblRecomQty = (Label)grdRestockItem.Rows[RowIndex].FindControl("lblRecomQty");
                    string itmcode = icode.Text;
                    ItemVM itm = new ItemVM();
                    itm =  Controllers.ItemCtrl.GetItem(itmcode);

                    trendList = Controllers.ReportItemCtrl.ShowVolumeReport(itmcode, itm.ReorderLevel);
                    foreach(ReportItemVM r in trendList)
                    {
                        r.Val2 = itm.ReorderLevel;
                        r.Val3 = itm.ReorderQty;
                    }


                    lblTrendTitle.Text = "Trend Analysis for " + itm.ItemCode + " : " + itm.Desc;
                    lblTrendiCode.Text = itmcode;
                    lblTrend = itmcode + ": " + itm.Desc;
                    lblTrendSubtitle.Text = " "+trendList[0].Label+" to "+trendList[trendList.Count - 1].Label;
                    lblTrendReccRL.Text = lblRecomLvl.Text;
                    lblTrendReccRQ.Text = lblRecomQty.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlTrend').modal();", true);//modal popup
                }

            }


        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            saveList();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlConfirm').modal();", true);//modal popup
        }
      
        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            List<ItemVM> updateitem = new List<ItemVM>();

            foreach (ItemVM item in editedItems)
            {
                ItemVM i = Controllers.ItemCtrl.GetItem(item.ItemCode);
                i.ReorderLevel = item.NewReorderLvl;
                i.ReorderQty = item.NewReorderQty;
                updateitem.Add(i);
            }


            bool success = Controllers.ItemCtrl.UpdateItems(updateitem);
            if (success)
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Item Reorder Level and Quantity are updated"), "Successfully update!", "success");
                //BindGrid();
            }
            else
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Item Reorder Level and Quantity Changes not Submitted"), "Something Went Wrong!", "error");
            }
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlConfirm').modal('toggle');", true);//modal popup
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlConfirm').modal('toggle');", true);//modal popup
        }

        protected void BtnUseTrend_Click(object sender, EventArgs e)
        {
            ItemVM trendItem = editedItems.Where(x => x.ItemCode == lblTrendiCode.Text).FirstOrDefault();
            trendItem.NewReorderLvl = Convert.ToInt32(lblTrendReccRL.Text);
            trendItem.NewReorderQty = Convert.ToInt32(lblTrendReccRQ.Text);
            grdRestockItem.DataSource = editedItems;
            grdRestockItem.DataBind();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#mdlTrend').modal('toggle');", true);
        }

        // Populates data to send to Chart.js
        [System.Web.Services.WebMethod]
        public static List<string> getChartData()
        {
            var returnData = new List<string>();

            var chartLabel = new StringBuilder();
            var chartData = new StringBuilder();
            var chartData2 = new StringBuilder();
            var chartData3 = new StringBuilder();
            chartLabel.Append("[");
            chartData.Append("[");
            chartData2.Append("[");
            chartData3.Append("[");
            for (int i = 0; i < trendList.Count; i++)
            {
                if (i < trendList.Count - 1)
                {
                    string s = (trendList[i].Label);
                    chartLabel.Append("'" + s + "', ");
                }
                else
                {
                    string s = (trendList[i].Label);
                    chartLabel.Append("'" + s + "'");
                }
            }
            for (int i = 0; i < trendList.Count; i++)
            {
                if (i < trendList.Count - 1)
                {
                    chartData.Append(trendList[i].Val1 + ", ");
                }
                else
                {
                    chartData.Append(trendList[i].Val1);
                }
            }
            for (int i = 0; i < trendList.Count; i++)
            {
                if (i < trendList.Count - 1)
                {
                    chartData2.Append(trendList[i].Val2 + ", ");
                }
                else
                {
                    chartData2.Append(trendList[i].Val2);
                }
            }
            for (int i = 0; i < trendList.Count; i++)
            {
                if (i < trendList.Count - 1)
                {
                    chartData3.Append(trendList[i].Val3 + ", ");
                }
                else
                {
                    chartData3.Append(trendList[i].Val3);
                }
            }
            chartData.Append("]");
            chartData2.Append("]");
            chartData3.Append("]");
            chartLabel.Append("]");

            returnData.Add(chartLabel.ToString());
            returnData.Add(chartData.ToString());
            returnData.Add(chartData2.ToString());
            returnData.Add(chartData3.ToString());
            returnData.Add(lblTrend); 
            returnData.Add("Current Reorder Level"); 
            returnData.Add("Current Reorder Quantity"); 
            return returnData;
        }

    }
}

