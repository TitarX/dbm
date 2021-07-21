$(document).ready(function () {
    $("#MainContentPlaceHolder_gridView").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
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

function excelExport() {
    $("#MainContentPlaceHolder_excelExportProcessButton").click();
}

function csvExport() {
    $("#MainContentPlaceHolder_csvExportProcessButton").click();
}