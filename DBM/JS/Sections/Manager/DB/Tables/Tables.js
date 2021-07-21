function dropTable(tableName) {
    confirm("Удалить таблицу?", function () {
        document.getElementById("tableName").value = tableName;
        $("#MainContentPlaceHolder_dropTableProcessButton").click();
    });
}

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

function openTable(tableName) {
    document.getElementById("tableName").value = tableName;
    $("#MainContentPlaceHolder_openTableProcessButton").click();
}