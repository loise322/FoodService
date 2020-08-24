<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Admin.Master" CodeBehind="MakeMenuFromFile.aspx.cs" Inherits="TravelLine.Food.Site.Admin.MakeMenuFromFile" EnableEventValidation="false" Title="Администрирование - Формирование меню" %>

<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Формирование меню из файла" />
    <asp:Calendar runat="server" ID="MenuCalendar"
        OnSelectionChanged="Calendar_SelectionChanged"
        SelectionMode="Day"></asp:Calendar>
    <div style="width: 100%; text-align: center; margin-bottom: 20px;">
        <asp:Label ID="lblDate" runat="server"></asp:Label>
    </div>

    <input type="hidden" id="hdnChange" value="false" />

    <script type="text/javascript" language="javascript">
        var hdnChange = document.getElementById("hdnChange");
        if (hdnChange != undefined) {
            hdnChange.value = "false";
        }

        window.onbeforeunload = pressed;
        function pressed(e) {
            if (e.target.tagName == "FORM") {
                document.getElementById("hdnChange").value = "false";
            }
            if (document.getElementById("hdnChange").value == "true") {
                return "Вы покидаете страницу, не сохранив всех сделанных изменений.";
            }
        }
    </script>

    <asp:GridView runat="server"
        ID="SupplierList"
        AutoGenerateColumns="false"
        DataKeyNames="SupplierId"
        CssClass="table table-bordered table-hover table-user suppliers-upload-list"
        ShowHeaderWhenEmpty="true"
        OnRowCommand="OnRowCommand">
        <Columns>
            <asp:BoundField Visible="false" DataField="SupplierId"  />

            <asp:BoundField ApplyFormatInEditMode="true"
                DataField="Name"
                HeaderText="Название поставщика"
                ConvertEmptyStringToNull="true" />

            <asp:BoundField ApplyFormatInEditMode="true"
                DataField="IsUploadedText"
                HeaderText=""
                ConvertEmptyStringToNull="true" />
            
            <asp:TemplateField ItemStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:FileUpload CssClass="btn btn-primary btn-xs" id="FileUploadControl" runat="server"  />
                    <asp:Button CssClass="btn btn-primary btn-xs" runat="server" Id="UploadButton" Text="Загрузить" CommandName="UploadMenuFile" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'/>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

    <h4><asp:Label ID="lblSupplier" runat="server"></asp:Label></h4>
    <% if (Dishes.Count > 0) { %>
    <input type="hidden" name="supplierId" value="<%= SelectedSupplierId %>" checked="checked">
    <table class="table table-bordered table-hover table-user" width="100%" cellpadding="10" cellspacing="10">
        <tr style="background-color: dodgerblue">
            <td style="padding: 3px 10px;">Наименование</td>
            <td style="text-align: center">Старая цена</td>
            <td style="text-align: center">Новая цена</td>
            <td style="text-align: center">Квота</td>
            <td style="text-align: center">Добавить в меню</td>
        </tr>

        <tr style="background-color: dodgerblue">
            <td style="padding: 3px 10px;" colspan="5" align="center">Салаты</td>
        </tr>

        <% foreach (var dish in Dishes.Where(d => d.Type == TravelLine.Food.Domain.Dishes.DishType.Salat))
           { %>
        <tr <% if (dish.IsNew)
               { %> style="background-color:coral" <% } %>>
            <td style="padding: 3px 10px;"><%= dish.Name %></td>
            <td style="text-align: center"><%= dish.Cost %></td>
            <td style="text-align: center; <% if (dish.Cost != dish.NewPrice && dish.NewPrice > 0)
                                              { %>; background-color:lightcoral <% } %>">
                <% if (dish.NewPrice > 0)
                   { %>
                    <%= dish.NewPrice %>
                <% } %>
            </td>
            <td style="text-align: center">
                    <%= dish.Quota %>
                </td>
            <td style="text-align: center">
                <% if (dish.IsNew) { %>
                    <input type="checkbox" name="newDish" value="<%= dish.Name %>" checked="checked">
                <% } else { %>
                    <input type="checkbox" name="menuDish" value="<%= dish.Id %>" checked="checked">
                <% } %>
            </td>
        </tr>
        <% } %>
        
        <tr style="background-color: dodgerblue">
            <td style="padding: 3px 10px;" colspan="5" align="center">Супы</td>
        </tr>

        <% foreach (var dish in Dishes.Where(d => d.Type == TravelLine.Food.Domain.Dishes.DishType.Soup))
           { %>
            <tr <% if (dish.IsNew)
                   { %> style="background-color:coral" <% } %>>
                <td style="padding: 3px 10px;"><%= dish.Name %></td>
                <td style="text-align: center"><%= dish.Cost %></td>
                <td style="text-align: center; <% if (dish.Cost != dish.NewPrice && dish.NewPrice > 0)
                                                  { %>; background-color:lightcoral <% } %>">
                    <% if (dish.NewPrice > 0)
                       { %>
                        <%= dish.NewPrice %>
                    <% } %>
                </td>
                <td style="text-align: center">
                    <%= dish.Quota %>
                </td>
                <td style="text-align: center">
                    <% if (dish.IsNew) { %>
                        <input type="checkbox" name="newDish" value="<%= dish.Name %>" checked="checked">
                    <% } else { %>
                        <input type="checkbox" name="menuDish" value="<%= dish.Id %>" checked="checked">
                    <% } %>
                </td>
            </tr>
        <% } %>
        
        <tr style="background-color: dodgerblue">
            <td style="padding: 3px 10px;" colspan="5" align="center">Вторые блюда</td>
        </tr>

        <% foreach (var dish in Dishes.Where(d => d.Type == TravelLine.Food.Domain.Dishes.DishType.SecondDish))
           { %>
            <tr <% if (dish.IsNew)
                   { %> style="background-color:coral" <% } %>>
                <td style="padding: 3px 10px;"><%= dish.Name %></td>
                <td style="text-align: center"><%= dish.Cost %></td>
                <td style="text-align: center; <% if (dish.Cost != dish.NewPrice && dish.NewPrice > 0)
                                                  { %>; background-color:lightcoral <% } %>">
                    <% if (dish.NewPrice > 0)
                       { %>
                        <%= dish.NewPrice %>
                    <% } %>
                </td>
                <td style="text-align: center">
                    <%= dish.Quota %>
                </td>
                <td style="text-align: center">
                    <% if (dish.IsNew) { %>
                        <input type="checkbox" name="newDish" value="<%= dish.Name %>" checked="checked">
                    <% } else { %>
                        <input type="checkbox" name="menuDish" value="<%= dish.Id %>" checked="checked">
                    <% } %>
                </td>
            </tr>
        <% } %>
        
        <tr style="background-color: dodgerblue">
            <td style="padding: 3px 10px;" colspan="5" align="center">Гарниры</td>
        </tr>


        <% foreach (var dish in Dishes.Where(d => d.Type == TravelLine.Food.Domain.Dishes.DishType.Garnish))
           { %>
            <tr <% if (dish.IsNew)
                   { %> style="background-color:coral" <% } %>>
                <td style="padding: 3px 10px;"><%= dish.Name %></td>
                <td style="text-align: center"><%= dish.Cost %></td>
                <td style="text-align: center; <% if (dish.Cost != dish.NewPrice && dish.NewPrice > 0)
                                                  { %>; background-color:lightcoral <% } %>">
                    <% if (dish.NewPrice > 0)
                       { %>
                        <%= dish.NewPrice %>
                    <% } %>
                </td>
                <td style="text-align: center">
                    <%= dish.Quota %>
                </td>
                <td style="text-align: center">
                    <% if (dish.IsNew) { %>
                        <input type="checkbox" name="newDish" value="<%= dish.Name %>" checked="checked">
                    <% } else { %>
                        <input type="checkbox" name="menuDish" value="<%= dish.Id %>" checked="checked">
                    <% } %>
                </td>
            </tr>
        <% } %>
    </table>
        
    <asp:Button ID="SaveBtn" runat="server" Text="Сохранить" Width="100" OnClick="SubmitData" />

    <% } %>
    
    
    <asp:Label ID="ErrorLabel" runat="server"></asp:Label>
</asp:Content>
