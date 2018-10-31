<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditSupplierPrice.aspx.cs" Inherits="Group8_AD_webapp.EditSupplierPrice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="../css/employee-style.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main">
            <asp:UpdatePanel ID="udpSupplier" runat="server"><ContentTemplate>
    <div class="form-group form-inline formstyle m-2 text-center col-12">
        <div class="row">
        <div class="col-md-3"><span class="subtitletext">Update Suppliers </span></div>
        <div class="col-md-8">
        <asp:DropDownList ID="ddlCategory" CssClass="ddlsearch form-control mx-2 bb" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="DdlCategory_SelectedIndexChanged" AutoPostBack="True">
            <asp:listitem text="All" value="All" />
        </asp:DropDownList>
        <asp:TextBox ID="txtSearch" CssClass="txtSearch form-control mx-2 controlheight bb" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" CssClass="btnSearch btn btn-primary button" Text="Search" OnClick="BtnSearch_Click" />
     </div></div></div>

        <div id="centermain">

                <div class="mobilespacer"></div>        
                <asp:Button ID="btnClear" CssClass="btn btn-warning pad-left10" style="color:#000; font-weight:700;" runat="server" Text="Clear Suppliers/Prices" OnClick="BtnClear_Click" />
          <asp:Label ID="lblPageCount" runat="server" Text="Label"></asp:Label>
    <asp:GridView ID="grdSupplier" runat="server" CssClass="table" PagerStyle-CssClass="pager" OnRowDataBound="GridView_RowDataBound"
        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GrdSupplier_PageIndexChanging" PageSize="20"> 
                   <Columns>
                <asp:TemplateField HeaderText="Item Code" SortExpression="ItemCode">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Desc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supplier1" SortExpression="Supplier1">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlSupplier1" runat="server"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price1 (SGD)" SortExpression="Price1">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPrice1" ClientIDMode="Static" runat="server" Text='<%# String.Format("{0:0.00}", Eval("Price1")) %>' Width="60px" TextMode="Number" min="0" step="0.01"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supplier2" SortExpression="Supplier2">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlSupplier2" runat="server"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price2 (SGD)" SortExpression="Price2">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPrice2" ClientIDMode="Static" runat="server" Text='<%# String.Format("{0:0.00}", Eval("Price2")) %>' Width="60px" TextMode="Number" min="0" step="0.01"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supplier3" SortExpression="Supplier3">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlSupplier3" runat="server"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price3 (SGD)" SortExpression="Price3">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPrice3" ClientIDMode="Static"  runat="server" Text='<%# String.Format("{0:0.00}", Eval("Price3")) %>' Width="60px" TextMode="Number" min="0" step="0.01"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        <EmptyDataTemplate>
            Sorry! There are no results that match your search criteria.
        </EmptyDataTemplate>
    </asp:GridView>

     <div class="row">
        <div class="col-xs-3 backarea">
            <a class="btn btn-back" href="~/Manager/StoreDashboard.aspx" runat="server">Dashboard</a>
        </div>
    <div class="col-xs-9  buttonarea">
        <asp:Button ID="btnSubmit" Cssclass="btn btn-success" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
    </div></div>
    </div>
</ContentTemplate></asp:UpdatePanel>

            <!-- modal content-->
    <div id="mdlConfirm" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-content4">
        <div class="modal-content modal-content4">
        <div class="panel panel-default">
        <div class="panel-heading"><button type="button" ID="btnClose" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true" style="font-size: 3.2rem"><strong>&times;</strong></span></button>
            <h3 class="detail-subtitle">Please Confirm Submitted Details</h3></div>
            <div class="panel-body">
            <asp:UpdatePanel ID="udpConfirmModal" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnConfirm" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
            </Triggers>
            <ContentTemplate>
                <div id="submitDetail" runat="server" class="detail-item detail-item4"><asp:ListView runat="server" ID="lstConfirm">
                    <LayoutTemplate>
                        <table runat="server" class="table table-detail">
                        <thead><tr id="grdHeader" runat="server">
                                    <th scope="col">Item Code</th>
                                    <th scope="col">Item Description</th>
                                    <th scope="col">Supplier 1</th>
                                    <th scope="col">Price 1</th>
                                    <th scope="col">Supplier 2</th>
                                    <th scope="col">Price 2</th>
                                    <th scope="col">Supplier 3</th>
                                    <th scope="col">Price 3</th>
                        </tr></thead>
                        <tbody><tr id="itemPlaceholder" runat="server"></tr></tbody>
                    </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode") %>' /></td>
                            <td><asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Desc") %>' /></td>
                            <td><asp:Label ID="lblSupp1" runat="server" Text='<%# Eval("SuppCode1") %>' /></td>
                            <td><asp:Label ID="lblPrice1" runat="server" Text='<%# String.Format("{0:0.00}", Eval("Price1")) %>' /></td>
                            <td><asp:Label ID="lblSupp2" runat="server" Text='<%# Eval("SuppCode2") %>' /></td>
                            <td><asp:Label ID="lblPrice2" runat="server" Text='<%# String.Format("{0:0.00}", Eval("Price2")) %>' /></td>
                            <td><asp:Label ID="lblSupp3" runat="server" Text='<%# Eval("SuppCode3") %>' /></td>
                            <td><asp:Label ID="lblPrice3" runat="server" Text='<%# String.Format("{0:0.00}", Eval("Price3")) %>' /></td></tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        There are no items that match your search criteria!
                    </EmptyDataTemplate>
                 </asp:ListView></div>
                <asp:Label ID="lblEmptyChange" runat="server" Text="Label"></asp:Label>

                
                <div class="action-btn action-btn2">
                    <!-- <asp:Button ID="btnFinalCancel" class="btn btn-danger btn-msize" runat="server" Text="Cancel" /> -->
                    <asp:Button ID="btnConfirm" class="btn btn-success btn-msize" OnClick="BtnConfirm_Click" runat="server" Text="Confirm" />
                  </div>
              </div>
                </ContentTemplate>
              </asp:UpdatePanel>
       </div></div></div>
    </div>


        <!-- modal content-->
    <div id="mdlClear" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog2 modal-lg">
        <div class="modal-content modal-content3">
        <div class="panel panel-default">
        <div class="panel-heading"><button type="button" ID="btnClose2" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true" style="font-size: 3.2rem"><strong>&times;</strong></span></button>
            
            <div class="panel-body panel-body2">
                <h4 class="detail-subtitle">Please click Submit if you wish to save your changes!<br /><br />
                    This will reset all data on this page (including already entered data)<br /><br /> 
                     Confirm if you wish to proceed.</h4></div>

                <div class="action-btn action-btn2">
                     <asp:Button ID="btnConfirmClear" class="btn btn-danger btn-msize" OnClick="BtnConfirmClear_Click" runat="server" Text="Confirm" /> 
                </div>

              </div>
       </div></div></div></div>
    </div></div>


</asp:Content>
<asp:Content ID="cphPageScript" ContentPlaceHolderID="cphScript" runat="server">
   <script type="text/javascript">

        function openClearModal() {
            $('#mdlClear').modal('show');
             }
         function openModal() {
            $('#mdlConfirm').modal('show');
       }

    </script>
</asp:Content>