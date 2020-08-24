<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserTransfer.ascx.cs" Inherits="TravelLine.Food.Site.Controls.UserTransfer" %>

<div class="form-horizontal">
    <div class="form-group">
        <input type="hidden" name="userLegalId" />
        <asp:Label CssClass="control-label col-sm-3" runat="server" Text="Причина перевода" AssociatedControlID="lbTransferReason" />
        <div class="col-sm-4">
            <asp:ListBox runat="server" ID="lbTransferReason" SelectionMode="Single" Rows="1" Width="300px" CssClass="form-control">
                <asp:ListItem Value="1" Text="Отпуск"></asp:ListItem>
                <asp:ListItem Value="2" Text="Больничный"></asp:ListItem>
                <asp:ListItem Value="3" Text="Командировка"></asp:ListItem>
                <asp:ListItem Value="4" Text="Увольнение"></asp:ListItem>
                <asp:ListItem Value="5" Text="Прием"></asp:ListItem>
            </asp:ListBox>
        </div>
    </div>
    <div class="form-group" id="legalId">
        <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblLegalTeam" Text="Юр. лицо" AssociatedControlID="lbLegalId" />
        <div class="col-sm-4">
            <asp:ListBox runat="server" ID="lbLegalId" SelectionMode="Single" Rows="1" Width="300px" CssClass="form-control"></asp:ListBox>
        </div>
    </div>
    <div class="form-group" id="startDate">
        <asp:Label CssClass="control-label col-sm-3" id="startText" runat="server" Text="С:" AssociatedControlID="txtDateStart" />
        <div class="col-sm-4">
            <asp:TextBox ID="txtDateStart" CssClass="form-control" runat="server" Width="300px" autocomplete="off" readonly></asp:TextBox>
        </div>
    </div>
    <div class="form-group" id="endDate">
        <asp:Label CssClass="control-label col-sm-3" runat="server" Text="По:" AssociatedControlID="txtDateEnd" />
        <div class="col-sm-4">
            <asp:TextBox ID="txtDateEnd" CssClass="form-control" runat="server" Width="300px" autocomplete="off" readonly></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <p id="errorMessage" class="text-danger col-sm-offset-3 col-sm-9">ErrorText</p>
    </div>
    <div class="form-group">
        <div class="col-sm-offset-3 col-sm-9">
            <asp:Button ID="btnSaveUserLegal" runat="server" Text="Сохранить" CssClass="btn btn-primary" />
            &nbsp;
            <asp:HyperLink runat="server" CssClass="btn btn-default" ID="CloseModal">Назад</asp:HyperLink>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        var currentUser = <%= User != null ? User.Id : 0 %>;
        let startDate = "#ctl00_content_UserTransfer_txtDateStart";
        let endDate = "#ctl00_content_UserTransfer_txtDateEnd";
        let transferReason = "#ctl00_content_UserTransfer_lbTransferReason";
        let legal = "#ctl00_content_UserTransfer_lbLegalId";
        let startDateGroup = "#startDate";
        let endDateGroup = "#endDate";
        let legalIdGroup = "#legalId";

        $("#errorMessage").hide();
        function clearWarnings() {
            $(startDate).removeClass("border-warn");
            $(endDate).removeClass("border-warn");
        }

        function checkValues() {
            if ($(startDate).val() == "") {
                $(startDate).addClass("border-warn");
            }
            else {
                $(startDate).removeClass("border-warn");
            }
            if ($(endDate).val() == "") {
                $(endDate).addClass("border-warn");
            }
            else {
                $(endDate).removeClass("border-warn");
            }
        }

        function SendData() {
            let check = true;
            let transferVal = $(transferReason).val();
            if (transferVal == "1" || transferVal == "2" || transferVal == "3") {
                if ($(startDate).val() == "" || $(endDate).val() == "") {
                    checkValues();
                    $("#errorMessage").text("Заполните даты.");
                    $("#errorMessage").show();
                    check = false;
                }
                if (($(startDate).val()) > ($(endDate).val()) && check) {
                    $(startDate).addClass("border-warn");
                    $(endDate).addClass("border-warn");
                    $("#errorMessage").text("Дата начала позднее даты завершения.");
                    $("#errorMessage").show();
                    check = false;
                }
            }
            else if (transferVal == "4" || transferVal == "5" ) {  
                if (($(startDate).val() == "")) {
                    $("#errorMessage").text("Заполните дату.");
                    $("#errorMessage").show();
                    if ($(startDate).val() == "") {
                        $(startDate).addClass("border-warn");
                    }
                    else {
                        $(startDate).removeClass("border-warn");
                    }
                    check = false;
                }
            }

            if (check) {
                $("#ctl00_content_UserTransfer_btnSaveUserLegal").prop('disabled', true);
                $("#errorMessage").hide();
                clearWarnings();

                $.post('/api/user/save-userlegal', { TransferDto: { UserId: currentUser, StartDate: $(startDate).val(), EndDate: $(endDate).val(), TransferReason: $(transferReason).val(), LegalId: $(legal).val(), UserLegalId: $('[name=userLegalId]').val(), MinDate: null, MaxDate: null } } )
                    .done(function () {
                        location.reload();
                    })
                    .fail(function (data) {
                        $("#errorMessage").text(data.responseJSON["Message"]);
                        $("#errorMessage").show();
                        $("#ctl00_content_UserTransfer_btnSaveUserLegal").prop('disabled', false);
                    }); 
                
            }
        }

        $("#ctl00_content_UserTransfer_btnSaveUserLegal").on("click", function () {
            clearWarnings();
            $("#errorMessage").hide();
            SendData(startDate, endDate, transferReason);
        });

        $(startDateGroup).show();
        $(endDateGroup).show();

        $(transferReason).change(function () {
            selectedReason = $(this).find(':selected').val();
            switch (parseInt(selectedReason)) {
                case 1: // Отпуск
                case 2: // Больничный
                case 3: // Командировка
                    $(startDateGroup).show();
                    $(endDateGroup).show();
                    $(legalIdGroup).show();
                    $("#errorMessage").hide();
                    $("#ctl00_content_UserTransfer_startText").html("C:");
                    clearWarnings();
                    break;
               
                case 4: // Увольнение
                    $(startDateGroup).show();
                    $(endDateGroup).hide();
                    $(legalIdGroup).hide();
                    $("#errorMessage").hide();
                    $("#ctl00_content_UserTransfer_startText").html("Последний рабочий день:");
                    clearWarnings();
                    break;
                case 5: // Прием
                    $(startDateGroup).show();
                    $(endDateGroup).hide();
                    $(legalIdGroup).show();
                    $("#errorMessage").hide();
                    $("#ctl00_content_UserTransfer_startText").html("С:");
                    clearWarnings();
                    break;
            }
        });
    });
</script>
