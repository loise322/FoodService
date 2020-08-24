<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Expense.aspx.cs" Inherits="TravelLine.Food.Site.Admin.Expense"  Title="Администрирование - Расходы"%>
<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Расходы" />
        <asp:Calendar ID="Calendar4Expence" runat="server" BackColor="White" BorderColor="White"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px"
            NextPrevFormat="FullMonth" Width="350px" OnSelectionChanged="ChangeSelected" >
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TodayDayStyle BackColor="#CCCCCC" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
            <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True"
                Font-Size="12pt" ForeColor="#333399" />
        </asp:Calendar>
        <asp:Label ID="FDLabel" runat="server" Text="Начальная дата: " />
        <asp:Label ID="FD" runat="server" Text="" /><br />
        <asp:Label ID="SDLabel" runat="server" Text="Конечная дата: " />
        <asp:Label ID="SD" runat="server" Text="" />
        <asp:HiddenField ID="LastDate" runat="server" Value="" /><br />
        
        <div class="form-group form-inline" style="margin-top: 16px;">
            <asp:DropDownList ID="ddlListExpense" runat="server" CssClass="form-control">
                <asp:ListItem Selected="True">Все</asp:ListItem>
                <asp:ListItem>По командам</asp:ListItem>
                <asp:ListItem>По группам</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Sbmt" Text="Сформировать" runat="server" OnClick="Page_Load" CssClass="btn btn-primary" />
        </div>
                
        <% 
            if (ddlListExpense.SelectedIndex == 0)
            {
                Union[] all = GetAllList();
                Response.Write(GetHTMLUnionList(all));
            }

            if ( ddlListExpense.SelectedIndex == 1 )
            {
                Union[] legals = GetLegalList();
                Response.Write( GetHTMLUnionList( legals ) );
            }
            
            if (ddlListExpense.SelectedIndex == 2)
            {
                Union[] deliveryOffices = GetDeliveryOfficeList();
                Response.Write(GetHTMLUnionList(deliveryOffices));               
            }

        %>
                
</asp:Content>        
