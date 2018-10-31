<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Group8_AD_webapp.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

    <link href="../css/manager-style.css" rel="stylesheet" />
    <link href="../css/monthpicker.css" rel="stylesheet" />
    <link href="https://code.jquery.com/ui/1.12.1/themes/ui-lightness/jquery-ui.css" rel="stylesheet" />
    <link href="../css/month-picker-style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-sm-9 col-sm-offset-3 col-lg-10 col-lg-offset-2 main" style="margin-top:40px;">
        <div class="row">
            <div class="col-lg-6">
                <h3 class="page-header">
                    <asp:Label ID="lblHeader" runat="server" Text="Label"></asp:Label>
                    <asp:LinkButton ID="btnBar" ClientIDMode="Static" CssClass="listbutton active" runat="server" Text="" OnClick="BtnBar_Click"><i class="fa fa-bar-chart"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnList" ClientIDMode="Static" CssClass="listbutton " runat="server" Text="" OnClick="BtnList_Click"><i class="fa fa-list"></i></asp:LinkButton></h3>
            </div>
            <div class="col-lg-6">
                <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-primary btn-export" OnClick="btnExport_Click" />
                <asp:Button ID="btnExport_Chart" runat="server" Text="Export" CssClass="btn btn-primary btn-export" OnClientClick="SaveReport()" OnClick="ExportToPDF" />
            </div>
        </div>
        <!--/.row-->
        <!--/.row-->

        <div class="col-md-12">
            <div class="col-md-6">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <asp:Label ID="lblReportTitle" Style="font-size: 2.4rem;" runat="server" Text="Label"></asp:Label>

                    </div>
                    <asp:UpdatePanel ID="udpChartArea" runat="server">
                        <ContentTemplate>
                            <div class="panel-body">

                                <div id="showchart" runat="server">
                                    <div class="canvas-wrapper" style="height: 500px;">
                                        <canvas id="myChart"></canvas>
                                    </div>
                                    <div class="text-center">
                                        <asp:Label ID="lblSubtitle" Style="font-size: 1.8rem;" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>

                                <div id="showlist" runat="server">
                                    <asp:GridView ID="lstData" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Label" HeaderText="" SortExpression="Val1" ItemStyle-Width="150px" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# IsVolume ? Eval("Val1") : String.Format("{0:C}", (Double)Eval("Val1")) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# IsVolume ? Eval("Val1") : String.Format("{0:C}", (Double)Eval("Val2")) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <div class="text-center">
                                        <asp:Label ID="lblSubtitle2" Style="font-size: 1.8rem;" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="col-lg-8">
                        <div class="panel panel-default">
                            <div class="panel-body tabs">
                                <ul class="nav nav-tabs">
                                    <li class="active">
                                        <asp:LinkButton runat="server" ID="ByDept" ClientIDMode="Static" href="#tab1" data-toggle="tab">By Department</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="BySupp" ClientIDMode="Static" href="#tab2" data-toggle="tab">By Supplier</asp:LinkButton></li>

                                </ul>
                                <div class="tab-content tab-box">
                                    <div class="tab-pane fade in active" id="tab1">
                                        <div>


                                            <div style="padding-left: 15px;">
                                                <div class="form-group">
                                                    <asp:Label ID="lblDept" runat="server" class="" Text="Department 1"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlDepartment1" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Select Department</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="clear: both; padding-top: 20px;">
                                            <div style="padding-left: 15px;">
                                                <div class="form-group">
                                                    <asp:Label ID="Label3" runat="server" class="" Text="Department 2"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlDepartment2" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Select Department</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>


                                    </div>
                                    <div class="tab-pane fade" id="tab2">

                                        <div>

                                            <div style="padding-left: 15px;">
                                                <div class="form-group">
                                                    <asp:Label ID="lblSupplier" runat="server" class="" Text="Supplier 1"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlSupplier1" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Select Supplier</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div style="clear: both; padding-top: 20px;">
                                            <div style="padding-left: 15px;">
                                                <div class="form-group">
                                                    <asp:Label ID="Label4" runat="server" class="" Text="Supplier 2"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlSupplier2" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Select Supplier</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--/.panel-->

                    <div class="col-lg-4">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab1" data-toggle="tab">Options</a></li>

                        </ul>
                        <div>
                            <div>
                                <div class="form-group mt-15">
                                    <asp:HiddenField ID="IsDept" Value="true" ClientIDMode="Static" runat="server" />
                                    <asp:Label ID="lblCategory" runat="server" class="" Text="Category"></asp:Label>
                                </div>
                            </div>

                            <div class="">
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem Value="All">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-body tabs">
                            <ul class="nav nav-tabs">
                                <li class="active">
                                    <asp:LinkButton runat="server" href="#tabMonth" ID="ByMonth" data-toggle="tab" ClientIDMode="Static">By Month</asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton runat="server" href="#tabDate" data-toggle="tab" ID="ByDate" ClientIDMode="Static">By Date Range</asp:LinkButton></li>

                            </ul>
                            <div class="tab-content tab-box">
                                <div class="tab-pane fade in active" id="tabMonth">

                                    <div class="col-lg-6">

                                        <div>
                                            <div class="form-group mt-15">
                                                <asp:Label ID="Label1" runat="server" class="" Text="Select Month(s) to display"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="input-group">
                                            <asp:HiddenField ID="IsMonth" Value="true" ClientIDMode="Static" runat="server" />
                                            <asp:TextBox ID="txtMonthPick" ClientIDMode="Static" autocomplete="off" OnTextChanged="TxtMonthPick_TextChanged" AutoPostBack="true" placeholder="Month-Year" runat="server" CssClass="form-control calendar-db"></asp:TextBox>
                                            <span class="input-group-addon calendar-db"><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                        <div class="text-right mt-15">


                                        <asp:Button ID="btnClearList" CssClass="btn btn-warning blacktext" OnClick="BtnClear_Click" runat="server" Text=" Clear List" /></div>


                                    </div>
                                    <div class="col-lg-6">
                                        <asp:UpdatePanel ID="udpMonths" runat="server">

                                            <ContentTemplate>

                                            <div class="montharea">
                                                <asp:ListView ID="lstMonths" runat="server"><ItemTemplate>
                                                <asp:Label runat="server" ID="lblMonths" CssClass="monthareali" Text='<%# ((DateTime)Container.DataItem).ToString("MMM-yyyy") %>'></asp:Label><asp:LinkButton ID="btnRemove" OnClick="BtnRemove_Click" runat="server"><span style="color:var(--color-btn-danger); margin-left:10px;"><i class="fa fa-times-circle"></i></span></asp:LinkButton> <br />
                                                </ItemTemplate></asp:ListView>
                                            </div></ContentTemplate></asp:UpdatePanel>
                                        <div  class="text-right">
                                        <div class="report-btn mt-15">
                                                <asp:Button ID="btnMonth" CssClass="btn btn-success" OnClick="BtnMonth_Click" runat="server" Text="Generate By Month" />
                                            </div> </div> 
                                    </div>
                            </div>
                            <div class="tab-pane fade" id="tabDate">
                                <div class="col-md-6">
                                         <div class="input-group">
                                            <asp:TextBox ID="txtFromDate" ClientIDMode="Static" autocomplete="off" placeholder="from: day-month-year" runat="server" CssClass="form-control calendar-db"></asp:TextBox>
                                            <span class="input-group-addon calendar-db"><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                    </div>
                                 <div class="col-md-6">
                                         <div class="input-group">
                                            <asp:TextBox ID="txtToDate" ClientIDMode="Static" autocomplete="off" placeholder="to: day-month-year" runat="server" CssClass="form-control calendar-db"></asp:TextBox>
                                            <span class="input-group-addon calendar-db"><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                        </div>
                                </div>
                                <div class="col-md-12 text-right">
                                          <div class="report-btn mt-15 text-right">
                                            <asp:Button ID="btnRange" OnClick="BtnRange_Click" CssClass="btn btn-success" runat="server" Text="Generate By Range" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
    <script>
        function SaveReport() {

            var image = document.getElementById("myChart").toDataURL("image/png");
           
            image = image.replace('data:image/png;base64,', '');
            console.log(image);
            $.ajax({
                type: 'POST',
                url: 'Reports.aspx/SaveReport',
                data: '{ "imageData" : "' + image + '" }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (msg) {
                    alert('Image saved!');
                },
            });

            PageMethods.ExportToPDF();
        }
    </script>
</asp:Content>
<asp:Content ID="cphPageScript" ContentPlaceHolderID="cphScript" runat="server">
    <script src="<%=ResolveClientUrl("~/js/monthpicker.js")%>"></script>
    <script src="<%=ResolveClientUrl("~/js/report-script.js")%>"></script>
</asp:Content>
