<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuSelector.ascx.cs" Inherits="TravelLine.Food.Site.Controls.MenuSelector" %>

<%@ Register Src="~/Controls/MenuItem.ascx" TagName="MenuItem" TagPrefix="uc1" %>

<div style="position: relative; display: <%= ((Display)? "block": "none")%>">
    <script type="text/javascript">
        var flag = <%= ((Display)? "true": "false") %>;
    </script>

    <table class="table table-dish menu-bg">
        <tbody>
            <tr>
                <td class="col-md-3">
                    <div class="item-container">
                        <asp:Repeater runat="server" ID="repSalat">
                            <ItemTemplate>
                                <uc1:MenuItem runat="server" Dish="<%# (TravelLine.Food.Core.Dishes.DishModel)Container.DataItem %>" AvailableQuota="<%# (int) GetAvailableQuota( (TravelLine.Food.Core.Dishes.DishModel) Container.DataItem ) %>"></uc1:MenuItem>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </td>
                <td class="col-md-3">
                    <div class="item-container">
                        <asp:Repeater runat="server" ID="repSoup">
                            <ItemTemplate>
                                <uc1:MenuItem runat="server" Dish="<%# (TravelLine.Food.Core.Dishes.DishModel)Container.DataItem %>" AvailableQuota="<%# (int) GetAvailableQuota( (TravelLine.Food.Core.Dishes.DishModel) Container.DataItem ) %>"></uc1:MenuItem>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </td>
                <td class="col-md-3">
                    <div class="item-container">
                        <asp:Repeater runat="server" ID="repSecondDish">
                            <ItemTemplate>
                                <uc1:MenuItem runat="server" Dish="<%# (TravelLine.Food.Core.Dishes.DishModel)Container.DataItem %>" AvailableQuota="<%# (int) GetAvailableQuota( (TravelLine.Food.Core.Dishes.DishModel) Container.DataItem ) %>"></uc1:MenuItem>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </td>
                <td class="col-md-3">
                    <div class="item-container">
                        <asp:Repeater runat="server" ID="repGarnish">
                            <ItemTemplate>
                                <uc1:MenuItem runat="server" Dish="<%# (TravelLine.Food.Core.Dishes.DishModel)Container.DataItem %>" AvailableQuota="<%# (int) GetAvailableQuota( (TravelLine.Food.Core.Dishes.DishModel) Container.DataItem ) %>"></uc1:MenuItem>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>
