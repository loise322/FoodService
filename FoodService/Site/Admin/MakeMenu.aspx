<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Admin.Master" CodeBehind="MakeMenu.aspx.cs" Inherits="TravelLine.Food.Site.Admin.Make_Order" EnableEventValidation="false" Title="Администрирование - Формирование меню" %>
<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
  <uc:AdminHeader runat="server" ID="HeaderControl" Text="Формирование меню" />
  <asp:Calendar runat="server" ID="MenuCalendar" 
                OnSelectionChanged="Calendar_SelectionChanged"
                SelectionMode="Day"
                ></asp:Calendar> 
 <div style="width: 100%; text-align: center">
  <asp:Label ID="lblDate" runat="server"></asp:Label>
 </div>
 
 <div style="margin: 20px 0;">
     <div style="margin-right: 15px; float: left; margin-top: 5px; font-weight: bold;">Поставщик:</div>
     <asp:DropDownList runat="server" ID="ddlSuppliers" AutoPostBack="true" CssClass="form-control" DataValueField="Id" DataTextField="Name" style="width: 300px;" />
 </div>
 <input type="hidden" id="hdnChange" value="false" />
 
 <script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $(".save-button").click(function () {
            document.getElementById("hdnChange").value = "false";
        });
    });
     
    var hdnChange = document.getElementById("hdnChange");    
    if (hdnChange != undefined) 
    {
        hdnChange.value = "false";        
    }

    window.onbeforeunload = pressed;
    function pressed(e) 
    {
        if (e.target.tagName == "FORM")
        {
            document.getElementById("hdnChange").value = "false";
        }
        if (document.getElementById("hdnChange").value == "true")
        {            
            return "Вы покидаете страницу, не сохранив всех сделанных изменений.";
        }
    }    
 </script>
  
  <table border="1" cellpadding="" cellspacing="0">
  <tr>
    <td>Салат</td>
    <td>
        Фильтр: <input type="text" onkeyup="FilterList(this, '<%= SalatsList.ClientID %>')" style="width: 200px" /> <br />
        <asp:ListBox runat="server" ID="SalatsList" SelectionMode="Multiple" CssClass="admin-list-box" ></asp:ListBox></td>
    <td>
        <input type="button" onclick="AddValue('<%= SalatsList.ClientID %>', '<%= SalatsListMenu.ClientID %>')" value="add"/><br />
        <input type="button" onclick="DelValue('<%= SalatsListMenu.ClientID %>')" value="del"/>
    </td>
    <td><asp:ListBox runat="server" ID="SalatsListMenu" SelectionMode="Multiple" CssClass="admin-list-box" ></asp:ListBox></td>    
  </tr>
  <tr>
    <td>Суп</td>
    <td>
      Фильтр: <input type="text" onkeyup="FilterList(this, '<%= SoupList.ClientID %>')" style="width: 200px"/> <br />
      <asp:ListBox runat="server" ID="SoupList" SelectionMode="Multiple" CssClass="admin-list-box"></asp:ListBox></td>
    <td>
        <input type="button" onclick="AddValue('<%= SoupList.ClientID %>', '<%= SoupListMenu.ClientID %>')" value="add"/><br />
        <input type="button" onclick="DelValue('<%= SoupListMenu.ClientID %>')" value="del"/>
    </td>    
    <td><asp:ListBox runat="server" ID="SoupListMenu" SelectionMode="Multiple" CssClass="admin-list-box"></asp:ListBox></td>    
  </tr>
  <tr>
    <td>Гарнир</td>
    <td>
      Фильтр: <input type="text" onkeyup="FilterList(this, '<%= GarnishList.ClientID %>')" style="width: 200px"/> <br />
      <asp:ListBox runat="server" ID="GarnishList" SelectionMode="Multiple" CssClass="admin-list-box"></asp:ListBox></td>
    <td>
        <input type="button" onclick="AddValue('<%= GarnishList.ClientID %>', '<%= GarnishListMenu.ClientID %>')" value="add"/><br />
        <input type="button" onclick="DelValue('<%= GarnishListMenu.ClientID %>')" value="del"/>
    </td>    
    <td><asp:ListBox runat="server" ID="GarnishListMenu" SelectionMode="Multiple" CssClass="admin-list-box"></asp:ListBox></td>    
  </tr>
  <tr>
    <td>Второе блюдо</td>
    <td>
      Фильтр: <input type="text" onkeyup="FilterList(this, '<%= SecondDishList.ClientID %>')" style="width: 200px"/> <br />
      <asp:ListBox runat="server" ID="SecondDishList" SelectionMode="Multiple" CssClass="admin-list-box"></asp:ListBox></td>
    <td>
        <input type="button" onclick="AddValue('<%= SecondDishList.ClientID %>', '<%= SecondDishMenu.ClientID %>')" value="add"/><br />
        <input type="button" onclick="DelValue('<%= SecondDishMenu.ClientID %>')" value="del"/>
    </td>    
    <td><asp:ListBox runat="server" ID="SecondDishMenu" SelectionMode="Multiple" CssClass="admin-list-box"></asp:ListBox></td>    
  </tr>
  <tr>
    <td colspan="9" align="right">          
      <asp:HiddenField runat="server" ID="hdnDishIdsList" />
      <asp:Button ID="SaveBtn" runat="server" CssClass="save-button" Text="Сохранить" Width="100" OnClick="SubmitData" />
    </td>
  </tr>
  </table>
</asp:Content>
