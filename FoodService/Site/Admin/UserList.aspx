<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="TravelLine.Food.Site.Admin.UserManage" Title="Администрирование - Управление пользователями" %>

<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Управление пользователями" />

    <div class="form-inline">
        <div class="form-group">
            <asp:Label runat="server" ID="lblDeliveryOffice" Text="Офис:" AssociatedControlID="lbDeliveryOffice" />
            <asp:ListBox runat="server" ID="lbDeliveryOffice" SelectionMode="Single" CssClass="form-control" Width="200px" Rows="1" style="margin-right: 5px;"></asp:ListBox>
        </div>
        <div class="form-group">
            <asp:Label runat="server" ID="lblLegal" Text="Юр. лицо:" AssociatedControlID="lbLegal" />
            <asp:ListBox runat="server" ID="lbLegal" SelectionMode="Single" CssClass="form-control" Width="200px" Rows="1" style="margin-right: 5px;"></asp:ListBox>
        </div>
        <div class="form-group">
            <asp:Label CssClass="checkbox-inline" runat="server" ID="lblEnable" AssociatedControlID="chbEnable">
                <asp:CheckBox runat="server" ID="chbEnable" />
                Показать неактивных пользователей
            </asp:Label>
        </div>
    </div>
    <div class="form-inline">
        <div class="form-group">
            <asp:Label runat="server" ID="lblName" Text="Имя:" AssociatedControlID="tbName" />
            <asp:TextBox runat="server" ID="tbName" CssClass="form-control" Width="400px"></asp:TextBox>
        </div>
    </div>
    <div class="form-inline">
        <asp:Button ID="btnSearch" runat="server" OnClick="ApplyFilter" CssClass="btn btn-primary" Text="Поиск" />
        &nbsp;
        <asp:Button ID="btnClear" runat="server" OnClick="ClearFilter" CssClass="btn btn-default" Text="Сбросить" />
        &nbsp;
        <asp:HyperLink ID="AddUser" runat="server" CssClass="btn btn-primary" NavigateUrl="~/Admin/UserEdit.aspx?ID=0">Добавить</asp:HyperLink>
    </div>

    <asp:GridView runat="server"
        ID="UserList"
        AllowPaging="true"
        CssClass="table table-bordered table-hover table-user"
        AutoGenerateColumns="false"
        DataKeyNames="Id"
        PageSize="50"
        OnPageIndexChanging="OnPageIndexChanging"
        OnRowDeleting="RowDeleting"
        OnRowDataBound="RowCreating">
        
        <Columns>
            <asp:BoundField Visible="false" DataField="Enable" />
            <asp:BoundField ApplyFormatInEditMode="true"
                DataField="Name"
                HeaderText="Имя пользователя"
                ConvertEmptyStringToNull="true"/>

            <asp:BoundField
                DataField="DeliveryOffice"
                HeaderText="Офис"
                />

            <asp:BoundField
                DataField="Legal"
                HeaderText="Юр. лицо"
                />

            <asp:BoundField DataField="Id"
                Visible="false" />

            <asp:HyperLinkField
                DataNavigateUrlFormatString="~/Admin/UserEdit.aspx?id={0}"
                DataNavigateUrlFields="Id"
                ControlStyle-CssClass="btn btn-primary btn-xs"
                ItemStyle-CssClass="text-center"
                Text="Изменить" />

            <asp:TemplateField
                ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger btn-xs" runat="server" OnClientClick="return ConfirmDeleteCommand()" CommandName="Delete">Удалить</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
    <script type="text/javascript">
        function ConfirmDeleteCommand() {
            return window.confirm('Удалить?');
        }
    </script>
</asp:Content>
