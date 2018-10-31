using System;
using System.Collections.Generic;
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
    public partial class CatalogueDash : System.Web.UI.Page
    {
        static List<ItemVM> allItems = new List<ItemVM>();
        static List<ItemVM> items = new List<ItemVM>();
        static public bool IsBmkTab;

        static List<RequestDetailVM> bookmarkList = new List<RequestDetailVM>();
        static List<ItemVM> frequentList = new List<ItemVM>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Service.UtilityService.CheckRoles("Employee");
            
            if (!IsPostBack)
            {
                // Adds active class to menu Item (sidebar)
                Main master = (Main)this.Master;
                master.ActiveMenu("catalogue");

                // Prepare dropdowns and Catalogue/Bookmarks/Recommendations
                PopulateDropDowns();
                PopulateCatalogue();
                showgrid.Visible = true;
                showlist.Visible = false;
                IsClean.Value = "false";
                IsBmkTab = true;
                PopulateSidePanel();
                BindSidePanel();

                // Populates items for searching
                allItems = (Controllers.ItemCtrl.GetAllItems()).OrderBy(x => x.Desc).ToList();
                lstSearch.DataSource = allItems;
                lstSearch.DataBind();
            }

            ddlsearchcontent.Visible = false;
        }

        // Populates Category and Page Count choice dropdowns
        protected void PopulateDropDowns()
        {
            ddlCategory.DataSource = Controllers.ItemCtrl.GetCategory();
            ddlCategory.DataBind();

            List<string> pagecounts = new List<string> { "6", "9", "12", "All" };
            ddlPageCount.DataSource = pagecounts;
            ddlPageCount.SelectedIndex = 1;
            ddlPageCount.DataBind();
        }

        // Populates catalogue items
        protected void PopulateCatalogue()
        {
            lblCatTitle.Text = "Catalogue";
            items = (Controllers.ItemCtrl.GetAllItems()).OrderBy(x => x.Desc).ToList();

            BindGrids();
        }

        // Databind Catalogue Grid/List
        protected void BindGrids()
        {
            lstCatalogue.DataSource = items;
            lstCatalogue.DataBind();

            grdCatalogue.DataSource = items;
            grdCatalogue.DataBind();

            int max = dpgGrdCatalogue.StartRowIndex + dpgGrdCatalogue.MaximumRows;
            int min = (dpgGrdCatalogue.StartRowIndex + 1);
            if (items.Count < max)
            {
                max = items.Count;
            } 
            if(items.Count == 0)
            {
                min = 0;
            }
            lblPageCount.Text = "Showing " + min  + " to " + max + " of " + items.Count();
        }

        // Populates Bookmark/Recommendation panel
        protected void PopulateSidePanel()
        {
            int empId = (int)Session["empId"];
            RequestVM bookmarkReq = Controllers.RequestCtrl.GetReq(empId, "Bookmarked").FirstOrDefault();
            if (bookmarkReq != null)
            {
                int bmkid = bookmarkReq.ReqId;
                List<RequestDetailVM> bookmarkDetails = Controllers.RequestDetailCtrl.GetReqDetList(bmkid);
                bookmarkDetails = BusinessLogic.AddItemDescToReqDet(bookmarkDetails);
                bookmarkList = bookmarkDetails.OrderBy(x => x.Desc).ToList();
            }

            frequentList = Controllers.ItemCtrl.GetFrequentList(empId);
            frequentList = frequentList.OrderBy(x => x.Desc).ToList();
        }

        // Binds Bookmark/Recommendation panel
        protected void BindSidePanel()
        {
            if (IsBmkTab == true)
            {
                lstBookmarks.DataSource = bookmarkList;
                lstBookmarks.DataBind();
            }
            else
            {
                lstBookmarks.DataSource = frequentList;
                lstBookmarks.DataBind();
            }
        }

        // Searches Catalogue
        protected void DoSearch()
        {
            string cataloguequery = (string)Session["Query"];
            string querycat = (string)Session["QueryCat"];

            if (cataloguequery != "")
            {
                if (querycat == "All")
                {
                    items = allItems.Where(x => x.Desc.ToLower().Contains(cataloguequery)).OrderBy(y=>y.Desc).ToList(); 
                }
                else
                {
                    items = allItems.Where(x => x.Cat == querycat && x.Desc.ToLower().Contains(cataloguequery)).OrderBy(y => y.Desc).ToList(); 
                }
            }
            else
            {
                if (querycat == "All")
                {
                    items = new List<ItemVM>(allItems);
                }
                else
                {
                    items = allItems.Where(x => x.Cat == querycat).ToList();  
                }
            }

            lblCatTitle.Text = "Search Results";
            BindGrids();
        }

        // Rebinds listviews upon changes
        protected void LstCatalogue_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpgGrdCatalogue.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            dpgLstCatalogue.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            dpgGrdCatalogue2.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            dpgLstCatalogue2.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindGrids();
        }
        
        // Adds Item to Bookmarks
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
                PopulateSidePanel();
                bookmarkList = bookmarkList.OrderByDescending(x => x.ReqLineNo).ToList();
                BtnShowBmk_Click(btnShowBmk,EventArgs.Empty);
                
                master.ShowToastr(this, String.Format("{0} Added to Bookmarks",description), "Item Added Successfully", "success");
            }
            else
            {
                master.ShowToastr(this, String.Format("{0} Not Added to Bookmarks", description), "Something Went Wrong", "error");
            }
        }

        // Adds item to Cart (Unsubmitted Request List)
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var item = (ListViewItem)btn.NamingContainer;
            TextBox txtQty = (TextBox)item.FindControl("spnQty");
            Label lblItemCode = (Label)item.FindControl("lblItemCode");
            Label lblDescription = (Label)item.FindControl("lblDescription");
            string itemCode = lblItemCode.Text;
            int reqQty = Convert.ToInt32(txtQty.Text);
            string description = lblDescription.Text;

            int empId = (int)Session["empId"];
            bool success = Controllers.RequestDetailCtrl.AddToCart(empId, itemCode, reqQty);
            Main master = (Main)this.Master;
            if (success)
            {
                master.FillCart();
                master.UpdateCartCount();

                master.ShowToastr(this, String.Format("{0} Qty:{1} Added to Order", description, reqQty), "Item Added Successfully", "success");
            }
            else
            {
                master.ShowToastr(this, String.Format("Item {0} Not Added", description), "Something Went Wrong!", "error");
            }
        }

        // Switches from List to Picture View
        protected void BtnGrid_Click(object sender, EventArgs e)
        {
            showgrid.Visible = true;
            showlist.Visible = false;
            if (IsClean.Value == "true")
            {
                sidepanelarea.Style.Add("display", "none");
            }
            else
            {
                sidepanelarea.Style.Add("display", "block");
            }
        }

        //Switches from Picture to List view
        protected void BtnList_Click(object sender, EventArgs e)
        {
            showgrid.Visible = false;
            showlist.Visible = true;
            if (IsClean.Value == "true")
            {
                sidepanelarea.Style.Add("display", "none");
            }
            else
            {
                sidepanelarea.Style.Add("display", "block");
            }
        }

        // Go to cart if cart is not empty
        protected void btnCart_Click(object sender, EventArgs e)
        {
            int empId = (int)Session["empId"];
            RequestVM cart = Controllers.RequestCtrl.GetReq(empId, "Unsubmitted").FirstOrDefault();
            if (cart != null)
            {
                if (Controllers.RequestDetailCtrl.GetReqDetList(cart.ReqId).Count != 0)
                {
                    Response.Redirect("~/Employee/RequestList.aspx");
                }
            }
        }

        // Search Catalogue
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            GetSearchQuery();
            DoSearch();
        }

        // Saves search query to session
        protected void GetSearchQuery()
        {
            Session["Query"] = txtSearch.Text.ToLower();
            Session["QueryCat"] = ddlCategory.Text;
        }

        // Saves category query to session
        protected void DdlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Query"] = "";
            Session["QueryCat"] = ddlCategory.Text;
            DoSearch();
        }

        // Populates dropdown when search terms entered
        protected void TxtSearch_Changed(object sender, EventArgs e)
        {
            string cataloguequery = txtSearch.Text.ToLower();
            string querycat = ddlCategory.Text;
            List<ItemVM> searchitems = new List<ItemVM>();
            if (querycat == "All")
            {
                searchitems = allItems.Where(x => x.Desc.ToLower().Contains(cataloguequery)).OrderBy(y => y.Desc).Take(5).ToList();
            } 
            else{
                searchitems = allItems.Where(x => x.Cat == querycat && x.Desc.Contains(cataloguequery)).OrderBy(y => y.Desc).ToList();
            }
            lstSearch.DataSource = searchitems;
            lstSearch.DataBind();
            ddlsearchcontent.Visible = true;
        }

        // Rebinds search list upon changes
        protected void LstSearch_PagePropertiesChanged(object sender, EventArgs e)
        {
            items = new List<ItemVM>();
            string searchquery = txtSearch.Text;
            List<ItemVM> searchitems = items.Where(x => x.Desc.ToLower().Contains(searchquery)).OrderBy(y => y.Desc).Take(5).ToList();
            lstSearch.DataSource = searchitems;
            lstSearch.DataBind();
            ddlsearchcontent.Visible = true;
        }

        // Changes page count when dropdown option chosen
        protected void DdlPageCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlPageCount.SelectedValue == "All"){
                dpgGrdCatalogue.PageSize = Convert.ToInt32(items.Count());
                dpgLstCatalogue.PageSize = Convert.ToInt32(items.Count());
                lblPageCount.Text = ddlPageCount.SelectedValue;
            }
            else
            {
                dpgGrdCatalogue.PageSize = Convert.ToInt32(ddlPageCount.SelectedValue);
                dpgLstCatalogue.PageSize = Convert.ToInt32(ddlPageCount.SelectedValue);
            }

            dpgGrdCatalogue.SetPageProperties(dpgGrdCatalogue.StartRowIndex, dpgGrdCatalogue.MaximumRows, true);
            dpgLstCatalogue.SetPageProperties(dpgGrdCatalogue.StartRowIndex, dpgGrdCatalogue.MaximumRows, true);
            dpgGrdCatalogue2.SetPageProperties(dpgGrdCatalogue.StartRowIndex, dpgGrdCatalogue.MaximumRows, true);
            dpgLstCatalogue2.SetPageProperties(dpgGrdCatalogue.StartRowIndex, dpgGrdCatalogue.MaximumRows, true);
            BindGrids();
        }

        // Opens Bookmarks panel
        protected void BtnShowBmk_Click(object sender, EventArgs e)
        {
            IsBmkTab = true;
            sidepanelarea.Style.Add("display", "block");
            BindSidePanel();
            btnShowBmk.CssClass = "active";
            btnShowRecc.CssClass = "";
            if (bookmarkPanel.Visible == false)
            {
                bookmarkPanel.Visible = true;
            }
        }

        // Opens recommendation panel
        protected void BtnShowRecc_Click(object sender, EventArgs e)
        {
            IsBmkTab = false;
            sidepanelarea.Style.Add("display", "block");
            BindSidePanel();
            btnShowBmk.CssClass = "";
            btnShowRecc.CssClass = "active";
            if (bookmarkPanel.Visible == false)
            {
                bookmarkPanel.Visible = true;
            }
        }

        // Makes Bookmarks Panel Visible/Hidden
        protected void BtnOpenBmk_Click(object sender, EventArgs e)
        {
            sidepanelarea.Style.Add("display", "block");
            if (bookmarkPanel.Visible == true)
            {
                bookmarkPanel.Visible = false;
            }
            else
            {
                bookmarkPanel.Visible = true;
                BindSidePanel();
            }
        }
    }
}