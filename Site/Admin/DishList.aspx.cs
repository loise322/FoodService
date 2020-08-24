using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravelLine.Food.Core;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Domain.Dishes;

namespace TravelLine.Food.Site.Admin
{
    public partial class MealList : Page
    {
        private readonly IDishService _dishService;

        private List<DishModel> _dishes;
        private string _filter = String.Empty;

        public MealList( IDishService dishService )
        {
            _dishService = dishService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            _dishes = _dishService.GetAllDishes();
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );
            _filter = FilterBox.Text;
            if ( !IsPostBack )
            {
                grdDishList.DataSource = GetMealList();
            }
            else if ( _filter.Length > 0 )
            {
                grdDishList.DataSource = GetMealListFiltered( _filter );
            }
            else
            {
                grdDishList.DataSource = GetMealList();
                grdDishList.DataBind();
            }

            grdDishList.DataBind();
        }

        private DataTable GetMealListFiltered( string filter )
        {
            DataTable res = GetTable();
            filter = filter.ToLower();
            foreach ( DishModel dish in _dishes )
            {
                if ( dish.Name.ToLower().Contains( filter ) )
                {
                    res.Rows.Add( dish.Id, dish.Name, dish.Cost, TextType( dish.Type ), dish.Weight );
                }
            }
            return res;
        }

        private DataTable GetMealList()
        {
            DataTable res = GetTable();
            foreach ( DishModel dish in _dishes )
            {
                res.Rows.Add( dish.Id, dish.Name, dish.Cost, TextType( dish.Type ), dish.Weight );
            }
            return res;
        }

        private DataTable GetTable()
        {
            DataTable res = new DataTable();
            res.Columns.Add( "id_dish" );
            res.Columns.Add( "dish_name" );
            res.Columns.Add( "cost" );
            res.Columns.Add( "type" );
            res.Columns.Add( "weight" );
            return res;
        }

        public string TextType( object text )
        {
            return Library.GetStringType( ( DishType )Convert.ToInt32( text ) );
        }

        public void PageIndexChanging( object sender, GridViewPageEventArgs e )
        {
            ( ( GridView )sender ).PageIndex = e.NewPageIndex;
        }

        protected void ProcessRow( object sender, CommandEventArgs e )
        {
            int param = Convert.ToInt32( e.CommandArgument );
            DishModel dish = _dishService.GetDish( param );
            if ( dish != null && !_dishService.IsDishUsed( dish.Id ) )
            {
                _dishService.Remove( dish.Id );
                Page_Load( sender, e );
            }
            else
            {
                ScriptManager.RegisterStartupScript( this, typeof( Page ), "ErrorMsg",
                    "$(document).ready(function(){ alert('Данное блюдо уже используется.') });", true );
            }

        }

        protected void ApplyFilter( object sender, EventArgs e )
        {
            _filter = FilterBox.Text;
        }
    }
}
