using System;

namespace TravelLine.Food.Site.Controls
{
    public partial class AdminHeader : System.Web.UI.UserControl
    {
        private string mText = "";
        public string Text
        {
            get { return mText; }
            set { mText = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitle.Text = Text;
        }
    }
}