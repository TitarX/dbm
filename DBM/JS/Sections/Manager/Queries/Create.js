$(document).ready(function () {
    $("#commandName").css("background-color", "#DDDDDD");

    $("#MainContentPlaceHolder_gridView").prepend($("<thead></thead>").append($("tr").eq(11))).dataTable({
        "oLanguage": {
            "sUrl": "/CSS/jQuery/Tables/ru_RU.txt"
        },
        "bJQueryUI": true,
        "sPaginationType": "full_numbers",
        "aaSorting": [[0, "asc"]],
        "aLengthMenu": [
        [10, 20, 50, 100, -1],
        [10, 20, 50, 100, "Все"]]
    });
});

function setBackgroundColor(el) {
    $(el).css('background-color', 'lemonchiffon');
}

function removeBackgroundColor(el) {
    $(el).css('background-color', '#FFFFFF');
}

function changeNameTextBoxState(el) {
    if (el.checked) {
        $("#commandName").attr("disabled", false);
        $("#commandName").css("background-color", "#FFFFFF");
    }
    else {
        $("#commandName").attr("disabled", "disabled");
        $("#commandName").css("background-color", "#DDDDDD");
    }
}

function typeChanged(el) {
    switch (el.value) {
        case "SELECT":
            $(".hintClass").remove();
            $("#hintID").append(
                "<span class='hintClass'>"
                + "SELECT column_name FROM table_name"
                + "</span>");
            break;
        case "INSERT":
            $(".hintClass").remove();
            $("#hintID").append(
                "<span class='hintClass'>"
                + "INSERT INTO table_name (column1, column2, column3) VALUES (value1, value2, value3)"
                + "</span>");
            break;
        case "UPDATE":
            $(".hintClass").remove();
            $("#hintID").append(
                "<span class='hintClass'>"
                + "UPDATE table_name SET column1=value, column2=value2 WHERE some_column=some_value"
                + "</span>");
            break;
        case "DELETE":
            $(".hintClass").remove();
            $("#hintID").append(
                "<span class='hintClass'>"
                + "DELETE FROM table_name WHERE some_column=some_value"
                + "</span>");
            break;
        default:
            $(".hintClass").remove();
            break;
    }
}

function excelExport() {
    $("#MainContentPlaceHolder_excelExportProcessButton").click();
}

function csvExport() {
    $("#MainContentPlaceHolder_csvExportProcessButton").click();
}