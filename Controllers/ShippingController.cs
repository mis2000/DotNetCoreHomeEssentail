using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySqlBasicCore.Filters;
using MySqlBasicCore.Models;
using MySqlBasicCore.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace MySqlBasicCore.Controllers
{
    [Authentication]
    public class ShippingController : Controller
    {
        private readonly IOptions<Appsettings> _appSettings;
        private readonly DBContext _dbContext;

        public ShippingController(IOptions<Appsettings> appSettings, DBContext dbContext)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
        }

        public ActionResult BOL()
        {
            List<Bol_1_ViewModel> bol_1_List = new List<Bol_1_ViewModel>();
            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("select bol_1.*,orders.name,orders.shipname,orders.shipaddress1,orders.shipaddress2,orders.shipaddress3,orders.clerk,bol2_PO from bol_1 join bol_2 on (bol_1.bol1_no = bol_2.bol2_No  and  bol_1.bol1_order_no = bol_2.bol2_order_no) left join orders on orders.ordernum = bol_1.bol1_order_no;");

                bol_1_List = (from row in ds.Tables[0].AsEnumerable()
                              select new Bol_1_ViewModel
                              {
                                  bol1_no = Convert.ToString(row["bol1_no"]),
                                  bol1_date = Convert.ToString(row["bol1_date"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["bol1_date"]),
                                  bol1_custnum = Convert.ToString(row["bol1_custnum"]),
                                  bol1_name = Convert.ToString(row["name"]),
                                  bol1_sname = Convert.ToString(row["shipname"]),
                                  bol1_adrs1 = Convert.ToString(row["shipaddress1"]),
                                  bol1_adrs2 = Convert.ToString(row["shipaddress2"]),
                                  bol1_adrs3 = Convert.ToString(row["shipaddress3"]),
                                  bol1_city = Convert.ToString(row["bol1_city"]),
                                  bol1_state = Convert.ToString(row["bol1_state"]),
                                  bol1_zip = Convert.ToString(row["bol1_zip"]),
                                  bol1_Lctn = Convert.ToString(row["bol1_Lctn"]),
                                  bol1_pro_no = Convert.ToString(row["bol1_pro_no"]),
                                  bol1_scac = Convert.ToString(row["bol1_scac"]),
                                  bol1_frght_terms = Convert.ToString(row["bol1_frght_terms"]),
                                  bol1_ttl_pkgs = Convert.ToString(row["bol1_ttl_pkgs"]) == "" ? (Int32?)null : Convert.ToInt32(row["bol1_ttl_pkgs"]),
                                  bol1_ttl_weight = Convert.ToString(row["bol1_ttl_weight"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["bol1_ttl_weight"]),
                                  bol1_ttlValue = Convert.ToString(row["bol1_ttlValue"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["bol1_ttlValue"]),
                                  bol1_HE_WH = Convert.ToString(row["bol1_HE_WH"]),
                                  bol1_order_no = Convert.ToString(row["bol1_order_no"]),
                                  bol1_ref = Convert.ToString(row["clerk"]),
                                  bol1_PO_No = Convert.ToString(row["bol2_PO"]),
                                  bol1_carrierName = Convert.ToString(row["bol1_carrierName"]),
                                  bol1_carrierPhone = Convert.ToString(row["bol1_carrierPhone"]),
                                  bol1_PkupDate = Convert.ToString(row["bol1_PkupDate"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["bol1_PkupDate"]),
                                  bol1_pkupTime = Convert.ToString(row["bol1_pkupTime"])
                              }).ToList();
            }
            catch (Exception Ex)
            {
            }
            return View(bol_1_List);
        }

        public ActionResult BOLDetail(string id)
        {
            List<Bol_2_ViewModel> bol_2_List = new List<Bol_2_ViewModel>();
            try
            {
                if (String.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index");
                }
                ViewBag.OrderNo = id;
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("select * from bol_2 where bol2_No =" + id);

                bol_2_List = (from row in ds.Tables[0].AsEnumerable()
                              select new Bol_2_ViewModel
                              {
                                  bol2_order_no = Convert.ToString(row["bol2_order_no"]) == "" ? (int?)null : Convert.ToInt32(row["bol2_order_no"]),
                                  bol2_pkgs = Convert.ToString(row["bol2_pkgs"]) == "" ? (int?)null : Convert.ToInt32(row["bol2_pkgs"]),
                                  bol2_weight = Convert.ToString(row["bol2_weight"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["bol2_weight"]),
                                  bol2_value = Convert.ToString(row["bol2_value"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["bol2_value"]),
                              }).ToList();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

            return View(bol_2_List);
        }

        public ActionResult Import_BOL()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Import_BOL(IFormFile file)
        {

            try
            {


                if (file == null || file.Length == 0)
                    return Content("file not selected");


                var bolLists = new List<TovBol>();
                using (var sreader = new StreamReader(file.OpenReadStream()))
                {
                    //First line is header. If header is not passed in csv then we can neglect the below line.
                    string[] headers = sreader.ReadLine().Split(',');
                    //Loop through the records
                    while (!sreader.EndOfStream)
                    {
                        string[] rows = sreader.ReadLine().Split(',');
                        string TovBol_custnum = "";
                        long TovBol_Ordernum = 0;
                        string TovBol_PO = "";
                        DateTime TovBol_OrderDate;
                        DateTime TovBol_CancelDate;
                        DateTime TovBol_ShipDate;
                        DateTime TovBol_UpdateDate;
                        long TovBol_Bol = 0;
                        long TovBol_Ref;
                        long TovBol_Boxes;
                        decimal TovBol_Value;
                        string TovBol_scac;
                        long TovBol_pro;
                        string TovBol_Whse;
                        string TovBol_freightTerms;

                        TovBol_custnum = Convert.ToString(rows[0]);
                        long.TryParse(Convert.ToString(rows[1]), out TovBol_Ordernum);
                        TovBol_PO = (Convert.ToString(rows[2]));
                        DateTime.TryParse(Convert.ToString(rows[3]), out TovBol_OrderDate);
                        DateTime.TryParse(Convert.ToString(rows[4]), out TovBol_CancelDate);
                        DateTime.TryParse(Convert.ToString(rows[5]), out TovBol_ShipDate);
                        DateTime.TryParse(Convert.ToString(rows[6]), out TovBol_UpdateDate);
                        long.TryParse(Convert.ToString(rows[7]), out TovBol_Bol);
                        long.TryParse(Convert.ToString(rows[8]), out TovBol_Ref);
                        long.TryParse(Convert.ToString(rows[9]), out TovBol_Boxes);
                        decimal.TryParse(Convert.ToString(rows[10]), out TovBol_Value);
                        TovBol_scac = Convert.ToString(rows[11]);
                        long.TryParse(Convert.ToString(rows[12]), out TovBol_pro);
                        TovBol_Whse = Convert.ToString(rows[13]);
                        TovBol_freightTerms = Convert.ToString(rows[14]);
                        


                        bolLists.Add(new TovBol
                        {
                            TovBol_custnum = TovBol_custnum,
                            TovBol_Ordernum = TovBol_Ordernum,
                            TovBol_PO = TovBol_PO,
                            TovBol_OrderDate = TovBol_OrderDate,
                            TovBol_CancelDate = TovBol_CancelDate,
                            TovBol_ShipDate = TovBol_ShipDate,
                            TovBol_UpdateDate = TovBol_UpdateDate,
                            TovBol_Bol = TovBol_Bol,
                            TovBol_Ref = TovBol_Ref,
                            TovBol_Boxes = TovBol_Boxes,
                            TovBol_Value = TovBol_Value,
                            TovBol_scac = TovBol_scac,
                            TovBol_pro = TovBol_pro,
                            TovBol_Whse = TovBol_Whse,
                            TovBol_freightTerms = TovBol_freightTerms,
                        });
                    }
                }

                _dbContext.tbl_TovBol.AddRange(bolLists);
                _dbContext.SaveChanges();

                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("call import_BOL();");
                ViewBag.imported = 1;


            }
            catch (Exception ex)
            {

                ViewBag.imported = 0;
            }
            return View();
        }


        [HttpPost]
        public JsonResult UpdateBolDetail(string bol1_no, string bol1_order_no, string column,string bol1_carrierName, string bol1_carrierPhone, DateTime ? bol1_PkupDate, DateTime ? bol1_pkupTime)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ItemclassViewModel model = new ItemclassViewModel();
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                var query = "";
                if (column== "bol1_carrierName")
                {
                    query = "Update bol_1 set bol1_carrierName='"+ bol1_carrierName + "'  where bol1_no ='" + bol1_no + "' and  bol1_order_no='" + bol1_order_no + "' ";
                }
                else if (column == "bol1_carrierPhone")
                {
                    query = "Update bol_1 set bol1_carrierPhone='" + bol1_carrierPhone + "'  where bol1_no ='" + bol1_no + "' and  bol1_order_no='" + bol1_order_no + "' ";
                }
                else if (column == "bol1_PkupDate")
                {
                    query = "Update bol_1 set bol1_PkupDate='" + bol1_PkupDate.Value.ToString("yyyy/MM/dd") + "'   where bol1_no ='" + bol1_no + "' and  bol1_order_no='" + bol1_order_no + "' ";
                }
                else  
                {
                    query = "Update bol_1 set bol1_pkupTime='" + bol1_pkupTime.Value.ToString("HH:mm") + "'  where bol1_no ='" + bol1_no + "' and  bol1_order_no='" + bol1_order_no + "' ";
                }
                
                DataSet ds = dbfunction.GetDataset(query);
                response.Status = "1";
                response.Message = "Detail updated successfully";
            }
            catch (Exception ex)
            {
            }

            return Json(response);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}