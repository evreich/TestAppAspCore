if (window.onload) {
    if ($("#alert-closing") !== null) {
        $("#alert-closing").fadeOut(5000);
    }
}

var onSuccessConfirmOrder = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-success"></div>`);
    alertMessage.text(context.message);
    $(`#${context.id}`).remove();
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};

var onFailedConfirmOrder = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-danger"></div>`);
    alertMessage.text(`Ошибка! Заказ не был подтверджен!`);
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};

var onSuccessCancelOrder = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-success"></div>`);
    alertMessage.text(context.message);
    $(`#${context.id}`).remove();
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};

var onFailedCancelOrder = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-danger"></div>`);
    alertMessage.text(`Ошибка! Заказ не был отменен!`);
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};