<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderCalendar.ascx.cs" Inherits="TravelLine.Food.Site.Controls.OrderCalendar" %>

<ul class="nav nav-tabs nav-days">
    <asp:Repeater runat="server" ID="repDaysList">
     <ItemTemplate>
      <li class="<%# GetStatusStyle((TravelLine.Food.Core.Calendar.CalendarItem)Container.DataItem) %> col-md-2">
       <asp:HyperLink runat="server" NavigateUrl='<%# TravelLine.Food.Core.Library.GetDateSelectionLink( ((TravelLine.Food.Core.Calendar.CalendarItem)Container.DataItem).Date) %>' Text="<%# GetDayHeaderText((TravelLine.Food.Core.Calendar.CalendarItem)Container.DataItem) %>"></asp:HyperLink>
      </li>     
     </ItemTemplate>
    </asp:Repeater>
</ul>
