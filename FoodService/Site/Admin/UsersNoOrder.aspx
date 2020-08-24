<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="UsersNoOrder.aspx.cs" Inherits="TravelLine.Food.Site.Admin.UsersNoOrder" Title="Администрирование - Пользователи не заказавшие еду" %>
<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
<uc:AdminHeader runat="server" ID="HeaderControl" Text="Пользователи не заказавшие еду" />

<asp:Calendar runat="server" ID="clDate"></asp:Calendar>
<br />
<asp:GridView runat="server" ID="gwUsersList" AutoGenerateColumns="false">
<Columns>
  <asp:BoundField DataField="Name" HeaderText="Имя пользователя" />
</Columns>
</asp:GridView>
</asp:Content>
