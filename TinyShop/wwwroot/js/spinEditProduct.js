function productMinusClick(element, updateCart = false) {
	var $input = $(element).parent().find('input');
	var count = parseInt($input.val()) - 1;
	count = count < 1 ? 1 : count;
	$input.val(count);
	$input.trigger('change');
	if (updateCart) {
		var productId = $(element).data('product-id');		
		removeOneFromCartRequest(productId);
	}
	return false;
}
function productPlusClick(element, updateCart = false) {
	var $input = $(element).parent().find('input');
	$input.val(parseInt($input.val()) + 1);
	$input.trigger('change');
	if (updateCart) {
		var productId = $(element).data('product-id');
		var quantity = 1;
		addToCartRequest(productId, quantity);
	}
	return false;
}
function setProductQuantity(elementId, quantity) {
	var $input = $('#' + elementId);
    $input.val(quantity);
    $input.trigger('change');    
}