<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="DishList.aspx.cs" Inherits="TravelLine.Food.Site.Admin.MealList" Title="Администрирование - Список продуктов" %>

<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Список продуктов" />

    <div class="form-inline" style="padding-bottom: 20px;">
        <asp:Label runat="server">Фильтр:</asp:Label>
        <asp:TextBox runat="server" ID="FilterBox" Width="200px" CssClass="form-control"></asp:TextBox>
        <asp:Button runat="server" CssClass="btn btn-primary" OnClick="ApplyFilter" Text="Применить" />
    </div>

    <asp:GridView runat="server" ID="grdDishList" AllowPaging="true" PageSize="25" OnPageIndexChanging="PageIndexChanging"
        AutoGenerateColumns="false" CssClass="table table-bordered table-hover table-user">
        <PagerSettings Position="Bottom" Mode="Numeric" />
        <Columns>
            <asp:BoundField Visible="false" DataField="id_dish" />
            <asp:BoundField HeaderText="Название" DataField="dish_name" />
            <asp:BoundField HeaderText="Стоимость" DataField="cost" ControlStyle-Width="100" />
            <asp:BoundField HeaderText="Тип" DataField="type" HeaderStyle-Width="100" />
            <asp:BoundField HeaderText="Вес" DataField="weight" HeaderStyle-Width="40" />
            <asp:HyperLinkField DataTextField="id_dish"
                DataTextFormatString="Изменить"
                ItemStyle-CssClass="text-center"
                ControlStyle-CssClass="btn btn-primary btn-xs"
                DataNavigateUrlFields="id_dish"
                DataNavigateUrlFormatString="~/Admin/DishEdit.aspx?ID={0}" />
            <asp:TemplateField ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Button runat="server"
                        Text="Удалить"
                        CssClass="btn btn-danger btn-xs"
                        OnClientClick="return window.confirm('Удалить ?')"
                        OnCommand="ProcessRow"
                        CommandName="DeleteDish"
                        CommandArgument='<%# Eval("id_dish") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:HyperLink runat="server" NavigateUrl="~/Admin/DishEdit.aspx?ID=0" CssClass="btn btn-primary">Добавить</asp:HyperLink>
</asp:Content>
