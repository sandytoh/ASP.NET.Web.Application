<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CatalogueDash.aspx.cs" Inherits="Group8_AD_webapp.CatalogueDash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
     <link href="../css/employee-style.css" rel="stylesheet" />
    <link href="../css/add-style.css" rel="stylesheet" />

    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div id="main">
         <div class="form-group form-inline formstyle m-2 text-center">
        <span class="titletext mt-5 ml-5"><asp:Label ID="lblCatTitle" runat="server" Text="Label"></asp:Label></span>

        <asp:LinkButton ID="btnGrid" Cssclass="listbutton btnGrid active" runat="server" Text="Button" OnClick="BtnGrid_Click"><i class="fa fa-th-large"></i></asp:LinkButton>
        <asp:LinkButton ID="btnList" Cssclass="listbutton btnList" runat="server" Text="Button" OnClick="BtnList_Click"><i class="fa fa-list"></i></asp:LinkButton>

        <asp:DropDownList ID="ddlCategory" CssClass="ddlSearch form-control controlheight bb" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="DdlCategory_SelectedIndexChanged" AutoPostBack="true">
            <asp:listitem text="All" value="All" />
        </asp:DropDownList>
             <div class="dd-search">
        <asp:TextBox ID="txtSearch" CssClass="txtSearch form-control controlheight bb" runat="server" OnTextChanged="TxtSearch_Changed" AutoPostBack ="True"></asp:TextBox>

         <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtSearch" />
        </Triggers>
        <ContentTemplate>
            <div ID="ddlsearchcontent" class="ddlsearchcontent showsearch" runat="server">
                 <ul class="dd-searchcontent dropdown-alerts showsearch" runat="server">
            <asp:ListView ID="lstSearch" runat="server" OnPagePropertiesChanged="LstSearch_PagePropertiesChanged">
            <ItemTemplate>
                <li class="showsearch"><a runat="server" href="#">
                <table>
                <tr>
                    <td style="display:none;"><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>'/></td>
                    <td style="width:80px;" rowspan="2" class="showsearch"><img src="../img/stationery/<%# Eval("ItemCode") %>.jpg" width="80" class="img-responsive"></td>
                    <td class="sidedesc searchdesc showsearch"><asp:Label ID="lblDescription" runat="server" Text='<%#String.Format("{0:C}",Eval("Desc"))%>' /></td>
                </tr>
                 <tr>
                    <td colspan="3" class="bmkright showsearch"> <asp:TextBox ID="spnQty" type="number" Cssclass="vertalign movedownside showsearch" runat="server" min="1"  Value="1" Width="60px" />
                   <asp:Button ID="btnAdd" CssClass="btn-add-list vertalign btn ml-10 showsearch" runat="server" Text="ADD" OnClick="BtnAdd_Click"/></td>
                </tr>
                </table>
                </a></li>
            </ItemTemplate>
            <EmptyDataTemplate>
                <li><span class="noresult showsearch">No suggestions available!</span></li>
                <!-- Add Back Button here -->
            </EmptyDataTemplate>
        </asp:ListView>
            <li class="showsearch" style="text-align:right;">
                <a href="RequestList.aspx" class="btn btn-gotocart" OnClick="LstSearchbtnAdd_Click" runat="server"><i class="fa fa-shopping-cart"></i>&nbsp; CART</a>
                <asp:LinkButton ID="btnSearch2" CssClass="btn btn-add ml-10 " OnClick="BtnSearch_Click" runat="server" AutoPostBack="true"><i class="fa fa-search-plus"></i> SEE MORE</asp:LinkButton>
            </li>
                 </ul></div>
        </ContentTemplate></asp:UpdatePanel></div>
            
        <asp:Button ID="btnSearch" runat="server" CssClass="btnSearch btn btn-add button" Text="Search" OnClick="BtnSearch_Click" />
             <a ID="btnClean" class="btnClean btn btn-primary"><i class="fa fa-expand"></i></a><asp:hiddenfield id="IsClean" ClientIDMode="Static" runat="server"/>
        </div>




        <div id="centermain">

        <asp:UpdatePanel ID="udpCatalogue" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnGrid" />
            <asp:AsyncPostBackTrigger ControlID="btnList" />
             <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch2" />
            <asp:AsyncPostBackTrigger ControlID="ddlCategory" />
        </Triggers>
        <ContentTemplate>
<%--            <script type="text/javascript">
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(toastr_message);
            </script>--%>
            <span class="pad-left10">Items Per Page: </span><asp:DropDownList ID="ddlPageCount" runat="server" OnSelectedIndexChanged="DdlPageCount_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            <asp:Label ID="lblPageCount" CssClass="lblPage" runat="server" Text="Label"></asp:Label>

    <div class="row">
    <div class="col-xs-12 col-lg-8">
    <div id="showgrid" class="showgrid" runat="server">
    <div class="dpager col-12"><br />
    <asp:DataPager ID="dpgGrdCatalogue" runat="server" PageSize="9" PagedControlID="grdCatalogue">
         <Fields>
            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="true"   ShowLastPageButton="false" ShowNextPageButton="false" PreviousPageText="Prev" ButtonCssClass="pagingbutton" />
            <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="pagingbutton" ButtonType="Button" CurrentPageLabelCssClass="currentpg" PreviousPageText="..." NextPreviousButtonCssClass="pagingbutton" />
            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" ButtonCssClass="pagingbutton" />
      </Fields>
    </asp:DataPager></div>

     <!-- Listview -->
       <asp:ListView ID="grdCatalogue" runat="server" OnPagePropertiesChanging="LstCatalogue_PagePropertiesChanging">
        <ItemTemplate>

          <div class="col-xs-12 col-sm-6 col-md-4">
         <table class="product-wrapper2" >
            <tr><td class="p-3"><div class="imagewrapper">
                <asp:LinkButton ID="btnBookmark" CssClass="btn-bookmark btn btn-warning" OnClick="BtnBookmark_Click" runat="server"><i class="fa fa-bookmark"></i> </asp:LinkButton>
                <img src="../img/stationery/<%# Eval("ItemCode") %>.jpg" class="img-responsive"></div>
                </td></tr>                
            <tr><td class="item-description smalldesc">
                <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' Visible="False" />
                <div class="" ><asp:Label ID="lblDescription" runat="server" CssClass="blank" Text='<%#Eval("Desc") %>'></asp:Label></div></td></tr>
            <tr><td class="form-inline lblQty movedown">
               <span class=""> Qty: 
                   <asp:TextBox ID="spnQty" type="number" Cssclass="form-control controlheight txtboxmove" width="80px" runat="server" min="1"  Value="1" /></span><br />
                 </td></tr>
            <tr><td class="p-1 m-auto">
                <asp:Button ID="btnAdd" CssClass="btn-add btn-add2  btn" runat="server" Text="ADD TO CART" OnClick="BtnAdd_Click"/>
                </td></tr></table>
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <span class="noresult">Sorry! There are no items matching your search.</span>
        </EmptyDataTemplate>
        </asp:ListView>


        <div class="dpager col-xs-12"><br />
        <asp:DataPager ID="dpgGrdCatalogue2" runat="server" PageSize="9" OnPagePropertiesChanging="LstCatalogue_PagePropertiesChanging" PagedControlID="grdCatalogue">
             <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="true"   ShowLastPageButton="false" ShowNextPageButton="false" PreviousPageText="Prev" ButtonCssClass="pagingbutton" />
                <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="pagingbutton" ButtonType="Button" CurrentPageLabelCssClass="currentpg" PreviousPageText="..." NextPreviousButtonCssClass="pagingbutton" />
                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" ButtonCssClass="pagingbutton" />
          </Fields>
        </asp:DataPager></div>
    </div>

        <div id="showlist" class="showlist" runat="server">
        <div class="dpager col-xs-12"><br />
        <asp:DataPager ID="dpgLstCatalogue" runat="server" PageSize="9" PagedControlID="lstCatalogue" OnPagePropertiesChanging="LstCatalogue_PagePropertiesChanging">
             <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="true"   ShowLastPageButton="false" ShowNextPageButton="false" PreviousPageText="Prev" ButtonCssClass="pagingbutton" />
                <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="pagingbutton" ButtonType="Button" CurrentPageLabelCssClass="currentpg" PreviousPageText="..." NextPreviousButtonCssClass="pagingbutton" />
                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" ButtonCssClass="pagingbutton" />
          </Fields>
        </asp:DataPager></div>

       <div class=" col-xs-12"> 
        <asp:ListView runat="server" ID="lstCatalogue" OnPagePropertiesChanging="LstCatalogue_PagePropertiesChanging">
        <LayoutTemplate>
            <table runat="server" class="table list-table">
                <thead><tr id="grdHeader" runat="server">
                        <th scope="col" style="display:none;">Item Code</th>
                        <th scope="col">Product Description</th>
                        <th scope="col">Quantity</th>
                        <th scope="col">Units</th>
                        <th scope="col"></th>
                </tr></thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td style="display:none;"><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>'/></td>
                <td><asp:Label ID="lblDescription" runat="server" Text='<%#String.Format("{0:C}",Eval("Desc"))%>' /></td>
                <td> <asp:TextBox ID="spnQty" type="number" Cssclass="vertalign controlheight txtconstraint" runat="server" min="1"  Value="1"/></td>
                <td><asp:Label ID="lbllstUOM" runat="server" Text='<%# Eval("UOM") %>'/></td>
                <td><asp:Button ID="btnAdd" CssClass="btn-add-list vertalign btn" runat="server" Text="ADD TO CART" OnClick="BtnAdd_Click"/></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <span class="noresult">Sorry! There are no items matching your search.</span>
        </EmptyDataTemplate>
        </asp:ListView>
           </div>

        <div class="dpager col-xs-12"><br />
        <asp:DataPager ID="dpgLstCatalogue2" runat="server" PageSize="9" OnPagePropertiesChanging="LstCatalogue_PagePropertiesChanging" PagedControlID="lstCatalogue">
             <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="true"   ShowLastPageButton="false" ShowNextPageButton="false" PreviousPageText="Prev" ButtonCssClass="pagingbutton" />
                <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="pagingbutton" ButtonType="Button" CurrentPageLabelCssClass="currentpg" PreviousPageText="..." NextPreviousButtonCssClass="pagingbutton" />
                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" ButtonCssClass="pagingbutton" />
          </Fields>
        </asp:DataPager></div>

        </div>
    </div>
    <div id="sidepanelarea" runat="server" class="sidepanelarea col-lg-4">
        <div class="bookmark-panel-top">
            <ul class="nav nav-tabss">
              <li><asp:LinkButton ID="btnShowBmk" CssClass="active" OnClick="BtnShowBmk_Click" AutoPostBack="true" runat="server">Bookmarks</asp:LinkButton></li>
              <li><asp:LinkButton ID="btnShowRecc" OnClick="BtnShowRecc_Click" runat="server" AutoPostBack="true">Recommended</asp:LinkButton></li>
              <li> <asp:LinkButton ID="btnOpenBmk" CssClass="openbmk" OnClick="BtnOpenBmk_Click" runat="server"><i class="fa fa-angle-double-down"></i></asp:LinkButton></li>
            </ul>
            </div>

          <asp:UpdatePanel ID="udpBookmarks" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnShowBmk" />
            <asp:AsyncPostBackTrigger ControlID="btnShowRecc" />
        </Triggers>
        <ContentTemplate>
                        <div id="bookmarkPanel" class="bookmark-panel" runat="server">
            <div class="tab-content panelcontent">
               <asp:ListView ID="lstBookmarks" runat="server">
                <ItemTemplate>
                <div class="bmkwrapper">
                <table>
                <tr>
                    <td style="display:none;"><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>'/></td>
                    <td rowspan="2" style="width:90px;"><img src="../img/stationery/<%# Eval("ItemCode") %>.jpg" width="92" class=""></td>
                    <td class="sidedesc"><asp:Label ID="lblDescription" runat="server" Text='<%#String.Format("{0:C}",Eval("Desc"))%>' /></td>
                </tr>
                 <tr>
                    <td colspan="3" class="bmkright"> <asp:TextBox ID="spnQty" type="number" Cssclass="vertalign movedownside" runat="server" min="1"  Value="1" Width="60px" />
                   <asp:Button ID="btnAdd" CssClass="btn-add-list vertalign btn" runat="server" Text="ADD" OnClick="BtnAdd_Click"/></td>
                </tr>
                </table>
                </div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div class="bmkwrapper">
                        <table>
                        <tr>
                            <td style="display:none;"><asp:Label ID="lblItemCode" runat="server" Text=''/></td>
                            <td rowspan="2" style="width:90px;"><img src="../images/0000.png" width="92" class=""></td>
                            <td class="emptysidedesc"><asp:Label ID="lblDescription" runat="server" Text='Your List is Empty.<br/> Add something!' /></td>
                        </tr>
                         <tr>
                        </tr>
                    </table>
                    </div>
                </EmptyDataTemplate>
                </asp:ListView>
                            </div>
        </div>
              </ContentTemplate>
            </asp:UpdatePanel>

    </div>

        </ContentTemplate>
        </asp:UpdatePanel>

    </div></div>
    <div id="clean" runat="server" class="clean"></div>


</asp:Content>
<asp:Content ID="cphPageScript" ContentPlaceHolderID="cphScript" runat="server">
        <script src="<%=ResolveClientUrl("~/js/catalogue-script.js")%>"></script>
</asp:Content>
