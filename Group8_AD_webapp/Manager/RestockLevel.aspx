<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RestockLevel.aspx.cs" Inherits="Group8_AD_webapp.Manager.RestockLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <link href="../css/employee-style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main">

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="subtitletext" style="margin-top: 8px;">Change Restock Level and Quantity</div>
                <div class="form-group form-inline m-2 text-center col-12">
                    <div class="sh-section-reorder">


                        <div style="display: inline-block;">
                            <div style="display: inline-block; display:none;">
                                <span class="lbl-inherit" style="vertical-align: text-bottom">Threshold :</span>
                                <asp:DropDownList ID="ddlThreshold" CssClass="form-control mx-2" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlThreshold_SelectedIndexChanged">
                                    <%--<asp:ListItem Text="0%" Value="0" />
                                    <asp:ListItem Text="5%" Value="0.05" />
                                    <asp:ListItem Text="10%" Value="0.1" />
                                    <asp:ListItem Text="15%" Value="0.15" />--%>
                                    <asp:ListItem Text="0%" Value="0.0" />
                                   <%-- <asp:ListItem Text="50%" Value="0.5" />
                                    <asp:ListItem Text="75%" Value="0.75" />
                                    <asp:ListItem Text="100%" Value="1" />--%>

                                </asp:DropDownList>
                            </div>
                            <span class="lbl-inherit" style="vertical-align: text-bottom">Category :</span>
                            <asp:DropDownList ID="ddlCategory" CssClass="form-control mx-2" runat="server" AppendDataBoundItems="True" AutoPostBack="false" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value="All" />
                            </asp:DropDownList>
                            <div style="display: inline-block;">
                                <asp:TextBox ID="txtSearch" CssClass="form-control controlheight bb" runat="server"></asp:TextBox>
                               
                            </div>
                            <div style="display: inline-block; vertical-align:top;">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btnSearch btn btn-primary button" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Label ID="lblPageCount" runat="server" Text="Label"></asp:Label>
                <asp:GridView ID="grdRestockItem" PagerStyle-CssClass="pager" AllowPaging="True" runat="server" OnPageIndexChanging="grdRestockItem_PageIndexChanging" PageSize="8" CssClass="table table-bordered" AutoGenerateColumns="False" OnRowCommand="grdRestockItem_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Product">
                            <ItemTemplate>
                                <table class="table borderless">
                                    <tr>
                                        <td style="text-align: left; padding: 0px;">
                                            <asp:Label CssClass="item-info" ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' Visible="True" />
                                            <span class="product-stock" style="float: right";>  
                                            <asp:LinkButton ID="btnViewTrend" CommandName="Trend" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  runat="server">View Trend</asp:LinkButton>
                                            </span>
                                            <br />
                                            <span class="product-stock">
                                                <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Desc")%>' /></span><br />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reorder Level">
                            <ItemTemplate>
                                <asp:Label CssClass="restock-content" ID="lblRestockLevel" runat="server" Text='<%# Eval("ReorderLevel") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Recommended Reorder Level">
                            <ItemTemplate>
                                <asp:Label CssClass="restock-content" ID="lblRecomLevel" runat="server" Text='<%# Eval("ReccReorderLvl") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Button ID="btnReLevel" runat="server" CommandName="ReLevel" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn btn-primary restock-content" Text="use" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Change Reorder Level">
                            <ItemTemplate>
                                <asp:TextBox ID="txtChangeReLevel" runat="server" CssClass="p-2 restock-content" type="number" Text='<%# Eval("NewReorderLvl") %>' min="0" Width="50px" Height="34px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reorder Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblRestockQty" CssClass="restock-content" runat="server" Text='<%# Eval("ReorderQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Recommended Reorder Qty">
                            <ItemTemplate>
                                <asp:Label ID="lblRecomQty" CssClass="restock-content" runat="server" Text='<%# Eval("ReccReorderQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Button ID="btnReLevelQty" CommandName="ReQty"  CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn btn-primary restock-content" runat="server" Text="use" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Change Reorder Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtChangeRestockQty" type="number" CssClass="p-2 restock-content" Text='<%# Eval("NewReorderQty") %>' runat="server" min="0" Width="50px" Height="34px" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        There is nothing in the list 
                    </EmptyDataTemplate>
                </asp:GridView>

                <div class="row">

                    <div class="buttonarea update-area">

                        <asp:Button ID="btnUpdate" CssClass="btn btn-success updatebtn" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

     <%-- modal content--%>

            <div id="mdlTrend" class="mdlTrend modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                <div class="modal-dialog fix-modal modal-lg">
                        <asp:UpdatePanel ID="udpTrend" runat="server"><ContentTemplate>
                    <div class="modal-content">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true" style="font-size: 32px;"><strong>&times;</strong></span>
                                </button>
                                <h3 class="detail-subtitle">
                                    <asp:Label ID="lblTrendTitle" class="detail-subtitle" style="font-size: 2rem;" runat="server" Text=""></asp:Label></h3>
                            </div>

                            <div class="panel-body lightbox-scroll">

                                <div class="detail-info">
                                         <div class="canvas-wrapper" style="height: 500px; width:100%;">
                                                        <canvas id="myChart"> </canvas></div>
                                                     <div class="text-center">
                                                    <asp:Label ID="lblSubtitle" style="font-size:1.8rem;" runat="server" Text=""></asp:Label></div>
                                        <table class="detail-info-col">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        Date Range: <asp:Label ID="lblTrendSubtitle" runat="server" style="font-weight:700;"></asp:Label></td>
                                                </tr>
                                                <tr><td></td></tr>
                                               <tr>
                                                   <td style="text-align:left;">Recommended Reorder Level: <asp:Label ID="lblTrendReccRL" runat="server" style="font-weight:700;" Text="Label"></asp:Label></td>
                                                   <td style="text-align:left;">Recommended Reorder Quantity: <asp:Label ID="lblTrendReccRQ" runat="server" style="font-weight:700;" Text="Label"></asp:Label></td>
                                                   <td>
                                                       <asp:Label ID="lblTrendiCode" runat="server" Visible="false"></asp:Label>
                                                       <asp:Button ID="btnUseTrend" runat="server" CssClass="btn btn-primary" OnClick="BtnUseTrend_Click" style="margin-left:50px;" Text="Apply" /></td>
                                               </tr>
                                            </tbody>

                                        </table>


                                    </div>

                                </div>

                               
                            </div>

                        </div>
                    </div>        </ContentTemplate>
    </asp:UpdatePanel>
                </div>
            </div>



    <div id="mdlConfirm" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true" style="font-size: 3.2rem"><strong>&times;</strong></span></button>
                        <h3 class="detail-subtitle">Change Restock Level & Quantity</h3>
                    </div>
                    <div class="panel-body">
                        You are about to change the restock levels and restock quantities.<br />
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

</asp:Content>
<asp:Content ID="cphPageScript" ContentPlaceHolderID="cphScript" runat="server">
        <script src="<%=ResolveClientUrl("~/js/restocktrend-script.js")%>"></script>
</asp:Content>
