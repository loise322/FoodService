<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Admin.Master" CodeBehind="Default.aspx.cs" Inherits="TravelLine.Food.Site.Admin.Default" %>
<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ContentPlaceHolderID="content" runat="server">    
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Администрирование" />
</asp:Content>
