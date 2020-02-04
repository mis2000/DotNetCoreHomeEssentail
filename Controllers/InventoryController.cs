using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySqlBasicCore.Filters;
using MySqlBasicCore.Models;
using MySqlBasicCore.Utility;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MySqlBasicCore.Controllers
{
    [Authentication]
    public class InventoryController : Controller
    {
        private readonly IOptions<Appsettings> _appSettings;

        public InventoryController(IOptions<Appsettings> appSettings)
        {
            _appSettings = appSettings;
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
            var test = "2";
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
                    }
                    else
                    {
                        ViewBag.ItemDetail = itemutility.GetNextItemDetailByItemNumOrDescription(model.SearchItemnum, false);
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
                    }
                    else
                    {
                        ViewBag.ItemDetail = itemutility.GetNextItemDetailByItemNumOrDescription(model.SearchItemnum, false);
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

        [HttpPost]
        public JsonResult GetItemDetail(string SearchItemnum = "", string SearchDescription = "", string OperationType = "Contains")
        {
            //Note : you can bind same list from database
            List<ItemNumDescriptionViewModel> itemList = new List<ItemNumDescriptionViewModel>();
            ItemUtility itemutility = new ItemUtility(_appSettings);
            itemList = itemutility.GetItemNameDescriptionByItemNumOrDescription(SearchItemnum ?? "", SearchDescription ?? "", OperationType);

            return Json(itemList);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}