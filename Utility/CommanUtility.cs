using Microsoft.Extensions.Options;
using MySqlBasicCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace MySqlBasicCore.Utility
{
    public class CommanUtility
    {
        private readonly IOptions<Appsettings> _appSettings;

        public CommanUtility(IOptions<Appsettings> appSettings)
        {
            _appSettings = appSettings;
        }

        
        public List<MenuViewModel> GetUserMenus(string roleId )
        {
            List<MenuViewModel> UserMenus = new List<MenuViewModel>();
            try
            {   
                DataSet ds;
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);

                string query = "select menus.* from userclaims join menus on userclaims.MenuId =  menus.MenuId  where RoleId =" + roleId + "";
                ds = dbfunction.GetDataset(query);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    UserMenus = (from row in ds.Tables[0].AsEnumerable()
                                select new MenuViewModel
                                {
                                    Name = Convert.ToString(row["Name"]),
                                    Action = Convert.ToString(row["Action"]),
                                    Controller= Convert.ToString(row["Controller"]),
                                    Childmenus = Convert.ToString(row["Childmenus"]),
                                    Parent = Convert.ToString(row["Parent"]),
                                    Icon = Convert.ToString(row["Icon"]),
                                    Parenticon = Convert.ToString(row["Parenticon"])

                                }).ToList();
                }
            }
            catch (Exception ex)
            {
            }

            return UserMenus;


        }

       
    }
}