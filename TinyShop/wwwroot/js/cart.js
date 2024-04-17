function addToCartRequest(element) {
    var productId = $(element).data("product-id");
    $.ajax({
        type: "GET",
        url: "/Cart/AddToCart/" + productId,
        data: null,
        success: function (result) {
            $("#cart").replaceWith(result);
        },
    });
}

function goToOrder() {
    window.location.href = "/Cart/Index?returnurl=/Home/Index";
}