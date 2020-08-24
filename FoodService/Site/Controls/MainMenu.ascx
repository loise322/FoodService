<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainMenu.ascx.cs" Inherits="TravelLine.Food.Site.Controls.MainMenu" %>

<nav class="navbar navbar-inverse navbar-fixed-top">
    <div class="container">
        <div class="navbar-header">
            <a class="navbar-brand" href="/">
                <img src="../Images/logo.png" alt="logo" />
                Заказ обедов</a>
        </div>
        <div id="navbar" class="navbar-collapse collapse">
            <ul class="nav navbar-nav navbar-right">
                <li><asp:HyperLink NavigateUrl="~/Camera.aspx?source=leninskiy" runat="server">Кухня на Ленинском</asp:HyperLink></li>
                <li><asp:HyperLink NavigateUrl="~/Camera.aspx?source=zavodskoy" runat="server">Кухня на Заводском</asp:HyperLink></li>
                <li><asp:HyperLink NavigateUrl="~/Account.aspx" runat="server">Личный кабинет</asp:HyperLink></li>
                <li>
                    <div class="nav navbar-nav navbar-form form-group">
                        <asp:DropDownList Width="220px" CssClass="form-control" runat="server" ID="lbUserList" OnSelectedIndexChanged="UpdateUser" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </li>
                <li class="price_total alert-info" id="totalSum" runat="server" visible="false"><a>Итого: <b id="orderSumm">0</b> &#8381</a></li>
            </ul>
        </div>
    </div>
</nav>
