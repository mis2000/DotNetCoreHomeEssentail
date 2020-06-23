using AutoMapper;
using AutoMapper.QueryableExtensions;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlBasicCore.Filters;
using MySqlBasicCore.Models;
using MySqlBasicCore.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MySqlBasicCore.Controllers
{
    [Authentication]
    public class OrderController : Controller
    {
        private readonly IOptions<Appsettings> _appSettings;
        private readonly DBContext _dbContext;
        private readonly IConfigurationProvider _mappingConfiguration;

        public OrderController(IOptions<Appsettings> appSettings, DBContext dbContext, IConfigurationProvider mappingConfiguration)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
            _mappingConfiguration = mappingConfiguration;
        }

        public ActionResult List()
        {
            return View("List", new Order_MasterViewModel());
        }

        [HttpPost]
        public ActionResult LoadOrderDatatable([FromBody]JqueryDataTablesParameters param)
        {
            List<Order_MasterViewModel> OrderList = new List<Order_MasterViewModel>();
            try
            {
                HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonSerializer.Serialize(param));
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("Select *,NoteCount from orders left join (select Ordernum,count(*)NoteCount from ordernotes group by Ordernum ) a on  orders.Ordernum = a.Ordernum order by Orderdate desc limit 50;");
                OrderList = (from row in ds.Tables[0].AsEnumerable()
                             select new Order_MasterViewModel
                             {
                                 Custnum = Convert.ToString(row["Custnum"]),
                                 ordernum = Convert.ToString(row["ordernum"]),
                                 Orderdate = Convert.ToDateTime(row["Orderdate"]),
                                 Credit = Convert.ToString(row["Credit"]),
                                 Freight = Convert.ToDecimal(row["Freight"]),
                                 Slnum = Convert.ToString(row["Slnum"]),
                                 Slnum2 = Convert.ToString(row["Slnum2"]),
                                 D0 = Convert.ToString(row["D0"]),
                                 Name = Convert.ToString(row["Name"]),
                                 Address1 = Convert.ToString(row["Address1"]),
                                 Address2 = Convert.ToString(row["Address2"]),
                                 Address3 = Convert.ToString(row["Address3"]),
                                 Shipname = Convert.ToString(row["Shipname"]),
                                 Shipaddress1 = Convert.ToString(row["Shipaddress1"]),
                                 Shipaddress2 = Convert.ToString(row["Shipaddress2"]),
                                 Shipaddress3 = Convert.ToString(row["Shipaddress3"]),
                                 Terms = Convert.ToString(row["Terms"]),
                                 Via = Convert.ToString(row["Via"]),
                                 Backorder = Convert.ToString(row["Backorder"]),
                                 Ponum = Convert.ToString(row["Ponum"]),
                                 Shippeddate = Convert.ToString(row["Shippeddate"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["Shippeddate"]),
                                 Shipdate = Convert.ToDateTime(row["Shipdate"]),
                                 Canceldate = Convert.ToString(row["Canceldate"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["Canceldate"]),
                                 Edidate = Convert.ToDateTime(row["Edidate"]),
                                 Custnote = Convert.ToString(row["Custnote"]),
                                 clerk = Convert.ToString(row["clerk"]),
                                 Poammount = Convert.ToDecimal(row["Poammount"]),
                                 Commission = Convert.ToDecimal(row["Commission"]),
                                 Status = Convert.ToString(row["Status"]),
                                 D1 = Convert.ToString(row["D1"]),
                                 D2 = Convert.ToString(row["D2"]),
                                 Creditmemo = Convert.ToString(row["Creditmemo"]),
                                 Storenum = Convert.ToString(row["Storenum"]),
                                 Dept = Convert.ToString(row["Dept"]),
                                 Ordertype = Convert.ToString(row["Ordertype"]),
                                 WeborderId = Convert.ToString(row["WeborderId"]),
                                 IsOpenOrder = Convert.ToString(row["IsOpenOrder"]),
                                 NoteCount = Convert.ToString(row["NoteCount"]) == "" ?0 : Convert.ToInt32(row["NoteCount"]),
                             }).ToList();

                return new JsonResult(new JqueryDataTablesResult<Order_MasterViewModel>
                {
                    Draw = param.Draw,
                    Data = OrderList,
                    RecordsFiltered = 10,
                    RecordsTotal = 10
                });
            }
            catch (Exception Ex)
            {
            }
            return View("List", OrderList);
        }

        public ActionResult Detail(string id)
        {
            List<Order_DetailViewModel> order_DetailList = new List<Order_DetailViewModel>();
            try
            {
                if (String.IsNullOrEmpty(id))
                {
                    return RedirectToAction("List");
                }

                ViewBag.OrderNo = id;
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("select * from orderlines where Ordernum ='" + id + "' order by Itemnum");
                order_DetailList = (from row in ds.Tables[0].AsEnumerable()
                                    select new Order_DetailViewModel
                                    {
                                        Ordernum = Convert.ToString(row["Ordernum"]),
                                        Itemnum = Convert.ToString(row["Itemnum"]),
                                        Qtyordered = Convert.ToInt32(row["Qtyordered"]),
                                        Qtyinvoiced = Convert.ToInt32(row["Qtyinvoiced"]),
                                        Saleprice = Convert.ToDecimal(row["Saleprice"]),
                                        Listprice = Convert.ToDecimal(row["Listprice"]),
                                        Commission = Convert.ToDecimal(row["Commission"]),
                                    }).ToList();
            }
            catch (Exception ex)
            {
                return RedirectToAction("List");
            }

            return View(order_DetailList);
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody]JqueryDataTablesParameters param)
        {
            try
            {
                HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonSerializer.Serialize(param));
                List<Order_MasterViewModel> list = new List<Order_MasterViewModel>();

                var showAll = param.AdditionalValues.Last();
                string queryDef = "";
                if (showAll=="true")
                {
                    queryDef = @"SELECT Custnum,orders.ordernum,Orderdate,Credit,Freight, Slnum, Slnum2, D0, NAME, Address1, Address2, Address3, Shipname, Shipaddress1, Shipaddress2, Shipaddress3, Terms, Via,
                                                                              Backorder,Tax,Terminal, Ponum,Shippeddate, Shipdate, Canceldate, Edidate, Custnote, clerk, Poammount, Commission, STATUS, D1, D2, Creditmemo, Storenum,
                                                                              Dept, Ordertype, WeborderId, IsOpenOrder, NoteCount,'a' AS ACTION  FROM orders LEFT JOIN (SELECT Ordernum,COUNT(*)NoteCount,YEAR FROM ordernotes
                                                                              GROUP BY Ordernum,YEAR  ) a ON  (SUBSTRING((orders.Ordernum ),1,6)= a.Ordernum AND  SUBSTRING((orders.Ordernum ),8,2)= a.Year)
                                                                           ";
                }
                else
                {
                    queryDef = @"SELECT Custnum,orders.ordernum,Orderdate,Credit,Freight, Slnum, Slnum2, D0, NAME, Address1, Address2, Address3, Shipname, Shipaddress1, Shipaddress2, Shipaddress3, Terms, Via,
                                                                              Backorder,Tax,Terminal, Ponum,Shippeddate, Shipdate, Canceldate, Edidate, Custnote, clerk, Poammount, Commission, STATUS, D1, D2, Creditmemo, Storenum,
                                                                              Dept, Ordertype, WeborderId, IsOpenOrder, NoteCount,'a' AS ACTION  FROM orders LEFT JOIN (SELECT Ordernum,COUNT(*)NoteCount,YEAR FROM ordernotes
                                                                              GROUP BY Ordernum,YEAR  ) a ON  (SUBSTRING((orders.Ordernum ),1,6)= a.Ordernum AND  SUBSTRING((orders.Ordernum ),8,2)= a.Year)
                                                                              WHERE IsOpenOrder =1                                                                          
                                                                            ";
                }

              
               IQueryable<Order> query = _dbContext.tbl_Orders.FromSqlRaw(queryDef).AsNoTracking();
                

                query = SearchOptionsProcessor<Order_MasterViewModel, Order>.Apply(query, param.Columns);
                query = SortOptionsProcessor<Order_MasterViewModel, Order>.Apply(query, param);

                var size = await query.CountAsync();
                var orderNum = "";
                var custnumOrName = "";
                var Shipdate = "";
                DateTime ShipDateD = DateTime.Now;
                var i = 0;
                var additionalValues = param.AdditionalValues.SkipLast(1);
                foreach (var column in additionalValues)
                {
                    if (i == 0)
                    {
                        orderNum = column;
                    }
                    else if (i == 1)
                    {
                        custnumOrName = column;
                    }
                    else
                    {
                        Shipdate = column;
                        if (Convert.ToString(Shipdate) == "1/1/0001 12:00:00 AM")
                        {
                            Shipdate = "";
                        }
                        if (Shipdate != "")
                        {
                            DateTime.TryParse(Shipdate, out ShipDateD);
                        }

                    }
                    i++;
                }

                if (orderNum != "" || custnumOrName != "" || Shipdate != "")
                {
                    query = query.Where(w =>
                                   ((orderNum == "" ? true : w.ordernum.Contains(orderNum)))
                                   && ((custnumOrName == "" ? true : (w.Custnum.Contains(custnumOrName) ? true : (w.Name.Contains(custnumOrName)))))
                                     && ((Shipdate == "" ? true : w.Shipdate == ShipDateD)));
                }


                foreach (var column in param.Columns)
                {
                    if (column.Data == "ordernum")
                    {
                        column.Searchable = true;
                        column.Search.Value = orderNum;
                    }
                    if (column.Data == "Shipdate")
                    {
                        column.Searchable = true;
                        column.Search.Value = orderNum;
                    }
                    if (column.Data == "Shipdate")
                    {
                        column.Searchable = true;
                        column.Search.Value = orderNum;
                    }
                }

            

                var items = await query
                    .AsNoTracking()
                    .Skip((param.Start / param.Length) * param.Length)
                    .Take(param.Length)
                    .ProjectTo<Order_MasterViewModel>(_mappingConfiguration)
                    .ToArrayAsync();


                var result = new JqueryDataTablesPagedResults<Order_MasterViewModel>
                {
                    Items = items,
                    TotalSize = size
                };

                return new JsonResult(new JqueryDataTablesResult<Order_MasterViewModel>
                {
                    Draw = param.Draw,
                    Data = result.Items,
                    RecordsFiltered = result.TotalSize,
                    RecordsTotal = result.TotalSize
                });
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> LoadTable1([FromBody]JqueryDataTablesParameters param)
        {
            try
            {
                HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonSerializer.Serialize(param));
                List<Order_MasterViewModel> list = new List<Order_MasterViewModel>();
                var mySqlQuery = "select count(*) from orders";
                var searchQuery = "";
                DbfunctionUtility dbfunctionUtility = new DbfunctionUtility(_appSettings);
                var dsResult = dbfunctionUtility.GetDataset(mySqlQuery);
                var totalRecords = Convert.ToInt32(dsResult.Tables[0].Rows[0][0]);
                var searchRecords = 0;
                mySqlQuery = "SET @row_number = 0; SELECT t.* FROM  (  SELECT *, (@row_number:=@row_number + 1) AS RowNo  FROM orders )t  where 1=1  ";
                var searchCount = 0;

                var orderNum = "";
                var custnumOrName = "";
                var Shipdate = "";
                var i = 0;
                foreach (var column in param.AdditionalValues)
                {
                    if (i == 0)
                    {
                        orderNum = column;
                    }
                    else if (i == 1)
                    {
                        custnumOrName = column;
                    }
                    else
                    {
                        Shipdate = column;
                    }
                    i++;
                }

                if (!string.IsNullOrEmpty(orderNum))
                {
                    searchCount++;
                    mySqlQuery += " and  ordernum   like '%" + orderNum + "%' ";
                }

                if (!string.IsNullOrEmpty(Shipdate) && (Convert.ToString(Shipdate) != "1/1/0001 12:00:00 AM"))
                {
                    searchCount++;
                    mySqlQuery += " and   DATE_FORMAT(Shipdate, '%m/%d/%Y') = '" + Shipdate + "' ";
                }
                if (!string.IsNullOrEmpty(custnumOrName))
                {
                    searchCount++;
                    mySqlQuery += " and  Custnum   like '%" + custnumOrName + "%' ";
                }
                var orderByColumn = param.Columns[param.Order[0].Column];
                mySqlQuery += " and RowNo > " + ((param.Start / param.Length) * param.Length) + "  ";
                if (orderByColumn.Data == "Action")
                {
                    mySqlQuery += " order by Orderdate desc ";
                }
                else
                {
                    mySqlQuery += " order by  " + orderByColumn.Data + " " + param.Order[0].Dir + "";
                }

                mySqlQuery += " limit " + param.Length;
                if (param.Start == 0 && searchCount == 0)
                {
                    mySqlQuery = "select * from orders order by Orderdate desc limit 50";
                }
                var queryResult = _dbContext.tbl_Orders.FromSqlRaw(mySqlQuery).ToList<Order>();
                if (queryResult.Count() == 0 && (!string.IsNullOrEmpty(custnumOrName)))
                {
                    mySqlQuery = "SET @row_number = 0; SELECT t.* FROM  (  SELECT *, (@row_number:=@row_number + 1) AS RowNo  FROM orders )t  where 1=1  ";
                    searchCount = 0;

                    if (!string.IsNullOrEmpty(orderNum))
                    {
                        searchCount++;
                        mySqlQuery += " and  ordernum   like '%" + orderNum + "%' ";
                    }

                    if (!string.IsNullOrEmpty(Shipdate))
                    {
                        searchCount++;
                        mySqlQuery += " and   DATE_FORMAT(Shipdate, '%m/%d/%Y') = '" + Shipdate + "' ";
                    }
                    if (!string.IsNullOrEmpty(custnumOrName))
                    {
                        searchCount++;
                        mySqlQuery += " and  Name   like '%" + custnumOrName + "%' ";
                    }
                    orderByColumn = param.Columns[param.Order[0].Column];
                    searchQuery = mySqlQuery;
                    mySqlQuery += " and RowNo > " + ((param.Start / param.Length) * param.Length) + "  ";
                    if (orderByColumn.Data == "Action")
                    {
                        mySqlQuery += " order by Orderdate desc ";
                    }
                    else
                    {
                        mySqlQuery += " order by  " + orderByColumn.Data + " " + param.Order[0].Dir + "";
                    }

                    mySqlQuery += " limit " + param.Length;
                }
                if (searchQuery != "")
                {
                    var searchDs = dbfunctionUtility.GetDataset(searchQuery);
                    try
                    {
                        searchRecords = Convert.ToInt32(searchDs.Tables[0].Rows[0][0]);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return new JsonResult(new JqueryDataTablesResult<Order>
                {
                    Draw = param.Draw,
                    Data = queryResult,
                    RecordsFiltered = (searchCount == 0 ? totalRecords : (searchRecords == 0 ? queryResult.Count() : searchRecords)),
                    RecordsTotal = totalRecords
                });
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }


        [HttpPost]
        public JsonResult GetOrderNotes(string ordernum, DateTime orderDate)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var year = orderDate.Year.ToString().Substring(2, 2);
                ItemclassViewModel model = new ItemclassViewModel();
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                var query = "select * from ordernotes where (SUBSTRING(('" + ordernum + " '),1,6)=  Ordernum AND  SUBSTRING(('" + ordernum + "'),8,2)= Year)  ";
                DataSet ds = dbfunction.GetDataset(query);
                if (ds.Tables.Count > 0)
                {
                    var notes = (from row in ds.Tables[0].AsEnumerable()
                                 select new OrderNoteViewModel
                                 {
                                     Line = Convert.ToString(row["Line"]) == "" ? 0 : Convert.ToInt32(row["Line"]),
                                     Year = Convert.ToString(row["Year"]),
                                     Ordernum = Convert.ToString(row["Ordernum"]),
                                     Note = Convert.ToString(row["Note"]),
                                 }).ToList();

                    return Json(notes);
                }
                return Json(response);
            }
            catch (Exception ex)
            {
            }

            return Json(response);
        }

    }
}