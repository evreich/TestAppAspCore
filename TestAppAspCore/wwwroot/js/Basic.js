var onSuccessAddBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-success"></div>`);
    alertMessage.text(`Книга ${context.bookTitle} успешно добавлена в корзину!`);
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
    $("#countBooks").text(context.countBooks);
};

var onFailedAddBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-danger"></div>`);
    alertMessage.text(`Ошибка! Книга не добавлена в корзину!`);
    $("#messageBox").append(alertMessage);
    closingAlert();
};
