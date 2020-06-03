using JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MySqlBasicCore.Models
{
    public class Bol_1_ViewModel
    {
        public string bol1_no { get; set; }
        public int ? bol1_order_no { get; set; }
        public DateTime? bol1_date { get; set; }
        public string bol1_custnum { get; set; }
        public string bol1_name { get; set; }
        public string bol1_adrs1 { get; set; }
        public string bol1_city { get; set; }
        public string bol1_state { get; set; }
        public string bol1_zip { get; set; }
        public string bol1_Lctn { get; set; }
        public string bol1_pro_no { get; set; }
        public string bol1_scac { get; set; }
        public string bol1_frght_terms { get; set; }
        public int ? bol1_ttl_pkgs { get; set; }
        public decimal? bol1_ttl_weight { get; set; }
        public string bol1_HE_WH { get; set; }
        public DateTime? bol1_OrderDate { get; set; }
        public DateTime? bol1_CancelDate { get; set; }
        public Decimal? bol1_ttlValue { get; set; }
    }

    public class Bol_2_ViewModel
    {
        public int? bol2_order_no { get; set; }
        public int? bol2_pkgs { get; set; }
        public decimal? bol2_weight { get; set; }
        public int? bol2_No { get; set; }
        public string bol2_PO { get; set; }
        public decimal? bol2_value { get; set; }
    }

    public class LoginViewModel
    {
        [MaxLength(ErrorMessage = "Username should be less than or equal 150 character length")]
        [Required(ErrorMessage = "Please enter username")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string Username { get; set; }

        [MaxLength(ErrorMessage = "Username should be less than or equal 150 character length")]
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }

    public class ItemViewModel
    {
        public string OperationType { get; set; }
        public string SearchItemnumDescription { get; set; }
        public string SearchItemnum { get; set; }
        public string SearchDescription { get; set; }
        public int Itemid { get; set; }
        public string Category { get; set; }
        public string Itemnum { get; set; }
        public string Description { get; set; }
        public string Class { get; set; }
        public decimal? Landedcost { get; set; }
        public decimal? Cost { get; set; }
        public int? Min1 { get; set; }
        public decimal? Price1 { get; set; }
        public int? Min2 { get; set; }
        public decimal? Price2 { get; set; }
        public int? Min3 { get; set; }
        public decimal? Price3 { get; set; }
        public int? Onhand { get; set; }
        public int? Commited { get; set; }
        public int? Onorder { get; set; }
        public int? Onwater { get; set; }
        public int? Ytdsold { get; set; }
        public decimal? Ytdsales { get; set; }
        public int? Lysold { get; set; }
        public decimal? Lysales { get; set; }
        public string Upc { get; set; }
        public string Size { get; set; }
        public string Selectioncode { get; set; }
        public string Selectiondesc { get; set; }
        public string PatternCode { get; set; }
        public string PatternDesc { get; set; }
        public string UsageCode { get; set; }
        public string UsageDesc { get; set; }
        public string Vendor { get; set; }
        public string Vendorcode { get; set; }
        public decimal? Pforeign { get; set; }
        public decimal? Weight { get; set; }
        public int? Defectivecount { get; set; }
        public decimal? Costprev { get; set; }
        public decimal? Listprev { get; set; }
        public int? Pack { get; set; }
        public int? Casepack { get; set; }
        public int? Innerpack { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public decimal? Z { get; set; }
        public decimal? CF { get; set; }
        public decimal? Xi { get; set; }
        public decimal? Yi { get; set; }
        public decimal? Zi { get; set; }
        public int? C1 { get; set; }
        public DateTime? Ship1 { get; set; }
        public DateTime? Eta1 { get; set; }
        public int? C2 { get; set; }
        public DateTime? Ship2 { get; set; }
        public DateTime? Eta2 { get; set; }
        public int? C3 { get; set; }
        public DateTime? Ship3 { get; set; }
        public DateTime? Eta3 { get; set; }
        public string Origin { get; set; }
        public string Deptnum { get; set; }
        public string Deptname { get; set; }
        public string Buyer { get; set; }
        public decimal? Volume { get; set; }
        public double? Available { get; set; }
        public DateTime Createdon { get; set; }
        public int Deleted { get; set; }
        public string Proposal { get; set; }
        public decimal? ItemLength { get; set; }
        public decimal? ItemWidth { get; set; }
        public decimal? ItemHeight { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDesc { get; set; }
        public string GeneralCode { get; set; }
        public string GeneralDesc { get; set; }
        public decimal? CaseLength { get; set; }
        public decimal? CaseWidth { get; set; }
        public decimal? BoxLength { get; set; }
        public decimal? BoxWidth { get; set; }
        public decimal? BoxHeight { get; set; }
        public string Qrsselcode { get; set; }
        public string Qrsseldesc { get; set; }
    }

    public class ItemNumDescriptionViewModel
    {
        public string Itemnum { get; set; }
        public string Description { get; set; }
    }

    public class Order_MasterViewModel
    {
        public string Action { get; set; }

        [Searchable]
        [Sortable]
        public DateTime Shipdate { get; set; }

        [Searchable]
        [Sortable]
        public DateTime? Canceldate { get; set; }

        [SearchableString]
        [Sortable]
        [Display(Name = "Ordernum")]
        public string ordernum { get; set; }

        [SearchableString]
        [Sortable(Default = true)]
        public string Custnum { get; set; }

        [Searchable]
        [Sortable]
        public DateTime Orderdate { get; set; }

        [SearchableString]
        [Sortable]
        public string Name { get; set; }

        [SearchableString]
        [Sortable]
        public string Shipname { get; set; }

        [SearchableString]
        [Sortable]
        public string Shipaddress1 { get; set; }

        [SearchableString]
        [Sortable]
        public string Shipaddress2 { get; set; }

        [SearchableString]
        [Sortable]
        public string Shipaddress3 { get; set; }

        [SearchableString]
        [Sortable]
        public string Address1 { get; set; }

        [SearchableString]
        [Sortable]
        public string Address2 { get; set; }

        [SearchableString]
        [Sortable]
        public string Address3 { get; set; }

        [SearchableString]
        [Sortable]
        public string Terms { get; set; }

        [SearchableString]
        [Sortable]
        public string Via { get; set; }

        [SearchableString]
        [Sortable]
        public string Backorder { get; set; }

        [SearchableString]
        [Sortable]
        public string Tax { get; set; }

        [SearchableString]
        [Sortable]
        public string Ponum { get; set; }

        [Searchable]
        [Sortable]
        public DateTime? Shippeddate { get; set; }



        [Searchable]
        [Sortable]
        public DateTime Edidate { get; set; }

        [SearchableString]
        [Sortable]
        public string Terminal { get; set; }

        [SearchableString]
        [Sortable]
        public string Custnote { get; set; }

        [SearchableString]
        [Sortable]
        public string clerk { get; set; }

        [Searchable]
        [Sortable]
        public decimal Poammount { get; set; }

        [Searchable]
        [Sortable]
        public decimal Commission { get; set; }

        [SearchableString]
        [Sortable]
        public string Status { get; set; }

        [SearchableString]
        [Sortable]
        public string D1 { get; set; }

        [SearchableString]
        [Sortable]
        public string D2 { get; set; }

        [SearchableString]
        [Sortable]
        public string Creditmemo { get; set; }

        [SearchableString]
        [Sortable]
        public string Storenum { get; set; }

        [SearchableString]
        [Sortable]
        public string Dept { get; set; }

        [SearchableString]
        [Sortable]
        public string Ordertype { get; set; }

        [SearchableString]
        [Sortable]
        public string WeborderId { get; set; }

        [SearchableString]
        [Sortable]
        public string IsOpenOrder { get; set; }
        [SearchableString]
        [Sortable]
        public string Credit { get; set; }

        [Searchable]
        [Sortable]
        public decimal Freight { get; set; }

        [SearchableString]
        [Sortable]
        public string Slnum { get; set; }

        [SearchableString]
        [Sortable]
        public string Slnum2 { get; set; }

        [SearchableString]
        [Sortable]
        public string D0 { get; set; }
       
    }

    public class Order_DetailViewModel
    {
        public string Ordernum { get; set; }
        public string Itemnum { get; set; }
        public int Qtyordered { get; set; }
        public int Qtyinvoiced { get; set; }
        public decimal Saleprice { get; set; }
        public decimal Listprice { get; set; }
        public decimal Commission { get; set; }
    }

    public class ItemclassViewModel
    {
        public string ClassId { get; set; }

        [Required(ErrorMessage = "Please enter class")]
        [MaxLength(5, ErrorMessage = "Class should be less or equal to 5 characters.")]
        public string Class { get; set; }

        [Required(ErrorMessage = "Please enter Desc")]
        [MaxLength(40, ErrorMessage = "Class should be less or equal to 40 characters.")]
        public string Desc { get; set; }

        [Required(ErrorMessage = "Please enter Dept")]
        [MaxLength(25, ErrorMessage = "Class should be less or equal to 25 characters.")]
        public string Dept { get; set; }

        [Required(ErrorMessage = "Please enter Market")]
        [MaxLength(25, ErrorMessage = "Class should be less or equal to 25 characters.")]
        public string Market { get; set; }

        [Required(ErrorMessage = "Please enter Deptnum")]
        public int Deptnum { get; set; }

        [Required(ErrorMessage = "Please enter Buyer")]
        [MaxLength(2, ErrorMessage = "Class should be less or equal to 2 characters.")]
        public string Buyer { get; set; }
    }

    public class ResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class DeptProjectionViewModel
    {
        [Required(ErrorMessage = "Please enter department name")]
        [MaxLength(20, ErrorMessage = "Class should be less or equal to 20 characters.")]
        [Display(Name = "Department Name")]
        public string DeptName { get; set; }

        [Display(Name = "MM")]
        [Required(ErrorMessage = "Please enter mm")]
        public int mm { get; set; }

        [Required(ErrorMessage = "Please enter projection")]
        public decimal Projection { get; set; }
        public string DeptNameId { get; set; }

        public int mmId { get; set; }

    }

    public class InvoiceViewModel
    {
        public string Action { get; set; }
        [Searchable]
        [Sortable]
        public string ShipDate { get; set; }
        [Searchable]
        [Sortable]
        public string CancelDate { get; set; }
        [Searchable]
        [Sortable]
        public string OrderNum { get; set; }
        [Searchable]
        [Sortable]
        public string CustNum { get; set; }
        [Searchable]
        [Sortable]
        public string InvoiceNum { get; set; }
        [Searchable]
        [Sortable]
        public string OrderDate { get; set; }
        [Searchable]
        [Sortable]
        public string BillName { get; set; }
        [Searchable]
        [Sortable]
        public string BillAddress1 { get; set; }
        [Searchable]
        [Sortable]
        public string BillAddress2 { get; set; }
        [Searchable]
        [Sortable]
        public string BillAddress3 { get; set; }
        [Searchable]
        [Sortable]
        public string ShipName { get; set; }
        [Searchable]
        [Sortable]
        public string ShipAddress1 { get; set; }
        [Searchable]
        [Sortable]
        public string ShipAddress2 { get; set; }
        [Searchable]
        [Sortable]
        public string ShipAddress3 { get; set; }
        [Searchable]
        [Sortable]
        public string InvoiceDate { get; set; }
        [Searchable]
        [Sortable]
        public string ReleaseDate { get; set; }
        [Searchable]
        [Sortable]
        public string Freight { get; set; }
        [Searchable]
        [Sortable]
        public string SalesManNum { get; set; }
        [Searchable]
        [Sortable]
        public string SalesManNum2 { get; set; }

        [Searchable]
        [Sortable]
        public string TermId { get; set; }
        [Searchable]
        [Sortable]
        public string CarrierCode { get; set; }
        [Searchable]
        [Sortable]
        public string D2 { get; set; }
        [Searchable]
        [Sortable]
        public string PoNum { get; set; }


        [Searchable]
        [Sortable]
        public string EnterDate { get; set; }
        [Searchable]
        [Sortable]
        public string Terminal { get; set; }
        [Searchable]
        [Sortable]
        public string CustNote { get; set; }
        [Searchable]
        [Sortable]
        public string Clerk { get; set; }
        [Searchable]
        [Sortable]
        public decimal? NetTotal { get; set; }
        [Searchable]
        [Sortable]
        public decimal? Commision { get; set; }
        [Searchable]
        [Sortable]
        public string D4 { get; set; }
        [Searchable]
        [Sortable]
        public string D6 { get; set; }
        [Searchable]
        [Sortable]
        public string StoreNum { get; set; }
        [Searchable]
        [Sortable]
        public string Dept { get; set; }
        [Searchable]
        [Sortable]
        public string OrderType { get; set; }
        [Searchable]
        [Sortable]
        public string BolNum { get; set; }
        [Searchable]
        [Sortable]
        public string BolDate { get; set; }
        [Searchable]
        [Sortable]
        public string Tracking1 { get; set; }
        [Searchable]
        [Sortable]
        public string Tracking2 { get; set; }

        [Searchable]
        [Sortable]
        public bool? IsCreditMemo { get; set; }
        [Searchable]
        [Sortable]
        public bool? IsCreditHold { get; set; }
        [Searchable]
        [Sortable]
        public bool? IsFreightCollect { get; set; }


    }

    public class InvoicelineViewModel
    {
        public string Invoicenum { get; set; }
        public string Itemnum { get; set; }
        public int? Qtyordered { get; set; }
        public int? Qtyshipped { get; set; }
        public decimal? Saleprice { get; set; }
        public decimal? Listprice { get; set; }
        public decimal? Listlanded { get; set; }
        public decimal? Commission { get; set; }
        public decimal? Discount { get; set; }

    }

    public class UserViewModel
    {
          public int UserId { get; set; }

      
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter user name")]
        [EmailAddress(ErrorMessage ="Please enter valid email")]
        [MaxLength(50, ErrorMessage = "Role name should be less or equal to 50 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [MaxLength(15, ErrorMessage = "Role name should be greater than 7 characters and less or equal to 15 characters.")]
        [MinLength(8, ErrorMessage = "Role name should be greater than 7 characters and less or equal to 15 characters.")]
        public string Password { get; set; }


       
        [Required(ErrorMessage = "Please enter confirm password")]
        
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and confirm password should me same")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Please select role")]
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<RoleViewModel> RoleList { get; set; }
        public string Rolename { get; set; }
        public UserViewModel() 
        {
            RoleList = new List<RoleViewModel>();
        }
    }

    public class RoleViewModel
    {
         public int RoleId { get; set; }

        [Display(Name ="Role Name")]
        [Required(ErrorMessage = "Please enter role name")]
        [MaxLength(50, ErrorMessage = "Role name should be less or equal to 50 characters.")]
        public string Rolename { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }

    public class MenuViewModel
    {
         public int MenuId { get; set; }
         public string Name { get; set; }
        public string Parent { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Childmenus { get; set; }
        public string Icon { get; set; }
        public string Parenticon { get; set; }
        public bool Ischecked { get; set; }

    }
    public class UserclaimViewModel
    {
         
        public int ClaimId { get; set; }
        [Display(Name ="Role")]
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public string Columns { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<MenuViewModel> Menus { get; set; }
        public UserclaimViewModel()
        {
            Menus = new List<MenuViewModel>();
        }

    }

    public class DashboardViewModel
    {

        public List<DashboardLineChartViewModel> YTDList { get; set; }
        public List<DashboardLineChartViewModel> LYTDList { get; set; }



        public DashboardViewModel()
        {
            YTDList = new List<DashboardLineChartViewModel>();
            LYTDList = new List<DashboardLineChartViewModel>();
        }

    }

    public class DashboardLineChartViewModel
    {
        public int Month { get; set; }
        public decimal Poammount { get; set; }
        public string ordernum { get; set; }
    }

    public class DashboardBlockViewModel
    {
        public int TotalRecord { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class BoxAmountViewModel
    {
        public string TodayAmount { get; set; }
        public string YTDAmount { get; set; }
        public string LYTDAmount { get; set; }
        public string LastYearAmount { get; set; }

    }

    public class TovBolViewModel
    {

        public string TovBol_custnum { get; set; }
        public long TovBol_Ordernum { get; set; }
        public string TovBol_PO { get; set; }
        public DateTime? TovBol_OrderDate { get; set; }
        public DateTime? TovBol_CancelDate { get; set; }
        public DateTime? TovBol_ShipDate { get; set; }
        public DateTime? TovBol_UpdateDate { get; set; }
        public long TovBol_Bol { get; set; }
        public long? TovBol_Ref { get; set; }
        public long? TovBol_Boxes { get; set; }
        public decimal? TovBol_Value { get; set; }


    }
}