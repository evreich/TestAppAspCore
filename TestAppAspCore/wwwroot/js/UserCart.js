var onSuccessAddBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-success"></div>`);
    alertMessage.text(`Книга ${context.bookTitle} успешно добавлена в корзину!`);

    $(`#${context.bookId} small[name=countBook]`).text(context.bookCountInStore);
    if (context.bookCountInStore <= 0) {
        $(`#${context.bookId} button[name=addBookInCart]`).prop('disabled', true);
    }
    
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
    $("#countBooks").text(context.countBooksInCart);
};

var onFailedAddBook = function (context) {
    let alertMessage = $(`<div id="alert-closing" class="alert alert-danger"></div>`);
    alertMessage.text(`Ошибка! Книга не добавлена в корзину!`);
    $("#messageBox").append(alertMessage);
    alertMessage.fadeOut(5000);
};
