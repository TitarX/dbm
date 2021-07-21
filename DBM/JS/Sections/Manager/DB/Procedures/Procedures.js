function dropProcedure(procedureName) {
    confirm("Удалить процедуру?", function () {
        document.getElementById("procedureName").value = procedureName;
        $("#MainContentPlaceHolder_dropProcedureProcessButton").click();
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