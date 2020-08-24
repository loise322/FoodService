<%@ Page Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="TravelLine.Food.Site.Admin.UserEdit" Title="Администрирование - Редактирование пользователя" %>

<%@ Register Src="~/Controls/AdminHeader.ascx" TagName="AdminHeader" TagPrefix="uc" %>
<%@ Register Src="~/Controls/UserTransfer.ascx" TagName="UserTransfer" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="content" runat="server">
    <uc:AdminHeader runat="server" ID="HeaderControl" Text="Редактирование пользователя" />

    <asp:PlaceHolder runat="server" Visible="false" ID="phResult">
        <div class="form-group">
            <asp:Label ID="lblResult" CssClass="control-label" runat="server"></asp:Label>
        </div>
        <asp:HyperLink runat="server" CssClass="btn btn-primary" ID="lnkList" NavigateUrl="~/Admin/UserList.aspx">Список пользователей</asp:HyperLink>
        &nbsp;
        <asp:HyperLink runat="server" CssClass="btn btn-default" ID="lnkUser">Продолжить редактирование пользователя</asp:HyperLink>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" Visible="true" ID="phEdit">
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblUserName" Text="Имя Пользователя" AssociatedControlID="tbUserName" />
                <div class="col-sm-9">
                    <asp:TextBox runat="server" ID="tbUserName" Width="300px" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="tbUserName" ErrorMessage="Введите имя" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblDeliveryOffice" Text="Офис" AssociatedControlID="lbDeliveryOffice" />
                <div class="col-sm-9">
                    <asp:ListBox runat="server" ID="lbDeliveryOffice" SelectionMode="Single" Rows="1" Width="300px" CssClass="form-control"></asp:ListBox>
                </div>
            </div>
            <div class="form-group" runat="server" id="legalGroup">
                <asp:Label CssClass="control-label col-sm-3" runat="server" ID="lblLegal" Text="Юр. лицо" AssociatedControlID="lbLegal" />
                <div class="col-sm-9">
                    <asp:ListBox runat="server" ID="lbLegal" SelectionMode="Single" Width="300px" Rows="1" CssClass="form-control"></asp:ListBox>
                </div>
            </div>

            <div class="form-group" runat="server" id="applicationGroup"> 
            <asp:Label CssClass="control-label col-sm-3" id="applicationDate" runat="server" Text="Дата приема:" AssociatedControlID="tbApplicationDate" />
                <div class="col-sm-4">
                    <asp:TextBox ID="tbApplicationDate" CssClass="form-control" runat="server" Width="300px" autocomplete="off"></asp:TextBox>
                </div>
            </div>

            <asp:GridView runat="server"
                ID="LegalList"
                AllowPaging="true"
                CssClass="table table-bordered table-hover table-user"
                AutoGenerateColumns="false"
                DataKeyNames="Id"
                PageSize="50"
                OnPageIndexChanging="OnPageIndexChanging">
                <Columns>
                    <asp:BoundField Visible="false" DataField="Enable" />
                    <asp:BoundField ApplyFormatInEditMode="true"
                        DataField="Legal.Name"
                        HeaderText="Юр. лицо"
                        ConvertEmptyStringToNull="true"/>

                    <asp:BoundField Visible="false" DataField="Enable" />
                    <asp:BoundField ApplyFormatInEditMode="true"
                        DataField="StartDate"
                        DataFormatString = "{0:d}"
                        HeaderText="Дата начала"
                        ConvertEmptyStringToNull="true"/>

                    <asp:BoundField ApplyFormatInEditMode="true"
                        DataField="EndDate"
                        DataFormatString = "{0:d}"
                        HeaderText="Дата завершения"
                        ConvertEmptyStringToNull="true"/>

                    <asp:BoundField ApplyFormatInEditMode="true"
                        DataField="TransferReason"
                        HeaderText="Причина"
                        ConvertEmptyStringToNull="true"/>

                    <asp:TemplateField 
                        ItemStyle-CssClass="text-center">
                      <ItemTemplate>
                          <input class="btn btn-primary btn-xs" type="button" value="Изменить" onclick=<%#"editRecord(" + Eval("Id") + ");" %> <%# (bool)Eval("IsEditable") ? "" : "style='display:none'" %> />
                      </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField
                        ItemStyle-CssClass="text-center">
                      <ItemTemplate>
                          <input class="btn btn-danger btn-xs" type="button" value="Удалить" onclick=<%#"deleteRecord(" + Eval("Id") + ");" %> <%# (bool)Eval("IsRemovable") ? "" : "style='display:none'" %> />
                      </ItemTemplate>
                    </asp:TemplateField> 

                </Columns>
            </asp:GridView>

            <div class="form-group col-sm-offset-3 col-sm-9">
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Перевод" ID="transfer" onClientClick="return false" />
            </div>
 
            <div class="form-group">
                <div class="col-sm-offset-3 col-sm-9">
                    <asp:Button ID="btnSave" runat="server" OnClick="SaveUser" Text="Сохранить" CssClass="btn btn-primary" />
                    &nbsp;
                    <asp:HyperLink runat="server" CssClass="btn btn-default" NavigateUrl="~/Admin/UserList.aspx">Назад</asp:HyperLink>
                </div>
            </div>

            <div id="dialog-form" title="Перевод пользователя">
                <fieldset>
                    <uc:UserTransfer runat="server" ID="UserTransfer" />
                </fieldset>   
            </div>
        </div>

    <script type="text/javascript">
        $(document).ready(function () {
            let dialog;
            let startDate = "#ctl00_content_UserTransfer_txtDateStart";
            let endDate = "#ctl00_content_UserTransfer_txtDateEnd";
            let transferReason = "#ctl00_content_UserTransfer_lbTransferReason";
            let legal = "#ctl00_content_UserTransfer_lbLegalId";
            let applicationDate = "#ctl00_content_tbApplicationDate";
            <% 
                DateTime minDate = DateTime.Today.AddDays(2);
                if ( EditUser.UserLegals.LastOrDefault() != null )
                {
                    if ( EditUser.UserLegals.Last().StartDate > DateTime.Today.AddDays(2) )
                    {
                        minDate = EditUser.UserLegals.Last().StartDate;
                    }
                }
            %>
            let minStartDate = '<%= minDate.ToString("yyyy-MM-dd") %>';
            let userId = <%= EditUser != null ? EditUser.Id : 0 %>;
            let startDateGroup = "#startDate";
            let endDateGroup = "#endDate";
            let legalIdGroup = "#legalId";

            $.datepicker.setDefaults($.datepicker.regional["ru"]);

            $(applicationDate).datepicker({
                minDate: new Date().toISOString().slice(0, 10),
                dateFormat: 'yy-mm-dd'
            });

            deleteRecord = function (id) {
                var confirmDelete = confirm("Вы уверены что хотите удалить эту запись?");
                if (confirmDelete) {
                    $.post('/api/user/delete-userlegal', { Id: id, UserId: userId })
                        .done(function () {
                            location.reload();
                        })
                }
            }

            editRecord = function (id) {
                clearWarnings();
                $(transferReason).prop("disabled", true);
                dialog.dialog("open");
                $('[name=userLegalId]').val(id);

                $.get('/api/user/get-userlegal?id=' + id + '&userId=' + userId, function () {
                }).done(function (data) {
                    $(transferReason).val(data.TransferDto.TransferReason);
                    if (data.TransferDto.TransferReason == 5 || data.TransferDto.TransferReason == 4) {
                        $(endDateGroup).hide();
                        $(legal).prop("disabled", true);
                        clearWarnings();
                    }
                    else {
                        $(legal).prop("disabled", false);
                    }

                    if (data.TransferDto.TransferReason == 4) {
                        $(legalIdGroup).hide();
                    }

                    if (data.TransferDto.TransferReason == 5) {
                        $(legal).prop("disabled", false);
                    }

                    $(legal).val(data.TransferDto.LegalId);

                    $(startDate).datepicker("destroy");
                    $(endDate).datepicker("destroy");

                    if (data.TransferDto.MinDate != null && data.TransferDto.MaxDate != null) {
                        $(startDate).datepicker({
                            minDate: new Date(data.TransferDto.MinDate),
                            maxDate: new Date(data.TransferDto.MaxDate),
                            dateFormat: 'yy-mm-dd'
                        });

                        $(endDate).datepicker({
                            minDate: new Date(data.TransferDto.MinDate),
                            maxDate: new Date(data.TransferDto.MaxDate),
                            dateFormat: 'yy-mm-dd'
                        });

                        $(startDate).datepicker("setDate", new Date(data.TransferDto.StartDate));
                        $(endDate).datepicker("setDate", new Date(data.TransferDto.EndDate));
                    }
                    else {
                        if (data.TransferDto.MinDate == null) {
                            $(startDate).datepicker({
                                maxDate: new Date(data.TransferDto.MaxDate),
                                dateFormat: 'yy-mm-dd'
                            });
                        }
                        else {
                            $(startDate).datepicker({
                                minDate: new Date(data.TransferDto.MinDate),
                                dateFormat: 'yy-mm-dd'
                            });
                        }

                        $(endDate).datepicker({
                            minDate: new Date(data.TransferDto.MinDate),
                            dateFormat: 'yy-mm-dd'
                        });

                        $(startDate).datepicker("setDate", new Date(data.TransferDto.StartDate));
                        $(endDate).datepicker("setDate", new Date(data.TransferDto.EndDate));
                    }
                });
            }

            dialog = $("#dialog-form").dialog({
                autoOpen: false,
                width: 'auto',
                modal: true,
                close: function () {
                    dialog.dialog("close");
                    $(startDate).datepicker("destroy");
                    $(endDate).datepicker("destroy");
                    $('[name=userLegalId]').val("");
                    $(startDateGroup).show();
                    $(endDateGroup).show();
                    $(legalIdGroup).show();
                    $("#errorMessage").hide();
                    $(startDate).val('');
                    $(endDate).val('');
                    $(transferReason).val('1');
                    $(legal).val('1');
                    clearWarnings();
                }
            });

            $("#ctl00_content_UserTransfer_CloseModal").on('click', function () {
                $(startDateGroup).show();
                $(endDateGroup).show();
                $(legalIdGroup).show();
                $("#errorMessage").hide();
                clearWarnings();
                $(transferReason).prop("disabled", false);
                $(legal).prop("disabled", false);
                dialog.dialog("close");
                $("#errorMessage").hide();
            });


            $("#ctl00_content_transfer").on("click", function () {
                $(transferReason).prop("disabled", false);
                $(legal).prop("disabled", false);
                $(startDate).datepicker({ minDate: new Date(minStartDate), dateFormat: 'yy-mm-dd' });
                $(endDate).datepicker({ minDate: new Date(minStartDate), dateFormat: 'yy-mm-dd' });
                clearWarnings();
                dialog.dialog("open");
            });

            function clearWarnings() {
                $(startDate).removeClass("border-warn");
                $(endDate).removeClass("border-warn");
            }
        });
    </script>
        
    </asp:PlaceHolder>
</asp:Content>
