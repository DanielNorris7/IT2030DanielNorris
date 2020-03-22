$(function () {

	// Hide button click
	$("#hide_btn").click(function () {
		$(".hide_content").hide();
	});

	// Show button click
	$("#show_btn").click(function () {
		$(".hide_content").show();
	});

	// Fun button click
	$("#fun_btn").click(function () {
		//$("#mainJQ").toggleClass("fun");

		//$("#mainJQ").find("p").addClass("fun");
		$("#mainJQ").find("p").css("color", "purple");
	});

});