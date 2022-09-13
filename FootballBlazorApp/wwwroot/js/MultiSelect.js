window.getSelectedItemsInList = function (itemsInList) {
	var selectedItemsInList = [];

	for (var i = 0; i < itemsInList.options.length; i++) {
		if (itemsInList.options[i].selected) {
			selectedItemsInList[selectedItemsInList.length] = itemsInList.options[i].value;
		}
	}

	return selectedItemsInList;
};