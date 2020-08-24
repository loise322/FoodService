using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Site.Core;

namespace TravelLine.Food.Site.Admin
{
    public partial class EditMeal : System.Web.UI.Page
    {
        private readonly IDishService _dishService;
        private readonly ISupplierService _supplierService;

        private bool _Error = false;
        private DishModel _dish = null;

        public EditMeal( IDishService dishService, ISupplierService supplierService )
        {
            _dishService = dishService;
            _supplierService = supplierService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            int dishId = WebHelper.GetRequestParamAsInt( "Id" );
            if ( dishId == 0 )
            {
                _dish = new DishModel();
            }
            else
            {
                _dish = _dishService.GetDish( dishId );
                if ( _dish == null )
                {
                    ErrorPH.Visible = true;
                    DataPH.Visible = false;
                    _Error = true;
                }
            }
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );
            if ( !IsPostBack )
            {
                DishType.Items.Add( new ListItem( DishTypesStrings.Salat, Domain.Dishes.DishType.Salat.ToString() ) );
                DishType.Items.Add( new ListItem( DishTypesStrings.Soup, Domain.Dishes.DishType.Soup.ToString() ) );
                DishType.Items.Add( new ListItem( DishTypesStrings.Garnish, Domain.Dishes.DishType.Garnish.ToString() ) );
                DishType.Items.Add( new ListItem( DishTypesStrings.SecondDish, Domain.Dishes.DishType.SecondDish.ToString() ) );

                List<SupplierModel> suppliers = _supplierService.GetSuppliers();
                ddlSuppliers.DataSource = suppliers;
                ddlSuppliers.DataBind();
            }
            if ( !IsPostBack && !_Error && ( _dish != null ) )
            {
                DishName.Text = _dish.Name;
                DishDescr.Text = _dish.Description;
                DishCost.Text = _dish.Cost.ToString();
                DishType.SelectedIndex = DishType.Items.IndexOf( DishType.Items.FindByValue( _dish.Type.ToString() ) );
                DishWeight.Text = _dish.Weight.ToString();
                DishSingle.Checked = _dish.IsSingle;
                DishImg.ImageUrl = ConfigService.WebImagesStoreBig + _dish.ImagePath;
                ddlSuppliers.SelectedValue = _dish.SupplierId.ToString();
            }
        }

        protected void SaveMeal( object sender, EventArgs e )
        {
            try
            {
                _dish.Cost = Decimal.Parse( DishCost.Text.Replace( ".", "," ) );
                if ( _dish.Cost < 0 )
                {
                    throw new Exception();
                }
                _dish.Description = DishDescr.Text;
                _dish.Name = DishName.Text;
                _dish.IsSingle = DishSingle.Checked;
                _dish.Weight = Int32.Parse( DishWeight.Text.Replace( ".", "," ) );
                _dish.Type = (Domain.Dishes.DishType)Enum.Parse( typeof( Domain.Dishes.DishType ), DishType.Items[DishType.SelectedIndex].Value );
                _dish.SupplierId = Convert.ToInt32( ddlSuppliers.SelectedValue );
                SaveImage();
                _dishService.Save( _dish );

                SetRedirPage( true );
            }
            catch
            {
                SetRedirPage( false );
            }
        }

        protected void SetRedirPage( bool res )
        {
            ErrorPH.Visible = true;
            DataPH.Visible = false;

            lnkBack.NavigateUrl = String.Format( "~/Admin/DishEdit.aspx?Id={0}", _dish.Id );
            lnkMain.NavigateUrl = "~/Admin/DishList.aspx";

            if ( res )
            {
                lblResult.Text = "Данные успешно сохранены.";
            }
            else
            {
                lblResult.Text = "Ошибка при сохранение данных.";
                lblResult.Style[ "color" ] = "red";
            }
        }

        protected void SaveImage()
        {
            if ( UploadImg.HasFile )
            {
                _dishService.SaveImage( _dish, UploadImg.PostedFile );
            }
            if ( RemoveImg.Checked )
            {
                _dish.ImagePath = "";
            }
        }
    }
}
