using AutoMapper;
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


namespace MySqlBasicCore.Controllers
{
    [Authentication]
    public class UserSettingController : Controller
    {
        private readonly IOptions<Appsettings> _appSettings;
        private readonly DBContext _dbContext;
        private readonly IConfigurationProvider _mappingConfiguration;
        private int _userId = 0;
        public UserSettingController(IOptions<Appsettings> appSettings, DBContext dbContext, IConfigurationProvider mappingConfiguration, IHttpContextAccessor _HttpContextAccessor)
        {
            _appSettings = appSettings;
            _dbContext = dbContext;
            _mappingConfiguration = mappingConfiguration;
            _userId = Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("UserId"));
        }


        public ActionResult UserList()
        {
            List<UserViewModel> roleList = new List<UserViewModel>();
            try
            {
                roleList = (from user in _dbContext.tbl_Users
                            join role in _dbContext.tbl_Roles on user.RoleId equals role.RoleId
                            select new UserViewModel
                            {
                                UserId = user.UserId,
                                Username = user.Username,
                                Rolename = role.Rolename,
                                IsActive = user.IsActive,
                                Email = user.Email
                            }
                           ).ToList();
            }
            catch (Exception Ex)
            {
            }
            return View(roleList);
        }

        public ActionResult AddUser()
        {
            UserViewModel user = new UserViewModel();
            user.RoleList = _dbContext.tbl_Roles.Where(w => w.IsActive == true)
                            .Select(s => new RoleViewModel
                            {
                                Rolename = s.Rolename,
                                RoleId = s.RoleId
                            }).ToList();
            return View(user);
        }

        public ActionResult EditUser(string id)
        {
            UserViewModel model = new UserViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = Convert.ToInt32(id);
                    model.RoleList = _dbContext.tbl_Roles.Where(w => w.IsActive == true)
                          .Select(s => new RoleViewModel
                          {
                              Rolename = s.Rolename,
                              RoleId = s.RoleId
                          }).ToList();
                    var checkUser = _dbContext.tbl_Users.Where(w => w.UserId == userId).FirstOrDefault();
                    if (checkUser != null)
                    {
                        model.RoleId = checkUser.RoleId;
                        model.Username = checkUser.Username;
                        model.Email = checkUser.Email;
                        model.UserId = checkUser.UserId;
                    }
                    else
                    {
                        return RedirectToAction("UserList", "UserSetting");
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult AddUser(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Email = model.Email.Trim();
                    var checkRole = _dbContext.tbl_Users.Where(w => w.Username == model.Email).FirstOrDefault();
                    if (checkRole == null)
                    {
                        User user = new User()
                        {
                            Username = model.Email,
                            Password = model.Password,
                            Email = model.Email,
                            RoleId = model.RoleId,
                            IsActive = true,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now
                        };

                        _dbContext.tbl_Users.Add(user);
                        _dbContext.SaveChanges();
                        ViewBag.SuccessMessage = "User added successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "User name is already exists";

                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(UserViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    model.Email = model.Email.Trim();
                    var checkUser = _dbContext.tbl_Users.Where(w => w.Username == model.Email && w.UserId != model.UserId).FirstOrDefault();
                    if (checkUser == null)
                    {
                        var userDetail = _dbContext.tbl_Users.Where(w => w.UserId == model.UserId).FirstOrDefault();

                        userDetail.Username = model.Email;
                        userDetail.Email = model.Email;
                        userDetail.Password = model.Password;
                        userDetail.RoleId = model.RoleId;
                        userDetail.CreatedBy = _userId;
                        userDetail.CreatedDate = DateTime.Now;
                        _dbContext.SaveChanges();
                        ViewBag.SuccessMessage = "User updated successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "User name is already exists";

                    }
                }

            }
            catch (Exception ex)
            {
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteUser(string id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var userId = Convert.ToInt32(id);
                var checkUser = _dbContext.tbl_Users.Where(w => w.UserId == userId).FirstOrDefault();
                if (checkUser != null)
                {
                    checkUser.IsActive = false;
                    checkUser.ModifiedBy = _userId;
                    checkUser.ModifiedDate = DateTime.Now;
                    _dbContext.SaveChanges();
                    response.Status = "1";
                    response.Message = "User deleted successfully";
                }
                else
                {
                    response.Status = "0";
                    response.Message = "Not able to delete now. Contact to administrator";
                }

            }
            catch (Exception ex)
            {
            }

            return Json(response);
        }

        public ActionResult RoleList()
        {
            List<RoleViewModel> roleList = new List<RoleViewModel>();
            try
            {
                roleList = _dbContext.tbl_Roles.Select(s => new RoleViewModel
                {
                    RoleId = s.RoleId,
                    Rolename = s.Rolename,
                    IsActive = s.IsActive
                }).ToList();
            }
            catch (Exception Ex)
            {
            }
            return View(roleList);
        }

        public ActionResult AddRole()
        {
            return View();
        }

        public ActionResult EditRole(string id)
        {
            RoleViewModel model = new RoleViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    var roleId = Convert.ToInt32(id);
                    var checkRole = _dbContext.tbl_Roles.Where(w => w.RoleId == roleId).FirstOrDefault();
                    if (checkRole != null)
                    {
                        model.RoleId = checkRole.RoleId;
                        model.Rolename = checkRole.Rolename;


                    }
                    else
                    {
                        return RedirectToAction("RoleList", "UserSetting");
                    }


                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("RoleList", "UserSetting");
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult AddRole(RoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Rolename = model.Rolename.Trim();
                    var checkRole = _dbContext.tbl_Roles.Where(w => w.Rolename == model.Rolename).FirstOrDefault();
                    if (checkRole == null)
                    {
                        Role role = new Role()
                        {
                            Rolename = model.Rolename,
                            IsActive = true,
                            CreatedBy = _userId,
                            CreatedDate = DateTime.Now
                        };

                        _dbContext.tbl_Roles.Add(role);
                        _dbContext.SaveChanges();
                        ViewBag.SuccessMessage = "Role added successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Role name is already exists";

                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditRole(RoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Rolename = model.Rolename.Trim();
                    var checkRole = _dbContext.tbl_Roles.Where(w => w.Rolename == model.Rolename && w.RoleId != model.RoleId).FirstOrDefault();
                    if (checkRole == null)
                    {
                        var role = _dbContext.tbl_Roles.Where(w => w.RoleId == model.RoleId).FirstOrDefault();
                        role.Rolename = model.Rolename;
                        role.ModifiedBy = _userId;
                        role.ModifiedDate = DateTime.Now;
                        _dbContext.SaveChanges();
                        ViewBag.SuccessMessage = "Role updated successfully";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Role name is already exists";

                    }
                }
            }
            catch (Exception ex)
            {
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteRole(string id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ItemclassViewModel model = new ItemclassViewModel();
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                var query = " delete from tov.itemclass  where class ='" + id + "' ;delete from gdc.itemclass  where class ='" + id + "' ; ";
                DataSet ds = dbfunction.GetDataset(query);
                response.Status = "1";
                response.Message = "Class deleted successfully";
            }
            catch (Exception ex)
            {
            }

            return Json(response);
        }

        public ActionResult UserMenuList()
        {
            List<UserclaimViewModel> userClaims = new List<UserclaimViewModel>();
            try
            {
                userClaims = _dbContext.tbl_Roles.Where(w => w.IsActive == true)
                            .Select(s => new UserclaimViewModel
                            {
                                RoleId = s.RoleId,
                                RoleName = s.Rolename,
                                Menus = (from usermenu in _dbContext.tbl_Userclaim
                                         join menu in _dbContext.tbl_Menus on usermenu.MenuId equals menu.MenuId
                                         where usermenu.RoleId == s.RoleId
                                         select new MenuViewModel
                                         {
                                             MenuId = menu.MenuId,
                                             Name = menu.Name
                                         }).ToList()

                            }).ToList();
            }
            catch (Exception Ex)
            {
            }
            return View(userClaims);
        }

        public ActionResult AddUserMenu()
        {
            return View();
        }

        public ActionResult EditUserMenu(string id)
        {
            UserclaimViewModel model = new UserclaimViewModel();
            try
            {
                if (ModelState.IsValid)
                {
                    var roleId = Convert.ToInt32(id);
                    var checkRole = _dbContext.tbl_Roles.Where(w => w.RoleId == roleId).FirstOrDefault();
                    if (checkRole != null)
                    {
                        model.RoleName = checkRole.Rolename;
                        model.RoleId = roleId;
                        var roleMenus = _dbContext.tbl_Userclaim.Where(w => w.RoleId == roleId).Select(s => s.MenuId).ToList();
                        model.Menus = (from menu in _dbContext.tbl_Menus
                                       select new MenuViewModel
                                       {
                                           MenuId = menu.MenuId,
                                           Name = menu.Name,
                                           Ischecked = (roleMenus.Contains(menu.MenuId) ? true : false),
                                       }).ToList();



                    }
                    else
                    {
                        return RedirectToAction("UserMenuList", "UserSetting");
                    }


                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("UserMenuList", "UserSetting");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddUserMenu(ItemclassViewModel model)
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
                        query = "insert into tov.itemclass (Class,`Desc`,Dept,Market,Deptnum,Buyer) values ('" + model.Class + "', '" + model.Desc + "', '" + model.Dept + "', '" + model.Market + "', '" + model.Deptnum + "', '" + model.Buyer + "');insert into gdc.itemclass (Class,`Desc`,Dept,Market,Deptnum,Buyer) values ('" + model.Class + "', '" + model.Desc + "', '" + model.Dept + "', '" + model.Market + "', '" + model.Deptnum + "', '" + model.Buyer + "')";
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
        public ActionResult EditUserMenu(UserclaimViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var checkRole = _dbContext.tbl_Roles.Where(w => w.RoleId == model.RoleId).FirstOrDefault();
                    if (checkRole != null)
                    {
                        _dbContext.tbl_Userclaim.RemoveRange(_dbContext.tbl_Userclaim.Where(w => w.RoleId == model.RoleId));
                        _dbContext.SaveChanges();

                        foreach (var roleMenu in model.Menus)
                        {
                            if (roleMenu.Ischecked)
                            {
                                Userclaim claim = new Userclaim
                                {
                                    MenuId = roleMenu.MenuId,
                                    RoleId = model.RoleId,
                                    IsActive = true,
                                    Columns = "",
                                    CreatedBy = _userId,
                                    CreatedDate = DateTime.Now,

                                };
                                _dbContext.tbl_Userclaim.Add(claim);
                                _dbContext.SaveChanges();
                            }

                        }
                        ViewBag.SuccessMessage = "Detail added successfully";
                    }
                    else
                    {
                        return RedirectToAction("UserMenuList", "UserSetting");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("UserMenuList", "UserSetting");
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteUserMenu(string id)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                ItemclassViewModel model = new ItemclassViewModel();
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                var query = " delete from tov.itemclass  where class ='" + id + "' ;delete from gdc.itemclass  where class ='" + id + "' ; ";
                DataSet ds = dbfunction.GetDataset(query);
                response.Status = "1";
                response.Message = "Class deleted successfully";
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