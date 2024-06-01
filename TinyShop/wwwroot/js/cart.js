function addToCartRequest(productId, quantity) {    
    $.ajax({
        type: "GET",
        url: "/Cart/AddToCart/" + productId + "/" + quantity,
        data: null,
        success: function (result) {
            $("#cart").replaceWith(result);
            getCartTableRequest();
        },
    });
}

function removeOneFromCartRequest(element) {
    var productId = $(element).data("product-id");
    $.ajax({
        type: "GET",
        url: "/Cart/RemoveOneFromCart/" + productId,
        data: null,
        success: function (result) {
            $("#cart").replaceWith(result);
            getCartTableRequest();
        },
    });
}

function removeEntireLineRequest(productId) {       
    $.ajax({
        type: "GET",
        url: "/Cart/RemoveEntireLine/" + productId,
        data: null,
        success: function (result) {
            $("#cart").replaceWith(result);
            getCartTableRequest();            
        },
    });
}
function goToOrder() {
    window.location.href = "/Cart/Index?returnurl=/Home/Index";
}

function productPlus(element) {
    addToCartRequest(element);
}
function productMinus(element) {
    removeOneFromCartRequest(element);
}

function getCartTableRequest() {
    $.ajax({
        type: "GET",
        url: "/Cart/GetCartTableComponent",
        data: null,
        success: function (result) {
            $("#cartTable").replaceWith(result);
        },
    });
}