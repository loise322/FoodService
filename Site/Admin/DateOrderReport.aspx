<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="DateOrderReport.aspx.cs" Inherits="TravelLine.Food.Site.Admin.DateOrderReport" Title="Администрирование - Заказ для оформления документов" %>
<%@ Register Src="~/Controls/DateOrder.ascx" TagName="DateOrder" TagPrefix="uc" %>
<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content" >
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Заказ для оформления документов" />
    
    <asp:Calendar ID="DOCalendar" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" >
        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
        <TodayDayStyle BackColor="#CCCCCC" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True"
            Font-Size="12pt" ForeColor="#333399" />
    </asp:Calendar>
    
    <br />
    
    <div class="select-field">
        <div class="name">Организация:</div>
        <asp:DropDownList runat="server" ID="ddlLegals" CssClass="form-control select" DataValueField="Id" DataTextField="FullName" />
    </div>

    <div class="select-field">
        <div class="name">Поставщик:</div>
        <asp:DropDownList runat="server" ID="ddlSuppliers" CssClass="form-control select" DataValueField="Id" DataTextField="Name" />
    </div>

    <div>
        <asp:Button CssClass="btn btn-primary" runat="server" ID="btnRefresh" Text="Обновить" Width="120px" OnClick="Refresh"/>
    </div><br/>

    <div>    
        <asp:HyperLink ID="lnkPrint" Text="Версия для печати" runat="server" Target="_blank" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="lnkExportExcel" Text="Сохранить в Excel" runat="server" Target="_blank" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="lnkExportWord" Text="Сохранить в Word" runat="server" Target="_blank" />
    </div>
    <asp:Label ID="OrderStatus" runat="server" Font-Bold="true" /><br />
    <uc:DateOrder runat="server" ID="DateOrderControl" /><br />
</asp:Content>
