$(document).ready(function () {
    $("#messagesTable").dataTable({
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

function setMessageValues(messageID, userName, email, subject, messageDate) {
    var messageWindow = window.open("", "messageWindow", "width=640,height=480,location=no,status=yes,toolbar=no,menubar=no,directories=no,resizable=yes,scrollbars=yes");
    messageWindow.document.write("<html><head><title>Сообщение от " + userName + "</title></head><body style='text-align:center;background:#EEEEEE url(/DBM/Images/background/base.jpg);'>"
    + "<form><input type='button' value='Закрыть' onclick='self.close()' /></form>"
    + "<table align='left' width='100%' border='1px'>"
    + "<tr><td><strong>Имя</strong></td><td>" + userName + "</td></tr>"
    + "<tr><td><strong>Email</strong></td><td>" + email + "</td></tr>"
    + "<tr><td><strong>Дата</strong></td><td>" + messageDate + "</td></tr>"
    + "<tr><td><strong>Тема</strong></td><td>" + subject + "</td></tr>"
    + "<tr><td><strong>Сообщение</strong></td><td><textarea id='message' cols='60' rows='15' readonly='readonly'></textarea></td></tr>"
    + "</table></body></html>");
    messageWindow.moveTo(screen.width / 2 - 320, screen.height / 2 - 240);
    messageWindow.focus();

    getMessage(messageID, messageWindow);
}

function getMessage(messageID, messageWindow) {
    GetMessageCallback = function (result) {
        messageWindow.document.getElementById("message").value = result;
        window.location.reload();
    }
    PageMethods.GetMessage(messageID, GetMessageCallback, ErrorHandler, TimeOutHandler);
}

function TimeOutHandler(result) {
    alert("Не удалось получить сообщение, повторите позже");
}
function ErrorHandler(result) {
    alert("Не удалось получить сообщение, повторите позже");
}