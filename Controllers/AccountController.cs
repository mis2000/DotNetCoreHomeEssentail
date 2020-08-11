using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySqlBasicCore.Models;
using MySqlBasicCore.Utility;
using Newtonsoft.Json;
using System;
using System.Data;

namespace MySqlBasicCore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private readonly IOptions<Appsettings> _appSettings;

        public AccountController(IOptions<Appsettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public ActionResult Login()
        {
            HttpContext.Session.SetString("UserId", "");
            HttpContext.Session.SetString("Username", "");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                    DataSet ds = dbfunction.GetDataset("select * from users  where username ='" + model.Username + "' and Password ='" + model.Password + "'");

                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        ViewBag.ErrorMessage = "Incorrect username or password";
                    }
                    else
                    {
                        CommanUtility commanUtility = new CommanUtility(_appSettings);
                        var userMenus = commanUtility.GetUserMenus(Convert.ToString(ds.Tables[0].Rows[0]["RoleId"]));
                        HttpContext.Session.SetString("UserMenus", JsonConvert.SerializeObject(userMenus));
                        HttpContext.Session.SetString("UserId", Convert.ToString(ds.Tables[0].Rows[0]["Userid"]));
                        HttpContext.Session.SetString("RoleId", Convert.ToString(ds.Tables[0].Rows[0]["RoleId"]));
                        HttpContext.Session.SetString("Username", model.Username);
                        return RedirectToAction("Dashboard", "Home");
                    }
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