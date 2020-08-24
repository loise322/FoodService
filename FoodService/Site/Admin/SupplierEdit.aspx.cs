using System;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Site.Core;

namespace TravelLine.Food.Site.Admin
{
    public partial class SupplierEdit : System.Web.UI.Page
    {
        private readonly ISupplierService _supplierService;

        private SupplierModel _supplier;
        private int _supplierId = 0;

        public SupplierEdit( ISupplierService supplierService )
        {
            _supplierService = supplierService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            _supplierId = WebHelper.GetRequestParamAsInt( "Id" );
            _supplier = _supplierService.GetSupplier( _supplierId );

            if ( _supplierId == 0 )
            {
                HeaderControl.Text = "Создание поставщика";
            }
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );

            if ( _supplier != null )
            {
                tbName.Text = _supplier.Name;
                tbAddress.Text = _supplier.Address;
                tbContactPerson.Text = _supplier.ContactPerson;
                tbEmail.Text = _supplier.Email;
                tbPhone.Text = _supplier.Phone;
                tbLegalEntity.Text = _supplier.LegalEntity;
                tbDiscount.Text = _supplier.Discount.ToString();
                tbSalatWareCost.Text = _supplier.SalatWareCost.ToString();
                tbSoupWareCost.Text = _supplier.SoupWareCost.ToString();
                tbSecondWareCost.Text = _supplier.SecondWareCost.ToString();
            }
        }

        protected void SaveSupplier( object sender, EventArgs e )
        {
            SupplierModel supplierModel = new SupplierModel()
            {
                Id = _supplierId,
                Name = tbName.Text,
                Address = tbAddress.Text,
                ContactPerson = tbContactPerson.Text,
                Email = tbEmail.Text,
                Phone = tbPhone.Text,
                LegalEntity = tbLegalEntity.Text,
                Discount = Convert.ToInt32( tbDiscount.Text ),
                SalatWareCost = Convert.ToInt32( tbSalatWareCost.Text ),
                SoupWareCost = Convert.ToInt32( tbSoupWareCost.Text ),
                SecondWareCost = Convert.ToInt32( tbSecondWareCost.Text ),
            };

            _supplierService.SaveSupplier( supplierModel );
            _supplierId = supplierModel.Id;

            lblResult.Text = _supplierId != 0 ? "Данные сохранены" : "Ошибка при сохранении";

            phResult.Visible = true;
            phEdit.Visible = false;
            lnkUser.NavigateUrl = String.Format( "~/Admin/SupplierEdit.aspx?id={0}", _supplierId );
        }
    }
}
