<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Admin.Master" CodeBehind="SendEmail.aspx.cs" Inherits="TravelLine.Food.Site.Admin.SendEmail" ValidateRequest="false" Title="Администрирование - Отправка e-mail" %>

<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>

<asp:Content ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Отправка e-mail" />

    <div style="margin-bottom: 10px">
        <asp:Label runat="server" ID="lblStatus" Text="" />
    </div>

    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblTo" Text="Кому:" AssociatedControlID="lblTo" />
            <div class="col-sm-9">
                <asp:TextBox runat="server" ID="tbTo" Width="400px" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvTo" runat="server" ControlToValidate="tbTo" ErrorMessage="Введите адрес" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblSubject" Text="Тема:" AssociatedControlID="tbSubject" />
            <div class="col-sm-9">
                <asp:TextBox runat="server" ID="tbSubject" Width="400px" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="tbSubject" ErrorMessage="Введите тему" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblBody" Text="Сообщение:" AssociatedControlID="tbBody" />
            <div class="col-sm-9">
                <asp:TextBox runat="server" ID="tbBody" CssClass="form-control" Width="400px" TextMode="MultiLine" Rows="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvBody" runat="server" ControlToValidate="tbBody" ErrorMessage="Введите сообщение" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:Button ID="btnSend" runat="server" OnClick="SendMail" Text="Отправить" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>

</asp:Content>
