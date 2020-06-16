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
                DataSet ds = dbfunction.GetDataset(@"select 
                                                    max(Line)Line, count(*)Count, a.bol1_no,MAX(bol1_CancelDate)bol1_CancelDate,max(name)name,max(bol1_date)bol1_date, max(bol2_custnum)bol2_custnum,max(bol1_name)bol1_name,max(bol1_adrs1)bol1_adrs1,max(bol1_adrs2)bol1_adrs2,max(bol1_adrs3)bol1_adrs3,max(bol1_city)bol1_city,max(bol1_state)bol1_state,max(bol1_zip)bol1_zip,max(bol1_Lctn)bol1_Lctn,max(bol1_pro_no)bol1_pro_no,max(bol1_scac)bol1_scac,max(bol1_frght_terms)bol1_frght_terms,max(bol1_ttl_pkgs)bol1_ttl_pkgs,max(bol1_ttl_weight)bol1_ttl_weight,max(bol1_ttlValue)bol1_ttlValue,max(bol1_HE_WH)bol1_HE_WH,max(bol2_order_no)bol2_order_no,max(bol1_ref)bol1_ref, max(bol1_PO_No)bol1_PO_No,max(bol1_carrierName)bol1_carrierName,max(bol1_carrierPhone)bol1_carrierPhone,max(bol1_PkupDate)bol1_PkupDate,max(bol1_pkupTime)bol1_pkupTime,max(Conformation)Conformation
                                                    from  (select  count(*)Count, bol_1.bol1_no,MAX(bol1_CancelDate)bol1_CancelDate,max(name)name,max(bol1_date)bol1_date, max(bol2_custnum)bol2_custnum,max(bol1_name)bol1_name,max(shipaddress1)bol1_adrs1,max(shipaddress2)bol1_adrs2,max(shipaddress3)bol1_adrs3,max(bol1_city)bol1_city,max(bol1_state)bol1_state,max(bol1_zip)bol1_zip,max(bol1_Lctn)bol1_Lctn,max(bol1_pro_no)bol1_pro_no,max(bol1_scac)bol1_scac,max(bol1_frght_terms)bol1_frght_terms,max(bol1_ttl_pkgs)bol1_ttl_pkgs,max(bol1_ttl_weight)bol1_ttl_weight,max(bol1_ttlValue)bol1_ttlValue,max(bol1_HE_WH)bol1_HE_WH,max(bol2_order_no)bol2_order_no,max(clerk)bol1_ref, max(bol2_PO)bol1_PO_No,max(bol1_carrierName)bol1_carrierName,max(bol1_carrierPhone)bol1_carrierPhone,max(bol1_PkupDate)bol1_PkupDate,max(bol1_pkupTime)bol1_pkupTime,max(Conformation)Conformation
                                                    from bol_1 join bol_2 on (bol_1.bol1_no = bol_2.bol2_No )  
                                                    join orders on orders.ordernum = bol_2.bol2_order_no
                                                    group by bol_1.bol1_no) a  left join ordernotes on a.bol2_order_no = ordernotes.ordernum
                                                    WHERE bol1_ttl_weight  IS NULL                                            
                                                    group by a.bol1_no");

                bol_1_List = (from row in ds.Tables[0].AsEnumerable()
                              select new Bol_1_ViewModel
                              {
                                  Line = Convert.ToString(row["Line"]) == "" ? 0 : Convert.ToInt32(row["Line"]),
                                  Count = Convert.ToInt32(row["count"]),
                                  bol1_no = Convert.ToString(row["bol1_no"]),
                                  bol1_date = Convert.ToString(row["bol1_date"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["bol1_date"]),
                                  bol2_custnum = Convert.ToString(row["bol2_custnum"]),
                                  bol1_name = Convert.ToString(row["name"]),
                                  bol1_adrs1 = Convert.ToString(row["bol1_adrs1"]),
                                  bol1_adrs2 = Convert.ToString(row["bol1_adrs2"]),
                                  bol1_adrs3 = Convert.ToString(row["bol1_adrs3"]),
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
                                  bol2_order_no = Convert.ToString(row["bol2_order_no"]),
                                  bol1_ref = Convert.ToString(row["bol1_ref"]),
                                  bol1_PO_No = Convert.ToString(row["bol1_PO_No"]),
                                  bol1_carrierName = Convert.ToString(row["bol1_carrierName"]),
                                  bol1_carrierPhone = Convert.ToString(row["bol1_carrierPhone"]),
                                  bol1_PkupDate = Convert.ToString(row["bol1_PkupDate"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["bol1_PkupDate"]),
                                  bol1_CancelDate = Convert.ToString(row["bol1_CancelDate"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["bol1_CancelDate"]),
                                  bol1_pkupTime = Convert.ToString(row["bol1_pkupTime"]),
                                  Conformation = Convert.ToString(row["Conformation"])

                              }).ToList();
            }
            catch (Exception Ex)
            {
            }
            return View(bol_1_List);
        }

        [HttpPost]
        public ActionResult BOL(string submit)
        {
            List<Bol_1_ViewModel> bol_1_List = new List<Bol_1_ViewModel>();
            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                string query = "";
                if (submit=="yes")
                {
                    query =                          @"select 
                                                    max(Line)Line, count(*)Count, a.bol1_no,MAX(bol1_CancelDate)bol1_CancelDate,max(name)name,max(bol1_date)bol1_date, max(bol2_custnum)bol2_custnum,max(bol1_name)bol1_name,max(bol1_adrs1)bol1_adrs1,max(bol1_adrs2)bol1_adrs2,max(bol1_adrs3)bol1_adrs3,max(bol1_city)bol1_city,max(bol1_state)bol1_state,max(bol1_zip)bol1_zip,max(bol1_Lctn)bol1_Lctn,max(bol1_pro_no)bol1_pro_no,max(bol1_scac)bol1_scac,max(bol1_frght_terms)bol1_frght_terms,max(bol1_ttl_pkgs)bol1_ttl_pkgs,max(bol1_ttl_weight)bol1_ttl_weight,max(bol1_ttlValue)bol1_ttlValue,max(bol1_HE_WH)bol1_HE_WH,max(bol2_order_no)bol2_order_no,max(bol1_ref)bol1_ref, max(bol1_PO_No)bol1_PO_No,max(bol1_carrierName)bol1_carrierName,max(bol1_carrierPhone)bol1_carrierPhone,max(bol1_PkupDate)bol1_PkupDate,max(bol1_pkupTime)bol1_pkupTime,max(Conformation)Conformation
                                                    from  (select  count(*)Count, bol_1.bol1_no,MAX(bol1_CancelDate)bol1_CancelDate,max(name)name,max(bol1_date)bol1_date, max(bol2_custnum)bol2_custnum,max(bol1_name)bol1_name,max(shipaddress1)bol1_adrs1,max(shipaddress2)bol1_adrs2,max(shipaddress3)bol1_adrs3,max(bol1_city)bol1_city,max(bol1_state)bol1_state,max(bol1_zip)bol1_zip,max(bol1_Lctn)bol1_Lctn,max(bol1_pro_no)bol1_pro_no,max(bol1_scac)bol1_scac,max(bol1_frght_terms)bol1_frght_terms,max(bol1_ttl_pkgs)bol1_ttl_pkgs,max(bol1_ttl_weight)bol1_ttl_weight,max(bol1_ttlValue)bol1_ttlValue,max(bol1_HE_WH)bol1_HE_WH,max(bol2_order_no)bol2_order_no,max(clerk)bol1_ref, max(bol2_PO)bol1_PO_No,max(bol1_carrierName)bol1_carrierName,max(bol1_carrierPhone)bol1_carrierPhone,max(bol1_PkupDate)bol1_PkupDate,max(bol1_pkupTime)bol1_pkupTime,max(Conformation)Conformation
                                                    from bol_1 join bol_2 on (bol_1.bol1_no = bol_2.bol2_No )  
                                                    join orders on orders.ordernum = bol_2.bol2_order_no
                                                    group by bol_1.bol1_no) a  left join ordernotes on a.bol2_order_no = ordernotes.ordernum
                                                    group by a.bol1_no";
                }
                else
                {
                    query =                         @"select 
                                                    max(Line)Line, count(*)Count, a.bol1_no,MAX(bol1_CancelDate)bol1_CancelDate,max(name)name,max(bol1_date)bol1_date, max(bol2_custnum)bol2_custnum,max(bol1_name)bol1_name,max(bol1_adrs1)bol1_adrs1,max(bol1_adrs2)bol1_adrs2,max(bol1_adrs3)bol1_adrs3,max(bol1_city)bol1_city,max(bol1_state)bol1_state,max(bol1_zip)bol1_zip,max(bol1_Lctn)bol1_Lctn,max(bol1_pro_no)bol1_pro_no,max(bol1_scac)bol1_scac,max(bol1_frght_terms)bol1_frght_terms,max(bol1_ttl_pkgs)bol1_ttl_pkgs,max(bol1_ttl_weight)bol1_ttl_weight,max(bol1_ttlValue)bol1_ttlValue,max(bol1_HE_WH)bol1_HE_WH,max(bol2_order_no)bol2_order_no,max(bol1_ref)bol1_ref, max(bol1_PO_No)bol1_PO_No,max(bol1_carrierName)bol1_carrierName,max(bol1_carrierPhone)bol1_carrierPhone,max(bol1_PkupDate)bol1_PkupDate,max(bol1_pkupTime)bol1_pkupTime,max(Conformation)Conformation
                                                    from  (select  count(*)Count, bol_1.bol1_no,MAX(bol1_CancelDate)bol1_CancelDate,max(name)name,max(bol1_date)bol1_date, max(bol2_custnum)bol2_custnum,max(bol1_name)bol1_name,max(shipaddress1)bol1_adrs1,max(shipaddress2)bol1_adrs2,max(shipaddress3)bol1_adrs3,max(bol1_city)bol1_city,max(bol1_state)bol1_state,max(bol1_zip)bol1_zip,max(bol1_Lctn)bol1_Lctn,max(bol1_pro_no)bol1_pro_no,max(bol1_scac)bol1_scac,max(bol1_frght_terms)bol1_frght_terms,max(bol1_ttl_pkgs)bol1_ttl_pkgs,max(bol1_ttl_weight)bol1_ttl_weight,max(bol1_ttlValue)bol1_ttlValue,max(bol1_HE_WH)bol1_HE_WH,max(bol2_order_no)bol2_order_no,max(clerk)bol1_ref, max(bol2_PO)bol1_PO_No,max(bol1_carrierName)bol1_carrierName,max(bol1_carrierPhone)bol1_carrierPhone,max(bol1_PkupDate)bol1_PkupDate,max(bol1_pkupTime)bol1_pkupTime,max(Conformation)Conformation
                                                    from bol_1 join bol_2 on (bol_1.bol1_no = bol_2.bol2_No )  
                                                    join orders on orders.ordernum = bol_2.bol2_order_no
                                                    group by bol_1.bol1_no) a  left join ordernotes on a.bol2_order_no = ordernotes.ordernum
                                                    WHERE bol1_ttl_weight  IS NULL                                            
                                                    group by a.bol1_no";

                }
                DataSet ds = dbfunction.GetDataset(query);

                bol_1_List = (from row in ds.Tables[0].AsEnumerable()
                              select new Bol_1_ViewModel
                              {
                                  Line = Convert.ToString(row["Line"]) == "" ? 0 : Convert.ToInt32(row["Line"]),
                                  Count = Convert.ToInt32(row["count"]),
                                  bol1_no = Convert.ToString(row["bol1_no"]),
                                  bol1_date = Convert.ToString(row["bol1_date"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["bol1_date"]),
                                  bol2_custnum = Convert.ToString(row["bol2_custnum"]),
                                  bol1_name = Convert.ToString(row["name"]),
                                  bol1_adrs1 = Convert.ToString(row["bol1_adrs1"]),
                                  bol1_adrs2 = Convert.ToString(row["bol1_adrs2"]),
                                  bol1_adrs3 = Convert.ToString(row["bol1_adrs3"]),
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
                                  bol2_order_no = Convert.ToString(row["bol2_order_no"]),
                                  bol1_ref = Convert.ToString(row["bol1_ref"]),
                                  bol1_PO_No = Convert.ToString(row["bol1_PO_No"]),
                                  bol1_carrierName = Convert.ToString(row["bol1_carrierName"]),
                                  bol1_carrierPhone = Convert.ToString(row["bol1_carrierPhone"]),
                                  bol1_PkupDate = Convert.ToString(row["bol1_PkupDate"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["bol1_PkupDate"]),
                                  bol1_CancelDate = Convert.ToString(row["bol1_CancelDate"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["bol1_CancelDate"]),
                                  bol1_pkupTime = Convert.ToString(row["bol1_pkupTime"]),
                                  Conformation = Convert.ToString(row["Conformation"])

                              }).ToList();
            }
            catch (Exception Ex)
            {
            }
            ViewBag.showAll = submit;
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
                DataSet ds = dbfunction.GetDataset("select * from bol_2  join orders on orders.ordernum = bol_2.bol2_order_no  where bol2_No =" + id);

                bol_2_List = (from row in ds.Tables[0].AsEnumerable()
                              select new Bol_2_ViewModel
                              {
                                  bol2_order_no = Convert.ToString(row["bol2_order_no"]) == "" ? (int?)null : Convert.ToInt32(row["bol2_order_no"]),
                                  bol2_PO_No = Convert.ToString(row["bol2_PO"]),
                                  bol2_pkgs = Convert.ToString(row["bol2_pkgs"]) == "" ? (int?)null : Convert.ToInt32(row["bol2_pkgs"]),
                                  bol2_weight = Convert.ToString(row["bol2_weight"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["bol2_weight"]),
                                  bol2_value = Convert.ToString(row["bol2_value"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["bol2_value"]),
                                  bol2_custnum = Convert.ToString(row["bol2_custnum"]),
                                  bol2_sname = Convert.ToString(row["shipname"]),

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


        public ActionResult Import_BOL_Notes()
        {

            return View();
        }
        
        [HttpPost]
        public ActionResult Import_BOL_Notes(IFormFile file)
        {

            try
            {


                if (file == null || file.Length == 0)
                    return Content("file not selected");


                var orderNoteList = new List<Tovordernote>();
                using (var sreader = new StreamReader(file.OpenReadStream()))
                {
                    //First line is header. If header is not passed in csv then we can neglect the below line.
                    string[] headers = sreader.ReadLine().Split(',');
                    //Loop through the records
                    while (!sreader.EndOfStream)
                    {
                        string[] rows = sreader.ReadLine().Split(',');
                        string Ordernum = "";
                        string Year = "";
                        int Line = 0;
                        string Note = "";


                        Ordernum = Convert.ToString(rows[0]);
                        Year = (Convert.ToString(rows[1]));
                        int.TryParse(Convert.ToString(rows[2]), out Line);
                        Note = (Convert.ToString(rows[3]));




                        orderNoteList.Add(new Tovordernote
                        {
                            Ordernum = Ordernum,
                            Year = Year,
                            Line = Line,
                            Note = Note,
                        });
                    }
                }

                _dbContext.tbl_Tovordernote.AddRange(orderNoteList);
                _dbContext.SaveChanges();

                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("call import_Order_Notes();");
                ViewBag.imported = 1;


            }
            catch (Exception ex)
            {

                ViewBag.imported = 0;
            }
            return View();
        }


        [HttpPost]
        public JsonResult UpdateBolDetail(string bol1_no, string column, string bol1_carrierName, string bol1_carrierPhone, DateTime? bol1_PkupDate, DateTime? bol1_pkupTime, string Conformation)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ItemclassViewModel model = new ItemclassViewModel();
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                var query = "";
                if (column == "bol1_carrierName")
                {
                    query = "Update bol_1 set bol1_carrierName='" + bol1_carrierName + "'  where bol1_no ='" + bol1_no + "'  ";
                }
                else if (column == "bol1_carrierPhone")
                {
                    query = "Update bol_1 set bol1_carrierPhone='" + bol1_carrierPhone + "'  where bol1_no ='" + bol1_no + "'  ";
                }
                else if (column == "bol1_PkupDate")
                {
                    query = "Update bol_1 set bol1_PkupDate='" + bol1_PkupDate.Value.ToString("yyyy/MM/dd") + "'   where bol1_no ='" + bol1_no + "' ";
                }
                else if (column == "Conformation")
                {
                    query = "Update bol_1 set Conformation='" + Conformation + "'  where bol1_no ='" + bol1_no + "'  ";
                }
                else
                {
                    query = "Update bol_1 set bol1_pkupTime='" + bol1_pkupTime.Value.ToString("HH:mm") + "'  where bol1_no ='" + bol1_no + "'  ";
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


        [HttpPost]
        public JsonResult GetOrderNotes(string bol1_no)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ItemclassViewModel model = new ItemclassViewModel();
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                var query = "select * from ordernotes where ordernum in (select bol2_order_no from bol_2 where bol2_no='" + bol1_no + "' )";
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


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}