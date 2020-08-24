using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.DishQuotas;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Domain.Suppliers;

namespace TravelLine.Food.Site.Admin
{
    public partial class MakeMenuFromFile : System.Web.UI.Page
    {
        private readonly IDishService _dishService;
        private readonly IMenuService _menuService;
        private readonly ISupplierService _supplierService;
        private readonly IDishQuotaService _dishQuotaService;

        protected List<ImportedDishModel> Dishes;
        protected int SelectedSupplierId;

        public MakeMenuFromFile(
            IDishService dishService,
            IMenuService menuService,
            ISupplierService supplierService,
            IDishQuotaService dishQuotaService )
        {
            _dishService = dishService;
            _menuService = menuService;
            _supplierService = supplierService;
            _dishQuotaService = dishQuotaService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            Dishes = new List<ImportedDishModel>();
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );
            if ( !IsPostBack )
            {
                MenuCalendar.SelectedDate = DateTime.Now.Date;
            }

            MenuCalendar.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler( MenuCalendar_DayRender );
            lblDate.Text = MenuCalendar.SelectedDate.ToLongDateString();

            List<SupplierUploadItem> supplierUploadItems = _supplierService
                .GetSuppliers()
                .ConvertAll( s => new SupplierUploadItem()
                {
                    SupplierId = s.Id,
                    Name = s.Name,
                    IsUploaded = IsUploadedMenuForSupplier( s.Id, MenuCalendar.SelectedDate.Date )
                } );

            SupplierList.DataSource = supplierUploadItems;
            SupplierList.DataBind();
        }

        private bool IsUploadedMenuForSupplier( int supplierId, DateTime date )
        {
            MenuModel menu = _menuService.GetMenu( date );
            return menu != null && menu.Dishes != null && menu.Dishes.Any( d => d.SupplierId == supplierId );
        }

        protected void MenuCalendar_DayRender( object sender, System.Web.UI.WebControls.DayRenderEventArgs e )
        {
            DayStatus status = _menuService.GetStatus( e.Day.Date );
            switch ( status )
            {
                case DayStatus.Closed:
                    e.Cell.BackColor = System.Drawing.Color.Gold;
                    break;
                case DayStatus.PreparedForOrder:
                    e.Cell.BackColor = System.Drawing.Color.Green;
                    e.Cell.ForeColor = System.Drawing.Color.White;
                    break;

            }

            if ( e.Day.Date.Date == ( ( System.Web.UI.WebControls.Calendar )sender ).SelectedDate.Date )
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
                e.Cell.ForeColor = System.Drawing.Color.White;
            }
        }

        /// <summary>
        /// Получаем список блюд загруженных из файла
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        private List<ImportedDishModel> GetMenuList( int supplierId )
        {
            var result = new List<ImportedDishModel>();
            var existedSupplierDishes = new List<DishModel>();
            existedSupplierDishes.AddRange( _dishService.GetSupplierDishes( supplierId ) );
            SupplierModel supplier = _supplierService.GetSupplier( supplierId );

            HSSFWorkbook hssfwb;
            using ( FileStream file = new FileStream( Server.MapPath( "~/Uploads/" ) + "menu.xls", FileMode.Open, FileAccess.Read ) )
            {
                hssfwb = new HSSFWorkbook( file );
            }

            Dishes = new List<ImportedDishModel>();
            ISheet sheet = hssfwb.GetSheetAt( 0 );

            var existedDishesCache = new Dictionary<string, DishModel>();
            foreach ( DishModel existedSupplierDish in existedSupplierDishes )
            {
                string dishName = ClearName( existedSupplierDish.Name ).ToLower();
                if ( !existedDishesCache.ContainsKey( dishName ) )
                {
                    existedDishesCache.Add( dishName, existedSupplierDish );
                }

            }

            DishType dishType = DishType.Salat;
            for ( int row = 0; row <= sheet.LastRowNum; row++ )
            {
                if ( sheet.GetRow( row ) != null ) //null is when the row only contains empty cells 
                {
                    IRow callRow = sheet.GetRow( row );

                    if ( callRow.Cells.Count <= 1 )
                    {
                        continue;
                    }

                    switch ( callRow.Cells[ 1 ].StringCellValue )
                    {
                        case "Торты и пирожные":
                            dishType = DishType.Salat;
                            break;
                        case "Холодные блюда и закуски":
                            dishType = DishType.Salat;
                            break;
                        case "Первые блюда":
                            dishType = DishType.Soup;
                            break;
                        case "Вторые блюда":
                            dishType = DishType.SecondDish;
                            break;
                        case "Гарниры":
                            dishType = DishType.Garnish;
                            break;

                    }
                    if ( callRow.Cells[ 1 ].StringCellValue == "Горячие напитки" || callRow.Cells[ 1 ].StringCellValue.StartsWith( "Соус" ) )
                    {
                        break;
                    }

                    if ( callRow.Cells.Count > 14
                        && callRow.Cells[ 1 ].StringCellValue != String.Empty
                        && callRow.Cells[ 14 ].StringCellValue != String.Empty )
                    {
                        decimal newPrice;
                        int? quota = null;

                        try
                        {
                            newPrice = GetPriceValue( callRow.Cells[ 14 ].StringCellValue );
                            if ( supplier.Discount > 0 )
                            {
                                newPrice = Math.Round( newPrice * ( 1 - ( ( decimal )supplier.Discount / 100 ) ), 2 );
                            }
                            if ( callRow.Cells.Count > 19 && !String.IsNullOrEmpty( callRow.Cells[ 19 ].ToString() ) )
                            {
                                quota = GetQuotaValue( callRow.Cells[ 19 ].ToString() );
                            } 
                            else if ( callRow.Cells.Count > 20 && !String.IsNullOrEmpty( callRow.Cells[ 20 ].ToString() ) )
                            {
                                quota = GetQuotaValue( callRow.Cells[ 20 ].ToString() );
                            }
                        }
                        catch ( FormatException ) { throw new ArgumentException( $"Message ({callRow.Cells[ 14 ].StringCellValue})" ); }

                        string name = ClearName( callRow.Cells[ 1 ].StringCellValue );
                        existedDishesCache.TryGetValue( name.ToLower(), out DishModel existedDish );

                        if ( existedDish != null )
                        {
                            var dishImportModel = new ImportedDishModel( existedDish )
                            {
                                NewPrice = newPrice,
                                Quota = quota
                            };
                            result.Add( dishImportModel );
                        }
                        else
                        {
                            string description = ClearDescription( callRow.Cells[ 1 ].StringCellValue );
                            result.Add( new ImportedDishModel()
                            {
                                Name = name,
                                Cost = newPrice,
                                NewPrice = newPrice,
                                IsNew = true,
                                Type = dishType,
                                Description = description,
                                SupplierId = supplierId,
                                Quota = quota
                            } );
                        }
                    }
                }
            }

            return result;
        }

        private decimal GetPriceValue( string stringPrice )
        {
            return Convert.ToDecimal( stringPrice, CultureInfo.InvariantCulture );
        }

        private int? GetQuotaValue( string stringQuota )
        {
            if ( String.IsNullOrEmpty( stringQuota ) || String.IsNullOrWhiteSpace( stringQuota ) )
            {
                return null;
            }

            return Convert.ToInt32( stringQuota, CultureInfo.InvariantCulture );
        }

        private string ClearName( string name )
        {
            name = name.Split( '(' ).First();
            name = Regex.Replace( name, @"[\d]+\s[гр]+", "" );
            name = Regex.Replace( name, @"\(пост[^)]*\)", "" );
            name = Regex.Replace( name, @"пост[^\s]*", "" );
            name = Regex.Replace( name, @"\s+", " " );
            name = Regex.Replace( name, ",", "" );
            name = Regex.Replace( name, "\"", "'" );
            name = name.Trim();

            return name;
        }

        private string ClearDescription( string description )
        {
            description = description.Replace( "(буфет)", "" );
            description = description.Split( '(' ).Last().Split( ')' ).First();

            return description;
        }

        protected void Calendar_SelectionChanged( object sender, EventArgs e )
        {
        }

        protected void OnRowCommand( object sender, GridViewCommandEventArgs e )
        {
            if ( e.CommandName == "UploadMenuFile" )
            {
                int rowIndex = Convert.ToInt32( e.CommandArgument.ToString() );
                DoUploadMenuFile( rowIndex );
            }
        }

        private void DoUploadMenuFile( int rowIndex )
        {
            try
            {
                GridViewRow row = SupplierList.Rows[ rowIndex ];
                var uploadedMenuFile = ( FileUpload )row.FindControl( "FileUploadControl" );
                int supplierId = Convert.ToInt32( SupplierList.DataKeys[ rowIndex ].Value );
                SelectedSupplierId = supplierId;

                if ( uploadedMenuFile.HasFile )
                {
                    string filename = Path.GetFileName( uploadedMenuFile.FileName );
                    uploadedMenuFile.PostedFile.SaveAs( Server.MapPath( "~/Uploads/" ) + "menu.xls" );

                    Dishes = GetMenuList( supplierId );
                    SupplierModel supplier = _supplierService.GetSupplier( SelectedSupplierId );
                    lblSupplier.Text = $"Меню на {MenuCalendar.SelectedDate.Date:D}: {supplier.Name}";
                }
            }
            catch ( Exception ex )
            {
                ErrorLabel.Text = ex.Message + ex.StackTrace;
                ErrorLabel.Visible = true;
            }
        }
        protected void SubmitData( object obj, EventArgs e )
        {
            string newDishNames = Request.Form[ "newDish" ];
            string dishIds = Request.Form[ "menuDish" ];
            int supplierId = Convert.ToInt32( Request.Form[ "supplierId" ] );

            /// Получаем все блюда из файла
            List<ImportedDishModel> importedDishes = GetMenuList( supplierId );
            var existedDishes = new Dictionary<int, ImportedDishModel>();
            foreach ( ImportedDishModel importedDish in importedDishes )
            {
                if ( importedDish.Id > 0 && !existedDishes.ContainsKey( importedDish.Id ) )
                {
                    existedDishes.Add( importedDish.Id, importedDish );
                }
            }

            /// Готовим список существующих блюд для добавления в меню
            List<int> menuDishIds = new List<int>();
            if ( !String.IsNullOrEmpty( dishIds ) )
            {
                menuDishIds = dishIds.Split( ',' ).ToList().ConvertAll( Convert.ToInt32 );
            }

            /// Обновляем цены и квоту
            foreach ( int dishId in menuDishIds )
            {
                ImportedDishModel importedDish = existedDishes[ dishId ];

                if ( importedDish.NewPrice > 0 && importedDish.NewPrice != importedDish.Cost )
                {
                    DishModel dish = _dishService.GetDish( importedDish.Id );
                    dish.Cost = importedDish.NewPrice;
                    _dishService.Save( dish );
                }

                if ( importedDish.Quota.HasValue )
                {
                    _dishQuotaService.SetDishQuota( importedDish.Id, MenuCalendar.SelectedDate.Date, importedDish.Quota.Value );
                } 
                else
                {
                    _dishQuotaService.RemoveDishQuota( importedDish.Id, MenuCalendar.SelectedDate.Date );
                }
            }

            /// Добавляем новые блюда в базу и сохраняем квоту
            if ( !String.IsNullOrEmpty( newDishNames ) )
            {
                List<string> newMenuDishNames = newDishNames.Split( ',' ).ToList();
                foreach ( string newMenuDishName in newMenuDishNames )
                {
                    ImportedDishModel importedDish = importedDishes.FirstOrDefault( d => d.Name == newMenuDishName );
                    if ( importedDish != null )
                    {
                        DishModel dish = importedDish.ToDishModel();
                        _dishService.Save( dish );
                        menuDishIds.Add( dish.Id );
                        importedDish.Id = dish.Id;

                        if ( importedDish.Quota.HasValue && importedDish.Quota.Value > 0 )
                        {
                            _dishQuotaService.SetDishQuota( importedDish.Id, MenuCalendar.SelectedDate.Date, importedDish.Quota.Value );
                        }
                    }
                }
            }

            /// Сохраняем получившийся список в меню
            List<DishModel> dishes = _dishService.GetDishesByIds( menuDishIds );
            MenuModel menu = _menuService.GetMenu( MenuCalendar.SelectedDate.Date );
            if ( menu == null )
            {
                menu = new MenuModel
                {
                    IsOrdered = false,
                    Date = MenuCalendar.SelectedDate.Date,
                    Dishes = new List<DishModel>()
                };
            }

            menu.Dishes = menu.Dishes.Where( d => d.SupplierId != supplierId ).ToList();
            menu.Dishes.AddRange( dishes );

            _menuService.Save( menu );
            lblSupplier.Text = String.Empty;
        }

        protected class SupplierUploadItem
        {
            public int SupplierId { get; set; }

            public string Name { get; set; }

            public bool IsUploaded { get; set; }

            public string IsUploadedText => IsUploaded ? "Меню загружено" : "Меню не загружено";
        }

        protected class ImportedDishModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal Cost { get; set; }

            public string Description { get; set; }

            public DishType Type { get; set; }

            public int Weight { get; set; }

            public bool IsNew { get; set; }

            public decimal NewPrice { get; set; }

            public int SupplierId { get; set; }

            public int? Quota { get; set; }

            public ImportedDishModel()
            {

            }

            public ImportedDishModel( DishModel dishModel )
            {
                Id = dishModel.Id;
                Name = dishModel.Name;
                Cost = dishModel.Cost;
                Description = dishModel.Description;
                Type = dishModel.Type;
                Weight = dishModel.Weight;
                IsNew = false;
                NewPrice = dishModel.Cost;
                SupplierId = SupplierId;
                Quota = null;
            }

            public DishModel ToDishModel()
            {
                return new DishModel()
                {
                    Id = Id,
                    Name = Name,
                    Cost = Cost,
                    Description = Description,
                    Type = Type,
                    Weight = Weight,
                    SupplierId = SupplierId,
                };
            }
        }
    }
}
