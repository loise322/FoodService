<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMenu.ascx.cs" Inherits="TravelLine.Food.Site.Controls.AdminMenu" %>

<ul class="nav nav-pills nav-stacked">       
    <li <% if (Request.FilePath.Contains("DishList.asp")) Response.Write("class=\"active\""); %>">
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/DishList.aspx">Список продуктов</asp:HyperLink>
    </li>        
    <li <% if (Request.FilePath.Contains("UserList.aspx")) Response.Write("class=\"active\""); %>">
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/UserList.aspx">Управление пользователями</asp:HyperLink>  
    </li>        
    <li <% if (Request.FilePath.Contains("LegalList.aspx")) Response.Write("class=\"active\""); %>">      
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/LegalList.aspx">Управление юр. лицами</asp:HyperLink>        
    </li>         
    <li <% if (Request.FilePath.Contains("Suppliers.aspx")) Response.Write("class=\"active\""); %>">      
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/Suppliers.aspx">Управление поставщиками</asp:HyperLink>        
    </li>         
    <li <% if (Request.FilePath.Contains("MakeMenu.aspx")) Response.Write("class=\"active\""); %>">     
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/MakeMenu.aspx">Сформировать меню</asp:HyperLink>   
    </li>         
    <li <% if (Request.FilePath.Contains("MakeMenuFromFile.aspx")) Response.Write("class=\"active\""); %>">     
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/MakeMenuFromFile.aspx">Сформировать меню из файла</asp:HyperLink>   
    </li>         
    <li <% if (Request.FilePath.Contains("OrderListRep.aspx")) Response.Write("class=\"active\""); %>">
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/OrderListRep.aspx">Список заказов на день</asp:HyperLink>   
    </li>        
    <li <% if (Request.FilePath.Contains("DateOrder.aspx")) Response.Write("class=\"active\""); %>">
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/DateOrder.aspx">Заказ в столовую</asp:HyperLink>  
    </li>   
    <li <% if (Request.FilePath.Contains("DateOrderReport.aspx")) Response.Write("class=\"active\""); %>">
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/DateOrderReport.aspx">Заказ для оформления документов</asp:HyperLink>  
    </li>  
    <li <% if (Request.FilePath.Contains("Expense.aspx")) Response.Write("class=\"active\""); %>">  
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/Expense.aspx">Расходы</asp:HyperLink>
    </li>        
    <li <% if (Request.FilePath.Contains("UsersNoOrder.aspx")) Response.Write("class=\"active\""); %>">
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/UsersNoOrder.aspx">Пользователи не заказавшие еду</asp:HyperLink>  
    </li>        
    <li <% if (Request.FilePath.Contains("SendEmail.aspx"))  Response.Write("class=\"active\""); %>">
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/SendEmail.aspx">Отправка e-mail</asp:HyperLink>     
    </li>
    <li <% if (Request.FilePath.Contains("UserOrders.aspx"))  Response.Write("class=\"active\""); %>">
        <asp:HyperLink runat="server" NavigateUrl="~/Admin/UserOrders.aspx">Отчет по расходам</asp:HyperLink>     
    </li>
</ul>
