using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySqlBasicCore.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
           : base(options) { }
          
        public DbSet<Bol_1> tbl_Bol_1s { get; set; }
        public DbSet<Bol_2> tbl_Bol_2s { get; set; }
        public DbSet<Item> tbl_Items { get; set; }
        public DbSet<Order> tbl_Orders { get; set; }
        public DbSet<Orderline> tbl_Orderlines { get; set; }
        public DbSet<Itemclass> tbl_Itemclasses { get; set; }
        public DbSet<User> tbl_Users { get; set; }
        public DbSet<Role> tbl_Roles { get; set; }
        public DbSet<Menu> tbl_Menus { get; set; }
        public DbSet<Userclaim> tbl_Userclaim { get; set; }
        public DbSet<Invoiceline> tbl_Invoieclines { get; set; }
        public DbSet<Invoice> tbl_Invoice { get; set; }
        public DbSet<DeptProjection> tbl_DeptProjection { get; set; }
        public DbSet<TovBol> tbl_TovBol { get; set; }
        public DbSet<OrderNote> tbl_OrderNote { get; set; }
        public DbSet<Tovordernote> tbl_Tovordernote { get; set; }
        public DbSet<IndsellCompo> tbl_IndsellCompo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bol_2>(eb => { eb.HasNoKey(); });
            modelBuilder.Entity<Item>(eb => { eb.HasNoKey(); });
            modelBuilder.Entity<Order>(eb => { eb.HasNoKey(); });
            modelBuilder.Entity<Orderline>(eb => { eb.HasNoKey(); });
            modelBuilder.Entity<Itemclass>(eb => { eb.HasNoKey(); });
            modelBuilder.Entity<Invoice>(eb => { eb.HasNoKey(); });
            modelBuilder.Entity<Invoiceline>(eb => { eb.HasNoKey(); });
            modelBuilder.Entity<DeptProjection>(eb => { eb.HasNoKey(); });
            modelBuilder.Entity<OrderNote>(eb => { eb.HasNoKey(); });
            modelBuilder.Entity<IndsellCompo>(eb => { eb.HasNoKey(); });


        }
    }

    public class Bol_1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bol1_no { get; set; }

        public DateTime bol1_date { get; set; }
        public string bol1_name { get; set; }
        public string bol1_adrs1 { get; set; }
        public string bol1_city { get; set; }
        public string bol1_state { get; set; }
        public string bol1_zip { get; set; }
        public string bol1_Lctn { get; set; }
        public int bol1_pro_no { get; set; }
        public string bol1_scac { get; set; }
        public string bol1_frght_terms { get; set; }
        public int bol1_ttl_pkgs { get; set; }
        public decimal bol1_ttl_weight { get; set; }
        public string bol1_HE_WH { get; set; }
        public DateTime? bol1_OrderDate { get; set; }
        public DateTime? bol1_CancelDate { get; set; }
        public Decimal? bol1_ttlValue { get; set; }
        public string bol1_carrierName { get; set; }
        public string bol1_carrierPhone { get; set; }
        public DateTime? bol1_PkupDate { get; set; }
        public DateTime? bol1_pkupTime { get; set; }
        public string Conformation { get; set; }
        public int? bol1_pallet { get; set; }
        public string bol1_pallet_type { get; set; }
    }

    public class Bol_2
    {
        public string bol2_order_no { get; set; }
        public int bol2_pkgs { get; set; }
        public decimal bol2_weight { get; set; }
        public int? bol2_No { get; set; }
        public string bol2_PO { get; set; }
        public decimal? bol2_value { get; set; }
        public string bol2_custnum { get; set; }
    }

    public class Item
    {
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

    [Table("orders")]
    public class Order
    {
        [NotMapped]
        public string Action { get; set; }

       
        public int? NoteCount { get; set; }

        public string Custnum { get; set; }

        public string ordernum { get; set; }

        public DateTime Orderdate { get; set; }

        public string Credit { get; set; }

        public decimal Freight { get; set; }

        public string Slnum { get; set; }

        public string Slnum2 { get; set; }

        public string D0 { get; set; }

        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string Shipname { get; set; }

        public string Shipaddress1 { get; set; }

        public string Shipaddress2 { get; set; }

        public string Shipaddress3 { get; set; }

        public string Terms { get; set; }

        public string Via { get; set; }

        public string Backorder { get; set; }

        public string Tax { get; set; }

        public string Ponum { get; set; }

        public DateTime? Shippeddate { get; set; }

        public DateTime Shipdate { get; set; }

        public DateTime? Canceldate { get; set; }

        public DateTime Edidate { get; set; }

        public string Terminal { get; set; }

        public string Custnote { get; set; }

        public string clerk { get; set; }

        public decimal Poammount { get; set; }

        public decimal Commission { get; set; }

        public string Status { get; set; }

        public string D1 { get; set; }

        public string D2 { get; set; }

        public string Creditmemo { get; set; }

        public string Storenum { get; set; }

        public string Dept { get; set; }

        public string Ordertype { get; set; }

        public string WeborderId { get; set; }

        public bool IsOpenOrder { get; set; }
    }

    [Table("Orderlines")]
    public class Orderline
    {
        public int ID { get; set; }
        public string Ordernum { get; set; }
        public string Itemnum { get; set; }
        public int Qtyordered { get; set; }
        public int Qtyinvoiced { get; set; }
        public decimal Saleprice { get; set; }
        public decimal Listprice { get; set; }
        public decimal Commission { get; set; }
    }

    public class Itemclass
    {
        public string ClassId { get; set; }

        public string Class { get; set; }

        public string Desc { get; set; }

        public string Dept { get; set; }

        public string Market { get; set; }

        public int Deptnum { get; set; }

        public string Buyer { get; set; }
    }

    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate  { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Email { get; set; }
    }

    [Table("roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        public string Rolename { get; set; }

        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    [Table("menus")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }

        public string Name { get; set; }
        public string Parent { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Childmenus { get; set; }

    }

    [Table("userclaims")]
    public class Userclaim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClaimId { get; set; }
        public int MenuId { get; set; }
        public string Columns { get; set; }
      
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int RoleId { get; set; }

    }

    [Table("DeptProjection")]
    public class DeptProjection
    {
        
        public string DeptName { get; set; }

        public int mm { get; set; }

        public decimal Projection { get; set; }

    }

    [Table("invoice")]
    public class Invoice
    {

        public string CustNum { get; set; }
        public string InvoiceNum { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal? Freight { get; set; }
        public string SalesManNum { get; set; }
        public string SalesManNum2 { get; set; }
        public string BillName { get; set; }
        public string BillAddress1 { get; set; }
        public string BillAddress2 { get; set; }
        public string BillAddress3 { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipAddress3 { get; set; }
        public string TermId { get; set; }
        public string CarrierCode { get; set; }
        public string D2 { get; set; }
        public string PoNum { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? EnterDate { get; set; }
        public string Terminal { get; set; }
        public string CustNote { get; set; }
        public string Clerk { get; set; }
        public decimal? NetTotal { get; set; }
        public decimal? Commision { get; set; }
        public string D4 { get; set; }
        public string D6 { get; set; }
        public string StoreNum { get; set; }
        public string Dept { get; set; }
        public string OrderType { get; set; }
        public string BolNum { get; set; }
        public DateTime? BolDate { get; set; }
        public string Tracking1 { get; set; }
        public string Tracking2 { get; set; }
        public string OrderNum { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public bool? IsCreditMemo { get; set; }
        public bool? IsCreditHold { get; set; }
        public bool? IsFreightCollect { get; set; }


    }

    [Table("invoicelines")]
    public class Invoiceline
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

    [Table("TovBol")]
    public class TovBol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TovBol_id { get; set; }
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
        public string TovBol_scac { get; set; }
        public long TovBol_pro { get; set; }
        public string TovBol_Whse { get; set; }
        public string TovBol_freightTerms { get; set; }

    }

    [Table("ordernotes")]
    public class OrderNote
    {
        public string Ordernum { get; set; }
        public string Year { get; set; }
        public int Line { get; set; }
        public string Note { get; set; }
    }

    [Table("tov_ordernotes")]
    public class Tovordernote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Tovordernote_id { get; set; }

        public string Ordernum { get; set; }
        public string Year { get; set; }
        public int Line { get; set; }
        public string Note { get; set; }
    }

    [Table("indSell_Compo")]
    public class IndsellCompo
    {
        [Column(Order = 0)]
        public string indSell_ItemMaster { get; set; }
        
        [Column(Order = 1)]
        public string indSell_ItemComponent { get; set; }
        public int indSell_Allowed { get; set; }
      
    }

    [Table("deptamount")]
    public class deptamount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int deptamount_id { get; set; }

        public string Dept { get; set; }
        public decimal Amount { get; set; }
       
    }


}