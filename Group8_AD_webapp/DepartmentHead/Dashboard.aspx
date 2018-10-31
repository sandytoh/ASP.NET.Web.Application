<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Group8_AD_webapp.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .chartjs-render-monitor {
            width: 1000px !important;
            height: 450px !important;
        }

        @media (max-width: 768px) {
            .chartjs-render-monitor {
                width: 385px !important;
                height: 215px !important;
            }
        }
    </style>
    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
        <div class="row">
            <ol class="breadcrumb">
                <li><a href="#">
                    <em class="fa fa-home"></em>
                </a></li>
                <li class="active">Dashboard</li>
            </ol>
        </div>
        <!--/.row-->
        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header">Dashboard</h3>
            </div>
        </div>
        <!--/.row-->
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Charge-back
						<span class="pull-right clickable panel-toggle panel-button-tab-left"><em class="fa fa-toggle-up"></em></span>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">

                            <div class="panel panel-default">

                                <div class="col-md-2">
                                    <div class="form-group">


                                        <asp:DropDownList CssClass="form-control combo-a" ID="ddlMonth" runat="server" AutoPostBack="true"></asp:DropDownList>

                                        <br>
                                        <asp:RadioButtonList ID="rblChartType" runat="server" RepeatDirection="Horizontal" CellSpacing="20">
                                            <asp:ListItem Text="Bar" Value="1" Selected="True" />
                                            <asp:ListItem Text="Pie" Value="2" />
                                            <asp:ListItem Text="Doughnut" Value="3" />
                                        </asp:RadioButtonList>

                                    </div>
                                </div>

                            </div>
                            <!--/.panel-->
                        </div>
                        <div class="canvas-wrapper col-sm-offset-1">
                            <div id="dvChart">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--/.row-->

        <div class="row">
            <%--<div class="col-lg-12">
                <div class="panel-heading">
                    Settings
                </div>
            </div>--%>


            <div class="col-md-8">
                <div class="panel panel-primary">
                    <div class="panel-heading">Assign Delegate</div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblCurDelegate" runat="server" class="delegate-name" Text="Current Delegate :"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-3">

                                <div class="form-group">
                                    <div style="display: inline-block">
                                        <asp:TextBox CssClass="form-control" ID="txtCurDelegate" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <asp:UpdatePanel ID="updatepanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <button runat="server" id="btnRemoveDelegate" class="btn btn-danger btn-remove" onserverclick="RemoveDelegate">
                                                <i class="fa fa-times" aria-hidden="true"></i>
                                            </button>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                        </div>
                        <div class="box-2nd-child row">
                            <div class="col-lg-3">
                                <div class="form-group">

                                    <asp:DropDownList ID="ddlDelegate" runat="server" AppendDataBoundItems="True" CssClass="form-control combo-a">
                                        <asp:ListItem Value="0">Select Employee</asp:ListItem>

                                    </asp:DropDownList>
                                </div>

                                <asp:CompareValidator ID="delegate" runat="server" Display="Dynamic" ControlToValidate="ddlDelegate" ValueToCompare="0" ForeColor="red" Operator="NotEqual" ValidationGroup="addDel" ErrorMessage="Please Select Employee"></asp:CompareValidator>
                            </div>

                            <div class="col-lg-3">
                                <div class="form-group">

                                    <div class="input-group">

                                        <asp:TextBox ID="txtFromDate" ClientIDMode="Static" placeholder="dd/mm/yyyy" runat="server" CssClass="form-control" require="true"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar" aria-hidden="true"></i></span>

                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtFromDate" Display="Dynamic" ID="rftxtFromDate" runat="server" ForeColor="red" ValidationGroup="addDel" ErrorMessage="Please Select Date"></asp:RequiredFieldValidator>
                                <asp:UpdatePanel ID="updatepanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblDateCompare" runat="server" ForeColor="red" Text=""></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">

                                    <div class="input-group">

                                        <asp:TextBox ID="txtToDate" ClientIDMode="Static" placeholder="dd/mm/yyyy" runat="server" CssClass="form-control" require="true"></asp:TextBox>
                                        <span class="input-group-addon"><i class="fa fa-calendar" aria-hidden="true"></i></span>

                                    </div>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtToDate" Display="Dynamic" ID="rftxtToDate" runat="server" ForeColor="red" ValidationGroup="addDel" ErrorMessage="Please Select Date"></asp:RequiredFieldValidator>

                            </div>

                            <div class="col-lg-3">
                                <asp:UpdatePanel ID="updatepanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">

                                            <div class="input-group">


                                                <button runat="server" id="btnAddDelegate" causesvalidation="true" class="btn btn-success btn-remove" validationgroup="addDel" onserverclick="AddDelegate">
                                                    <i class="fa fa-check" aria-hidden="true"></i>
                                                </button>


                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="col-md-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">Assign Representative</div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" class="delegate-name" Text="Current Representative :"></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-6">

                                <asp:UpdatePanel ID="updatepanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group" style="display: inline">
                                            <div style="display: inline-block">
                                                <asp:TextBox CssClass="form-control" ID="txtRep" runat="server" Text="" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                            </div>
                        </div>
                        <div class="box-2nd-child row">
                            <div class="col-lg-6" style="margin-top: 14px;">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlRep" runat="server" AppendDataBoundItems="True" CssClass="form-control combo-a">
                                        <asp:ListItem Text="Select Employee" Value="0" />
                                    </asp:DropDownList>
                                </div>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ControlToValidate="ddlRep" ValueToCompare="0" ForeColor="red" Operator="NotEqual" ValidationGroup="addRep" ErrorMessage="Please Select Employee"></asp:CompareValidator>
                            </div>
                            <div class="col-lg-6" style="margin-top: 14px;">
                                <div class="form-group">
                                    <asp:UpdatePanel ID="updatepanel3" runat="server">
                                        <ContentTemplate>
                                            <button runat="server" causesvalidation="true" validationgroup="addRep" id="btnAddRep" class="btn btn-success btn-remove" onserverclick="AddRep">
                                                <i class="fa fa-check" aria-hidden="true"></i>
                                            </button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div id="mdlDeleRemove" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg confirm-modal">
            <div class="modal-content">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true" style="font-size: 3.2rem"><strong>&times;</strong></span></button>
                        <h3 class="detail-subtitle">Delegate Removal!</h3>
                    </div>
                    <div class="panel-body">
                        You are about to remove <span style="font-weight: bold;">
                            <asp:Label runat="server" ID="lblCurrentDel"></asp:Label></span> from being your delegate. Are you sure?<br />
                        <div class="action-btn" style="text-align: center; float: none;">

                            <asp:Button ID="btnRemovDelNo" class="btn btn-danger btn-msize" OnClick="btnRemovDelNo_Click" runat="server" Text="No" />
                            <asp:Button ID="btnRemovDelYes" class="btn btn-success btn-msize" OnClick="btnRemovDelYes_Click" runat="server" Text="Yes" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="mdlDeleSet" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg confirm-modal">
            <div class="modal-content">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true" style="font-size: 3.2rem"><strong>&times;</strong></span></button>
                        <h3 class="detail-subtitle">Delegate Addition!</h3>
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                You are about to add <span style="font-weight: bold;">
                                    <asp:Label runat="server" ID="lblSelectedDel"></asp:Label></span> to your delegate. Are you sure?
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <div class="action-btn" style="text-align: center; float: none;">

                            <asp:Button ID="Button2" class="btn btn-danger btn-msize" OnClick="btnSetDelNo_Click" runat="server" Text="No" />
                            <asp:Button ID="Button1" class="btn btn-success btn-msize" OnClick="btnSetDelYes_Click" runat="server" Text="Yes" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="mdlRepSet" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg confirm-modal">
            <div class="modal-content">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true" style="font-size: 3.2rem"><strong>&times;</strong></span></button>
                        <h3 class="detail-subtitle">Representative Addition!</h3>
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                You are about to add <span style="font-weight: bold;">
                                    <asp:Label runat="server" ID="lblSelectedRep"></asp:Label></span> to your representative. Are you sure?
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <div class="action-btn" style="text-align: center; float: none;">

                            <asp:Button ID="Button4" class="btn btn-danger btn-msize" OnClick="btnSetRepNo_Click" runat="server" Text="No" />
                            <asp:Button ID="Button3" class="btn btn-success btn-msize" OnClick="btnSetRepYes_Click" runat="server" Text="Yes" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- /.row -->


</asp:Content>

<asp:Content ID="cphPageScript" ContentPlaceHolderID="cphScript" runat="server">
    <script src="<%=ResolveClientUrl("~/js/depthead-script.js")%>"></script>
</asp:Content>
