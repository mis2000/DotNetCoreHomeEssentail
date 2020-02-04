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
    public class InvoiceController : Controller
    {
        private readonly IOptions<Appsettings> _appSettings;
        private readonly DBContext _dbContext;
        private readonly IConfigurationProvider _mappingConfiguration;

        public InvoiceController(IOptions<Appsettings> appSettings, DBContext dbContext, IConfigurationProvider mappingConfiguration)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
            _mappingConfiguration = mappingConfiguration;
        }
        public IActionResult List()
        {
            return View(new InvoiceViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody]JqueryDataTablesParameters param)
        {
            try
            {
                HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), JsonSerializer.Serialize(param));
                List<Order_MasterViewModel> list = new List<Order_MasterViewModel>();

                IQueryable<Invoice> query = _dbContext.tbl_Invoice.FromSqlRaw("select custNum  ,invoiceNum  , cast( releaseDate as char)   releaseDate,freight  ,salesManNum  ,salesManNum2  ,billName  ,billAddress1 ,billAddress2 ,billAddress3 ,shipName ,shipAddress1,shipAddress2,shipAddress3,termId  ,carrierCode  ,d2  ,poNum  , cast( orderDate as char) orderDate, cast( shipDate as char) shipDate,  CAST( cancelDate AS CHAR) cancelDate,cast( enterDate as char) enterDate,terminal  ,custNote ,clerk  ,netTotal  ,commision ,d4  ,d6  ,storeNum  ,dept  ,orderType  ,bolNum  ,cast( bolDate as char) bolDate ,tracking1  ,tracking2 ,orderNum  ,cast( invoiceDate as char) invoiceDate  ,isCreditMemo  ,isCreditHold  ,isFreightCollect from  v_Invoice").AsNoTracking();

                query = SearchOptionsProcessor<InvoiceViewModel, Invoice>.Apply(query, param.Columns);
                query = SortOptionsProcessor<InvoiceViewModel, Invoice>.Apply(query, param);

                var size = await query.CountAsync();
                var InvoiceNum = "";
                var OrderNum = "";
                var BillName = "";

                var i = 0;
                foreach (var column in param.AdditionalValues)
                {
                    if (i == 0)
                    {
                        InvoiceNum = column;
                    }
                    else if (i == 1)
                    {
                        OrderNum = column;
                    }
                    else
                    {
                        BillName = column;


                    }
                    i++;
                }

                if (InvoiceNum != "" || OrderNum != "" || BillName != "")
                {
                    query = query.Where(w =>
                                   ((InvoiceNum == "" ? true : w.InvoiceNum.Contains(InvoiceNum)))
                                   && ((OrderNum == "" ? true : w.InvoiceNum.Contains(OrderNum)))
                                     && ((BillName == "" ? true : w.BillName.Contains(BillName))));
                }


                foreach (var column in param.Columns)
                {
                    if (column.Data == "InvoiceNum")
                    {
                        column.Searchable = true;
                        column.Search.Value = InvoiceNum;
                    }
                    if (column.Data == "OrderNum")
                    {
                        column.Searchable = true;
                        column.Search.Value = OrderNum;
                    }
                    if (column.Data == "BillName")
                    {
                        column.Searchable = true;
                        column.Search.Value = BillName;
                    }
                }
                var items = await query
                    .AsNoTracking()
                    .Skip((param.Start / param.Length) * param.Length)
                    .Take(param.Length)
                    .ProjectTo<InvoiceViewModel>(_mappingConfiguration)
                    .ToArrayAsync();


                var result = new JqueryDataTablesPagedResults<InvoiceViewModel>
                {
                    Items = items,
                    TotalSize = size
                };

                return new JsonResult(new JqueryDataTablesResult<InvoiceViewModel>
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

        public ActionResult Detail(string id)
        {
            List<InvoicelineViewModel> invoice_DetailList = new List<InvoicelineViewModel>();
            try
            {
                if (String.IsNullOrEmpty(id))
                {
                    return RedirectToAction("List");
                }

                ViewBag.Invoicenum = id;
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("select * from v_Invoicelines where invoicenum ='" + id + "' order by Itemnum");
                invoice_DetailList = (from row in ds.Tables[0].AsEnumerable()
                                      select new InvoicelineViewModel
                                      {
                                          Invoicenum = Convert.ToString(row["Invoicenum"]),
                                          Itemnum = Convert.ToString(row["Itemnum"]),
                                          Qtyordered = Convert.ToString(row["Qtyordered"]) == "" ? (Int32?)null : Convert.ToInt32(row["Qtyordered"]),
                                          Qtyshipped = Convert.ToString(row["Qtyshipped"]) == "" ? (Int32?)null : Convert.ToInt32(row["Qtyshipped"]),
                                          Saleprice = Convert.ToString(row["Saleprice"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Saleprice"]),
                                          Listprice = Convert.ToString(row["Listprice"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Listprice"]),
                                          Listlanded = Convert.ToString(row["Listlanded"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Listlanded"]),
                                          Commission = Convert.ToString(row["Commission"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Commission"]),
                                          Discount = Convert.ToString(row["Discount"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Discount"])
                                      }).ToList();
            }
            catch (Exception ex)
            {
                return RedirectToAction("List");
            }

            return View(invoice_DetailList);
        }
    }
}