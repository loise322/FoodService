<%@ Page Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TravelLine.Food.Site.Default" Title="" %>

<%@ Register Src="~/Controls/OrderCalendar.ascx" TagName="OrderCalendar" TagPrefix="uc" %>
<%@ Register Src="~/Controls/MenuSelector.ascx" TagName="MenuSelector" TagPrefix="uc1" %>

<asp:Content ID="mainContent" ContentPlaceHolderID="phContent" runat="server">
    <div class="container">
        <div style="padding: 0 5px 5px; font-size: 18px; color: #0047a8;">
            Добро пожаловать в сервис заказа еды. <asp:Label ID="lblOrderStatus" runat="server"></asp:Label> <asp:Label ID="lblOrderConfirm" runat="server"></asp:Label>
        </div>

        <uc:OrderCalendar runat="server" ID="ocCalendar" />

        <div class="tab-content">
            <asp:PlaceHolder ID="phOrder" runat="server" Visible="true">
                <asp:HiddenField runat="server" ID="OrderResult" />

                <table class="table table-dish order-bg">
                    <thead>
                        <tr>
                            <th class="order-dish-type elem-width"><img src="Images/salat.png" alt="salat" /><span>Салат</span></th>
                            <th class="order-dish-type elem-width"><img src="Images/soup.png" alt="soup" /><span>Суп</span></th>
                            <th class="order-dish-type elem-width"><img src="Images/second_course.png" alt="second_course" /><span>Второе блюдо</span></th>
                            <th class="order-dish-type elem-width"><img src="Images/garnish.png" alt="garnish" /><span>Гарнир</span></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="col-md-3"><div class="item-container" id="salatOrder"></div></td>
                            <td class="col-md-3"><div class="item-container" id="soupOrder"></div></td>
                            <td class="col-md-3"><div class="item-container" id="secondDishOrder" ></div></td>
                            <td class="col-md-3"><div class="item-container" id="garnishOrder" ></div></td>
                        </tr>
                    </tbody>
                </table>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phMenu" runat="server" Visible="false">
                <div style="position: relative">
                <h3 style="margin: 10px; padding: 0; text-align: center;">Меню</h3> 
                <div class="btn btn-info" style="position: absolute; top: -3px; left: 0;" onclick="SelectRandomOrder()">Мне повезет!</div>
                <asp:Button runat="server" Text="Заказать" ID="btnOrder" CssClass="btn btn-danger" style="position: absolute; top: -3px; right: 0;" />
                </div>
            </asp:PlaceHolder>
            <uc1:MenuSelector runat="server" ID="ctlMenuSelector"></uc1:MenuSelector>
        </div>
    </div>

    <div id="NoGarnish" class="dish-element" style="display: none;">
        <img src="Images/no_garnir.png" height="60" alt="Гарнир не требуется" />
    </div>
    <div id="EmptyBox" class="emptybox">&nbsp;</div>
    <div id="DescriptionBlock" class="DescriptionBlock" style="display: none;">
        <div style="clear: both">
            <img id="DescriptionBlockImg" src="Images/no_garnir.png" alt="" width="200" height="156" />
        </div>
        <span id="DescriptionBlockTxt"></span>
    </div>
    <script type="text/javascript">
        var currentUser = <%= CurrentUser != null ? CurrentUser.Id : 0 %>;

        $(document).ready(function () {
            <%= OrderedList() %>

            $('.menu-bg [data-toggle=popover]').each(function () {
                addPopover($(this));
            });

            loadRatings();
        });
    </script>
</asp:Content>
