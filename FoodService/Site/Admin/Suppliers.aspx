<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Suppliers.aspx.cs" Inherits="TravelLine.Food.Site.Admin.Suppliers" Title="Администрирование - Управление командами" %>

<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Управление поставщиками" />

    <script type="text/javascript">
        function ConfirmDeleteCommand() {
            return window.confirm('Удалить ? \n Ok - Удалить команду и перенести её пользователей в базовую "Без команды" \n Cancel - Оставить всё как есть');
        }
    </script>

    <asp:GridView runat="server"
        ID="SupplierList"
        AllowPaging="true"
        AutoGenerateColumns="false"
        DataKeyNames="Id"
        PageSize="24"
        CssClass="table table-bordered table-hover table-user"
        ShowHeaderWhenEmpty="true"
        OnPageIndexChanging="OnPageIndexChanging" OnPreRender="OnPreRender">
        <Columns>
            <asp:BoundField Visible="false" DataField="Enable" />
            <asp:BoundField ApplyFormatInEditMode="true"
                DataField="Name"
                HeaderText="Название поставщика"
                ConvertEmptyStringToNull="true" />

            <asp:BoundField ApplyFormatInEditMode="true"
                DataField="Address"
                HeaderText="Адрес"
                ConvertEmptyStringToNull="true" />

            <asp:BoundField ApplyFormatInEditMode="true"
                DataField="ContactPerson"
                HeaderText="Контактное лицо"
                ConvertEmptyStringToNull="true" />

            <asp:BoundField ApplyFormatInEditMode="true"
                DataField="Email"
                HeaderText="Email"
                ConvertEmptyStringToNull="true" />

            <asp:BoundField ApplyFormatInEditMode="true"
                DataField="Phone"
                HeaderText="Телефон"
                ConvertEmptyStringToNull="true" />

            <asp:BoundField DataField="Id"
                Visible="false" />

            <asp:HyperLinkField
                DataNavigateUrlFormatString="~/Admin/SupplierEdit.aspx?id={0}"
                DataNavigateUrlFields="Id"
                ItemStyle-CssClass="text-center"
                ControlStyle-CssClass="btn btn-primary btn-xs"
                Text="Изменить" />

            <asp:TemplateField ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Удалить" CssClass="btn btn-danger btn-xs" OnClientClick="return ConfirmDeleteCommand()" OnCommand="ProcessRow" CommandName="DeleteLegal" CommandArgument='<%#  Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

    <asp:HyperLink ID="AddLegal" CssClass="btn btn-primary" runat="server" NavigateUrl="~/Admin/SupplierEdit.aspx?ID=0">Добавить</asp:HyperLink>
</asp:Content>
