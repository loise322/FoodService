using System;

namespace TravelLine.Food.Site
{
    public partial class Camera : System.Web.UI.Page
    {
        public string CameraTitle = "Кухня на Ленинском";
        public string CameraUrl = "http://axis-accc8e81e148.travelline.lan/axis-cgi/mjpg/video.cgi";

        protected void Page_Load( object sender, EventArgs e )
        {
            if (Request.QueryString["source"] == "zavodskoy")
            {
                CameraTitle = "Кухня на Заводском";
                CameraUrl = "http://yv1-camera:8090/stream.mjpg";
            }
        }
    }
}
