<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="SupplierEdit.aspx.cs" Inherits="TravelLine.Food.Site.Admin.SupplierEdit" Title="Администрирование - Редактирование команды" %>

<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Редактирование поставщика" />

    <asp:PlaceHolder runat="server" Visible="false" ID="phResult">
        <div class="form-group">
            <asp:Label ID="lblResult" CssClass="control-label" runat="server"></asp:Label>
        </div>

        <asp:HyperLink runat="server" ID="lnkList" CssClass="btn btn-primary" NavigateUrl="~/Admin/Suppliers.aspx">Поставщики</asp:HyperLink>
        &nbsp;
        <asp:HyperLink runat="server" ID="lnkUser" CssClass="btn btn-default">Продолжить редактирование поставщика</asp:HyperLink>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" Visible="true" ID="phEdit">
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblName" Text="Название" AssociatedControlID="tbName" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbName" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblAddress" Text="Адрес" AssociatedControlID="tbAddress" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbAddress" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblContactPerson" Text="Контактное лицо" AssociatedControlID="tbContactPerson" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbContactPerson" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblEmail" Text="Email" AssociatedControlID="tbEmail" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblPhone" Text="Телефон" AssociatedControlID="tbPhone" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbPhone" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblLegalEntity" Text="Юридическое лицо" AssociatedControlID="tbLegalEntity" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbLegalEntity" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblDiscount" Text="Скидка" AssociatedControlID="tbDiscount" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbDiscount" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblSalatWareCost" Text="Стоимость посуды для салата" AssociatedControlID="tbSalatWareCost" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbSalatWareCost" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblSoupWareCost" Text="Стоимость посуды для супа" AssociatedControlID="tbSoupWareCost" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbSoupWareCost" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblSecondWareCost" Text="Стоимость посуды для второго" AssociatedControlID="tbSecondWareCost" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbSecondWareCost" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-3 col-sm-9">
                    <asp:Button ID="btnSave" runat="server" OnClick="SaveSupplier" Text="Сохранить" CssClass="btn btn-primary" />
                    &nbsp;
                    <asp:HyperLink runat="server" CssClass="btn btn-default" NavigateUrl="~/Admin/Suppliers.aspx">Назад</asp:HyperLink>
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
