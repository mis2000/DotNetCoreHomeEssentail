using Microsoft.Extensions.Options;
using MySqlBasicCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace MySqlBasicCore.Utility
{
    public class ItemUtility
    {
        private readonly IOptions<Appsettings> _appSettings;

        public ItemUtility(IOptions<Appsettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public ItemViewModel GetItemDetailByItemNumOrDescription(string ItemNum = "", string ItemDescription = "")
        {
            ItemViewModel model = new ItemViewModel();

            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                string query = "";
                if (ItemNum != "")
                {
                    query = "select  * from items where  REPLACE(Itemnum, '\n', '')  = '" + ItemNum + "'   ";
                }
                else if (ItemDescription != "")
                {
                    query = "select  * from items where  description = '" + ItemDescription + "'  ";
                }
                else
                {
                    query = "select  * from items ORDER BY itemnum DESC limit 1  ";
                }
                DataSet ds = dbfunction.GetDataset(query);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];
                    model.Category = Convert.ToString(row["category"]);
                    model.Itemnum = Convert.ToString(row["itemnum"]);
                    model.Itemnum = Regex.Replace(model.Itemnum, @"\t|\n|\r", "");
                    model.Description = Convert.ToString(row["description"]);
                    model.Description = Regex.Replace(model.Description, @"\t|\n|\r", "");
                    model.Class = Convert.ToString(row["class"]);
                    model.Landedcost = Convert.ToString(row["landedcost"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["landedcost"]);
                    model.Cost = Convert.ToString(row["cost"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["cost"]);
                    model.Min1 = Convert.ToString(row["min1"]) == "" ? (int?)null : Convert.ToInt32(row["min1"]);
                    model.Price1 = Convert.ToString(row["price1"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["price1"]);
                    model.Min2 = Convert.ToString(row["min2"]) == "" ? (int?)null : Convert.ToInt32(row["min2"]);
                    model.Price2 = Convert.ToString(row["price2"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["price2"]);
                    model.Min3 = Convert.ToString(row["min3"]) == "" ? (int?)null : Convert.ToInt32(row["min3"]);
                    model.Price3 = Convert.ToString(row["price3"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["price3"]);
                    model.Onhand = Convert.ToString(row["onhand"]) == "" ? (int?)null : Convert.ToInt32(row["onhand"]);
                    model.Commited = Convert.ToString(row["commited"]) == "" ? (int?)null : Convert.ToInt32(row["commited"]);
                    model.Onorder = Convert.ToString(row["onorder"]) == "" ? (int?)null : Convert.ToInt32(row["onorder"]);
                    model.Onwater = Convert.ToString(row["Onwater"]) == "" ? (int?)null : Convert.ToInt32(row["Onwater"]);
                    model.Ytdsold = Convert.ToString(row["ytdsold"]) == "" ? (int?)null : Convert.ToInt32(row["ytdsold"]);
                    model.Ytdsales = Convert.ToString(row["ytdsales"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["ytdsales"]);
                    model.Lysold = Convert.ToString(row["lysold"]) == "" ? (int?)null : Convert.ToInt32(row["lysold"]);
                    model.Lysales = Convert.ToString(row["lysales"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["lysales"]);
                    model.Upc = Convert.ToString(row["upc"]);
                    model.Size = Convert.ToString(row["size"]);
                    model.Selectioncode = Convert.ToString(row["selectioncode"]);
                    model.Selectiondesc = Convert.ToString(row["selectiondesc"]);
                    model.PatternCode = Convert.ToString(row["patternCode"]);
                    model.PatternDesc = Convert.ToString(row["patternDesc"]);
                    model.UsageCode = Convert.ToString(row["usageCode"]);
                    model.UsageDesc = Convert.ToString(row["usageDesc"]);
                    model.Vendor = Convert.ToString(row["vendor"]);
                    model.Vendorcode = Convert.ToString(row["vendorcode"]);
                    model.Pforeign = Convert.ToString(row["pforeign"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["pforeign"]);
                    model.Weight = Convert.ToString(row["weight"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["weight"]);
                    model.Defectivecount = Convert.ToString(row["defectivecount"]) == "" ? (int?)null : Convert.ToInt32(row["defectivecount"]);
                    model.Costprev = Convert.ToString(row["costprev"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["costprev"]);
                    model.Listprev = Convert.ToString(row["listprev"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["listprev"]);
                    model.Pack = Convert.ToString(row["pack"]) == "" ? (int?)null : Convert.ToInt32(row["pack"]);
                    model.Casepack = Convert.ToString(row["casepack"]) == "" ? (int?)null : Convert.ToInt32(row["casepack"]);
                    model.Innerpack = Convert.ToString(row["innerpack"]) == "" ? (int?)null : Convert.ToInt32(row["innerpack"]);
                    model.X = Convert.ToString(row["X"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["X"]);
                    model.Y = Convert.ToString(row["Y"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Y"]);
                    model.Z = Convert.ToString(row["Z"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Z"]);
                    model.CF = Convert.ToString(row["CF"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["CF"]);
                    model.Xi = Convert.ToString(row["xi"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["xi"]);
                    model.Yi = Convert.ToString(row["yi"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["yi"]);
                    model.Zi = Convert.ToString(row["zi"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["zi"]);
                    model.C1 = Convert.ToString(row["C1"]) == "" ? (int?)null : Convert.ToInt32(row["C1"]);
                    if (row["ship1"] != null)
                    {
                        model.Ship1 = Convert.ToString(row["ship1"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["ship1"]);
                    }
                    if (row["eta1"] != null)
                    {
                        model.Eta1 = Convert.ToString(row["eta1"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["eta1"]);
                    }
                    model.C2 = Convert.ToString(row["c2"]) == "" ? (Int32?)null : Convert.ToInt32(row["c2"]);
                    if (row["ship2"] != null)
                    {
                        model.Ship2 = Convert.ToString(row["ship2"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["ship2"]);
                    }
                    if (row["eta2"] != null)
                    {
                        model.Eta2 = Convert.ToString(row["eta2"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["eta2"]);
                    }
                    model.C3 = Convert.ToString(row["c3"]) == "" ? (int?)null : Convert.ToInt32(row["c3"]);
                    if (row["ship3"] != null)
                    {
                        model.Ship3 = Convert.ToString(row["ship3"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["ship3"]);
                    }
                    if (row["eta3"] != null)
                    {
                        model.Eta3 = Convert.ToString(row["eta3"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["eta3"]);
                    }
                    model.Origin = Convert.ToString(row["origin"]);
                    model.Deptnum = Convert.ToString(row["deptnum"]);
                    model.Deptname = Convert.ToString(row["deptname"]);
                    model.Buyer = Convert.ToString(row["buyer"]);
                    model.Volume = Convert.ToString(row["volume"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["volume"]);
                    model.Available = Convert.ToString(row["available"]) == "" ? (double?)null : Convert.ToDouble(row["available"]);
                    if (row["createdon"] != null)
                    {
                        model.Createdon = Convert.ToDateTime(row["createdon"]);
                    }
                    model.Deleted = Convert.ToString(row["deleted"]) == "" ? 0 : Convert.ToInt32(row["deleted"]);
                    model.Proposal = Convert.ToString(row["proposal"]);
                    model.ItemLength = Convert.ToString(row["itemLength"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["itemLength"]);
                    model.ItemWidth = Convert.ToString(row["itemWidth"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["itemWidth"]);
                    model.ItemHeight = Convert.ToString(row["itemHeight"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["itemHeight"]);
                    model.MaterialCode = Convert.ToString(row["materialCode"]);
                    model.MaterialDesc = Convert.ToString(row["materialDesc"]);
                    model.GeneralCode = Convert.ToString(row["generalCode"]);
                    model.GeneralDesc = Convert.ToString(row["generalDesc"]);
                    model.CaseLength = Convert.ToString(row["caseLength"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["caseLength"]);
                    model.CaseWidth = Convert.ToString(row["caseWidth"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["caseWidth"]);
                    model.Casepack = Convert.ToString(row["caseWidth"]) == "" ? (Int32?)null : Convert.ToInt32(row["caseWidth"]);
                    model.BoxLength = Convert.ToString(row["boxLength"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["boxLength"]);
                    model.BoxWidth = Convert.ToString(row["boxWidth"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["boxWidth"]);
                    model.BoxHeight = Convert.ToString(row["boxHeight"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["boxHeight"]);
                    //model.Qrsselcode = Convert.ToString(row["qrsselcode"]);
                    //model.Qrsseldesc = Convert.ToString(row["qrsseldesc"]);
                }
            }
            catch (Exception ex)
            {
            }

            return model;
        }

        public List<ItemNumDescriptionViewModel> GetItemNameDescriptionByItemNumOrDescription(string ItemNum = "", string ItemDescription = "", string OperationType = "Contains")
        {
            List<ItemNumDescriptionViewModel> itemList = new List<ItemNumDescriptionViewModel>();

            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = new DataSet();
                string query = "";
                if (OperationType == "Contains")
                {
                    query = "select   REPLACE(Itemnum, '\n', '')Itemnum, Description from items where Itemnum =" + ItemNum + " union all select   REPLACE(Itemnum, '\n', '')Itemnum, Description from items where Itemnum like '%" + ItemNum + "%' and Itemnum !=" + ItemNum + "   limit 10";

                    ds = dbfunction.GetDataset(query);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = "select   REPLACE(Itemnum, '\n', '')Itemnum, Description from items where  description like '%" + ItemDescription + "%' ORDER BY itemnum DESC limit 10";
                    }
                }
                else
                {
                    query = "select   REPLACE(Itemnum, '\n', '')Itemnum, Description from items where Itemnum =" + ItemNum + "  union all select   REPLACE(Itemnum, '\n', '')Itemnum, Description from items where Itemnum like '" + ItemNum + "%' and Itemnum !=" + ItemNum + "   limit 10";

                    ds = dbfunction.GetDataset(query);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        query = "select   REPLACE(Itemnum, '\n', '')Itemnum, Description from items where  description like '" + ItemDescription + "%' ORDER BY itemnum DESC limit 10";
                    }
                }

                ds = dbfunction.GetDataset(query);
                itemList = (from row in ds.Tables[0].AsEnumerable()
                            select new ItemNumDescriptionViewModel
                            {
                                Itemnum = Regex.Replace(Convert.ToString(row["Itemnum"]), @"\t|\n|\r", ""),
                                Description = Regex.Replace(Convert.ToString(row["Description"]), @"\t|\n|\r", "")
                            }).ToList();
            }
            catch (Exception ex)
            {
            }

            return itemList;
        }

        public ItemViewModel GetNextItemDetailByItemNumOrDescription(string ItemNum = "", bool IsNext = true)
        {
            ItemViewModel model = new ItemViewModel();

            try
            {
                DataSet ds;
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                List<string> ItemNums = GetItemNums();
                for (int i = 0; i < ItemNums.Count; i++)
                {
                    if (ItemNums[i] == ItemNum)
                    {
                        if (IsNext)
                        {
                            model.Itemnum = ItemNums[i + 1];
                        }
                        else
                        {
                            model.Itemnum = ItemNums[i - 1];
                        }
                        i = ItemNums.Count;
                        break;
                    }
                }


                string query = "select * from  items  where  REPLACE(Itemnum, '\n', '')  = '" + model.Itemnum + "'";
                ds = dbfunction.GetDataset(query);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var row = ds.Tables[0].Rows[0];
                    model.Category = Convert.ToString(row["Category"]);
                    model.Itemnum = Convert.ToString(row["Itemnum"]);
                    model.Itemnum = Regex.Replace(model.Itemnum, @"\t|\n|\r", "");
                    model.Description = Convert.ToString(row["Description"]);
                    model.Description = Regex.Replace(model.Description, @"\t|\n|\r", "");
                    model.Class = Convert.ToString(row["Class"]);
                    model.Landedcost = Convert.ToString(row["Landedcost"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Landedcost"]);
                    model.Cost = Convert.ToString(row["Cost"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Cost"]);
                    model.Min1 = Convert.ToString(row["Min1"]) == "" ? (int?)null : Convert.ToInt32(row["Min1"]);
                    model.Price1 = Convert.ToString(row["Price1"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Price1"]);
                    model.Min2 = Convert.ToString(row["Min2"]) == "" ? (int?)null : Convert.ToInt32(row["Min2"]);
                    model.Price2 = Convert.ToString(row["Price2"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Price2"]);
                    model.Min3 = Convert.ToString(row["Min3"]) == "" ? (int?)null : Convert.ToInt32(row["Min3"]);
                    model.Price3 = Convert.ToString(row["Price3"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Price3"]);
                    model.Onhand = Convert.ToString(row["Onhand"]) == "" ? (int?)null : Convert.ToInt32(row["Onhand"]);
                    model.Commited = Convert.ToString(row["Commited"]) == "" ? (int?)null : Convert.ToInt32(row["Commited"]);
                    model.Onorder = Convert.ToString(row["Onorder"]) == "" ? (int?)null : Convert.ToInt32(row["Onorder"]);
                    model.Onwater = Convert.ToString(row["Onwater"]) == "" ? (int?)null : Convert.ToInt32(row["Onwater"]);
                    model.Ytdsold = Convert.ToString(row["Ytdsold"]) == "" ? (int?)null : Convert.ToInt32(row["Ytdsold"]);
                    model.Ytdsales = Convert.ToString(row["Ytdsales"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Ytdsales"]);
                    model.Lysold = Convert.ToString(row["Lysold"]) == "" ? (int?)null : Convert.ToInt32(row["Lysold"]);
                    model.Lysales = Convert.ToString(row["Lysales"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Lysales"]);
                    model.Upc = Convert.ToString(row["Upc"]);
                    model.Size = Convert.ToString(row["Size"]);
                    model.Selectioncode = Convert.ToString(row["Selectioncode"]);
                    model.Selectiondesc = Convert.ToString(row["Selectiondesc"]);
                    model.PatternCode = Convert.ToString(row["PatternCode"]);
                    model.PatternDesc = Convert.ToString(row["PatternDesc"]);
                    model.UsageCode = Convert.ToString(row["UsageCode"]);
                    model.UsageDesc = Convert.ToString(row["UsageDesc"]);
                    model.Vendor = Convert.ToString(row["Vendor"]);
                    model.Vendorcode = Convert.ToString(row["Vendorcode"]);
                    model.Pforeign = Convert.ToString(row["Cost"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Pforeign"]);
                    model.Weight = Convert.ToString(row["Cost"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Weight"]);
                    model.Defectivecount = Convert.ToString(row["Defectivecount"]) == "" ? (int?)null : Convert.ToInt32(row["Defectivecount"]);
                    model.Costprev = Convert.ToString(row["Costprev"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Costprev"]);
                    model.Listprev = Convert.ToString(row["Listprev"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Listprev"]);
                    model.Pack = Convert.ToString(row["Pack"]) == "" ? (int?)null : Convert.ToInt32(row["Pack"]);
                    model.Casepack = Convert.ToString(row["Casepack"]) == "" ? (int?)null : Convert.ToInt32(row["Casepack"]);
                    model.Innerpack = Convert.ToString(row["Casepack"]) == "" ? (int?)null : Convert.ToInt32(row["Innerpack"]);
                    model.X = Convert.ToString(row["X"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["X"]);
                    model.Y = Convert.ToString(row["Y"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Y"]);
                    model.Z = Convert.ToString(row["Z"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Z"]);
                    model.CF = Convert.ToString(row["CF"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["CF"]);
                    model.Xi = Convert.ToString(row["Xi"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Xi"]);
                    model.Yi = Convert.ToString(row["Yi"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Yi"]);
                    model.Zi = Convert.ToString(row["Zi"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Zi"]);
                    model.C1 = Convert.ToString(row["C1"]) == "" ? (int?)null : Convert.ToInt32(row["C1"]);
                    if (row["Ship1"] != null)
                    {
                        model.Ship1 = Convert.ToString(row["Ship1"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["Ship1"]);
                    }
                    if (row["Eta1"] != null)
                    {
                        model.Eta1 = Convert.ToString(row["Eta1"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["Eta1"]);
                    }
                    model.C2 = Convert.ToInt32(row["C2"]);
                    if (row["Ship2"] != null)
                    {
                        model.Ship2 = Convert.ToString(row["Ship2"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["Ship2"]);
                    }
                    if (row["Eta2"] != null)
                    {
                        model.Eta2 = Convert.ToString(row["Eta2"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["Eta2"]);
                    }
                    model.C3 = Convert.ToString(row["C3"]) == "" ? (int?)null : Convert.ToInt32(row["C3"]);
                    if (row["Ship3"] != null)
                    {
                        model.Ship3 = Convert.ToString(row["Ship3"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["Ship3"]);
                    }
                    if (row["Eta3"] != null)
                    {
                        model.Eta3 = Convert.ToString(row["Eta3"]) == "" ? (DateTime?)null : Convert.ToDateTime(row["Eta3"]);
                    }
                    model.Origin = Convert.ToString(row["Origin"]);
                    model.Deptnum = Convert.ToString(row["Deptnum"]);
                    model.Deptname = Convert.ToString(row["Deptname"]);
                    model.Buyer = Convert.ToString(row["Buyer"]);
                    model.Volume = Convert.ToString(row["Volume"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["Volume"]);
                    model.Available = Convert.ToString(row["Available"]) == "" ? (double?)null : Convert.ToDouble(row["Available"]);
                    if (row["Createdon"] != null)
                    {
                        model.Createdon = Convert.ToDateTime(row["Createdon"]);
                    }
                    model.Deleted = Convert.ToInt32(row["Deleted"]);
                    model.Proposal = Convert.ToString(row["Proposal"]);
                    model.ItemLength = Convert.ToString(row["ItemLength"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["ItemLength"]);
                    model.ItemWidth = Convert.ToString(row["ItemWidth"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["ItemWidth"]);
                    model.ItemHeight = Convert.ToString(row["ItemHeight"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["ItemHeight"]);
                    model.MaterialCode = Convert.ToString(row["MaterialCode"]);
                    model.MaterialDesc = Convert.ToString(row["MaterialDesc"]);
                    model.GeneralCode = Convert.ToString(row["GeneralCode"]);
                    model.GeneralDesc = Convert.ToString(row["GeneralDesc"]);
                    model.CaseLength = Convert.ToString(row["CaseLength"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["CaseLength"]);
                    model.CaseWidth = Convert.ToString(row["CaseWidth"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["CaseWidth"]);
                    model.BoxLength = Convert.ToString(row["BoxLength"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["BoxLength"]);
                    model.BoxWidth = Convert.ToString(row["BoxWidth"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["BoxWidth"]);
                    model.BoxHeight = Convert.ToString(row["BoxHeight"]) == "" ? (Decimal?)null : Convert.ToDecimal(row["BoxHeight"]);
                    model.Qrsselcode = Convert.ToString(row["Qrsselcode"]);
                    model.Qrsseldesc = Convert.ToString(row["Qrsseldesc"]);
                }
            }
            catch (Exception ex)
            {
            }

            return model;
        }

        public List<String> GetItemNums()
        {
            List<String> itemList = new List<String>();

            try
            {
                DbfunctionUtility dbfunction = new DbfunctionUtility(_appSettings);
                DataSet ds = new DataSet();
                string query = "select   REPLACE(Itemnum, '\n', '')Itemnum, Description from items  ORDER BY itemnum DESC";
                ds = dbfunction.GetDataset(query);
                itemList = (from row in ds.Tables[0].AsEnumerable()
                            select Regex.Replace(Convert.ToString(row["Itemnum"]), @"\t|\n|\r", "")
                            ).ToList();
            }
            catch (Exception ex)
            {
            }

            return itemList;
        }
    }
}