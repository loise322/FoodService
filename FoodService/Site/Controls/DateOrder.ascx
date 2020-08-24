<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateOrder.ascx.cs" Inherits="TravelLine.Food.Site.Controls.DateOrder" %>

<h3>
    <asp:Label runat="server" ID="lblTitle"></asp:Label>
    от
    <asp:Label runat="server" ID="dateDisp"></asp:Label>
</h3>

<asp:Label runat="server" ID="Label1" Text="Продавец : "></asp:Label>
<asp:Label runat="server" ID="lblRestaurant"></asp:Label><br />
<asp:Label runat="server" ID="Label2" Text="Покупатель : "></asp:Label>
<asp:Label runat="server" ID="lblDeliveryOffice"></asp:Label><br />

<asp:PlaceHolder ID="DOPH" runat="server">
    <asp:HiddenField ID="HiddeDate" runat="server" />
    <asp:GridView ID="OrderGrid" runat="server" AutoGenerateColumns="false" Font-Size="Small">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Наименование" ItemStyle-Width="300px" />
            <asp:BoundField DataField="Count" HeaderText="Кол-во" ItemStyle-CssClass="admin-item" />
            <asp:BoundField DataField="Cost" HeaderText="Цена" ItemStyle-CssClass="admin-item" />
            <asp:BoundField DataField="Sum" HeaderText="Сумма" ItemStyle-CssClass="admin-item" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="TotalCount" runat="server" CssClass="admin-tCount" />
    <asp:Label ID="TL" runat="server" Text="Итого: " CssClass="admin-tl" />
    <asp:Label ID="TotalLabel" runat="server" CssClass="admin-tcost" /><br />
    <asp:Label ID="taker" runat="server" Text="Отпустил : _____________" Font-Size="Small" />
    <asp:Label ID="getter" runat="server" Text="Получил : _____________" CssClass="admin-label" />
</asp:PlaceHolder>
