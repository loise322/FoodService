<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuItem.ascx.cs" Inherits="TravelLine.Food.Site.Controls.MenuItem" %>

<script type="text/javascript">AddDish(<%= GetDishInString() %>)</script>

<div data-toggle="popover" data-trigger="hover" class="dish-element <% if (AvailableQuota <= 0) { %> not-available <% } %>" onclick="addDishToOrder(this,<%= Dish.Id %>)" id="dish<%= Dish.Id %>" data-id="<%= Dish.Id %>">
    <div class="dish-name"><%= Dish.Name %></div>

    <div class="supplier-line">
        <div class="rating" data-user="<%= IsUserLiked() %>">
            <div class="likes"><span class="glyphicon glyphicon-thumbs-up"></span><i><%# GetLikes() %></i></div>
            &nbsp;
            <div class="dislikes"><span class="glyphicon glyphicon-thumbs-down"></span><i><%# GetDislikes() %></i></div>
        </div>
        <span class="supplier"><%= GetSupplierName() %></span>
    </div>
    <div class="clear"></div>
    <span class="price"><%= GetDishCost() %> ₽</span>
    <% if (AvailableQuota < 60) { %>
        <span class="available">Доступно: <%= AvailableQuota %></span>
    <% } %>
    <div class="clear"></div>
</div>
