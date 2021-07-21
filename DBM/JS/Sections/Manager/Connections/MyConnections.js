var currentTr;
var modalWindow;
var isReset;

$(document).ready(function () {
    $("#connectionsTable").dataTable({
        "oLanguage": {
            "sUrl": "/CSS/jQuery/Tables/ru_RU.txt"
        },
        "bJQueryUI": true,
        "sPaginationType": "full_numbers",
        "aaSorting": [[1, "asc"]],
        "aLengthMenu": [
        [10, 20, 50, 100, -1],
        [10, 20, 50, 100, "Все"]]
    });
});

function confirm(message, callback) {
    $('#confirm').modal({
        closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
        position: ["20%", ],
        overlayId: 'confirm-overlay',
        containerId: 'confirm-container',
        onShow: function (dialog) {
            var modal = this;

            $('.message', dialog.data[0]).append(message);

            $('.yes', dialog.data[0]).click(function () {
                if ($.isFunction(callback)) {
                    callback.apply();
                }
                modal.close();
            });
        }
    });
}

function deleteConnection() {
    modalWindow.close();
    confirm("Удалить подключение?", function () {
        $("#MainContentPlaceHolder_deleteConnectionServerButton").click();
    });
}

function setConnectionValues(thisTr, server, port, db, user, password, i) {
    currentTr = thisTr;
    $("#MainContentPlaceHolder_newNameTextBox").attr("value", $("#nameHidden" + i).val());
    $("#oldName").attr("value", $("#nameHidden" + i).val());
    $("#MainContentPlaceHolder_serverTextBox").attr("value", server);
    $("#MainContentPlaceHolder_portTextBox").attr("value", port);
    $("#MainContentPlaceHolder_dbTextBox").attr("value", db);
    $("#MainContentPlaceHolder_userTextBox").attr("value", user);
    $("#MainContentPlaceHolder_passwordTextBox").attr("value", password);
    var typeConnection = $("#typeHidden" + i).val();
    $("#MainContentPlaceHolder_typeDropDownList [value='" + typeConnection + "']").attr("selected", "selected");

    typeChanged(typeConnection);

    //Модальное окно
    if (!isReset) {
        modalWindow = $("#basic-modal-content").modal();
    }
    else {
        isReset = false;
    }
    //Модальное окно
}

function setBackgroundColor(el) {
    $(el).css("background-color", "lemonchiffon");
}

function removeBackgroundColor(el) {
    $(el).css("background-color", "#FFFFFF");
}

function resetConnectionValues() {
    isReset = true;
    currentTr.click();
}

function saveConnectionValues() {
    //
    var newName = $("#MainContentPlaceHolder_newNameTextBox").val();
    var oldName = $("#oldName").val();
    var server = $("#MainContentPlaceHolder_serverTextBox").val();
    var port = $("#MainContentPlaceHolder_portTextBox").val();
    var db = $("#MainContentPlaceHolder_dbTextBox").val();
    var user = $("#MainContentPlaceHolder_userTextBox").val();
    var password = $("#MainContentPlaceHolder_passwordTextBox").val();
    var type = $("#MainContentPlaceHolder_typeDropDownList option:selected").val();
    //

    //
    modalWindow.close();
    //

    //
    $("#MainContentPlaceHolder_newNameTextBox").attr("value", newName);
    $("#oldName").attr("value", oldName);
    $("#MainContentPlaceHolder_serverTextBox").attr("value", server);
    $("#MainContentPlaceHolder_portTextBox").attr("value", port);
    $("#MainContentPlaceHolder_dbTextBox").attr("value", db);
    $("#MainContentPlaceHolder_userTextBox").attr("value", user);
    $("#MainContentPlaceHolder_passwordTextBox").attr("value", password);
    $("#MainContentPlaceHolder_typeDropDownList [value='" + type + "']").attr("selected", "selected");
    //

    //
    $("#MainContentPlaceHolder_saveServerButton").click();
    //
}

function connectF() {
    //
    var newName = $("#MainContentPlaceHolder_newNameTextBox").val();
    var oldName = $("#oldName").val();
    var server = $("#MainContentPlaceHolder_serverTextBox").val();
    var port = $("#MainContentPlaceHolder_portTextBox").val();
    var db = $("#MainContentPlaceHolder_dbTextBox").val();
    var user = $("#MainContentPlaceHolder_userTextBox").val();
    var password = $("#MainContentPlaceHolder_passwordTextBox").val();
    var type = $("#MainContentPlaceHolder_typeDropDownList option:selected").val();
    //

    //
    modalWindow.close();
    //

    //
    $("#MainContentPlaceHolder_newNameTextBox").attr("value", newName);
    $("#oldName").attr("value", oldName);
    $("#MainContentPlaceHolder_serverTextBox").attr("value", server);
    $("#MainContentPlaceHolder_portTextBox").attr("value", port);
    $("#MainContentPlaceHolder_dbTextBox").attr("value", db);
    $("#MainContentPlaceHolder_userTextBox").attr("value", user);
    $("#MainContentPlaceHolder_passwordTextBox").attr("value", password);
    $("#MainContentPlaceHolder_typeDropDownList [value='" + type + "']").attr("selected", "selected");
    //

    //
    $("#MainContentPlaceHolder_connectServerButton").click();
    //
}

function typeChanged(v) {
    switch (v) {
        case "PostgreSQL":
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>БД:</span>");
            break;
        case "MySQL":
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>БД:</span>");
            break;
        case "Microsoft SQL Server":
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>БД:</span>");
            break;
        case "Oracle":
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>SID / Сервис:</span>");
            break;
        case "IBM DB2":
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>БД:</span>");
            break;
        case "Firebird":
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>БД / Путь:</span>");
            break;
    }
}