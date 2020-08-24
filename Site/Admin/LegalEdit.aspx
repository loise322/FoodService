<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="LegalEdit.aspx.cs" Inherits="TravelLine.Food.Site.Admin.LegalEdit" Title="Администрирование - Редактирование команды" %>

<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Редактирование юр. лица" />

    <asp:PlaceHolder runat="server" Visible="false" ID="phResult">
        <div class="form-group">
            <asp:Label ID="lblResult" CssClass="control-label" runat="server"></asp:Label>
        </div>

        <asp:HyperLink runat="server" ID="lnkList" CssClass="btn btn-primary" NavigateUrl="~/Admin/LegalList.aspx">Список юр. лиц</asp:HyperLink>
        &nbsp;
        <asp:HyperLink runat="server" ID="lnkUser" CssClass="btn btn-default">Продолжить редактирование юр. лиц</asp:HyperLink>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" Visible="true" ID="phEdit">
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblLegalName" Text="Название" AssociatedControlID="tbLegalName" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbLegalName" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="Label1" Text="Полное название" AssociatedControlID="tbFullName" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbFullName" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-3 col-sm-9">
                    <asp:Button ID="btnSave" runat="server" OnClick="SaveLegal" Text="Сохранить" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
