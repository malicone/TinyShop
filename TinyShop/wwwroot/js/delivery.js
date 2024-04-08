$(document).ready(function () {
    const NovaPoshtaWarehouseId = 2;
    $("#deliveryType").change(function () {
        var deliveryTypeId = $("#deliveryType").val();
        if (deliveryTypeId == NovaPoshtaWarehouseId) {
            showDeliveryControls();
            getRegions(setDefaultRegion);
        }
        else {
            hideDeliveryControls();
        }
    });
});
function hideDeliveryControls() {
    $("#regionContainer").attr("hidden", "hidden");
    $("#cityContainer").attr("hidden", "hidden");
    $("#warehouseContainer").attr("hidden", "hidden");
}
function showDeliveryControls() {
    $("#regionContainer").removeAttr("hidden");
    $("#cityContainer").removeAttr("hidden");
    $("#warehouseContainer").removeAttr("hidden");
}
function setDefaultRegion(deliveryTypeId, afterDataLoadFunc) {
    var url = "/Order/GetDefaultRegionId/" + deliveryTypeId;
    $.get(url, function (data) {
        $("#region").val(data);
        afterDataLoadFunc();
    });
}
function getRegions(afterDataLoadFunc) {
    var deliveryTypeId = $("#deliveryType").val();
    var url = "/Order/GetRegions/" + deliveryTypeId;
    $.getJSON(url, function (data) {
        $("#region").empty();
        $.each(data, function (index, row) {
            $("#region").append("<option value='" + row.id + "'>" + row.name + "</option>");
        });
        afterDataLoadFunc(deliveryTypeId, getCities);
    });
}
function getCities() {
    var deliveryTypeId = $("#deliveryType").val();
    var regionId = $("#region").val();
    var url = "/Order/GetCitiesByRegion/" + deliveryTypeId + "/" + regionId;
    $.getJSON(url, function (data) {
        $("#city").empty();
        $.each(data, function (index, row) {
            $("#city").append("<option value='" + row.id + "'>" + row.name + "</option>");
        });
        getWarehouses();
    });
}
function getWarehouses() {
    var deliveryTypeId = $("#deliveryType").val();
    var cityId = $("#city").val();
    var url = "/Order/GetWarehousesByCity/" + deliveryTypeId + "/" + cityId;
    $.getJSON(url, function (data) {
        $("#warehouse").empty();
        $.each(data, function (index, row) {
            $("#warehouse").append("<option value='" + row.id + "'>" + row.name + "</option>");
        });
    });
}
$(document).ready(function () {
    $("#region").change(getCities);
});
$(document).ready(function () {
    $("#city").change(getWarehouses);
});
