<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="DishEdit.aspx.cs" Inherits="TravelLine.Food.Site.Admin.EditMeal" Title="Администрирование - Редактирование блюда" %>

<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Редактирование блюда" />

    <asp:PlaceHolder ID="ErrorPH" runat="server" Visible="false">
        <div class="form-group">
            <asp:Label ID="lblResult" CssClass="control-label" runat="server"></asp:Label>
        </div>
        <asp:HyperLink CssClass="btn btn-primary" runat="server" ID="lnkMain" Text="Список блюд"></asp:HyperLink>
        &nbsp;
        <asp:HyperLink CssClass="btn btn-default" runat="server" ID="lnkBack" Text="Продолжить редактирование блюда"></asp:HyperLink>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="DataPH" runat="server" Visible="true">
         <div class="form-horizontal">
             <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblSupplier" Text="Поставщик" AssociatedControlID="ddlSuppliers" />
                <div class="col-sm-9">
                    <asp:DropDownList runat="server" ID="ddlSuppliers" AutoPostBack="true" CssClass="form-control" DataValueField="Id" DataTextField="Name" style="width: 400px;" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblDishName" Text="Название" AssociatedControlID="DishName" />
                <div class="col-sm-9">
                    <asp:TextBox CssClass="form-control" runat="server" ID="DishName" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDishName" runat="server" ControlToValidate="DishName" ErrorMessage="Введите название" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblDishDescr" Text="Описание" AssociatedControlID="DishDescr" />
                <div class="col-sm-9">
                    <asp:TextBox CssClass="form-control" runat="server" ID="DishDescr" Width="400px" Rows="6" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblDishCost" Text="Стоимость" AssociatedControlID="DishCost" />
                <div class="col-sm-9">
                    <asp:TextBox CssClass="form-control" runat="server" ID="DishCost" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDishCost" runat="server" ControlToValidate="DishCost" ErrorMessage="Введите стоимость" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="Label1" Text="Тип" AssociatedControlID="DishType" />
                <div class="col-sm-9">
                    <asp:ListBox CssClass="form-control" runat="server" Rows="1" ID="DishType" Width="400px"></asp:ListBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="Label2" Text="Изображение" AssociatedControlID="DishImg" />
                <div class="col-sm-9">
                    <asp:Image runat="server" ID="DishImg" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-3 col-sm-9">
                    <asp:FileUpload ID="UploadImg" runat="server" Width="300" ToolTip="only jpg" />
                </div>
            </div>
             <div class="form-group">
                <div class="col-sm-offset-3 col-sm-9">
                    <asp:Label CssClass="checkbox-inline" runat="server" ID="lblRemoveImg" AssociatedControlID="RemoveImg"><asp:CheckBox runat="server" ID="RemoveImg" />Удалить изображение</asp:Label>
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblDishWeight" Text="Вес" AssociatedControlID="DishWeight" />
                <div class="col-sm-9">
                    <asp:TextBox CssClass="form-control" runat="server" ID="DishWeight" Width="100" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-3 col-sm-9">
                    <asp:Label CssClass="checkbox-inline" runat="server" ID="lblDishSingle" AssociatedControlID="DishSingle"><asp:CheckBox runat="server" ID="DishSingle" />Без гарнира</asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-3 col-sm-9">
                    <asp:Button ID="SaveBtn" runat="server" Text="Сохранить" OnClick="SaveMeal" CssClass="btn btn-primary" />
                    &nbsp;   
                    <asp:HyperLink runat="server" CssClass="btn btn-default" NavigateUrl="~/Admin/DishList.aspx">Назад</asp:HyperLink>
                </div>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
