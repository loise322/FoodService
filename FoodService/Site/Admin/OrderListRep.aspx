<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="OrderListRep.aspx.cs" Inherits="TravelLine.Food.Site.OrderListRep" Title="Администрирование - Список заказов на день" %>
<%@ Register Src="~/Controls/OrderListOnFridge.ascx" TagName="OrderListOnFridge" TagPrefix="uc" %>
<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="content" >
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Список заказов на день" />
    
    <asp:Calendar ID="OrderCalendar" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" >
        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
        <TodayDayStyle BackColor="#CCCCCC" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True"
            Font-Size="12pt" ForeColor="#333399" />
    </asp:Calendar>
    <div>
        <div class="select-field">
            <div class="name">Офис:</div>
            <asp:DropDownList runat="server" ID="ddlDeliveryOffices" CssClass="form-control select" DataValueField="Id" DataTextField="Name" />
        </div>

        <div class="select-field">
             <div class="name">Поставщик:</div>
             <asp:DropDownList runat="server" ID="ddlSuppliers" CssClass="form-control select" DataValueField="Id" DataTextField="Name" />
        </div>

        <asp:Label runat="server" CssClass="checkbox-inline" AssociatedControlID="chbStudents">
            <asp:CheckBox runat="server" ID="chbStudents" />Студенты
        </asp:Label>
        <asp:Button runat="server" ID="refreshBtn" Text="Обновить" CssClass="btn btn-primary" Style="margin-left: 30px;"/>
    </div><br/>
    <asp:HyperLink ID="LinkB" runat="server" Text="Версия для печати"  Target="_blank"/>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="LinkC" Text="Сохранить в Excel" runat="server" Target="_blank" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="LinkD" Text="Сохранить в Word" runat="server" Target="_blank" /><br /><br />
<uc:OrderListOnFridge ID="OrderListOnFridge" runat="server" />

</asp:Content>
