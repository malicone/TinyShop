function minusClick(element) {
	var $input = $(element).parent().find('input');
	var count = parseInt($input.val()) - 1;
	count = count < 1 ? 1 : count;
	$input.val(count);
	$input.trigger('change');
	return false;
}
function plusClick(element) {
	var $input = $(element).parent().find('input');
	$input.val(parseInt($input.val()) + 1);
	$input.trigger('change');
	return false;
}
function setQuantity(elementId, quantity) {
	var $input = $('#' + elementId);
    $input.val(quantity);
    $input.trigger('change');    
}