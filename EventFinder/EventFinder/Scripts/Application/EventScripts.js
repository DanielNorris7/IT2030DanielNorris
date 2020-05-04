function searchFailed() {
	$("#searchresults").html("No Events Found.")
}

$(function () {
	$('.RemoveLink').click(function () {
		var id = $(this).attr("data-id");

		$.post("/ShoppingCart/RemoveFromCart", { "id": id }, function (data) {
			$("#update-message").text(data.Message);
			$("#item-count-" + data.DeleteId).text(data.ItemCount);

			if (data.ItemCount < 1) {
				$("#record-" + data.DeleteId).fadeOut();
			}
		});
	})
});