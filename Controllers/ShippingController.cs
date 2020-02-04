using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySqlBasicCore.Filters;
using MySqlBasicCore.Models;
using MySqlBasicCore.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MySqlBasicCore.Controllers
{
    [Authentication]
    public class ShippingController : Controller
    {
        private readonly IOptions<Appsettings> _appSettings;

        public ShippingController(IOptions<Appsettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public ActionResult BOL()
        {
            List<Bol_1_ViewModel> bol_1_List = new List<Bol_1_ViewModel>();
            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("Select * from bol_1");

                bol_1_List = (from row in ds.Tables[0].AsEnumerable()
                              select new Bol_1_ViewModel
                              {
                                  bol1_no = Convert.ToInt32(row["bol1_no"]),
                                  bol1_date = Convert.ToDateTime(row["bol1_date"]),
                                  bol1_custnum = Convert.ToInt32(row["bol1_custnum"]),
                                  bol1_name = Convert.ToString(row["bol1_name"]),
                                  bol1_adrs1 = Convert.ToString(row["bol1_adrs1"]),
                                  bol1_city = Convert.ToString(row["bol1_city"]),
                                  bol1_state = Convert.ToString(row["bol1_state"]),
                                  bol1_zip = Convert.ToString(row["bol1_zip"]),
                                  bol1_Lctn = Convert.ToString(row["bol1_Lctn"]),
                                  bol1_pro_no = Convert.ToInt32(row["bol1_pro_no"]),
                                  bol1_scac = Convert.ToString(row["bol1_scac"]),
                                  bol1_frght_terms = Convert.ToString(row["bol1_frght_terms"]),
                                  bol1_ttl_pkgs = Convert.ToInt32(row["bol1_ttl_pkgs"]),
                                  bol1_ttl_weight = Convert.ToDecimal(row["bol1_ttl_weight"]),
                                  bol1_HE_WH = Convert.ToString(row["bol1_HE_WH"])
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
                DataSet ds = dbfunction.GetDataset("select * from bol_2 where bol2_order_no =" + id);

                bol_2_List = (from row in ds.Tables[0].AsEnumerable()
                              select new Bol_2_ViewModel
                              {
                                  bol2_order_no = Convert.ToInt32(row["bol2_order_no"]),
                                  bol2_pkgs = Convert.ToInt32(row["bol2_pkgs"]),
                                  bol2_weight = Convert.ToDecimal(row["bol2_weight"]),
                              }).ToList();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

            return View(bol_2_List);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}