using AutoMapper;
using AutoMapper.QueryableExtensions;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySqlBasicCore.Filters;
using MySqlBasicCore.Models;
using MySqlBasicCore.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MySqlBasicCore.Controllers
{
    [Authentication]
    public class InventoryController : Controller
    {
        private readonly IOptions<Appsettings> _appSettings;
        private readonly IConfigurationProvider _mappingConfiguration;
        private readonly DBContext _dbContext;

        public InventoryController(IOptions<Appsettings> appSettings, IConfigurationProvider mappingConfiguration, DBContext dbContext)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
            _mappingConfiguration = mappingConfiguration;
        }

        public ActionResult Items()
        {

            ItemViewModel model = new ItemViewModel();
            try
            {
                ItemUtility itemutility = new ItemUtility(_appSettings);
                ViewBag.PageLoad = true;
            }
            catch (Exception ex)
            {


            }


            return View(model);

        }

        [HttpPost]
        public ActionResult Items(ItemViewModel model, string submit)
        {

            ItemUtility itemutility = new ItemUtility(_appSettings);
            if (submit != "submit")
            {
                if (String.IsNullOrEmpty(model.SearchItemnum))
                {
                    ViewBag.ItemDetail = itemutility.GetItemDetailByItemNumOrDescription(model.SearchItemnum ?? "", model.SearchDescription ?? "");
                }
                else
                {
                    if (submit == "up")
                    {
                        ViewBag.ItemDetail = itemutility.GetNextItemDetailByItemNumOrDescription(model.SearchItemnum, true);
                        if (ViewBag.ItemDetail.Category == null)
                        {
                            ViewBag.NoRecordArrowKey = true;
                        }
                    }
                    else
                    {
                        ViewBag.ItemDetail = itemutility.GetNextItemDetailByItemNumOrDescription(model.SearchItemnum, false);
                        if (ViewBag.ItemDetail.Category == null)
                        {
                            ViewBag.NoRecordArrowKey = true;
                        }
                    }
                }
            }
            else
            {
                ViewBag.ItemDetail = itemutility.GetItemDetailByItemNumOrDescription(model.SearchItemnum ?? "", model.SearchDescription ?? "");
            }

            if (ViewBag.ItemDetail.Category == null)
            {
                ViewBag.NoRecord = true;
                model.SearchItemnum = "";
                model.SearchDescription = "";
            }
            else
            {
                model.SearchItemnum = ViewBag.ItemDetail.Itemnum;
                model.SearchDescription = ViewBag.ItemDetail.Description;
                model.SearchItemnumDescription = ViewBag.ItemDetail.Itemnum + " " + ViewBag.ItemDetail.Description;
            }
            return View(model);
        }

        public ActionResult Display()
        {
            ItemViewModel model = new ItemViewModel();
            ItemUtility itemutility = new ItemUtility(_appSettings);
            ViewBag.PageLoad = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Display(ItemViewModel model, string submit)
        {
            ItemUtility itemutility = new ItemUtility(_appSettings);
            if (submit != "submit")
            {
                if (String.IsNullOrEmpty(model.SearchItemnum))
                {
                    ViewBag.ItemDetail = itemutility.GetItemDetailByItemNumOrDescription(model.SearchItemnum ?? "", model.SearchDescription ?? "");
                }
                else
                {
                    if (submit == "up")
                    {
                        ViewBag.ItemDetail = itemutility.GetNextItemDetailByItemNumOrDescription(model.SearchItemnum, true);
                        if (ViewBag.ItemDetail.Category == null)
                        {
                            ViewBag.NoRecordArrowKey = true;
                        }
                    }
                    else
                    {
                        ViewBag.ItemDetail = itemutility.GetNextItemDetailByItemNumOrDescription(model.SearchItemnum, false);
                        if (ViewBag.ItemDetail.Category == null)
                        {
                            ViewBag.NoRecordArrowKey = true;
                        }
                    }
                }
            }
            else
            {
                ViewBag.ItemDetail = itemutility.GetItemDetailByItemNumOrDescription(model.SearchItemnum ?? "", model.SearchDescription ?? "");
            }

            if (ViewBag.ItemDetail.Category == null)
            {
                ViewBag.NoRecord = true;
                model.SearchItemnum = "";
                model.SearchDescription = "";
                model.SearchItemnumDescription = model.SearchItemnum;
            }
            else
            {
                model.SearchItemnum = ViewBag.ItemDetail.Itemnum;
                model.SearchDescription = ViewBag.ItemDetail.Description;
                model.SearchItemnumDescription = ViewBag.ItemDetail.Itemnum + " " + ViewBag.ItemDetail.Description;
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult GetItemDetail(string SearchItemnum = "", string SearchDescription = "", string OperationType = "Contains")
        {
            //Note : you can bind same list from database
            List<ItemNumDescriptionViewModel> itemList = new List<ItemNumDescriptionViewModel>();
            ItemUtility itemutility = new ItemUtility(_appSettings);
            itemList = itemutility.GetItemNameDescriptionByItemNumOrDescription(SearchItemnum ?? "", SearchDescription ?? "", OperationType);

            return Json(itemList);
        }

        public ActionResult ItemComponentList()
        {
            return View(new IndsellCompoViewModel_Datatable());
        }

        public ActionResult EditItemComponent(string id)
        {
            EditIndsellCompoViewModel model = new EditIndsellCompoViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    DataSet ds = dbfunction.GetDataset(@"select indSell_Compo.*,itemmaster.Description as Item_master,itemcomponent.Description as Item_component
                                                    from indSell_Compo 
                                                    left join items as itemmaster on trim(indSell_ItemMaster) = itemmaster.Itemnum
                                                    left join items as itemcomponent on trim(indSell_Compo.indSell_ItemComponent) = itemcomponent.Itemnum where indSell_ItemMaster= " + id + "");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        model.ItemComponentList = (from row in ds.Tables[0].AsEnumerable()
                                                   select new IndsellCompoViewModel
                                                   {
                                                       indSell_ItemMaster = Convert.ToString(row["indSell_ItemMaster"]),
                                                       indSell_ItemComponent = Convert.ToString(row["indSell_ItemComponent"]),
                                                       indSell_Allowed_Bool = Convert.ToInt16(row["indSell_Allowed"]) == 1 ? true : false,
                                                       Item_master = Convert.ToString(row["Item_master"]),
                                                       Item_component = Convert.ToString(row["Item_component"]),
                                                   }).ToList();

                        var totalCount = model.ItemComponentList.Count();
                        var checkCount = model.ItemComponentList.Where(w=>w.indSell_Allowed_Bool==true).Count();

                        if (totalCount == checkCount)
                        {
                            ViewBag.showAll = 1;
                        }
                        else
                        {
                            ViewBag.showAll = 0;
                        }


                        if (model.ItemComponentList.Count() > 0)
                        {
                            model.indSell_ItemMaster = model.ItemComponentList[0].indSell_ItemMaster;
                            model.Item_master = model.ItemComponentList[0].Item_master;
                        }
                    }
                    else
                    {
                        return RedirectToAction("ItemComponentList", "Inventory");
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody]JqueryDataTablesParameters param)
        {
            try
            {
                //return new JsonResult(new { error = "Internal Server Error" });

                HttpContext.Session.SetString(nameof(JqueryDataTablesParameters), System.Text.Json.JsonSerializer.Serialize(param));

                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = dbfunction.GetDataset(@"select indSell_Compo.*,itemmaster.Description as Item_master,itemcomponent.Description as Item_component
                                                    from indSell_Compo 
                                                    left join items as itemmaster on trim(indSell_ItemMaster) = itemmaster.Itemnum
                                                    left join items as itemcomponent on trim(indSell_Compo.indSell_ItemComponent) = itemcomponent.Itemnum");

                IQueryable<IndsellCompoViewModel> itemComponentList = (from row in ds.Tables[0].AsEnumerable()
                                                                       select new IndsellCompoViewModel
                                                                       {
                                                                           indSell_ItemMaster = Convert.ToString(row["indSell_ItemMaster"]),
                                                                           indSell_ItemComponent = Convert.ToString(row["indSell_ItemComponent"]),
                                                                           indSell_Allowed = Convert.ToInt16(row["indSell_Allowed"]),
                                                                           Item_master = Convert.ToString(row["Item_master"]),
                                                                           Item_component = Convert.ToString(row["Item_component"]),
                                                                       }).AsQueryable();


                var size = itemComponentList.Count();


                if (Convert.ToString(param.AdditionalValues.FirstOrDefault()) != "")
                {
                    var serchValue = Convert.ToString(param.AdditionalValues.FirstOrDefault()).ToLower();
                    itemComponentList = itemComponentList.Where(w =>
                                  ((w.indSell_ItemMaster ?? "").ToLower().Contains(serchValue) ? true :
                                  ((w.indSell_ItemComponent ?? "").ToLower().Contains(serchValue) ? true :
                                  (w.indSell_Allowed).ToString().ToLower().Contains(serchValue) ? true :
                                  (w.Item_master).ToString().ToLower().Contains(serchValue) ? true :
                                  (w.Item_component).ToString().ToLower().Contains(serchValue) ? true : false)));

                }


                if (param.Length == -1)
                {
                    var items = itemComponentList
                                      .ProjectTo<IndsellCompoViewModel_Datatable>(_mappingConfiguration)
                                      .ToArray();


                    var result = new JqueryDataTablesPagedResults<IndsellCompoViewModel_Datatable>
                    {
                        Items = items,
                        TotalSize = size
                    };

                    return new JsonResult(new JqueryDataTablesResult<IndsellCompoViewModel_Datatable>
                    {
                        Draw = param.Draw,
                        Data = result.Items,
                        RecordsFiltered = result.TotalSize,
                        RecordsTotal = result.TotalSize
                    });
                }
                else
                {
                    var items = itemComponentList
                        .Skip((param.Start / param.Length) * param.Length)
                  .Take(param.Length)
                                      .ProjectTo<IndsellCompoViewModel_Datatable>(_mappingConfiguration)
                                      .ToArray();


                    var result = new JqueryDataTablesPagedResults<IndsellCompoViewModel_Datatable>
                    {
                        Items = items,
                        TotalSize = size
                    };

                    return new JsonResult(new JqueryDataTablesResult<IndsellCompoViewModel_Datatable>
                    {
                        Draw = param.Draw,
                        Data = result.Items,
                        RecordsFiltered = result.TotalSize,
                        RecordsTotal = result.TotalSize
                    });

                }



            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }

        [HttpPost]
        public ActionResult EditItemComponent(EditIndsellCompoViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);

                    foreach (var item in model.ItemComponentList)
                    {
                        var allowed = (item.indSell_Allowed_Bool ? 1 : 0);
                        dbfunction.GetDataset("Update indSell_Compo set indSell_Allowed = " + allowed + "  where trim(indSell_ItemMaster)='" + model.indSell_ItemMaster.Trim() + "' and trim(indSell_ItemComponent)= '" + item.indSell_ItemComponent.Trim() + "' ");
                    }

                    ViewBag.SuccessMessage = "Detail added successfully";
                    return View(model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Please enter valid values";
                }
            }
            catch (Exception ex)
            {
            }

            ViewBag.ErrorMessage = "Error occurred";

            return View(model);

        }

        public ActionResult NextEditItemComponent(string id)
        {
            EditIndsellCompoViewModel model = new EditIndsellCompoViewModel();
            try
            {


                var masterItems = _dbContext.tbl_IndsellCompo.Select(s => s.indSell_ItemMaster).Distinct().ToList();
                var nextItem = "";
                for (int i = 0; i < masterItems.Count(); i++)
                {
                    if (Convert.ToString(masterItems[i]).Trim() == id.Trim())
                    {
                        if (i == masterItems.Count())
                        {
                            nextItem = id;
                        }
                        else
                        {
                            nextItem = masterItems[i + 1];
                        }
                        
                        i = masterItems.Count() + 1;
                        break;
                    }
                }

                return RedirectToAction("EditItemComponent", new { id = nextItem });


            }
            catch (Exception ex)
            {
            }

            return View(model);

        }

        public ActionResult PreviousEditItemComponent(string id)
        {
            EditIndsellCompoViewModel model = new EditIndsellCompoViewModel();
            try
            {


                var masterItems = _dbContext.tbl_IndsellCompo.Select(s => s.indSell_ItemMaster).Distinct().ToList();
                var nextItem = "";
                for (int i = 0; i < masterItems.Count(); i++)
                {
                    if (Convert.ToString(masterItems[i]).Trim() == id.Trim())
                    {
                        if (i == 0)
                        {
                            nextItem = id;
                        }
                        else
                        {
                            nextItem = masterItems[i -1];
                        }

                        
                        i = masterItems.Count() + 1;
                        break;
                    }
                }
                if (nextItem == "")
                {
                    return RedirectToAction("EditItemComponent", new { id = id });
                }
                else
                {
                    return RedirectToAction("EditItemComponent", new { id = nextItem });
                }
                


            }
            catch (Exception ex)
            {
            }

            return View(model);

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}