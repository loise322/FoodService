<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FridgeOrderList.aspx.cs" Inherits="TravelLine.Food.Site.Admin.FridgeOrderList" Title="Администрирование - Версия для печати - Список заказов на день" %>
<%@ Register Src="~/Controls/OrderListOnFridge.ascx" TagName="OrderListOnFridge" TagPrefix="uc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Список заказов на день</title>
    <style type="text/css">
        @media print {
            body {-webkit-print-color-adjust: exact;}
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc:OrderListOnFridge ID="OrderListOnFridge" runat="server" />
    </div>
    </form>
</body>
</html>
