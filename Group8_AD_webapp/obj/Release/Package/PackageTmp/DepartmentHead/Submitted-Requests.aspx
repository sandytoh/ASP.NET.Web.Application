<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Submitted-Requests.aspx.cs" Inherits="Group8_AD_webapp.Submitted_Requests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main">
        <div class="row">
            <ol class="breadcrumb">
                <li><a href="#">
                    <em class="fa fa-home"></em>
                </a></li>
                <li class="active"></li>
            </ol>
        </div>
        <!--/.row-->
        <div class="row">
            <div class="col-lg-12">
                <h3 class="page-header">Submitted Requests</h3>
            </div>
            <div class="col-lg-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="panel-body">
                            <asp:ListView runat="server" ID="lstOrder" OnItemCommand="lstOrder_ItemCommand">
                                <LayoutTemplate>
                                    <table runat="server" class="table">

                                        <thead>
                                            <tr id="Tr1" runat="server">
                                                <th scope="col" style="display: none;">Request ID</th>
                                                <th scope="col">Name</th>
                                                <th scope="col">Submitted Date</th>
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
                                        <td style="display: none;">
                                            <asp:Label runat="server" ID="lblReqId" Text='<%# Eval("ReqId") %>' /></td>
                                        <td>
                                            <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("EmpName") %>' /></td>
                                        <td>
                                            <asp:Label runat="server" ID="Label5" Text='<%# Eval("ReqDateTime","{0:dd-MMM-yyyy}") %>' />
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnReqDetail" CommandName="ReqDetail" class="btn btn-primary" runat="server" CommandArgument='<%#Eval("ReqId")%>'>Details</asp:LinkButton>
                                            <%-- <asp:Button ID="btnReqDetail"  CommandName="ReqDetail" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ReqId") %>'   Text="Details" />--%>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <span class="noresult">There is nothing in the list.</span>
                                </EmptyDataTemplate>
                            </asp:ListView>

                        </div>
                        <asp:DataPager ID="DataPagerProducts" runat="server" PagedControlID="lstOrder"
                            PageSize="9" OnPreRender="DataPagerProducts_PreRender">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" PreviousPageText="Prev" ButtonCssClass="pagingbutton" />
                                <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="pagingbutton" ButtonType="Button" CurrentPageLabelCssClass="currentpg" PreviousPageText="..." NextPreviousButtonCssClass="pagingbutton" />
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" ButtonCssClass="pagingbutton" />
                            </Fields>
                        </asp:DataPager>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%-- modal content--%>

    <div id="myModal" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <button type="button" id="btn_modal" runat="server" class="close" onserverclick="btn_modal_Click" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true" style="font-size: 32px;"><strong>&times;</strong></span>
                                </button>
                                <h3 class="detail-subtitle">Submitted Request Details</h3>
                            </div>

                            <div class="panel-body">

                                <div class="detail-info">

                                    <div class="detail-info-left">
                                        <table class="detail-info-col">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="label1" runat="server" Text="Employee Name : "></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblEmpName" runat="server"></asp:Label></td>
                                                    <td>
                                            </tbody>

                                        </table>


                                    </div>

                                    <div class="detail-info-right">

                                        <div>
                                            <table class="detail-info-col">
                                                <tbody>
                                                    <tr style="display: none;">
                                                        <td>
                                                            <asp:Label ID="label2" runat="server" Text="Request ID : "></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblReqid" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="label3" runat="server" Text="Submitted date : "></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblSubmitteddate" runat="server"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>


                                        </div>
                                    </div>
                                </div>
                                <div class="detail-item">

                                    <asp:ListView runat="server" ID="lstShow">

                                        <LayoutTemplate>
                                            <table runat="server" class="table table-detail">
                                                <thead>
                                                    <tr id="grdHeader" runat="server">
                                                        <th scope="col" style="display: none">Item Code</th>
                                                        <th scope="col">Item Name</th>
                                                        <th scope="col">Quantity</th>
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
                                                <td style="display: none">
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ItemCode") %>' /></td>
                                                <td>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%#String.Format("{0:C}",Eval("Desc"))%>' /></td>
                                                <td>
                                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ReqQty") %>' /></td>
                                                <td></td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <span class="noresult">Sorry! There are no items in your list.
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--   <asp:UpdatePanel runat="server"><ContentTemplate>--%>
    <div id="mdlreject" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg confirm-modal">

            <div class="modal-content">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true" style="font-size: 3.2rem"><strong>&times;</strong></span></button>
                        <h3 class="detail-subtitle">Request Rejection</h3>
                    </div>
                    <div class="panel-body">
                        You are about to reject the request for <span style="font-weight: bold;">
                            <asp:Label runat="server" ID="lblEmployeename"></asp:Label></span>. Are you sure?<br />
                        <div class="action-btn" style="text-align: center; float: none;">
                            <asp:Button ID="btnRemovDelNo" class="btn btn-danger btn-msize" OnClick="btnRejectNo_Click" runat="server" Text="No" />
                            <asp:Button ID="btnRemovDelYes" class="btn btn-success btn-msize" OnClick="btnRejectYes_Click" runat="server" Text="Yes" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div id="mdlaccept" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg confirm-modal">
            <div class="modal-content">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true" style="font-size: 3.2rem"><strong>&times;</strong></span></button>
                        <h3 class="detail-subtitle">Request Approval!</h3>
                    </div>
                    <div class="panel-body">

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                You are about to approve the request for <span style="font-weight: bold;">
                                    <asp:Label runat="server" ID="lblReqEmployeename"></asp:Label></span>. Are you sure?

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="action-btn" style="text-align: center; float: none;">
                            <asp:Button ID="Button2" class="btn btn-danger btn-msize" OnClick="btnAcceptNo_Click" runat="server" Text="No" />
                            <asp:Button ID="Button1" class="btn btn-success btn-msize" OnClick="btnAcceptYes_Click" runat="server" Text="Yes" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--</ContentTemplate></asp:UpdatePanel>--%>
    <script type="text/javascript">
        function rejectwarning() {
            swal({
                title: "Request Rejection",
                text: "You are about to reject the request for " + '<%= lblEmpName.Text.ToString() %>' + ". Are you sure?",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        swal("Removed", {
                            icon: "success",
                            js: $('#myModal').modal('toggle')
                        });
                    } else {
                        swal("Cancelled");
                    }
                });
        }

        function approvalalert() {
            swal({
                title: "Request Approval",
                text: "You are about to approve the request for " + '<%= lblEmpName.Text.ToString() %>' + ". Are you sure?",
                icon: "success",
                buttons: true,
                dangerMode: false,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        swal("Approved", {
                            icon: "success",
                            js: $('#myModal').modal('toggle')
                        });
                    } else {
                        swal("Cancelled");
                    }
                });
        }

    </script>

    <%--  <script>$(document).ready(function () {
            $('#s-table').DataTable();
        });
    </script>--%>
</asp:Content>
