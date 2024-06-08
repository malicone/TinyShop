function updateCartSummary() {
    $.ajax({
        type: "GET",
        url: "/Cart/GetCartSummaryComponent/",
        data: null,
        cache: false,
        success: function (result) {            
            $("#cartSummary").replaceWith(result);
        },
    });
}

function addToCartRequest(productId, quantity) {    
    $.ajax({
        type: "GET",
        url: "/Cart/AddToCart/" + productId + "/" + quantity,
        data: null,
        success: function (result) {
            $("#cartSummary").replaceWith(result);
            getCartTableRequest();
        },
    });
}

function removeOneFromCartRequest(productId) {
    $.ajax({
        type: "GET",
        url: "/Cart/RemoveOneFromCart/" + productId,
        data: null,
        success: function (result) {
            $("#cartSummary").replaceWith(result);
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
            $("#cartSummary").replaceWith(result);
            getCartTableRequest();            
        },
    });
}
function goToOrder() {
    window.location.href = "/Cart/Index?returnurl=/Home/Index";
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