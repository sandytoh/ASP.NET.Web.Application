<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RequestHistory.aspx.cs" Inherits="Group8_AD_webapp.RequestHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
            <link href="../css/employee-style.css" rel="stylesheet" />
            <link href="../css/datepicker3.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div id="main"> <!-- col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 -->
         <div class="form-group form-inline formstyle2 text-center">
        <div class="col-lg-3">
        <span class="subtitletext mt-5 ml-5"><asp:Label ID="lblCatTitle" runat="server" Text="Request History"></asp:Label></span>
        </div>
        <div class="col-xs-12 col-lg-2">
        <asp:DropDownList ID="ddlStatus" CssClass="ddlStatus form-control" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="DdlStatus_SelectedIndexChanged" AutoPostBack="True">
            <asp:listitem text="All" value="All" />
        </asp:DropDownList>
         </div>
        <div class="col-xs-12 col-lg-2">
            <div class="form-group">
                <div class="input-group">
                    <asp:TextBox ID="txtStartDate" ClientIDMode="Static" autocomplete="off" placeholder="from: dd/mm/yyyy" runat="server" CssClass="form-control controlheight"></asp:TextBox>
                    <span class="input-group-addon controlheight"><i class="fa fa-calendar" aria-hidden="true"></i></span>
                </div> 
            </div> 
        </div>
        <div class="col-xs-12 col-lg-3">
            <div class="form-group"> 
                <div class="input-group">
                    <asp:TextBox ID="txtEndDate" ClientIDMode="Static" autocomplete="off" placeholder="to: dd/mm/yyyy" runat="server" CssClass="form-control controlheight"></asp:TextBox>
                    <span class="input-group-addon controlheight"><i class="fa fa-calendar" aria-hidden="true"></i></span>

                </div><asp:Button ID="btnSearch" runat="server" CssClass="btnSearch btn btn-add button" Text="Search" OnClick="BtnSearch_Click" />

            </div>
        </div>
        </div>



        
        <div id="centermain2">
            <div class="row">
            <div class="col-xs-12">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
               <div ID="divAlert" class="alert alert-success alert-dismissible" role="alert" runat="server">
                  <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                  <strong><asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label></strong>
                </div>
                        <asp:ListView runat="server" ID="lstRequests">
                            <LayoutTemplate>
                                <table runat="server" class="table">
                                    <thead>
                                        <tr id="thdReqHistory" runat="server">
                                            <th scope="col">Submitted Date</th>
                                            <th scope="col">Status</th>
                                            <th scope="col"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server"></tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><asp:Label runat="server" ID="lblReqDate" Text='<%# Eval("ReqDateTime","{0:dd-MMM-yyyy}") %>' /></td>
                                    <td><asp:Label runat="server" ID="lblStatus" Text='<%# Eval("Status") %>' /></td>
                                    <td><asp:LinkButton ID="btnReqDetail" CssClass="btn btn-primary" href='<%# "RequestList.aspx?reqid="+Eval("ReqId") %>' runat="server">Details</asp:LinkButton> </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <span class="noresult showsearch">Sorry! No requests found within those search parameters!</span>
                                <!-- Add Back Button here -->
                            </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>


        </div>
    </div>
        </div>

</asp:Content>
<asp:Content ID="cphPageScript" ContentPlaceHolderID="cphScript" runat="server">
        <script src="<%=ResolveClientUrl("~/js/requesthistory-script.js")%>"></script>
</asp:Content>
