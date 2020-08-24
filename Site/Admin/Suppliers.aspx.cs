using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Domain.Suppliers;

namespace TravelLine.Food.Site.Admin
{
    public partial class Suppliers : System.Web.UI.Page
    {
        private readonly ISupplierService _supplierService;

        private List<SupplierModel> _suppliers = null;

        public Suppliers( ISupplierService supplierService )
        {
            _supplierService = supplierService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            _suppliers = _supplierService.GetSuppliers();
        }

        protected void OnPreRender( object sender, EventArgs e )
        {
            base.OnPreRender( e );

            SupplierList.DataSource = _suppliers;
            SupplierList.DataBind();
        }

        protected void OnPageIndexChanging( object sender, GridViewPageEventArgs e )
        {
            ( ( GridView )sender ).PageIndex = e.NewPageIndex;
        }

        protected void ProcessRow( object sender, CommandEventArgs e )
        {
            int supplierId = Convert.ToInt32( e.CommandArgument );
            _supplierService.RemoveSupplier( supplierId );

            Page_Load( sender, e );
        }
    }
}
