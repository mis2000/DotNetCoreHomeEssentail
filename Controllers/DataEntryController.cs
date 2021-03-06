﻿using AutoMapper;
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
    public class DataEntryController : Controller
    {
        private readonly IOptions<Appsettings> _appSettings;
        private readonly DBContext _dbContext;
        private readonly IConfigurationProvider _mappingConfiguration;

        public DataEntryController(IOptions<Appsettings> appSettings, DBContext dbContext, IConfigurationProvider mappingConfiguration)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
            _mappingConfiguration = mappingConfiguration;
        }

        public ActionResult ItemClassList()
        {
            List<ItemclassViewModel> itemClassList = new List<ItemclassViewModel>();
            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("Select * from tov.itemclass");

                itemClassList = (from row in ds.Tables[0].AsEnumerable()
                                 select new ItemclassViewModel
                                 {
                                     Class = Convert.ToString(row["Class"]),
                                     Desc = Convert.ToString(row["Desc"]),
                                     Dept = Convert.ToString(row["Dept"]),
                                     Market = Convert.ToString(row["Market"]),
                                     Deptnum = Convert.ToInt32(row["Deptnum"]),
                                     Buyer = Convert.ToString(row["Buyer"])
                                 }).ToList();
            }
            catch (Exception Ex)
            {
            }
            return View(itemClassList);
        }

        public ActionResult AddItemClass()
        {
            return View();
        }

        public ActionResult EditItemClass(string id)
        {
            ItemclassViewModel model = new ItemclassViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    DataSet ds = dbfunction.GetDataset("select * from tov.itemclass where class= '" + id + "'");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.ClassId = Convert.ToString(ds.Tables[0].Rows[0]["Class"]);
                        model.Class = Convert.ToString(ds.Tables[0].Rows[0]["Class"]);
                        model.Desc = Convert.ToString(ds.Tables[0].Rows[0]["Desc"]);
                        model.Dept = Convert.ToString(ds.Tables[0].Rows[0]["Dept"]);
                        model.Market = Convert.ToString(ds.Tables[0].Rows[0]["Market"]);
                        model.Deptnum = Convert.ToInt32(ds.Tables[0].Rows[0]["Deptnum"]);
                        model.Buyer = Convert.ToString(ds.Tables[0].Rows[0]["Buyer"]);
                    }
                    else
                    {
                        return RedirectToAction("ItemClassList", "Inventory");
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
            return View();
        }

        [HttpPost]
        public ActionResult AddItemClass(ItemclassViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    var query = "select * from tov.itemclass where class = '" + model.Class + "'";
                    DataSet ds = dbfunction.GetDataset(query);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = "insert into tov.itemclass (Class,`Desc`,Dept,Market,Deptnum,Buyer) values ('" + model.Class + "', '" + model.Desc + "', '" + model.Dept + "', '" + model.Market + "', '" + model.Deptnum + "', '" + model.Buyer + "');insert into HEANDB.itemclass (Class,`Desc`,Dept,Market,Deptnum,Buyer) values ('" + model.Class + "', '" + model.Desc + "', '" + model.Dept + "', '" + model.Market + "', '" + model.Deptnum + "', '" + model.Buyer + "')";
                        ds = dbfunction.GetDataset(query);
                        ViewBag.SuccessMessage = "Class added successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Class name is already exists";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditItemClass(ItemclassViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    var query = "select * from tov.itemclass where class = '" + model.Class + "' and class !='" + model.ClassId + "' ";
                    DataSet ds = dbfunction.GetDataset(query);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = " update tov.itemclass set   Class = '" + model.Class + "',`Desc`= '" + model.Desc + "',Dept = '" + model.Dept + "',Market = '" + model.Market + "',Deptnum = '" + model.Deptnum + "',Buyer = '" + model.Buyer + "'          where class = " + model.ClassId + " ;";
                        ds = dbfunction.GetDataset(query);
                        query = "update HEANDB.itemclass set   Class = '" + model.Class + "',`Desc`= '" + model.Desc + "',Dept = '" + model.Dept + "',Market = '" + model.Market + "',Deptnum = '" + model.Deptnum + "',Buyer = '" + model.Buyer + "'          where class = " + model.ClassId + " ;";
                        ds = dbfunction.GetDataset(query);
                        ViewBag.SuccessMessage = "Class updated successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Class name is already exists";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteItemClass(string id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ItemclassViewModel model = new ItemclassViewModel();
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                var query = " delete from tov.itemclass  where class ='" + id + "' ;delete from HEANDB.itemclass  where class ='" + id + "' ; ";
                DataSet ds = dbfunction.GetDataset(query);
                response.Status = "1";
                response.Message = "Class deleted successfully";
            }
            catch (Exception ex)
            {
            }

            return Json(response);
        }

        public ActionResult DeptProjectionList()
        {
            List<DeptProjectionViewModel> itemClassList = new List<DeptProjectionViewModel>();
            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("Select * from tov.DeptProjection");

                itemClassList = (from row in ds.Tables[0].AsEnumerable()
                                 select new DeptProjectionViewModel
                                 {
                                     DeptName = Convert.ToString(row["DeptName"]),
                                     mm = Convert.ToInt32(row["mm"]),
                                     Projection = Convert.ToDecimal(row["Projection"]),
                                 }).ToList();

            }
            catch (Exception Ex)
            {
            }
            return View(itemClassList);
        }

        public ActionResult AddDeptProjection()
        {
            return View();
        }

        public ActionResult EditDeptProjection(string DeptName, string mm)
        {
            DeptProjectionViewModel model = new DeptProjectionViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    DataSet ds = dbfunction.GetDataset("select * from tov.DeptProjection where DeptName= '" + DeptName + "' and  mm= " + mm + ";");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.DeptName = Convert.ToString(ds.Tables[0].Rows[0]["DeptName"]);
                        model.DeptNameId = Convert.ToString(ds.Tables[0].Rows[0]["DeptName"]);
                        model.mm = Convert.ToInt32(ds.Tables[0].Rows[0]["mm"]);
                        model.mmId = Convert.ToInt32(ds.Tables[0].Rows[0]["mm"]);
                        model.Projection = Convert.ToDecimal(ds.Tables[0].Rows[0]["Projection"]);
                    }
                    else
                    {
                        return RedirectToAction("DeptProjectionList", "DataEntry");
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult AddDeptProjection(DeptProjectionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    var query = "select * from tov.DeptProjection where DeptName = '" + model.DeptName + "' and mm = " + model.mm + "";
                    DataSet ds = dbfunction.GetDataset(query);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = "insert into tov.DeptProjection (DeptName,mm,Projection) values ('" + model.DeptName + "', " + model.mm + ", " + model.Projection + ");";
                        ds = dbfunction.GetDataset(query);
                        ViewBag.SuccessMessage = "Department projection added successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Department projection detail is already exists";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditDeptProjection(DeptProjectionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    var query = "select * from tov.DeptProjection where DeptName = '" + model.DeptName + "' and mm =" + model.mm + " and   DeptName != '" + model.DeptNameId + "' and mm !=" + model.mmId + " ";
                    DataSet ds = dbfunction.GetDataset(query);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = " update tov.DeptProjection set   DeptName = '" + model.DeptName + "',mm = " + model.mm + ",Projection = " + model.Projection + "   where  DeptName = '" + model.DeptNameId + "' and mm =" + model.mmId + " ;";
                        ds = dbfunction.GetDataset(query);
                        ViewBag.SuccessMessage = "Department projection detail updated successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Department projection detail is already exists";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteDeptProjection(string DeptName, string mm)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ItemclassViewModel model = new ItemclassViewModel();
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                var query = " delete from tov.DeptProjection  where DeptName = '" + DeptName + "' and mm =" + mm + " ";
                DataSet ds = dbfunction.GetDataset(query);
                response.Status = "1";
                response.Message = "Department projection detail deleted successfully";
            }
            catch (Exception ex)
            {
            }

            return Json(response);
        }






















        public ActionResult DeptAmountList()
        {
            List<DeptAmountViewModel> deptAmountList = new List<DeptAmountViewModel>();
            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset("Select * from deptamount");

                deptAmountList = (from row in ds.Tables[0].AsEnumerable()
                                  select new DeptAmountViewModel
                                  {
                                      deptamount_id = Convert.ToInt32(row["deptamount_id"]),
                                      Dept = Convert.ToString(row["Dept"]),
                                      Amount = Convert.ToDecimal(row["Amount"])
                                  
                                  }).ToList();

            }
            catch (Exception Ex)
            {
            }
            return View(deptAmountList);
        }

        public ActionResult AddDeptAmount()
        {
            return View();
        }

        public ActionResult EditDeptAmount(string id)
        {
            DeptAmountViewModel model = new DeptAmountViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    DataSet ds = dbfunction.GetDataset("select * from deptamount where deptamount_id= " + id + ";");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.Dept = Convert.ToString(ds.Tables[0].Rows[0]["Dept"]);
                        model.Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"]);
                        model.deptamount_id = Convert.ToInt32(id);
                    }
                    else
                    {
                        return RedirectToAction("DeptAmountList", "DataEntry");
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult AddDeptAmount(DeptAmountViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    var query = "select * from deptamount where Dept = '" + model.Dept + "' ";
                    DataSet ds = dbfunction.GetDataset(query);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = "insert into deptamount (Dept,Amount) values ('" + model.Dept + "', " + model.Amount + ");";
                        ds = dbfunction.GetDataset(query);
                        ViewBag.SuccessMessage = "Department amount detail added successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Department amount detail is already exists";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditDeptAmount(DeptAmountViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    var query = "select * from deptamount where Dept = '" + model.Dept + "'  and   deptamount_id != '" + model.deptamount_id + "'  ";
                    DataSet ds = dbfunction.GetDataset(query);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = " update deptamount set   Dept = '" + model.Dept + "', Amount = " + model.Amount + "   where  deptamount_id = '" + model.deptamount_id + "';";
                        ds = dbfunction.GetDataset(query);
                        ViewBag.SuccessMessage = "Department amount detail updated successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Department amount detail is already exists";
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteDeptAmount(string id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ItemclassViewModel model = new ItemclassViewModel();
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                var query = " delete from deptamount  where deptamount_id = '" + id + "'  ";
                DataSet ds = dbfunction.GetDataset(query);
                response.Status = "1";
                response.Message = "Department amount detail deleted successfully";
            }
            catch (Exception ex)
            {
            }

            return Json(response);
        }



    }
}