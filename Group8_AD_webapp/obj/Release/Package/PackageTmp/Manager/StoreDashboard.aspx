<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StoreDashboard.aspx.cs" Inherits="Group8_AD_webapp.StoreDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
        <link href="../css/manager-style.css" rel="stylesheet" />
        <link href="../css/monthpicker.css" rel="stylesheet" />
    <link href="https://code.jquery.com/ui/1.12.1/themes/ui-lightness/jquery-ui.css" rel="stylesheet" />
    <link href="../css/month-picker-style.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id ="main">
    <div id="centermain2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtMonthPick" />
            </Triggers>
            <ContentTemplate>
                 <div class="form-group form-inline formstyle2 text-center">
                     <div class="row">
                <div class="col-md-2 text-left">
                    <span class="subtitletext">Dashboard</span>
             </div>
                <div class="col-xs-12 col-md-4">
            <div class="form-group">                    <span class="smalltext ml-10">View For Month of:</span>
                <div class="input-group">
                    <asp:TextBox ID="txtMonthPick" ClientIDMode="Static" placeholder="Month - Year" autocomplete="off" runat="server" CssClass="form-control controlheight" AutoPostBack="true" OnTextChanged="txtMonthPick_TextChanged"></asp:TextBox>
                    <span class="input-group-addon controlheight"><i class="fa fa-calendar" aria-hidden="true"></i></span></div> </div>
                    </div>
                <div class="col-xs-12 col-md-4">
                       <span class="smalltext ml-10"><asp:Label ID="lblDateRange" runat="server" Text="Label"></asp:Label></span>
                </div>
             </div>
        </div>

                <div class="phtrend">
            <canvas id="myChart" class="chart" width="800" height="450"> </canvas>
        </div>
                
        
    <div class ="row">
    <div class="col-md-6 tablepad">
        <div class="listtitletext">Top 10 Products By Request Quantity
            <asp:Button ID="btnMore" CssClass="btn btn-primary btnbold" runat="server" Text="See More" OnClick="btnMore_Click" />
        </div>
        <asp:GridView ID="grdTopProducts" runat="server" CssClass="table table-striped" AutoGenerateColumns="False"> 
            <Columns>
        <asp:TemplateField HeaderText="Item Code" SortExpression="ItemCode" ItemStyle-Width="100px">
            <ItemTemplate>
                <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description" SortExpression="Description">
            <ItemTemplate>
                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Desc") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("TempQtyReq") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
            </Columns>
    </asp:GridView></div>
    <div class="col-md-6 tablepad">
        <div class="listtitletext">Bottom 10 Products By Request Quantity
                    <asp:Button ID="btnMore2" CssClass="btn btn-primary btnbold" runat="server" Text="See More" OnClick="btnMore2_Click" />
        </div>
        <asp:GridView ID="grdBotProducts" runat="server" CssClass="table table-striped" AutoGenerateColumns="False"> 
            <Columns>
        <asp:TemplateField HeaderText="Item Code" SortExpression="ItemCode" ItemStyle-Width="100px">
            <ItemTemplate>
                <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Description" SortExpression="Description">
            <ItemTemplate>
                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Desc") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("TempQtyReq") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
            </Columns>
    </asp:GridView></div>
    </div>
         </ContentTemplate>
        </asp:UpdatePanel>
    </div></div>

</asp:Content>
<asp:Content ID="cphPageScript" ContentPlaceHolderID="cphScript" runat="server">
        <script src="<%=ResolveClientUrl("~/js/monthpicker.js")%>"></script>
        <script src="<%=ResolveClientUrl("~/js/storedash-script.js")%>"></script>
</asp:Content>
