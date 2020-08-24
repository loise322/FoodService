using System;
using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Domain.Dishes;

namespace TravelLine.Food.Site.Admin
{
    public partial class Make_Order : System.Web.UI.Page
    {
        private readonly IDishService _dishService;
        private readonly IMenuService _menuService;
        private readonly ISupplierService _supplierService;

        public Make_Order( 
            IDishService dishService, 
            IMenuService menuService,
            ISupplierService supplierService )
        {
            _dishService = dishService;
            _menuService = menuService;
            _supplierService = supplierService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {

        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );
            if ( !IsPostBack )
            {
                MenuCalendar.SelectedDate = DateTime.Today;

                List<SupplierModel> suppliers = _supplierService.GetSuppliers();
                ddlSuppliers.DataSource = suppliers;
                ddlSuppliers.DataBind();
            }

            MenuCalendar.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler( MenuCalendar_DayRender );

            lblDate.Text = MenuCalendar.SelectedDate.ToLongDateString();
            SaveBtn.OnClientClick = String.Format( "collectData('{0}','{1}','{2}','{3}','{4}')",
                                                    SalatsListMenu.ClientID,
                                                    SoupListMenu.ClientID,
                                                    SecondDishMenu.ClientID,
                                                    GarnishListMenu.ClientID,
                                                    hdnDishIdsList.ClientID );

            int supplierId = Convert.ToInt32( ddlSuppliers.SelectedValue );
            FillMenuList( supplierId );
            FillSelectedMeal( supplierId );
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

        private bool CheckDateForOrder( DateTime date )
        {
            return _menuService.GetStatus( date ) != DayStatus.Closed;
        }

        private MenuModel GetMealMenu()
        {
            return _menuService.GetMenu( MenuCalendar.SelectedDate );
        }

        private void FillSelectedMeal( int supplierId )
        {
            MenuModel menu = GetMealMenu();

            if ( menu != null )
            {
                List<DishModel> dlSalat = menu.GetByDishType( DishType.Salat );
                SalatsListMenu.DataSource = dlSalat.Where( d => d.SupplierId == supplierId );
                SalatsListMenu.DataValueField = "Id";
                SalatsListMenu.DataTextField = "NameCost";
                SalatsListMenu.DataBind();

                List<DishModel> dlSoup = menu.GetByDishType( DishType.Soup );
                SoupListMenu.DataSource = dlSoup.Where( d => d.SupplierId == supplierId );
                SoupListMenu.DataValueField = "Id";
                SoupListMenu.DataTextField = "NameCost";
                SoupListMenu.DataBind();

                List<DishModel> dlGarnish = menu.GetByDishType( DishType.Garnish );
                GarnishListMenu.DataSource = dlGarnish.Where( d => d.SupplierId == supplierId );
                GarnishListMenu.DataValueField = "Id";
                GarnishListMenu.DataTextField = "NameCost";
                GarnishListMenu.DataBind();

                List<DishModel> dlSecondDish = menu.GetByDishType( DishType.SecondDish );
                SecondDishMenu.DataSource = dlSecondDish.Where( d => d.SupplierId == supplierId );
                SecondDishMenu.DataValueField = "Id";
                SecondDishMenu.DataTextField = "NameCost";
                SecondDishMenu.DataBind();
            }
            else
            {
                SalatsListMenu.Items.Clear();
                SecondDishMenu.Items.Clear();
                SoupListMenu.Items.Clear();
                GarnishListMenu.Items.Clear();
            }
        }

        private void FillMenuList( int supplierId )
        {
            List<DishModel> dlSalat = _dishService.GetDishesByType( DishType.Salat );
            SalatsList.DataSource = dlSalat.Where( d => d.SupplierId == supplierId );
            SalatsList.DataValueField = "Id";
            SalatsList.DataTextField = "NameCost";
            SalatsList.DataBind();

            List<DishModel> dlSoup = _dishService.GetDishesByType( DishType.Soup );
            SoupList.DataSource = dlSoup.Where( d => d.SupplierId == supplierId );
            SoupList.DataValueField = "Id";
            SoupList.DataTextField = "NameCost";
            SoupList.DataBind();

            List<DishModel> dlSecondDish = _dishService.GetDishesByType( DishType.SecondDish );
            SecondDishList.DataSource = dlSecondDish.Where( d => d.SupplierId == supplierId );
            SecondDishList.DataValueField = "Id";
            SecondDishList.DataTextField = "NameCost";
            SecondDishList.DataBind();

            List<DishModel> dlGarnish = _dishService.GetDishesByType( DishType.Garnish );
            GarnishList.DataSource = dlGarnish.Where( d => d.SupplierId == supplierId );
            GarnishList.DataValueField = "Id";
            GarnishList.DataTextField = "NameCost";
            GarnishList.DataBind();
        }

        public void Calendar_SelectionChanged( object sender, EventArgs e )
        {
            SaveBtn.Enabled = CheckDateForOrder( MenuCalendar.SelectedDate.Date );
        }

        public void SubmitData( object obj, EventArgs e )
        {
            if ( !CheckDateForOrder( MenuCalendar.SelectedDate.Date ) )
            {
                // Нельзя сохранять меню если заказ неоткрыт
                return;
            }

            if ( hdnDishIdsList.Value.Length > 0 )
            {
                List<DishModel> dishes = _dishService.GetDishesByIds( hdnDishIdsList.Value.Split( ',' ).Select( i => Int32.Parse( i ) ) );

                MenuModel menu = _menuService.GetMenu( MenuCalendar.SelectedDate );
                if ( menu == null )
                {
                    menu = new MenuModel
                    {
                        IsOrdered = false,
                        Date = MenuCalendar.SelectedDate
                    };
                }

                int supplierId = Convert.ToInt32( ddlSuppliers.SelectedValue );
                menu.Dishes = menu.Dishes.Where( d => d.SupplierId != supplierId ).ToList();
                menu.Dishes.AddRange( dishes );

                _menuService.Save( menu );
            }
            else
            {
                return;
            }

        }

    }
}
