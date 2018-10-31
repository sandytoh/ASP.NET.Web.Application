using Group8_AD_webapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Author: Tang Shenqi: A0114523U

namespace Group8AD_WebAPI.BusinessLogic
{
    public class ReportItemBL
    {
        // get chargeback by month
        // done
        public static List<ReportItemVM> GetCBByMth(string deptCode, DateTime fromDate, DateTime toDate)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    List<DateTime> monthList = GetMonthList(fromDate, toDate);
                    List<ReportItemVM> riList = new List<ReportItemVM>();
                    List<Transaction> transList = entities.Transactions.ToList();
                    for (int i = 0; i < monthList.Count; i++)
                    {
                        double chargeBack = 0;
                        for (int j = 0; j < transList.Count; j++)
                        {
                            if (transList[j].UnitPrice != null && transList[j].DeptCode == deptCode
                                && DateTime.Compare(transList[j].TranDateTime, monthList[i]) >= 0
                                && DateTime.Compare(transList[j].TranDateTime, monthList[i].AddMonths(1)) < 0)
                            {
                                chargeBack = chargeBack + transList[j].QtyChange * (double)transList[j].UnitPrice;
                            }
                        }
                        chargeBack = Math.Round(chargeBack, 2);
                        string format = "yyyy MMM";
                        string label = monthList[i].ToString(format);
                        ReportItemVM ri = new ReportItemVM();
                        ri.Period = monthList[i];
                        ri.Label = label;
                        ri.Val1 = chargeBack * -1;
                        ri.Val2 = 0;
                        riList.Add(ri);
                    }
                    return riList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // get chargeback by date range
        // done
        public static List<ReportItemVM> GetCBByRng(string deptCode, DateTime fromDate, DateTime toDate)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    List<DateTime> weekList = GetWeekList(fromDate, toDate);
                    List<ReportItemVM> riList = new List<ReportItemVM>();
                    List<Transaction> transList = entities.Transactions.ToList();
                    for (int i = 0; i < weekList.Count; i++)
                    {
                        double chargeBack = 0;
                        for (int j = 0; j < transList.Count; j++)
                        {
                            if (transList[j].UnitPrice != null && transList[j].DeptCode == deptCode
                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                            {
                                chargeBack = chargeBack + transList[j].QtyChange * (double)transList[j].UnitPrice;
                            }
                        }
                        chargeBack = Math.Round(chargeBack, 2);
                        string format = "yyyy MMM d";
                        string label = weekList[i].ToString(format);
                        ReportItemVM ri = new ReportItemVM();
                        ri.Period = weekList[i];
                        ri.Label = label;
                        ri.Val1 = chargeBack * -1;
                        ri.Val2 = 0;
                        riList.Add(ri);
                    }
                    return riList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // get last ten transactions by itemCode
        // done
        public static List<TransactionVM> GetLastTenTrans(string itemCode)
        {
            try
            {
                List<TransactionVM> translist = new List<TransactionVM>();
                using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
                {
                    List<Transaction> list = entities.Transactions.Where(t => t.ItemCode == itemCode).
                        OrderByDescending(t => t.TranDateTime).ToList();
                    if (list != null)
                    {
                        for (int i = 0; i < 10 && i < list.Count; i++)
                        {
                            TransactionVM t = new TransactionVM();
                            t.TranId = list[i].TranId;
                            t.TranDateTime = list[i].TranDateTime;
                            t.ItemCode = list[i].ItemCode;
                            t.QtyChange = list[i].QtyChange;
                            if (list[i].UnitPrice != null)
                                t.UnitPrice = (double)list[i].UnitPrice;
                            else
                                t.UnitPrice = 0;
                            t.Desc = list[i].Desc;
                            t.DeptCode = list[i].DeptCode;
                            t.SuppCode = list[i].SuppCode;
                            t.VoucherNo = list[i].VoucherNo;
                            translist.Add(t);
                        }
                    }
                }
                return translist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // get monthly chargeback by a single date
        // done
        public static List<ReportItemVM> GetCBMonthly(DateTime toDate)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    int year = toDate.Year;
                    int month = toDate.Month;
                    DateTime startDate = new DateTime(year, month, 01, 00, 00, 00);
                    DateTime endDate = startDate.AddMonths(1);
                    List<ReportItemVM> rilist = new List<ReportItemVM>();
                    List<Transaction> translist = entities.Transactions.ToList();
                    List<Department> deptlist = entities.Departments.Where(d => d.DeptCode != "STOR").ToList();
                    for (int i = 0; i < deptlist.Count; i++)
                    {
                        double chargeBack = 0;
                        for (int j = 0; j < translist.Count; j++)
                        {
                            if (translist[j].UnitPrice != null && deptlist[i].DeptCode == translist[j].DeptCode &&
                                DateTime.Compare(translist[j].TranDateTime, startDate) >= 0 &&
                                DateTime.Compare(translist[j].TranDateTime, endDate) < 0)
                            {
                                chargeBack = chargeBack + translist[j].QtyChange * (double)translist[j].UnitPrice;
                            }
                        }
                        ReportItemVM ri = new ReportItemVM();
                        chargeBack = Math.Round(chargeBack, 2);
                        ri.Period = startDate;
                        ri.Label = deptlist[i].DeptName;
                        ri.Val1 = chargeBack * -1;
                        ri.Val2 = 0;
                        rilist.Add(ri);
                    }
                    return rilist;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // get monthly chargeback by fromDate and toDate
        // done
        public static List<ReportItemVM> GetCBMonthly(DateTime fromDate, DateTime toDate)
        {
            // by default, fromDate will be the first day of the month and toDate will be the last day of the month
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    List<ReportItemVM> rilist = new List<ReportItemVM>();
                    List<Transaction> translist = entities.Transactions.ToList();
                    List<Department> deptlist = entities.Departments.Where(d => d.DeptCode != "STOR").ToList();
                    for (int i = 0; i < deptlist.Count; i++)
                    {
                        double chargeBack = 0;
                        for (int j = 0; j < translist.Count; j++)
                        {
                            if (translist[j].UnitPrice != null && deptlist[i].DeptCode == translist[j].DeptCode &&
                                DateTime.Compare(translist[j].TranDateTime, fromDate) >= 0 &&
                                DateTime.Compare(translist[j].TranDateTime, toDate) < 0)
                            {
                                chargeBack = chargeBack + translist[j].QtyChange * (double)translist[j].UnitPrice;
                            }
                        }
                        ReportItemVM ri = new ReportItemVM();
                        chargeBack = Math.Round(chargeBack, 2);
                        ri.Period = fromDate;
                        ri.Label = deptlist[i].DeptName;
                        ri.Val1 = chargeBack * -1;
                        ri.Val2 = 0;
                        rilist.Add(ri);
                    }
                    return rilist;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // get monthly volume
        // done
        public static List<ReportItemVM> GetVolMonthly(DateTime toDate)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    int year = toDate.Year;
                    int month = toDate.Month;
                    DateTime startDate = new DateTime(year, month, 01, 00, 00, 00);
                    DateTime endDate = startDate.AddMonths(1);
                    List<ReportItemVM> rilist = new List<ReportItemVM>();
                    //List<Request> rlist = entities.Requests.ToList();
                    List<Item> ilist = entities.Items.ToList();
                    //List<Transaction> tlist = entities.Transactions.ToList();
                    for (int i = 0; i < ilist.Count; i++)
                    {
                        ReportItemVM ri = new ReportItemVM();
                        ri.Period = startDate;
                        ri.Label = ilist[i].ItemCode;
                        ri.Val1 = 0;
                        ri.Val2 = 0;
                        rilist.Add(ri);
                    }

                    // Loop through all Requests Fulfilling Criteria
                    List<Request> rlist = entities.Requests.Where(x => (x.Status.Equals("Fulfilled") || x.Status.Equals("Approved")) && (x.ReqDateTime <= endDate && x.ReqDateTime >= startDate)).ToList();
                    for (int j = 0; j < rlist.Count; j++)
                    {
                        int reqId = rlist[j].ReqId;

                        // Loop through all RequestDetails And Add ReqQty to Respective Item in ivmlist
                        List<RequestDetail> rdlist = entities.RequestDetails.Where(x => x.ReqId == reqId).ToList();
                        for (int k = 0; k < rdlist.Count; k++)
                        {
                            if (rdlist[k].ReqQty != 0) { rilist.Find(x => x.Label.Equals(rdlist[k].ItemCode)).Val1 += rdlist[k].ReqQty; }
                        } // Loop through rd
                    } // Loop through r

                    return rilist;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // get volume within a range
        // done
        public static List<ItemVM> GetVolume(DateTime fromDate, DateTime toDate)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    // Create Item List and Clear TempQyuReq
                    List<Item> ilist = entities.Items.ToList();
                    List<ItemVM> ivmlist = new List<ItemVM>();
                    for (int i = 0; i < ilist.Count; i++)
                    {
                        ItemVM item = new ItemVM();
                        item.ItemCode = ilist[i].ItemCode;
                        item.Cat = ilist[i].Cat;
                        item.Desc = ilist[i].Desc;
                        item.Location = ilist[i].Location;
                        item.UOM = ilist[i].UOM;
                        item.IsActive = ilist[i].IsActive;
                        item.Balance = ilist[i].Balance;
                        item.ReorderLevel = ilist[i].ReorderLevel;
                        item.ReorderQty = ilist[i].ReorderQty;
                        item.TempQtyDisb = ilist[i].TempQtyDisb;
                        item.TempQtyCheck = ilist[i].TempQtyCheck;
                        item.SuppCode1 = ilist[i].SuppCode1;
                        item.Price1 = ilist[i].Price1 ?? default(double);
                        item.SuppCode2 = ilist[i].SuppCode2;
                        item.Price2 = ilist[i].Price2 ?? default(double);
                        item.SuppCode3 = ilist[i].SuppCode3;
                        item.Price3 = ilist[i].Price3 ?? default(double);

                        item.TempQtyReq = 0;

                        ivmlist.Add(item);
                    }

                    // Loop through all Requests Fulfilling Criteria
                    List<Request> rlist = entities.Requests.Where(x => (x.Status.Equals("Fulfilled") || x.Status.Equals("Approved")) && (x.ReqDateTime <= toDate && x.ReqDateTime >= fromDate)).ToList();
                    for (int j = 0; j < rlist.Count; j++)
                    {
                        int reqId = rlist[j].ReqId;

                        // Loop through all RequestDetails And Add ReqQty to Respective Item in ivmlist
                        List<RequestDetail> rdlist = entities.RequestDetails.Where(x => x.ReqId == reqId).ToList();
                        for (int k = 0; k < rdlist.Count; k++)
                        {
                            if (rdlist[k].ReqQty != 0) { ivmlist.Find(x => x.ItemCode.Equals(rdlist[k].ItemCode)).TempQtyReq += rdlist[k].ReqQty; }
                        } // Loop through rd
                    } // Loop through r

                    return ivmlist;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // show cost report
        // done
        public static List<ReportItemVM> ShowCostReport(string dept1, string dept2, string supp1, string supp2,
            string cat, List<DateTime> dates, bool byMonth)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    List<ReportItemVM> riList = new List<ReportItemVM>();
                    if (dept1 != null && dept2 != null && (supp1 == null || supp2 == null))
                    {
                        if (byMonth == true)
                        {
                            List<DateTime> dtList = GetMonthList(dates);
                            List<Transaction> transList = entities.Transactions.ToList();
                            if (cat == "All")
                            {
                                for (int i = 0; i < dtList.Count; i++)
                                {
                                    double chargeBack1 = 0;
                                    double chargeBack2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                        {
                                            chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                        {
                                            chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                    }
                                    chargeBack1 = Math.Round(chargeBack1, 2);
                                    chargeBack2 = Math.Round(chargeBack2, 2);
                                    string format = "yyyy MMM";
                                    string label = dtList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = dtList[i];
                                    ri.Label = label;
                                    ri.Val1 = chargeBack1 * -1;
                                    ri.Val2 = chargeBack2 * -1;
                                    riList.Add(ri);
                                }
                            }
                            else
                            {
                                List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                                for (int i = 0; i < dtList.Count; i++)
                                {
                                    double chargeBack1 = 0;
                                    double chargeBack2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                    }
                                    chargeBack1 = Math.Round(chargeBack1, 2);
                                    chargeBack2 = Math.Round(chargeBack2, 2);
                                    string format = "yyyy MMM";
                                    string label = dtList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = dtList[i];
                                    ri.Label = label;
                                    ri.Val1 = chargeBack1 * -1;
                                    ri.Val2 = chargeBack2 * -1;
                                    riList.Add(ri);
                                }
                            }
                        }
                        else
                        {
                            if (dates.Count >= 2)
                            {
                                DateTime dt1 = dates[0];
                                DateTime dt2 = dates[1];
                                if (DateTime.Compare(dt1, dt2) > 0)
                                {
                                    DateTime temp = dt1;
                                    dt1 = dt2;
                                    dt2 = temp;
                                }
                                List<DateTime> weekList = GetWeekList(dt1, dt2);
                                List<Transaction> transList = entities.Transactions.ToList();
                                if (cat == "All")
                                {
                                    for (int i = 0; i < weekList.Count; i++)
                                    {
                                        double chargeBack1 = 0;
                                        double chargeBack2 = 0;
                                        for (int j = 0; j < transList.Count; j++)
                                        {
                                            if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                            {
                                                chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                            }
                                            if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                            {
                                                chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                            }
                                        }
                                        chargeBack1 = Math.Round(chargeBack1, 2);
                                        chargeBack2 = Math.Round(chargeBack2, 2);
                                        string format = "yyyy MMM d";
                                        string label = weekList[i].ToString(format);
                                        ReportItemVM ri = new ReportItemVM();
                                        ri.Period = weekList[i];
                                        ri.Label = label;
                                        ri.Val1 = chargeBack1 * -1;
                                        ri.Val2 = chargeBack2 * -1;
                                        riList.Add(ri);
                                    }
                                }
                                else
                                {
                                    List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                                    for (int i = 0; i < weekList.Count; i++)
                                    {
                                        double chargeBack1 = 0;
                                        double chargeBack2 = 0;
                                        for (int j = 0; j < transList.Count; j++)
                                        {
                                            if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                                && InItemList(transList[j].ItemCode, iList) == true)
                                            {
                                                chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                            }
                                            if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                                && InItemList(transList[j].ItemCode, iList) == true)
                                            {
                                                chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                            }
                                        }
                                        chargeBack1 = Math.Round(chargeBack1, 2);
                                        chargeBack2 = Math.Round(chargeBack2, 2);
                                        string format = "yyyy MMM d";
                                        string label = weekList[i].ToString(format);
                                        ReportItemVM ri = new ReportItemVM();
                                        ri.Period = weekList[i];
                                        ri.Label = label;
                                        ri.Val1 = chargeBack1 * -1;
                                        ri.Val2 = chargeBack2 * -1;
                                        riList.Add(ri);
                                    }
                                }
                            }
                        }
                    }
                    else if (supp1 != null && supp2 != null && (dept1 == null || dept2 == null))
                    {
                        if (byMonth == true)
                        {
                            List<DateTime> dtList = GetMonthList(dates);
                            List<Transaction> transList = entities.Transactions.ToList();
                            if (cat == "All")
                            {
                                for (int i = 0; i < dtList.Count; i++)
                                {
                                    double chargeBack1 = 0;
                                    double chargeBack2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                        {
                                            chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                        {
                                            chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                    }
                                    chargeBack1 = Math.Round(chargeBack1, 2);
                                    chargeBack2 = Math.Round(chargeBack2, 2);
                                    string format = "yyyy MMM";
                                    string label = dtList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = dtList[i];
                                    ri.Label = label;
                                    ri.Val1 = chargeBack1;
                                    ri.Val2 = chargeBack2;
                                    riList.Add(ri);
                                }
                            }
                            else
                            {
                                List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                                for (int i = 0; i < dtList.Count; i++)
                                {
                                    double chargeBack1 = 0;
                                    double chargeBack2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                        }
                                    }
                                    chargeBack1 = Math.Round(chargeBack1, 2);
                                    chargeBack2 = Math.Round(chargeBack2, 2);
                                    string format = "yyyy MMM";
                                    string label = dtList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = dtList[i];
                                    ri.Label = label;
                                    ri.Val1 = chargeBack1;
                                    ri.Val2 = chargeBack2;
                                    riList.Add(ri);
                                }
                            }
                        }
                        else
                        {
                            if (dates.Count >= 2)
                            {
                                DateTime dt1 = dates[0];
                                DateTime dt2 = dates[1];
                                if (DateTime.Compare(dt1, dt2) > 0)
                                {
                                    DateTime temp = dt1;
                                    dt1 = dt2;
                                    dt2 = temp;
                                }
                                List<DateTime> weekList = GetWeekList(dt1, dt2);
                                List<Transaction> transList = entities.Transactions.ToList();
                                if (cat == "All")
                                {
                                    for (int i = 0; i < weekList.Count; i++)
                                    {
                                        double chargeBack1 = 0;
                                        double chargeBack2 = 0;
                                        for (int j = 0; j < transList.Count; j++)
                                        {
                                            if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                            {
                                                chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                            }
                                            if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                            {
                                                chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                            }
                                        }
                                        chargeBack1 = Math.Round(chargeBack1, 2);
                                        chargeBack2 = Math.Round(chargeBack2, 2);
                                        string format = "yyyy MMM d";
                                        string label = weekList[i].ToString(format);
                                        ReportItemVM ri = new ReportItemVM();
                                        ri.Period = weekList[i];
                                        ri.Label = label;
                                        ri.Val1 = chargeBack1;
                                        ri.Val2 = chargeBack2;
                                        riList.Add(ri);
                                    }
                                }
                                else
                                {
                                    List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                                    for (int i = 0; i < weekList.Count; i++)
                                    {
                                        double chargeBack1 = 0;
                                        double chargeBack2 = 0;
                                        for (int j = 0; j < transList.Count; j++)
                                        {
                                            if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                                && InItemList(transList[j].ItemCode, iList) == true)
                                            {
                                                chargeBack1 = chargeBack1 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                            }
                                            if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                                && InItemList(transList[j].ItemCode, iList) == true)
                                            {
                                                chargeBack2 = chargeBack2 + transList[j].QtyChange * (double)transList[j].UnitPrice;
                                            }
                                        }
                                        chargeBack1 = Math.Round(chargeBack1, 2);
                                        chargeBack2 = Math.Round(chargeBack2, 2);
                                        string format = "yyyy MMM d";
                                        string label = weekList[i].ToString(format);
                                        ReportItemVM ri = new ReportItemVM();
                                        ri.Period = weekList[i];
                                        ri.Label = label;
                                        ri.Val1 = chargeBack1;
                                        ri.Val2 = chargeBack2;
                                        riList.Add(ri);
                                    }
                                }
                            }
                        }
                    }
                    return riList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }     
        }

        // show volume report
        // done
        public static List<ReportItemVM> ShowVolumeReport(string dept1, string dept2, string supp1, string supp2, 
            string cat, List<DateTime> dates, bool byMonth)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                try
                {
                    List<ReportItemVM> riList = new List<ReportItemVM>();
                    if (dept1 != null && dept2 != null && (supp1 == null || supp2 == null))
                    {
                        if (byMonth == true)
                        {
                            List<DateTime> dtList = GetMonthList(dates);
                            List<Transaction> transList = entities.Transactions.ToList();
                            if (cat == "All")
                            {
                                for (int i = 0; i < dtList.Count; i++)
                                {
                                    double volume1 = 0;
                                    double volume2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                        {
                                            volume1 = volume1 + transList[j].QtyChange;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                        {
                                            volume2 = volume2 + transList[j].QtyChange;
                                        }
                                    }
                                    string format = "yyyy MMM";
                                    string label = dtList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = dtList[i];
                                    ri.Label = label;
                                    ri.Val1 = volume1 * -1;
                                    ri.Val2 = volume2 * -1;
                                    riList.Add(ri);
                                }
                            }
                            else
                            {
                                List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                                for (int i = 0; i < dtList.Count; i++)
                                {
                                    double volume1 = 0;
                                    double volume2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            volume1 = volume1 + transList[j].QtyChange;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            volume2 = volume2 + transList[j].QtyChange;
                                        }
                                    }
                                    string format = "yyyy MMM";
                                    string label = dtList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = dtList[i];
                                    ri.Label = label;
                                    ri.Val1 = volume1 * -1;
                                    ri.Val2 = volume2 * -1;
                                    riList.Add(ri);
                                }
                            }
                        }
                        else
                        {
                            if (dates.Count >= 2)
                            {
                                DateTime dt1 = dates[0];
                                DateTime dt2 = dates[1];
                                if (DateTime.Compare(dt1, dt2) > 0)
                                {
                                    DateTime temp = dt1;
                                    dt1 = dt2;
                                    dt2 = temp;
                                }
                                List<DateTime> weekList = GetWeekList(dt1, dt2);
                                List<Transaction> transList = entities.Transactions.ToList();
                                if (cat == "All")
                                {
                                    for (int i = 0; i < weekList.Count; i++)
                                    {
                                        double volume1 = 0;
                                        double volume2 = 0;
                                        for (int j = 0; j < transList.Count; j++)
                                        {
                                            if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                            {
                                                volume1 = volume1 + transList[j].QtyChange;
                                            }
                                            if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                            {
                                                volume2 = volume2 + transList[j].QtyChange;
                                            }
                                        }
                                        string format = "yyyy MMM d";
                                        string label = weekList[i].ToString(format);
                                        ReportItemVM ri = new ReportItemVM();
                                        ri.Period = weekList[i];
                                        ri.Label = label;
                                        ri.Val1 = volume1 * -1;
                                        ri.Val2 = volume2 * -1;
                                        riList.Add(ri);
                                    }
                                }
                                else
                                {
                                    List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                                    for (int i = 0; i < weekList.Count; i++)
                                    {
                                        double volume1 = 0;
                                        double volume2 = 0;
                                        for (int j = 0; j < transList.Count; j++)
                                        {
                                            if (transList[j].UnitPrice != null && transList[j].DeptCode == dept1
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                                && InItemList(transList[j].ItemCode, iList) == true)
                                            {
                                                volume1 = volume1 + transList[j].QtyChange;
                                            }
                                            if (transList[j].UnitPrice != null && transList[j].DeptCode == dept2
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                                && InItemList(transList[j].ItemCode, iList) == true)
                                            {
                                                volume2 = volume2 + transList[j].QtyChange;
                                            }
                                        }
                                        string format = "yyyy MMM d";
                                        string label = weekList[i].ToString(format);
                                        ReportItemVM ri = new ReportItemVM();
                                        ri.Period = weekList[i];
                                        ri.Label = label;
                                        ri.Val1 = volume1 * -1;
                                        ri.Val2 = volume2 * -1;
                                        riList.Add(ri);
                                    }
                                }
                            }
                        }
                    }
                    else if (supp1 != null && supp2 != null && (dept1 == null || dept2 == null))
                    {
                        if (byMonth == true)
                        {
                            List<DateTime> dtList = GetMonthList(dates);
                            List<Transaction> transList = entities.Transactions.ToList();
                            if (cat == "All")
                            {
                                for (int i = 0; i < dtList.Count; i++)
                                {
                                    double volume1 = 0;
                                    double volume2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                        {
                                            volume1 = volume1 + transList[j].QtyChange;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0)
                                        {
                                            volume2 = volume2 + transList[j].QtyChange;
                                        }
                                    }
                                    string format = "yyyy MMM";
                                    string label = dtList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = dtList[i];
                                    ri.Label = label;
                                    ri.Val1 = volume1;
                                    ri.Val2 = volume2;
                                    riList.Add(ri);
                                }
                            }
                            else
                            {
                                List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                                for (int i = 0; i < dtList.Count; i++)
                                {
                                    double volume1 = 0;
                                    double volume2 = 0;
                                    for (int j = 0; j < transList.Count; j++)
                                    {
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            volume1 = volume1 + transList[j].QtyChange;
                                        }
                                        if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i]) >= 0
                                            && DateTime.Compare(transList[j].TranDateTime, dtList[i].AddMonths(1)) < 0
                                            && InItemList(transList[j].ItemCode, iList) == true)
                                        {
                                            volume2 = volume2 + transList[j].QtyChange;
                                        }
                                    }
                                    string format = "yyyy MMM";
                                    string label = dtList[i].ToString(format);
                                    ReportItemVM ri = new ReportItemVM();
                                    ri.Period = dtList[i];
                                    ri.Label = label;
                                    ri.Val1 = volume1;
                                    ri.Val2 = volume2;
                                    riList.Add(ri);
                                }
                            }
                        }
                        else
                        {
                            if (dates.Count >= 2)
                            {
                                DateTime dt1 = dates[0];
                                DateTime dt2 = dates[1];
                                if (DateTime.Compare(dt1, dt2) > 0)
                                {
                                    DateTime temp = dt1;
                                    dt1 = dt2;
                                    dt2 = temp;
                                }
                                List<DateTime> weekList = GetWeekList(dt1, dt2);
                                List<Transaction> transList = entities.Transactions.ToList();
                                if (cat == "All")
                                {
                                    for (int i = 0; i < weekList.Count; i++)
                                    {
                                        double volume1 = 0;
                                        double volume2 = 0;
                                        for (int j = 0; j < transList.Count; j++)
                                        {
                                            if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                            {
                                                volume1 = volume1 + transList[j].QtyChange;
                                            }
                                            if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0)
                                            {
                                                volume2 = volume2 + transList[j].QtyChange;
                                            }
                                        }
                                        string format = "yyyy MMM d";
                                        string label = weekList[i].ToString(format);
                                        ReportItemVM ri = new ReportItemVM();
                                        ri.Period = weekList[i];
                                        ri.Label = label;
                                        ri.Val1 = volume1;
                                        ri.Val2 = volume2;
                                        riList.Add(ri);
                                    }
                                }
                                else
                                {
                                    List<Item> iList = entities.Items.Where(x => x.Cat == cat).ToList();
                                    for (int i = 0; i < weekList.Count; i++)
                                    {
                                        double volume1 = 0;
                                        double volume2 = 0;
                                        for (int j = 0; j < transList.Count; j++)
                                        {
                                            if (transList[j].UnitPrice != null && transList[j].SuppCode == supp1
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                                && InItemList(transList[j].ItemCode, iList) == true)
                                            {
                                                volume1 = volume1 + transList[j].QtyChange;
                                            }
                                            if (transList[j].UnitPrice != null && transList[j].SuppCode == supp2
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i]) >= 0
                                                && DateTime.Compare(transList[j].TranDateTime, weekList[i].AddDays(7)) < 0
                                                && InItemList(transList[j].ItemCode, iList) == true)
                                            {
                                                volume2 = volume2 + transList[j].QtyChange;
                                            }
                                        }
                                        string format = "yyyy MMM d";
                                        string label = weekList[i].ToString(format);
                                        ReportItemVM ri = new ReportItemVM();
                                        ri.Period = weekList[i];
                                        ri.Label = label;
                                        ri.Val1 = volume1;
                                        ri.Val2 = volume2;
                                        riList.Add(ri);
                                    }
                                }
                            }
                        }
                    }
                    return riList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        // show volume report by item
        // done
        public static List<ReportItemVM> ShowVolumeReport(string iCode, double maxQty)
        {
            using (SA46Team08ADProjectContext entities = new SA46Team08ADProjectContext())
            {
                List<ReportItemVM> riList = new List<ReportItemVM>();

                // This is the actual implementation which will work and display a reasonable trend if we have more data
                //List<int> rIdList = entities.Requests.Where(x => x.ReqDateTime >= DateTime.Now.AddDays(-365) && x.ReqDateTime <= DateTime.Now).Select(x => x.ReqId).ToList();
                //List<RequestDetail> rdList = null; if (rIdList.Count > 0) rdList = entities.RequestDetails.Where(x => rIdList.Contains(x.ReqId) && x.ItemCode.Equals(iCode)).ToList();
                //if (rdList.Count > 0)
                //{
                //    foreach (RequestDetail rd in rdList)
                //    {
                //        ReportItemVM ri = new ReportItemVM();
                //        ri.Period = (DateTime)entities.Requests.Where(x => x.ReqId == rd.ReqId).First().ReqDateTime.Value.Date;
                //        ReportItemVM ri_exist = riList.Find(x => x.Period == ri.Period);
                //        if (ri_exist != null) ri_exist.Val1 += rd.ReqQty;
                //        else
                //        {
                //            ri.Val1 = rd.ReqQty;
                //            riList.Add(ri);
                //        }
                //    }
                //}

                // For the sake of demonstration, a random number generator is used to populate a random trend
                Random rnd = new Random();
                DateTime startDate = DateTime.Now.AddDays(-365);
                for (int i = 0; i < 53; i++)
                {
                    ReportItemVM ri = new ReportItemVM();
                    ri.Period = startDate.AddDays(i * 7);
                    ri.Label = $"{ri.Period:yyyy MMM dd}";
                    ri.Val1 = rnd.NextDouble() * ((maxQty*1.2) - (maxQty*0.3)) + (maxQty * 0.3);
                    riList.Add(ri);
                }

                return riList;
            }
        }

        // service methods
        public static bool InItemList(string itemCode, List<Item> iList)
        {
            bool isIn = false;
            for (int i = 0; i < iList.Count; i++)
            {
                if (iList[i].ItemCode == itemCode)
                    isIn = true;
            }
            return isIn;
        }

        public static List<DateTime> GetMonthList(List<DateTime> dates)
        {
            List<DateTime> dtList = new List<DateTime>();
            for (int i = 0; i < dates.Count; i++)
            {
                int year = dates[i].Year;
                int month = dates[i].Month;
                DateTime tempDate = new DateTime(year, month, 01, 00, 00, 00);
                dtList.Add(tempDate);
            }
            dtList.Sort();
            List<DateTime> distinct = dtList.Distinct().ToList();
            return distinct;
        }

        public static List<DateTime> GetMonthList(DateTime fromDate, DateTime toDate)
        {
            if (DateTime.Compare(fromDate, toDate) > 0)
            {
                DateTime temp = fromDate;
                fromDate = toDate;
                toDate = temp;
            }
            List<DateTime> monthList = new List<DateTime>();
            int fromYear = fromDate.Year;
            int fromMonth = fromDate.Month;
            DateTime startMonth = new DateTime(fromYear, fromMonth, 01, 00, 00, 00);
            int toYear = toDate.Year;
            int toMonth = toDate.Month;
            DateTime endMonth = new DateTime(toYear, toMonth, 01, 00, 00, 00);

            monthList.Add(startMonth);
            while (DateTime.Compare(startMonth, endMonth) < 0)
            {
                startMonth = startMonth.AddMonths(1);
                monthList.Add(startMonth);

            }
            return monthList;
        }

        public static List<DateTime> GetWeekList(DateTime fromDate, DateTime toDate)
        {
            if (DateTime.Compare(fromDate, toDate) > 0)
            {
                DateTime temp = fromDate;
                fromDate = toDate;
                toDate = temp;
            }
            List<DateTime> weekList = new List<DateTime>();
            int fromYear = fromDate.Year;
            int fromMonth = fromDate.Month;
            int fromDay = fromDate.Day;
            DateTime startWeek = new DateTime(fromYear, fromMonth, fromDay, 00, 00, 00);
            if (fromDate.DayOfWeek == DayOfWeek.Tuesday) startWeek = startWeek.AddDays(-1);
            else if (fromDate.DayOfWeek == DayOfWeek.Wednesday) startWeek = startWeek.AddDays(-2);
            else if (fromDate.DayOfWeek == DayOfWeek.Thursday) startWeek = startWeek.AddDays(-3);
            else if (fromDate.DayOfWeek == DayOfWeek.Friday) startWeek = startWeek.AddDays(-4);
            else if (fromDate.DayOfWeek == DayOfWeek.Saturday) startWeek = startWeek.AddDays(-5);
            else if (fromDate.DayOfWeek == DayOfWeek.Sunday) startWeek = startWeek.AddDays(-6);
            else startWeek = new DateTime(fromYear, fromMonth, fromDay, 00, 00, 00);

            int toYear = toDate.Year;
            int toMonth = toDate.Month;
            int toDay = toDate.Day;
            DateTime endWeek = new DateTime(toYear, toMonth, toDay, 00, 00, 00);
            if (toDate.DayOfWeek == DayOfWeek.Tuesday) endWeek = endWeek.AddDays(-1);
            else if (toDate.DayOfWeek == DayOfWeek.Wednesday) endWeek = endWeek.AddDays(-2);
            else if (toDate.DayOfWeek == DayOfWeek.Thursday) endWeek = endWeek.AddDays(-3);
            else if (toDate.DayOfWeek == DayOfWeek.Friday) endWeek = endWeek.AddDays(-4);
            else if (toDate.DayOfWeek == DayOfWeek.Saturday) endWeek = endWeek.AddDays(-5);
            else if (toDate.DayOfWeek == DayOfWeek.Sunday) endWeek = endWeek.AddDays(-6);
            else endWeek = new DateTime(toYear, toMonth, toDay, 00, 00, 00);

            weekList.Add(startWeek);
            while (DateTime.Compare(startWeek, endWeek) < 0)
            {
                startWeek = startWeek.AddDays(7);
                weekList.Add(startWeek);

            }
            return weekList;
        }
    }
}