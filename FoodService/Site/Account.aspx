<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="TravelLine.Food.Site.Account" %>

<%@ Register Src="~/Controls/MenuItem.ascx" TagName="MenuItem" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phContent" runat="server">
    <div class="container">
        <div style="padding: 0 5px 5px; font-size: 18px; color: #0047a8;">Личный кабинет</div>

        <ul class="nav nav-tabs" id="Tabs">
            <li><a data-toggle="tab" href="#today">Заказ на сегодня</a></li>
            <li><a data-toggle="tab" href="#report">Отчет за месяц</a></li>
            <li style="display: none;"><a data-toggle="tab" href="#people">Доверенные лица</a></li>
        </ul>

        <div class="tab-content">
            <div id="today" class="tab-pane fade">
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
                            <td class="col-md-3">
                                <div class="item-container">
                                    <asp:Repeater ID="salatOrder" runat="server">
                                        <ItemTemplate>
                                            <uc1:MenuItem runat="server" Dish="<%# (TravelLine.Food.Core.Dishes.DishModel)Container.DataItem %>"></uc1:MenuItem>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                            <td class="col-md-3">
                                <div class="item-container">
                                    <asp:Repeater ID="soupOrder" runat="server">
                                        <ItemTemplate>
                                            <uc1:MenuItem runat="server" Dish="<%# (TravelLine.Food.Core.Dishes.DishModel)Container.DataItem %>"></uc1:MenuItem>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                            <td class="col-md-3">
                                <div class="item-container">
                                    <asp:Repeater ID="secondDishOrder" runat="server">
                                        <ItemTemplate>
                                            <uc1:MenuItem runat="server" Dish="<%# (TravelLine.Food.Core.Dishes.DishModel)Container.DataItem %>"></uc1:MenuItem>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                            <td class="col-md-3">
                                <div class="item-container">
                                    <asp:Repeater ID="garnishOrder" runat="server">
                                        <ItemTemplate>
                                            <uc1:MenuItem runat="server" Dish="<%# (TravelLine.Food.Core.Dishes.DishModel)Container.DataItem %>"></uc1:MenuItem>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div id="report" class="tab-pane fade">
                <div style="padding: 10px;">
                    <div class="form-inline form-group">
                        <asp:Label runat="server">Период:</asp:Label>
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1">Январь</asp:ListItem>
                            <asp:ListItem Value="2">Февраль</asp:ListItem>
                            <asp:ListItem Value="3">Март</asp:ListItem>
                            <asp:ListItem Value="4">Апрель</asp:ListItem>
                            <asp:ListItem Value="5">Май</asp:ListItem>
                            <asp:ListItem Value="6">Июнь</asp:ListItem>
                            <asp:ListItem Value="7">Июль</asp:ListItem>
                            <asp:ListItem Value="8">Август</asp:ListItem>
                            <asp:ListItem Value="9">Сентябрь</asp:ListItem>
                            <asp:ListItem Value="10">Октябрь</asp:ListItem>
                            <asp:ListItem Value="11">Ноябрь</asp:ListItem>
                            <asp:ListItem Value="12">Декабрь</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Показать" CssClass="btn btn-primary" />
                        <asp:Label ID="lblHackerSum" runat="server" ForeColor="White" style="float: right;"></asp:Label>
                    </div>

                    <div id="tblReport" runat="server" visible="false">
                        <table class="table table-bordered table-hover">
                            <tr>
                                <th class="col-md-3" style="text-align: center;">День</th>
                                <th class="col-md-3" style="text-align: right;">Стоимость еды</th>
                                <th class="col-md-3" style="text-align: right;">Стоимость посуды</th>
                                <th class="col-md-3" style="text-align: right;">Всего</th>
                            </tr>
                            <asp:Repeater ID="reportOrders" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center;"><%# ( (TravelLine.Food.Core.Orders.UserOrderPriceModel)Container.DataItem ).Date.ToString("dd MMMM yyyy") %></td>
                                        <td style="text-align: right;"><%# ( (TravelLine.Food.Core.Orders.UserOrderPriceModel)Container.DataItem ).Cost.ToString("N") %></td>
                                        <td style="text-align: right;"><%# ( (TravelLine.Food.Core.Orders.UserOrderPriceModel)Container.DataItem ).DishesCost.ToString("N") %></td>
                                        <td style="text-align: right;"><%# ( (TravelLine.Food.Core.Orders.UserOrderPriceModel)Container.DataItem ).Total.ToString("N") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater> 
                            <tr>
                                <th class="text-right">Итого <asp:Label ID="lblDays" runat="server"></asp:Label></th>
                                <th class="text-right"><asp:Label ID="lblTotalCost" runat="server"></asp:Label></th>
                                <th class="text-right"><asp:Label ID="lblTotalDishesCost" runat="server"></asp:Label></th>
                                <th class="text-right"><asp:Label ID="lblTotal" runat="server"></asp:Label></th>
                            </tr>
                            <tr>
                                <th class="text-right" colspan="3">Сумма сверх квоты:</th>
                                <th class="text-right"><asp:Label ID="lblOverquota" runat="server"></asp:Label></th>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>

            <div id="people" class="tab-pane fade">
                people
            </div>
        </div>
    </div>
    <asp:HiddenField ID="TabName" runat="server" />
    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "today";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });

        var currentUser = <%= CurrentUser != null ? CurrentUser.Id : 0 %>;

        $(document).ready(function () {
            $('.order-bg [data-toggle=popover]').each(function () {
                addPopover($(this));
            });

            loadRatings();
        });
    </script>
</asp:Content>
