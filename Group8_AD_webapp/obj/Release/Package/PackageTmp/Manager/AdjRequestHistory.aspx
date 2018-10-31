<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdjRequestHistory.aspx.cs" Inherits="Group8_AD_webapp.Manager.AdjRequestHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="../css/employee-style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div id="main">
                <!-- col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 -->
                <div class="form-group form-inline formstyle2 text-center">
                    <div class="col-lg-8">
                        <span class="subtitletext mt-5 ml-5">
                            <asp:Label ID="lblCatTitle" runat="server" Text="Stock Adjustment Request"></asp:Label></span>
                    </div>
                    <div class="col-xs-12 col-lg-2">
                        <asp:DropDownList ID="ddlStatus" CssClass="ddlStatus form-control" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="All" Value="All" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="centermain2">
                    <div class="row">
                        <div class="col-xs-12">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:ListView runat="server" ID="lstRequests" OnItemCommand="lstRequests_ItemCommand">
                                        <LayoutTemplate>

                                            <table id="req-table" class="table display dataTable">
                                                <thead>
                                                    <tr id="thdReqHistory" runat="server">

                                                        <th scope="col">Submitted Date</th>
                                                        <th scope="col">Voucher No.</th>
                                                        <th scope="col">Status</th>
                                                        <th scope="col"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                                    <%--<tr id="itemPlaceholder" runat="server"></tr>--%>
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" EnableViewState="false" ID="lblReqDate" Text='<%# Eval("DateTimeIssued","{0:dd-MMM-yyyy}") %>' /></td>
                                                <td>
                                                    <asp:Label runat="server" EnableViewState="false" ID="lblVoucher" Text='<%# Eval("VoucherNo") %>' /></td>
                                                <td>
                                                    <asp:Label runat="server" EnableViewState="false" ID="lblStatus" Text='<%# Eval("Status") %>' /></td>
                                                <td>
                                                    <asp:LinkButton ID="btnReqDetail" CssClass="btn btn-primary" CommandName="Detail" CommandArgument='<%#Eval("VoucherNo")%>' runat="server">Details</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <span class="noresult showsearch">Sorry! No requests found!</span>
                                            <!-- Add Back Button here -->
                                        </EmptyDataTemplate>
                                    </asp:ListView>

                                    </div>
                            
                        </div>
                          
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:DataPager ID="DataPagerProducts" runat="server" PagedControlID="lstRequests"
                                        PageSize="9" OnPreRender="DataPagerProducts_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" PreviousPageText="Prev" ButtonCssClass="pagingbutton" />
                                            <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="pagingbutton" ButtonType="Button" CurrentPageLabelCssClass="currentpg" PreviousPageText="..." NextPreviousButtonCssClass="pagingbutton" />
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" ButtonCssClass="pagingbutton" />
                                        </Fields>
                                    </asp:DataPager>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <%-- modal content--%>
            <div id="myModal" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                <div class="modal-dialog fix-modal modal-lg">
                    <div class="modal-content">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true" style="font-size: 32px;"><strong>&times;</strong></span>
                                </button>
                                <h3 class="detail-subtitle">Stock Adjustment Request Details</h3>
                            </div>

                            <div class="panel-body lightbox-scroll">

                                <div class="detail-info">

                                    <div class="detail-info-left">
                                        <table class="detail-info-col">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="label1" runat="server" Text="Status : "></asp:Label>
                                                        <asp:Label ID="lblstatus" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="label7" runat="server" Text="Voucher No. : "></asp:Label>
                                                        <asp:Label ID="lblvnum" runat="server"></asp:Label></td>
                                                </tr>

                                            </tbody>

                                        </table>


                                    </div>

                                </div>
                                <div class="detail-item">

                                    <asp:ListView runat="server" ID="lstShow">

                                        <LayoutTemplate>
                                            <table runat="server" class="table table-detail">
                                                <thead>
                                                    <tr>
                                                        <th scope="col" style="display: none">Item Code</th>
                                                        <th scope="col">Item Description</th>
                                                        <th scope="col">Discrepancy</th>
                                                        <th scope="col">Value</th>
                                                        <th scope="col">Reason</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="display: none">
                                                    <asp:Label ID="Label4" EnableViewState="false" runat="server" Text='<%# Eval("ItemCode") %>' /></td>
                                                <td>
                                                    <asp:Label ID="Label5" EnableViewState="false" runat="server" Text='<%# Eval("Desc") %>' /></td>

                                                <td>
                                                    <asp:Label ID="Label3" runat="server" EnableViewState="false" Text='<%# Eval("QtyChange") %>' /></td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" EnableViewState="false" Text='<%# Eval("Value","{0:C}") %>' /></td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" EnableViewState="false" Text='<%# Eval("Reason") %>' /></td>

                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <span class="noresult">Sorry! There are no items to show.<br />
                                            </span>
                                        </EmptyDataTemplate>
                                    </asp:ListView>

                                </div>

                                <div>
                                    <div class="align-bottom-left">
                                        <p>Comments (Optional)</p>

                                        <asp:TextBox ID="txtComments" TextMode="multiline" Columns="50" Rows="5" runat="server" class="txt-area" />
                                    </div>
                                    <div class="action-btn">

                                        <asp:Button ID="btnReject" class="btn btn-danger btn-msize" OnClick="btnReject_Click" runat="server" Text="Reject" />
                                        <asp:Button ID="btnAccept" class="btn btn-success btn-msize" OnClick="btnAccept_Click" runat="server" Text="Accept" />
                                    </div>


                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="mdlConfirm" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <button type="button" id="btncClose" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true" style="font-size: 3.2rem"><strong>&times;</strong></span></button>
                        <h3 class="detail-subtitle">Stock Adjustment Approval</h3>
                    </div>
                    <div class="panel-body">
                        You are about to approve adjustment request.<br />
                        Are you sure?
            
                
                <div class="action-btn">
                    <asp:Button ID="btnCancel" class="btn btn-danger btn-msize" OnClick="btnNo_Click" runat="server" Text="No" />

                    <asp:Button ID="btnConfirm" class="btn btn-success btn-msize" OnClick="btnConfirm_Click" runat="server" Text="Yes" />

                </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="mdlRejConfirm" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true" style="font-size: 3.2rem"><strong>&times;</strong></span></button>
                        <h3 class="detail-subtitle">Stock Adjustment Rejection</h3>
                    </div>
                    <div class="panel-body">
                       You are about to reject adjustment request.<br />
                        Are you sure?
            
                
                <div class="action-btn">
                    <asp:Button ID="Button2" class="btn btn-danger btn-msize" OnClick="btnRejNo_Click" runat="server" Text="No" />

                    <asp:Button ID="Button1" class="btn btn-success btn-msize" OnClick="btnRejConfirm_Click" runat="server" Text="Yes" />

                </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
