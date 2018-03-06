window.onload = function () {
    if (document.getElementById("createBook")) {
        document.getElementById("createBook").addEventListener("click", () => {
            window.location = "Book/Create";
        });
    }
}