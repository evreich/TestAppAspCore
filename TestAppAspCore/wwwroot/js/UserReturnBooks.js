var onSuccessReturnBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-success"></div>`);
    alertMessage.text(context.message);
    $(`#${context.id}`).remove();
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};

var onFailedReturnBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-danger"></div>`);
    alertMessage.text(`Ошибка! Книга не была возвращена!`);
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};