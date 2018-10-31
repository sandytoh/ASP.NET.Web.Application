using Group8_AD_webapp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.Services;

namespace Group8_AD_webapp
{
    // Author: Toh Shu Hui Sandy, A0180548Y
    // Version 1.0 Initial Release
    public partial class Reports : System.Web.UI.Page
    {
        static List<DateTime> datesList;
        static List<DateTime> monthsList;
        static List<ReportItemVM> cbList;
        static string lbl0;
        static string lbl1;
        static string lbl2;
        static string lbl3;
        static public bool IsVolume;
        Main master;

        protected void Page_Load(object sender, EventArgs e)
        {
            Service.UtilityService.CheckRoles("Store");

            master = (Main)this.Master;
            if (!IsPostBack)
            {
                // Adds active class to menu Item (sidebar)
                Main master = (Main)this.Master;


                if (Request.QueryString["type"] == "volume")
                {
                    IsVolume = true;
                    lblHeader.Text = "Volume Reports";
                    master.ActiveMenu("reports2");
                }
                else
                {
                    IsVolume = false;
                    lblHeader.Text = "Cost Reports";
                    master.ActiveMenu("reports");
                }

                datesList = new List<DateTime>();
                monthsList = new List<DateTime>();
                cbList = new List<ReportItemVM>();
                FillDropDowns();
                DemoChart();
                lblReportTitle.Text = "Welcome";
                showlist.Visible = false;
                btnExport.Visible = false;
            }
        }

        // Populates chart upon first load
        protected void DemoChart()
        {
            List<DateTime> demoList = new List<DateTime>();
            demoList.Add(DateTime.Today.AddMonths(-1));
            demoList.Add(DateTime.Today.AddMonths(-2));
            demoList.Add(DateTime.Today.AddMonths(-3));
            
            lbl1 = "Claims";
            lbl2 = "Commerce";
            lbl0 = "Month";

            if (IsVolume)
            {
                lbl3 = "Ordered Quantity";
                cbList = Controllers.ReportItemCtrl.ShowVolumeReport("CLAI", "COMM", null, null, "All", demoList, true);
                lblSubtitle.Text = "Claims Department vs Commerce Department";
                lblSubtitle2.Text = "Ordered Quantity";
            }
            else
            {
                lbl3 = "Chargeback(SGD)";
                cbList = Controllers.ReportItemCtrl.ShowCostReport("CLAI", "COMM", null, null, "All", demoList, true);
                lblSubtitle.Text = "Claims Department vs Commerce Department";
                lblSubtitle2.Text = "Chargeback (SGD)";
            }

            FillDataList();
        }

        // Popualates Dept/Supplier Dropdowns
        protected void FillDropDowns()
        {
            ddlCategory.DataSource = Controllers.ItemCtrl.GetCategory();
            ddlCategory.DataBind();

            List<SupplierVM> suppliers = Controllers.SupplierCtrl.GetAllSupp();
            foreach (SupplierVM s in suppliers)
            {
                ddlSupplier1.Items.Add(new System.Web.UI.WebControls.ListItem(s.SuppName, s.SuppCode));
                ddlSupplier2.Items.Add(new System.Web.UI.WebControls.ListItem(s.SuppName, s.SuppCode));
            }

            List<DepartmentVM> departments = Controllers.DepartmentCtrl.GetAllDept();
            foreach (DepartmentVM d in departments)
            {
                ddlDepartment1.Items.Add(new System.Web.UI.WebControls.ListItem(d.DeptName, d.DeptCode));
                ddlDepartment2.Items.Add(new System.Web.UI.WebControls.ListItem(d.DeptName, d.DeptCode));
            }
        }

        // Populates Data Listview
        protected void FillDataList()
        {
            lstData.DataSource = cbList;
            lstData.DataBind();
            lstData.HeaderRow.Cells[0].Text = lbl0;
            lstData.HeaderRow.Cells[1].Text = lbl1 + " Department";
            lstData.HeaderRow.Cells[2].Text = lbl2 + " Department";
        }

        // Adds month to month list
        protected void TxtMonthPick_TextChanged(object sender, EventArgs e)
        {
            if (txtMonthPick.Text != "")
            {
                string d = txtMonthPick.Text;
                DateTime tempDate = DateTime.ParseExact(txtMonthPick.Text, "MMMM yyyy", CultureInfo.InvariantCulture);
                if (!monthsList.Contains(tempDate))
                {
                    monthsList.Add(tempDate);
                    monthsList = monthsList.OrderBy(x => x.Date).ToList();
                }
                else
                {
                    master.ShowToastr(this, "", "Month already added", "error");
                }
                txtMonthPick.Text = "";
                lstMonths.DataSource = monthsList;
                lstMonths.DataBind();
            }
            ClearChart();
        }

        // Removes month from month List
        protected void BtnRemove_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var item = (ListViewItem)btn.NamingContainer;
            Label lblMonth = (Label)item.FindControl("lblMonths");
            DateTime month = DateTime.ParseExact(lblMonth.Text, "MMM-yyyy", CultureInfo.InvariantCulture);
            monthsList.Remove(month);
            lstMonths.DataSource = monthsList;
            lstMonths.DataBind();
        }

        // Clears Month List
        protected void BtnClear_Click(object sender, EventArgs e)
        {
            monthsList = new List<DateTime>();
            lstMonths.DataSource = monthsList;
            lstMonths.DataBind();
            ClearChart();
        }

        // Generates chart by Months
        protected void BtnMonth_Click(object sender, EventArgs e)
        {
            GenerateGraph(true);
        }

        // Generates chart by Date Range
        protected void BtnRange_Click(object sender, EventArgs e)
        {
            GenerateGraph(false);
        }



        // Gets data for chart
        protected void GenerateGraph(bool byMonth)
        {
            string cat = ddlCategory.Text;

            if (IsDept.Value == "true")
            {
                if (ddlDepartment1.SelectedValue != "0" && ddlDepartment2.SelectedValue != "0")
                {
                    string dept1 = ddlDepartment1.SelectedValue;
                    string dept2 = ddlDepartment2.SelectedValue;

                    if (dept1 != dept2)
                    {
                        if (byMonth)
                        {
                            if (monthsList.Count != 0)
                            {
                                GetDeptReport(dept1, dept2, cat, monthsList, byMonth);
                                FillDataList();
                            }
                            else
                            {
                                master.ShowToastr(this, "", "Month List is Empty!", "error");
                                ClearChart();
                            }
                        }
                        else
                        {
                            if (txtFromDate.Text != "" && txtToDate.Text != "")
                            {
                                DateTime d1 = DateTime.ParseExact(txtFromDate.Text, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                                DateTime d2 = DateTime.ParseExact(txtToDate.Text, "dd-MMM-yyyy", CultureInfo.InvariantCulture);

                                if (d2.CompareTo(d1) >= 0)
                                {
                                    datesList = new List<DateTime>();
                                    datesList.Add(d1);
                                    datesList.Add(d2);
                                    GetDeptReport(dept1, dept2, cat, datesList, byMonth);
                                    FillDataList();
                                }
                                else
                                {
                                    master.ShowToastr(this, "", "End Date must be after Start Date", "error");
                                    ClearChart();
                                }
                            }
                            else
                            {
                                master.ShowToastr(this, "", "Dates cannot be Empty!", "error");
                                ClearChart();
                            }
                        }
                    }
                    else
                    {
                        master.ShowToastr(this, "", "Please select 2 different departments!", "error");
                        ClearChart();
                    }

                }
                else
                {
                    master.ShowToastr(this, "", "Please select 2 departments!", "error");
                    ClearChart();
                }
            }
            else
            {
                if (ddlSupplier1.SelectedValue != "0" && ddlSupplier2.SelectedValue != "0")
                {
                    string supp1 = ddlSupplier1.SelectedValue;
                    string supp2 = ddlSupplier2.SelectedValue;
                    if (supp1 != supp2)
                    {
                        if (byMonth)
                        {
                            if (monthsList.Count != 0)
                            {
                                GetSupplierReport(supp1, supp2, cat, monthsList, byMonth);
                                FillDataList();

                            }
                            else
                            {
                                master.ShowToastr(this, "", "Month List is Empty!", "error");
                                ClearChart();
                            }
                        }
                        else
                        {
                            if (txtFromDate.Text != "" && txtToDate.Text != "")
                            {
                                DateTime d1 = DateTime.ParseExact(txtFromDate.Text, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                                DateTime d2 = DateTime.ParseExact(txtToDate.Text, "dd-MMM-yyyy", CultureInfo.InvariantCulture);

                                if (d2.CompareTo(d1) >= 0)
                                {
                                    datesList = new List<DateTime>();
                                    datesList.Add(d1);
                                    datesList.Add(d2);
                                    GetSupplierReport(supp1, supp2, cat, datesList, byMonth);
                                    FillDataList();
                                }
                                else
                                {
                                    master.ShowToastr(this, "", "End Date must be after Start Date", "error");
                                    ClearChart();
                                }
                            }
                            else
                            {
                                master.ShowToastr(this, "", "Dates cannot be Empty!", "error");
                                ClearChart();
                            }
                        }

                    }
                    else
                    {
                        master.ShowToastr(this, "", "Please select 2 different suppliers!", "error");
                        ClearChart();
                    }
                }
                else
                {
                    master.ShowToastr(this, "", "Please select 2 suppliers!", "error");
                    ClearChart();
                }
            }
        }

        // gets Department vs Department Report
        protected void GetDeptReport(string dept1, string dept2, string cat, List<DateTime> timeframe, bool byMonth)
        {
            lbl1 = (ddlDepartment1.SelectedItem.Text).Replace("Department", "");
            lbl2 = (ddlDepartment2.SelectedItem.Text).Replace("Department", "");
            if (byMonth) lbl0 = "Month";
            else lbl0 = "Week Of";

            if (IsVolume)
            {
                cbList = Controllers.ReportItemCtrl.ShowVolumeReport(dept1, dept2, null, null, cat, timeframe, byMonth);
                lblReportTitle.Text = "Department Volume Report for Category:" + cat;
                lbl3 = "Ordered Quantity";
            }
            else
            {
                cbList = Controllers.ReportItemCtrl.ShowCostReport(dept1, dept2, null, null, cat, timeframe, byMonth);
                lblReportTitle.Text = "Department Cost Report for Category:" + cat;
                lbl3 = "Chargeback (SGD)";
            }

            lblSubtitle2.Text = lbl3;

            lblSubtitle.Text = ddlDepartment1.SelectedItem.Text + " vs " + ddlDepartment2.SelectedItem.Text;
        }

        // gets Supplier vs Supplier Report
        protected void GetSupplierReport(string supp1, string supp2, string cat, List<DateTime> timeframe, bool byMonth)
        {
            lbl1 = ddlSupplier1.SelectedItem.Text;
            lbl2 = ddlSupplier2.SelectedItem.Text;
            if (byMonth) lbl0 = "Month";
            else lbl0 = "Week Of";

            if (IsVolume)
            {
                cbList = Controllers.ReportItemCtrl.ShowVolumeReport(null, null, supp1, supp2, cat, timeframe, byMonth);
                lblReportTitle.Text = "Supplier Volume Report for Category:" + cat;
                lbl3 = "Ordered Quantity";
            }
            else
            {
                cbList = Controllers.ReportItemCtrl.ShowCostReport(null, null, supp1, supp2, cat, timeframe, byMonth);
                lblReportTitle.Text = "Supplier Cost Report for Category:" + cat;
                lblSubtitle2.Text = "Amount Paid (SGD)";
            }

            lblSubtitle2.Text = lbl3;
            lblSubtitle.Text = ddlSupplier1.SelectedItem.Text + " vs " + ddlSupplier2.SelectedItem.Text;
        }

        // Populates data to send to Chart.js
        [System.Web.Services.WebMethod]
        public static List<string> getChartData()
        {
            var returnData = new List<string>();

            var chartLabel = new StringBuilder();
            var chartData = new StringBuilder();
            var chartData2 = new StringBuilder();
            chartLabel.Append("[");
            chartData.Append("[");
            chartData2.Append("[");
            for (int i = 0; i < cbList.Count; i++)
            {
                if (i < cbList.Count - 1)
                {
                    string s = (cbList[i].Label);
                    chartLabel.Append("'" + s + "', ");
                }
                else
                {
                    string s = (cbList[i].Label);
                    chartLabel.Append("'" + s + "'");
                }
            }
            for (int i = 0; i < cbList.Count; i++)
            {
                if (i < cbList.Count - 1)
                {
                    chartData.Append(cbList[i].Val1 + ", ");
                }
                else
                {
                    chartData.Append(cbList[i].Val1);
                }
            }
            for (int i = 0; i < cbList.Count; i++)
            {
                if (i < cbList.Count - 1)
                {
                    chartData2.Append(cbList[i].Val2 + ", ");
                }
                else
                {
                    chartData2.Append(cbList[i].Val2);
                }
            }
            chartData.Append("]");
            chartData2.Append("]");
            chartLabel.Append("]");

            returnData.Add(chartLabel.ToString());
            returnData.Add(chartData.ToString());
            returnData.Add(chartData2.ToString());
            returnData.Add(lbl1);
            returnData.Add(lbl2);
            returnData.Add(lbl3);
            return returnData;
        }

        // Clears Chart upon invalid selection
        protected void ClearChart()
        {
            cbList = new List<ReportItemVM>();
            lbl1 = "";
            lbl2 = "";
            lstData.DataSource = cbList;
            lstData.DataBind();
            lblSubtitle.Text = "";
            lblSubtitle2.Text = "";
        }

        // Clears Chart
        protected void OnChange(object sender, EventArgs e)
        {
            ClearChart();
        }

        // Changes from List to Bar Chart
        protected void BtnBar_Click(object sender, EventArgs e)
        {
            showchart.Visible = true;
            showlist.Visible = false;
            btnList.CssClass = "listbutton";
            btnBar.CssClass = "listbutton active";
            btnExport_Chart.Visible = true;
            btnExport.Visible = false;
        }

        // Changes from Bar chart to List
        protected void BtnList_Click(object sender, EventArgs e)
        {
            showchart.Visible = false;
            showlist.Visible = true;
            btnBar.CssClass = "listbutton";
            btnList.CssClass = "listbutton active";
            btnExport_Chart.Visible = false;
            btnExport.Visible = true;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            lstData.AllowPaging = false;
            lstData.RenderControl(hw);

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 50f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            //using header class started
            writer.PageEvent = new Header();
            Paragraph welcomeParagraph = new Paragraph();
            pdfDoc.Add(welcomeParagraph);
            //using header class ended

            htmlparser.Parse(sr);
            pdfDoc.Close();

            Response.Write(pdfDoc);
            Response.End();
                       
        }

        [WebMethod]
        public static void SaveReport(string imageData)
        {
            //Server.MapPath
            string path = HttpContext.Current.Server.MapPath("~/PDF/") + "\\Report.png";

            FileStream fs = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            byte[] data = Convert.FromBase64String(imageData);

            bw.Write(data);
            bw.Close();
           
        }



        public void ExportToPDF(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Server.MapPath("~/PDF/") + "\\Report.png";
            byte[] imagebytes = File.ReadAllBytes(path);
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagebytes);
            
            image.ScaleAbsolute(600f, 300f);        
                        
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();
                //document.Add(image);

                //pdf and image size customize by Noel Noel Han
                PdfPTable headerTbl = new PdfPTable(1);
                headerTbl.TotalWidth = 300;
                headerTbl.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell cell = new PdfPCell(image);
                cell.Border = 0;
                cell.PaddingLeft = 10;

                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;

                headerTbl.AddCell(cell);
                headerTbl.WriteSelectedRows(0, -1, 80, 500, writer.DirectContent);
                //pdf and image size customize

                //using header class started by Neol Noel Han
                writer.PageEvent = new Header_Landscape_A4();
                Paragraph welcomeParagraph = new Paragraph();
                document.Add(welcomeParagraph);
                //using header class ended

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=Chart_Report.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        //Header all pages(Portrait)
	//By Noel Noel Han
        public partial class Header : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document doc)
            {
                string imageurl = HttpContext.Current.Server.MapPath("~/Content/logo.png");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imageurl);
                logo.ScaleAbsolute(140, 15);

                //Paragraph header = new Paragraph("LOGIC UNIVERSITY", FontFactory.GetFont(FontFactory.TIMES, 25, iTextSharp.text.Font.NORMAL));
                //header.Alignment = Element.ALIGN_LEFT;
                logo.Alignment = Element.ALIGN_CENTER;
                PdfPTable headerTbl = new PdfPTable(1);
                headerTbl.TotalWidth = 300;
                headerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell = new PdfPCell(logo);
                cell.Border = 0;
                cell.PaddingLeft = 10;

                headerTbl.AddCell(cell);
                headerTbl.WriteSelectedRows(0, -1, 230, 820, writer.DirectContent);
            }
        }

        //Header all pages(LandscapeA4)
	//By Noel Noel Han
        public partial class Header_Landscape_A4 : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document doc)
            {
                string imageurl = HttpContext.Current.Server.MapPath("~/Content/logo.png");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imageurl);
                logo.ScaleAbsolute(140, 15);

                //Paragraph header = new Paragraph("LOGIC UNIVERSITY", FontFactory.GetFont(FontFactory.TIMES, 25, iTextSharp.text.Font.NORMAL));
                //header.Alignment = Element.ALIGN_LEFT;
                //logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                logo.Alignment = Element.ALIGN_CENTER;
                PdfPTable headerTbl = new PdfPTable(1);
                headerTbl.TotalWidth = 300;
                headerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell = new PdfPCell(logo);
                cell.Border = 0;
                cell.PaddingLeft = 10;

                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;

                headerTbl.AddCell(cell);
                headerTbl.WriteSelectedRows(0, -1, 270, 550, writer.DirectContent);
            }
        }
    }
}