$(document).ready(function () {
    $("#connectionName").css("background-color", "#DDDDDD");
});

function setBackgroundColor(el) {
    $(el).css('background-color', 'lemonchiffon');
}

function removeBackgroundColor(el) {
    $(el).css('background-color', '#FFFFFF');
}

function changeNameTextBoxState(el) {
    if (el.checked) {
        $("#connectionName").attr("disabled", false);
        $("#connectionName").css("background-color", "#FFFFFF");
    }
    else {
        $("#connectionName").attr("disabled", "disabled");
        $("#connectionName").css("background-color", "#DDDDDD");
    }
}

function typeChanged(el) {
    switch (el.value) {
        case "PostgreSQL":
            $("#MainContentPlaceHolder_portTextBox").attr("value", "5432");
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>БД:</span>");
            break;
        case "MySQL":
            $("#MainContentPlaceHolder_portTextBox").attr("value", "3306");
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>БД:</span>");
            break;
        case "Microsoft SQL Server":
            $("#MainContentPlaceHolder_portTextBox").attr("value", "1433");
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>БД:</span>");
            break;
        case "Oracle":
            $("#MainContentPlaceHolder_portTextBox").attr("value", "1521");
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>SID / Сервис:</span>");
            break;
        case "IBM DB2":
            $("#MainContentPlaceHolder_portTextBox").attr("value", "50000");
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>БД:</span>");
            break;
        case "Firebird":
            $("#MainContentPlaceHolder_portTextBox").attr("value", "3050");
            $(".dbClass").remove();
            $("#dbID").append("<span class='dbClass'>БД / Путь:</span>");
            break;
    }
}