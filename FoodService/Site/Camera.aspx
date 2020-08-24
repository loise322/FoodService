<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Camera.aspx.cs" Inherits="TravelLine.Food.Site.Camera" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phContent" runat="server">
    <div class="container">
	    <h3 id="warnText"><%=CameraTitle %>. Камера отключится через <span id="countdown"></span> сек.</h3>
        <img id="cam" style="-webkit-user-select: none;" src="<%=CameraUrl %>" width="100%" />
	    <script>
            var seconds = 30;
            var secondsRemain = 0 + seconds;
            var warnText = document.getElementById('warnText');
            var countdown = document.getElementById('countdown');
            countdown.innerText = '' + secondsRemain;
            var interval = setInterval(function() {
                --secondsRemain;
                countdown.innerText = '' + secondsRemain;
                if (secondsRemain == 0) {
                    clearInterval(interval);
                    var camElem = document.getElementById('cam');
                    camElem.src = 'data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7';
                    warnText.innerHTML = 'Срок показа камеры истек. Нажмите F5, чтобы возобновить показ.';
                };
            }, 1000);
        </script>
    </div>
</asp:Content>
