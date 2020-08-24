<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderListOnFridge.ascx.cs" Inherits="TravelLine.Food.Site.Controls.OrderListOnFridge" %>

<asp:HiddenField ID="HiddenDate" runat="server" />
<asp:Label runat="server" ID="dateDisp" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
<asp:Label runat="server" ID="lblDeliveryOffice"></asp:Label>
<asp:Label runat="server" ID="lblLegal"></asp:Label>
<asp:GridView ID="OrderGrid" runat="server" AutoGenerateColumns="false" GridLines="Both" BorderColor="black" >
    <Columns>
        <asp:BoundField DataField="UserName" ItemStyle-BorderColor="black" HeaderStyle-BorderColor="black" ItemStyle-Font-Size="Small" HeaderStyle-Font-Size="small" />
        <asp:BoundField DataField="Salat" HeaderText="Салат" HeaderStyle-Font-Bold="true" ItemStyle-BorderColor="black" HeaderStyle-BorderColor="black" ItemStyle-Font-Size="Small" HeaderStyle-Font-Size="small" />
        <asp:BoundField DataField="Soup" HeaderText="Суп" HeaderStyle-Font-Bold="true" ItemStyle-BorderColor="black" HeaderStyle-BorderColor="black" ItemStyle-Font-Size="Small" HeaderStyle-Font-Size="small" />
        <asp:BoundField DataField="SecondDish" HeaderText="Второе блюдо" HeaderStyle-Font-Bold="true" ItemStyle-BorderColor="black" HeaderStyle-BorderColor="black" ItemStyle-Font-Size="Small" HeaderStyle-Font-Size="small" />
        <asp:BoundField DataField="Garnish" HeaderText="Гарнир" HeaderStyle-Font-Bold="true" ItemStyle-BorderColor="black" HeaderStyle-BorderColor="black" ItemStyle-Font-Size="Small" HeaderStyle-Font-Size="small" />
    </Columns>
</asp:GridView>
