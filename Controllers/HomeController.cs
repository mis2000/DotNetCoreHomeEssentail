using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlBasicCore.Filters;
using MySqlBasicCore.Models;
using MySqlBasicCore.Utility;
using System;
using System.Data;
using System.Linq;

namespace MySqlBasicCore.Controllers
{
    [Authentication]
    public class HomeController : Controller
    {
        private readonly IOptions<Appsettings> _appSettings;
        private readonly DBContext _dbContext;

        public HomeController(IOptions<Appsettings> appSettings, DBContext dbContext)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
        }

        public ActionResult Dashboard()
        {
            DashboardViewModel model = new DashboardViewModel();
            try
            {

                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = new DataSet();


            }
            catch (Exception ex)
            {


            }

            return View(model);
        }

        [HttpPost]
        public JsonResult GetChartDetail(string boxName, string chartType)
        {
            DashboardViewModel model = new DashboardViewModel();
            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = new DataSet();
                if (boxName == "orders")
                {

                    ds = dbfunction.GetDataset("SELECT count(*) No, MONTH(h.orderdate) Date,  FORMAT(SUM(h.poammount),2) AS Total FROM orders h WHERE YEAR(h.orderdate) = YEAR(CURDATE())  AND MONTH(h.orderdate) <= MONTH(CURDATE()) AND DAY(h.orderdate) <= DAY(CURDATE())   GROUP BY MONTH(h.orderdate);");
                    model.YTDList = (from row in ds.Tables[0].AsEnumerable()
                                     select new DashboardLineChartViewModel
                                     {
                                         Month = Convert.ToInt16(row["Date"]),
                                         Poammount = Convert.ToDecimal(row["Total"]),
                                         ordernum = Convert.ToString(row["No"]),
                                     }).ToList();

                }
                else if (boxName == "sales")
                {

                    ds = dbfunction.GetDataset("SELECT count(*) No, MONTH(h.orderdate) Date,  FORMAT(SUM(h.netTotal),2) AS Total FROM v_Invoice h WHERE  (YEAR(h.invoiceDate) = YEAR(CURDATE())  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE()))  AND isCreditMemo != 1 GROUP BY MONTH(h.invoiceDate);");
                    model.YTDList = (from row in ds.Tables[0].AsEnumerable()
                                     select new DashboardLineChartViewModel
                                     {
                                         Month = Convert.ToInt16(row["Date"]),
                                         Poammount = Convert.ToDecimal(row["Total"]),
                                         ordernum = Convert.ToString(row["No"]),
                                     }).ToList();

                    ds = dbfunction.GetDataset("SELECT   count(*) No, MONTH(h.orderdate) Date, FORMAT(SUM(h.netTotal),2) AS Total FROM v_Invoice h WHERE  (YEAR(h.invoiceDate) = (YEAR(CURDATE())-1)  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE()))  AND isCreditMemo != 1 GROUP BY MONTH(h.invoiceDate);");
                    model.LYTDList = (from row in ds.Tables[0].AsEnumerable()
                                      select new DashboardLineChartViewModel
                                      {
                                          Month = Convert.ToInt16(row["Date"]),
                                          Poammount = Convert.ToDecimal(row["Total"]),
                                          ordernum = Convert.ToString(row["No"]),
                                      }).ToList();

                }
                else if (boxName == "credits")
                {

                    ds = dbfunction.GetDataset("SELECT  count(*) No, MONTH(h.orderdate) Date, FORMAT(SUM(h.netTotal),2)Total FROM v_Invoice h WHERE (YEAR(h.invoiceDate) = YEAR(CURDATE())  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE())) AND    isCreditMemo = 1 GROUP BY MONTH(h.invoiceDate);");
                    model.YTDList = (from row in ds.Tables[0].AsEnumerable()
                                     select new DashboardLineChartViewModel
                                     {
                                         Month = Convert.ToInt16(row["Date"]),
                                         Poammount = Convert.ToDecimal(row["Total"]),
                                         ordernum = Convert.ToString(row["No"]),
                                     }).ToList();

                    ds = dbfunction.GetDataset("SELECT   count(*) No, MONTH(h.orderdate) Date, FORMAT(SUM(h.netTotal),2)Total FROM v_Invoice h WHERE (YEAR(h.invoiceDate) = (YEAR(CURDATE())-1)  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE())) AND    isCreditMemo = 1 GROUP BY MONTH(h.invoiceDate);");
                    model.LYTDList = (from row in ds.Tables[0].AsEnumerable()
                                      select new DashboardLineChartViewModel
                                      {
                                          Month = Convert.ToInt16(row["Date"]),
                                          Poammount = Convert.ToDecimal(row["Total"]),
                                          ordernum = Convert.ToString(row["No"]),
                                      }).ToList();

                }
                else if (boxName == "boxes")
                {

                    ds = dbfunction.GetDataset("SELECT  count(*) No, MONTH(h.orderdate) Date, FORMAT(SUM(qtyshipped),0) AS Total FROM v_Invoice h, v_Invoicelines d WHERE d.invoicenum = h.invoicenum AND (YEAR(h.invoiceDate) = YEAR(CURDATE())  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE())) AND isCreditMemo != 1  GROUP BY MONTH(h.invoiceDate); ");
                    model.YTDList = (from row in ds.Tables[0].AsEnumerable()
                                     select new DashboardLineChartViewModel
                                     {
                                         Month = Convert.ToInt16(row["Date"]),
                                         Poammount = Convert.ToDecimal(row["Total"]),
                                         ordernum = Convert.ToString(row["No"]),
                                     }).ToList();


                    ds = dbfunction.GetDataset("SELECT SELECT  count(*) No, MONTH(h.orderdate) Date, FORMAT(SUM(qtyshipped),0) AS Total FROM v_Invoice h, v_Invoicelines d WHERE d.invoicenum = h.invoicenum AND (YEAR(h.invoiceDate) = (YEAR(CURDATE())-1)  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE())) AND isCreditMemo != 1 GROUP BY MONTH(h.invoiceDate);");
                    model.LYTDList = (from row in ds.Tables[0].AsEnumerable()
                                      select new DashboardLineChartViewModel
                                      {
                                          Month = Convert.ToInt16(row["Date"]),
                                          Poammount = Convert.ToDecimal(row["Total"]),
                                          ordernum = Convert.ToString(row["No"]),
                                      }).ToList();

                }

            }
            catch (Exception ex)
            {
            }

            return Json(model);
        }

        [HttpPost]
        public JsonResult GetBoxAmount(string boxName)
        {
            BoxAmountViewModel model = new BoxAmountViewModel();
            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = new DataSet();
                if (boxName == "orders")
                {
                    ds = dbfunction.GetDataset("SELECT   FORMAT(SUM(h.poammount),2) AS Total FROM orders h WHERE (YEAR(h.orderdate) = YEAR(CURDATE())  AND MONTH(h.orderdate) = MONTH(CURDATE()) AND DAY(h.orderdate) = DAY(CURDATE()))  GROUP BY YEAR(h.orderdate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.TodayAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.TodayAmount = "0";
                    }

                    ds = dbfunction.GetDataset("SELECT   FORMAT(SUM(h.poammount),2) AS Total FROM orders h WHERE YEAR(h.orderdate) = YEAR(CURDATE())  AND MONTH(h.orderdate) <= MONTH(CURDATE()) AND DAY(h.orderdate) <= DAY(CURDATE())   GROUP BY YEAR(h.orderdate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.YTDAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.YTDAmount = "0";
                    }

                    ds = dbfunction.GetDataset("SELECT  FORMAT(SUM(h.poammount),2) AS Total FROM orders h WHERE YEAR(h.orderdate) = (YEAR(CURDATE())-1)  GROUP BY YEAR(h.orderdate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.LastYearAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.LastYearAmount = "0";
                    }

                }
                if (boxName == "sales")
                {
                    ds = dbfunction.GetDataset(" SELECT  FORMAT(SUM(h.netTotal),2) AS Total FROM v_Invoice h WHERE  (YEAR(h.invoiceDate) = YEAR(CURDATE())  AND MONTH(h.invoiceDate) = MONTH(CURDATE()) AND DAY(h.invoiceDate) = DAY(CURDATE()))  AND isCreditMemo != 1 GROUP BY YEAR(h.invoiceDate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.TodayAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.TodayAmount = "0";
                    }


                    ds = dbfunction.GetDataset(" SELECT   FORMAT(SUM(h.netTotal),2) AS Total FROM v_Invoice h WHERE  (YEAR(h.invoiceDate) = YEAR(CURDATE())  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE()))  AND isCreditMemo != 1 GROUP BY YEAR(h.invoiceDate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.YTDAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.YTDAmount = "0";
                    }

                    ds = dbfunction.GetDataset("SELECT   FORMAT(SUM(h.netTotal),2) AS Total FROM v_Invoice h WHERE  (YEAR(h.invoiceDate) = (YEAR(CURDATE())-1)  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE()))  AND isCreditMemo != 1 GROUP BY YEAR(h.invoiceDate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.LYTDAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.LYTDAmount = "0";
                    }

                    ds = dbfunction.GetDataset("SELECT   FORMAT(SUM(h.netTotal),2) AS Total FROM v_Invoice h WHERE  (YEAR(h.invoiceDate) = (YEAR(CURDATE())-1) )  AND isCreditMemo != 1 GROUP BY YEAR(h.invoiceDate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.LastYearAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.LastYearAmount = "0";
                    }

                }
                if (boxName == "credits")
                {
                    ds = dbfunction.GetDataset("SELECT    FORMAT(SUM(h.netTotal),2)Total  FROM v_Invoice h WHERE (YEAR(h.invoiceDate) = YEAR(CURDATE())  AND MONTH(h.invoiceDate) = MONTH(CURDATE()) AND DAY(h.invoiceDate) = DAY(CURDATE())) AND    isCreditMemo = 1 GROUP BY YEAR(h.invoiceDate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.TodayAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.TodayAmount = "0";
                    }


                    ds = dbfunction.GetDataset("SELECT  FORMAT(SUM(h.netTotal),2)Total FROM v_Invoice h WHERE (YEAR(h.invoiceDate) = YEAR(CURDATE())  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE())) AND    isCreditMemo = 1 GROUP BY YEAR(h.invoiceDate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.YTDAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.YTDAmount = "0";
                    }

                    ds = dbfunction.GetDataset("SELECT   FORMAT(SUM(h.netTotal),2)Total FROM v_Invoice h WHERE (YEAR(h.invoiceDate) = (YEAR(CURDATE())-1)  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE())) AND    isCreditMemo = 1 GROUP BY YEAR(h.invoiceDate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.LYTDAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.LYTDAmount = "0";
                    }

                    ds = dbfunction.GetDataset("SELECT   FORMAT(SUM(h.netTotal),2)Total FROM v_Invoice h WHERE (YEAR(h.invoiceDate) = (YEAR(CURDATE())-1)  ) AND    isCreditMemo = 1 GROUP BY YEAR(h.invoiceDate);");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.LastYearAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                    }
                    else
                    {
                        model.LastYearAmount = "0";
                    }
                }
                if (boxName == "boxes")
                {
                    ds = dbfunction.GetDataset("SELECT   FORMAT(SUM(qtyshipped),0) AS Total FROM v_Invoice h, v_Invoicelines d WHERE d.invoicenum = h.invoicenum AND (YEAR(h.invoiceDate) = YEAR(CURDATE())  AND MONTH(h.invoiceDate) = MONTH(CURDATE()) AND DAY(h.invoiceDate) = DAY(CURDATE())) AND isCreditMemo != 1;");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.TodayAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        model.TodayAmount = model.TodayAmount == "" ? "0" : model.TodayAmount;
                    }
                    else
                    {
                        model.TodayAmount = "0";
                    }


                    ds = dbfunction.GetDataset("SELECT   FORMAT(SUM(qtyshipped),0) AS Total FROM v_Invoice h, v_Invoicelines d WHERE d.invoicenum = h.invoicenum AND (YEAR(h.invoiceDate) = YEAR(CURDATE())  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE())) AND isCreditMemo != 1; ");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.YTDAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        model.YTDAmount = model.YTDAmount == "" ? "0" : model.YTDAmount;
                    }
                    else
                    {
                        model.YTDAmount = "0";
                    }

                    ds = dbfunction.GetDataset("SELECT  FORMAT(SUM(qtyshipped),0) AS Total FROM v_Invoice h, v_Invoicelines d WHERE d.invoicenum = h.invoicenum AND (YEAR(h.invoiceDate) = (YEAR(CURDATE())-1)  AND MONTH(h.invoiceDate) <= MONTH(CURDATE()) AND DAY(h.invoiceDate) <= DAY(CURDATE())) AND isCreditMemo != 1;");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.LYTDAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        model.LYTDAmount = model.LYTDAmount == "" ? "0" : model.LYTDAmount;

                    }
                    else
                    {
                        model.LYTDAmount = "0";
                    }

                    ds = dbfunction.GetDataset("SELECT   FORMAT(SUM(qtyshipped),0) AS Total FROM v_Invoice h, v_Invoicelines d WHERE d.invoicenum = h.invoicenum AND (YEAR(h.invoiceDate) = (YEAR(CURDATE())-1)  ) AND isCreditMemo != 1;");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.LastYearAmount = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        model.LastYearAmount = model.LastYearAmount == "" ? "0" : model.LastYearAmount;

                    }
                    else
                    {
                        model.LastYearAmount = "0";
                    }

                }

            }
            catch (Exception ex)
            {
            }

            return Json(model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}