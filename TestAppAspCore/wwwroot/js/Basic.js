window.onload = function () {
    closingAlert();
};

var onSuccessAddBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-success"></div>`);
    alertMessage.text(`Книга ${context.bookTitle} успешно добавлена в корзину!`);
    $("#messageBox").append(alertMessage);
    closingAlert();
    $("#countBooks").text(context.countBooks);
};

var onFailedAddBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-danger"></div>`);
    alertMessage.text(`Ошибка! Книга не добавлена в корзину!`);
    $("#messageBox").append(alertMessage);
    closingAlert();
};

function closingAlert() {
    let alert = $("#alert-closing");
    if (alert)
        alert.fadeOut(5000);
};