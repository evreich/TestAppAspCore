var onSuccessConfirmBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-success"></div>`);
    alertMessage.text(context.message);
    $(`#${context.id}`).remove();
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};

var onFailedConfirmBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-danger"></div>`);
    alertMessage.text(`Ошибка! Книга не была принята!`);
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};

var onSuccessCancelBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-warning"></div>`);
    alertMessage.text(context.message);
    $(`#${context.id}`).remove();
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};

var onFailedCancelBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-danger"></div>`);
    alertMessage.text(`Ошибка! Книга не была отменена!`);
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};
