<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="UserOrders.aspx.cs" Inherits="TravelLine.Food.Site.Admin.UserOrders"  Title="Администрирование - Заказы пользователей"%>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Отчет по расходам" />

    <div class="form-inline form-group">
        <asp:Label runat="server">Тип:</asp:Label>
        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
            <asp:ListItem Value="1" Selected="True">За весь месяц</asp:ListItem>
            <asp:ListItem Value="2">По дням</asp:ListItem>
            <asp:ListItem Value="3">По переводам</asp:ListItem>
        </asp:DropDownList>
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
        <asp:Label runat="server">Организация:</asp:Label>
        <asp:DropDownList runat="server" ID="ddlLegals" CssClass="form-control select" DataValueField="Id" DataTextField="FullName" />
        
    </div>

    <div class="form-group">
        <input type="submit" value="Применить" class="btn btn-primary" />
    </div>

    <div class="form-group">
        <asp:Button ID="btnExport" runat="server" Text="Выгрузить в Excel" CssClass="btn btn-primary" OnClick="btnExport_Click" />
    </div>

    <%
        Response.Write( GetHTML() );
    %>            
</asp:Content>        
