<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProductVolume.aspx.cs" Inherits="Group8_AD_webapp.ProductVolume" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
            <link href="../css/manager-style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main">
        <asp:UpdatePanel ID="udpProductVol" runat="server"><ContentTemplate>
        <div class="form-group form-inline formstyle2 text-center row">
        <div class="col-lg-3">
        <span class="subtitletext mt-5 ml-5"><asp:Label ID="lblCatTitle" runat="server" Text="Product Ranking"></asp:Label></span>
        </div>
        <div class="col-xs-12 col-lg-2">
        <asp:DropDownList ID="ddlCategory" CssClass="ddlsearch form-control bb" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="DdlCategory_SelectedIndexChanged" AutoPostBack="True">
            <asp:listitem text="All" value="All" />
        </asp:DropDownList>
         </div>
        <div class="col-xs-12 col-lg-2">
            <div class="form-group">
                <div class="input-group">
                    <asp:TextBox ID="txtStartDate" ClientIDMode="Static" placeholder="from: day-month-year" autocomplete="off" runat="server" CssClass="form-control controlheight bb"></asp:TextBox>
                    <span class="input-group-addon controlheight bb"><i class="fa fa-calendar" aria-hidden="true"></i></span>
                </div> 
            </div> 
        </div>
        <div class="col-xs-12 col-lg-4 text-left">
            <div class="form-group"> 
                <div class="input-group">
                    <asp:TextBox ID="txtEndDate" ClientIDMode="Static" placeholder="to: day-month-year" autocomplete="off" runat="server" CssClass="form-control controlheight bb"></asp:TextBox>
                    <span class="input-group-addon controlheight bb"><i class="fa fa-calendar" aria-hidden="true"></i></span>

                </div><asp:Button ID="btnSearch" runat="server" CssClass="btnSearch btn btn-add button controlheight" Text="Search" OnClick="BtnSearch_Click" />

            </div>
        </div>
        </div>

        <div id="centermain2">
            <asp:hiddenfield id="IsDesc" ClientIDMode="Static" runat="server"/>
            <div class=" form-inline"> Sort Direction: 
           <asp:DropDownList ID="ddlSortDirection" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlSortDirection_SelectedIndexChanged">
                           <asp:listitem text="Ascending" value="asc" />
                           <asp:listitem text="Descending" value="desc" />
           </asp:DropDownList>
            <asp:Label ID="lblDateRange" class="ml-10 bold" runat="server" Text=""></asp:Label></div>
    <asp:GridView ID="lstProductVolume" ClientIDMode="Static" CssClass="display" runat="server" AutoGenerateColumns="False">
                   <Columns>
                       <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode" />
                       <asp:BoundField DataField="Desc" HeaderText="Description" SortExpression="Desc" />
                       <asp:BoundField DataField="TempQtyReq" HeaderText="Quantity" SortExpression="TempQtyReq" />
                       <asp:BoundField DataField="SuppCode1" HeaderText="Supplier 1" SortExpression="SuppCode1" />
                       <asp:TemplateField><HeaderTemplate>Price (SGD)</HeaderTemplate>
                           <ItemTemplate><asp:Label runat="server" Text='<%# String.Format("{0:C}", (Double)Eval("Price1")) %>'/></ItemTemplate></asp:TemplateField>
            </Columns>
    </asp:GridView>
     <div class="row">
        <div class="col-xs-3 backarea">
            <asp:Button ID="btnReqList" Cssclass="btn btn-back" OnClick="BtnBack_Click" runat="server" Text="Back"  />
        </div></div>
    </div>
</ContentTemplate></asp:UpdatePanel></div>

</asp:Content>
<asp:Content ID="cphPageScript" ContentPlaceHolderID="cphScript" runat="server">
        <script src="<%=ResolveClientUrl("~/js/monthpicker.js")%>"></script>
        <script src="<%=ResolveClientUrl("~/js/productvolume-script.js")%>"></script>
</asp:Content>