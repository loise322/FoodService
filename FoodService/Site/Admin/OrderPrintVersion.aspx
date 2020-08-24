<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPrintVersion.aspx.cs" Inherits="TravelLine.Food.Site.Admin.OrderPrintVersion" Title="Администрирование - Версия для печати - Заказ в столовую" %>
<%@ Register Src="~/Controls/DateOrder.ascx" TagName="DateOrder" TagPrefix="uc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">    
    <title>Заказ в столовую</title>
</head>
<body style="font-size:smaller">
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
        <td>
            <uc:DateOrder ID="dateOrder" runat="server" />
        </td>    
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
