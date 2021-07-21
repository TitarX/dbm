var modalWindow;
var isReset;
var currentTr;
var forRootGlobal;

$(document).ready(function () {
    $("#usersTable").dataTable({
        "oLanguage": {
            "sUrl": "/DBM/CSS/jQuery/Tables/ru_RU.txt"
        },
        "bJQueryUI": true,
        "sPaginationType": "full_numbers",
        "aaSorting": [[0, "asc"]],
        "aLengthMenu": [
        [10, 20, 50, 100, -1],
        [10, 20, 50, 100, "Все"]]
    });
});

function completeDelete(userName) {
    modalWindow.close();
    confirm("Удалить пользователя " + userName + " и все связанные с ним данные?", function () {
        $("#MainContentPlaceHolder_completeDeleteServerButton").click();
    });
}

function incompleteDelete(userName) {
    modalWindow.close();
    confirm("Удалить пользователя " + userName + ", оставив связанные с ним данные?", function () {
        $("#MainContentPlaceHolder_incompleteDeleteServerButton").click();
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

function setUserValues(thisTr, forRoot, userName, email, isAdmin) {
    forRootGlobal = forRoot;
    currentTr = thisTr;
    $("#name").attr("value", userName);
    $("#userName").attr("value", userName);
    $("#MainContentPlaceHolder_emailTextBox").attr("value", email);

    if (forRoot == "true") {
        if (isAdmin.toLowerCase() == "true") {
            $("#adminCheckbox").attr("checked", true);
        }
        else {
            $("#adminCheckbox").attr("checked", false);
        }
    }

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

function resetUserValues() {
    isReset = true;
    currentTr.click();
}

function saveUserValues() {
    //
    var email = $("#MainContentPlaceHolder_emailTextBox").val();
    var isAdmin;
    if (forRootGlobal == "true") {
        isAdmin = $("#adminCheckbox").prop("checked");
    }
    //

    //
    modalWindow.close();
    //

    //
    $("#MainContentPlaceHolder_emailTextBox").attr("value", email);
    if (forRootGlobal == "true") {
        if (isAdmin) {
            $("#adminCheckbox").attr("checked", true);
        }
        else {
            $("#adminCheckbox").attr("checked", false);
        }
    }
    //

    //
    $("#MainContentPlaceHolder_saveServerButton").click();
    //
}

function unlockUser() {
    modalWindow.close();
    $("#MainContentPlaceHolder_unlockUserServerButton").click();
}

function resetPassword() {
    modalWindow.close();
    $("#MainContentPlaceHolder_resetPasswordServerButton").click();
}