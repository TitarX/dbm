var currentTr;
var modalWindow;
var isReset;

$(document).ready(function () {
    $("#queriesTable").dataTable({
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

function deleteQuery() {
    modalWindow.close();
    confirm("Удалить запрос?", function () {
        $("#MainContentPlaceHolder_deleteQueryServerButton").click();
    });
}

function setQueryValues(thisTr, i) {
    currentTr = thisTr;
    $("#MainContentPlaceHolder_newNameTextBox").attr("value", $("#nameHidden" + i).val());
    $("#oldName").attr("value", $("#nameHidden" + i).val());
    $("#MainContentPlaceHolder_queryTextBox").attr("value", $("#queryHidden" + i).val());

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

function resetQueryValues() {
    isReset = true;
    currentTr.click();
}

function saveQueryValues() {
    //
    var newName = $("#MainContentPlaceHolder_newNameTextBox").val();
    var oldName = $("#oldName").val();
    var query = $("#MainContentPlaceHolder_queryTextBox").val();
    //

    //
    modalWindow.close();
    //

    //
    $("#MainContentPlaceHolder_newNameTextBox").attr("value", newName);
    $("#oldName").attr("value", oldName);
    $("#MainContentPlaceHolder_queryTextBox").attr("value", query);
    //

    //
    $("#MainContentPlaceHolder_saveServerButton").click();
    //
}

function prepareF() {
    //
    var newName = $("#MainContentPlaceHolder_newNameTextBox").val();
    var oldName = $("#oldName").val();
    var query = $("#MainContentPlaceHolder_queryTextBox").val();
    //

    //
    modalWindow.close();
    //

    //
    $("#MainContentPlaceHolder_newNameTextBox").attr("value", newName);
    $("#oldName").attr("value", oldName);
    $("#MainContentPlaceHolder_queryTextBox").attr("value", query);
    //

    //
    $("#MainContentPlaceHolder_prepareServerButton").click();
    //
}