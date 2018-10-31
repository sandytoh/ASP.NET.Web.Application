using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Group8_AD_webapp.Models;
using Newtonsoft.Json;

namespace Group8_AD_webapp
{
    // Author: Toh Shu Hui Sandy, A0180548Y
    // Version 1.0 Initial Release
    public partial class EditSupplierPrice : System.Web.UI.Page
    {
        static List<ItemVM> items = new List<ItemVM>();
        static List<ItemVM> editedItems = new List<ItemVM>();
        static List<ItemVM> submitItems = new List<ItemVM>();
        static List<String> suppliers;
        static bool isClear = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Service.UtilityService.CheckRoles("Store");

            if (!IsPostBack)
            {
                // Adds active class to menu Item (sidebar)
                Main master = (Main)this.Master;
                master.ActiveMenu("updatesupp");

                // Populates Search Bar
                suppliers = Controllers.SupplierCtrl.GetSupplierCodes();
                ddlCategory.DataSource = Controllers.ItemCtrl.GetCategory();
                ddlCategory.DataBind();

                // Populates items in list
                items = Controllers.ItemCtrl.GetAllItems();
                editedItems = JsonConvert.DeserializeObject<List<ItemVM>>(JsonConvert.SerializeObject(items));
                BindGrid();
            }
        }

        // Populates dropdowns within gridview
        protected void GridView_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddl1 = (DropDownList)e.Row.FindControl("ddlSupplier1");
                var ddl2 = (DropDownList)e.Row.FindControl("ddlSupplier2");
                var ddl3 = (DropDownList)e.Row.FindControl("ddlSupplier3");
                ddl1.Items.Add("");
                ddl2.Items.Add("");
                ddl3.Items.Add("");
                foreach (string s in suppliers)
                {
                    ddl1.Items.Add(s);
                    ddl2.Items.Add(s);
                    ddl3.Items.Add(s);
                }
                ddl1.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SuppCode1"));
                ddl2.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SuppCode2"));
                ddl3.SelectedValue = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SuppCode3"));
            }
        }

        // Populates gridview
        protected void BindGrid()
        {
            grdSupplier.DataSource = editedItems;
            grdSupplier.DataBind();

            int min = (grdSupplier.PageIndex) * grdSupplier.PageSize;
            int max = (min + grdSupplier.PageSize);
            if (editedItems.Count < max)
            {
                max = editedItems.Count;
            }
            lblPageCount.Text = "Showing " + (min + 1) + " to " + max + " of " + editedItems.Count.ToString();
        }

        // Save data upon pagination
        protected void GrdSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (SaveList())
            {
                grdSupplier.PageIndex = e.NewPageIndex;
                BindGrid();
            }
        }

        // Saves Supplier/Price data in list
        protected bool SaveList()
        {
            Main master = (Main)this.Master;
            lblPageCount.Text = "";
            foreach (GridViewRow row in grdSupplier.Rows)
            {
                int pagestart = grdSupplier.PageIndex * grdSupplier.PageSize;
                int i = pagestart + row.RowIndex;

                if(!(((TextBox)row.FindControl("txtPrice1")).Text).Any(x => !char.IsLetter(x)) ||
                    !(((TextBox)row.FindControl("txtPrice2")).Text).Any(x => !char.IsLetter(x)) ||
                    !(((TextBox)row.FindControl("txtPrice3")).Text).Any(x => !char.IsLetter(x)))
                {
                    master.ShowToastr(this, "Please enter a number", 
                        "Price for "+ ((Label)row.FindControl("lblDescription")).Text+" is blank or contains a letter", "error");
                    return false;
                }
                else
                {
                    if (editedItems[i].ItemCode == ((Label)row.FindControl("lblItemCode")).Text)
                    {
                        editedItems[i].SuppCode1 = ((DropDownList)row.FindControl("ddlSupplier1")).Text;
                        editedItems[i].SuppCode2 = ((DropDownList)row.FindControl("ddlSupplier2")).Text;
                        editedItems[i].SuppCode3 = ((DropDownList)row.FindControl("ddlSupplier3")).Text;
                        editedItems[i].Price1 = Convert.ToDouble(((TextBox)row.FindControl("txtPrice1")).Text);
                        editedItems[i].Price2 = Convert.ToDouble(((TextBox)row.FindControl("txtPrice2")).Text);
                        editedItems[i].Price3 = Convert.ToDouble(((TextBox)row.FindControl("txtPrice3")).Text);
                    }
                }
            }

            return true;
        }

        // Repopulates list upon category dropdown change
        protected void DdlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateEditedList();
            if (submitItems.Count != 0)
            {
                isClear = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openClearModal();", true);
            }
            else
            {
                DoSearch();
            }
        }

        // Performs search
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            GenerateEditedList();
            if(submitItems.Count != 0)
            {
                isClear = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openClearModal();", true);
            }
            else
            {
                DoSearch();
            }
        }

        // Performs search
        protected void DoSearch()
        {
            string searchquery = txtSearch.Text;
            string querycat = ddlCategory.Text;
            if (querycat == "All")
            {
                editedItems = JsonConvert.DeserializeObject<List<ItemVM>>(JsonConvert.SerializeObject(items));
                editedItems = editedItems.Where(x => x.Desc.ToLower().Contains(searchquery) || x.ItemCode.ToLower().Contains(searchquery)).ToList();
            }
            else
            {
                editedItems = JsonConvert.DeserializeObject<List<ItemVM>>(JsonConvert.SerializeObject(items));
                editedItems = editedItems.Where(x => x.Cat == querycat && (x.Desc.ToLower().Contains(searchquery) || x.ItemCode.ToLower().Contains(searchquery))).ToList();
            }
            BindGrid();
        }

        // Pops up confirm clear modal
        protected void BtnClear_Click(object sender, EventArgs e)
        {
            isClear = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openClearModal();", true);
        }

        // Clears textboxes/ Dropdowns
        protected void BtnConfirmClear_Click(object sender, EventArgs e)
        {
            if (isClear)
            {
                ClearTextBoxes(Page.Controls);
                for (int i = 0; i < editedItems.Count; i++)
                {
                    ItemVM tempitem = editedItems[i];
                    editedItems[i] = new ItemVM();
                    editedItems[i].ItemCode = tempitem.ItemCode;
                    editedItems[i].Desc = tempitem.Desc;
                }
                BindGrid();
            }
            else
            {
                DoSearch();
            }

        }

        // Clears textboxes/ Dropdowns
        private void ClearTextBoxes(ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;

                if (ctrl is DropDownList)
                    ((DropDownList)ctrl).SelectedIndex = 0;
                ClearTextBoxes(ctrl.Controls);
            }
        }

        // Submits and saves Supplier/Price list
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (SaveList()) { 
            submitItems = editedItems.Where(x => x.SuppCode1 != "" && x.Price1 != 0).ToList();
            submitItems = submitItems.Where(
                x => x.SuppCode1 != items.Where(y => y.ItemCode == x.ItemCode).First().SuppCode1
                || Math.Round(Convert.ToDouble(x.Price1),2) != Math.Round(Convert.ToDouble(items.Where(y => y.ItemCode == x.ItemCode).First().Price1),2)
                || Math.Round(Convert.ToDouble(x.Price2),2) != Math.Round(Convert.ToDouble(items.Where(y => y.ItemCode == x.ItemCode).First().Price2),2)
                || Math.Round(Convert.ToDouble(x.Price3),2) != Math.Round(Convert.ToDouble(items.Where(y => y.ItemCode == x.ItemCode).First().Price3),2)
                || x.SuppCode2 != items.Where(y => y.ItemCode == x.ItemCode).First().SuppCode2
                || x.SuppCode3 != items.Where(y => y.ItemCode == x.ItemCode).First().SuppCode3
            ).ToList();
            lstConfirm.DataSource = submitItems;
            lstConfirm.DataBind();
            if(submitItems.Count == 0)
            {
                lblEmptyChange.Text = "You have not made any changes!";
                btnConfirm.Visible = false;
                submitDetail.Visible = false;
            }
            else
            {
                    lblEmptyChange.Text = "";
                    btnConfirm.Visible = true;
                submitDetail.Visible = true;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        // Checks for items that have changed information and are not blank
        protected void GenerateEditedList()
        {
            if (SaveList())
            {
                submitItems = editedItems.Where(x => x.SuppCode1 != "" && x.Price1 != 0).ToList();
                submitItems = submitItems.Where(
                    x => x.SuppCode1 != items.Where(y => y.ItemCode == x.ItemCode).First().SuppCode1
                    || Math.Round(Convert.ToDouble(x.Price1), 2) != Math.Round(Convert.ToDouble(items.Where(y => y.ItemCode == x.ItemCode).First().Price1), 2)
                    || Math.Round(Convert.ToDouble(x.Price2), 2) != Math.Round(Convert.ToDouble(items.Where(y => y.ItemCode == x.ItemCode).First().Price2), 2)
                    || Math.Round(Convert.ToDouble(x.Price3), 2) != Math.Round(Convert.ToDouble(items.Where(y => y.ItemCode == x.ItemCode).First().Price3), 2)
                    || x.SuppCode2 != items.Where(y => y.ItemCode == x.ItemCode).First().SuppCode2
                    || x.SuppCode3 != items.Where(y => y.ItemCode == x.ItemCode).First().SuppCode3
                ).ToList();
            }
         }

        // Confirms and saves submission of edited Supplier/Price list
        protected void BtnConfirm_Click(object sender, EventArgs e)
        {
            bool success = Controllers.ItemCtrl.UpdateSuppliers(submitItems);
            if (success)
            {
                Session["Message"] = "Items Updated Successfully";
                Response.Redirect("StoreDashboard.aspx");   
            }
            else
            {
                Main master = (Main)this.Master;
                master.ShowToastr(this, String.Format("Item Changes not Submitted"), "Something Went Wrong!", "error");
            }
        }
    }
}