(function($) {
	"use strict";

	var pollId = $("#pollId").text();

	function showResult() {
		$.ajax({
			url: "/Poll/PollViewState",
			cache: false,
			data: { "pollId": pollId },
			success: function (data) {
				$("#target").html(data);
			}
		});
	}
	
	var time = null;

	$.ajax({
		url: "/Poll/GetTimeForFinfish",
		cache: false,
		type: "Get",
		async: false,
		data: { "pollId": pollId },
		success: function(data) {
			time = data;
		},
		error: function() {
		}
	});

	var containerWidth = $("#dateProgressContainer").width();
	var startWidth = $("#dateProgress").width(); 
	if (time == null || startWidth >= containerWidth) {
		return;
	}

	var percent = 100 * (startWidth / containerWidth);

	function incProgressBar() {
		containerWidth = $("#dateProgressContainer").width();
		
		if (percent < 100) {
			$("#dateProgress").width((++percent) * containerWidth / 100);
		}
		else {
			showResult();
			clearInterval(intervalId);
		}
	}

	var intervalId = setInterval(incProgressBar, time);

})(jQuery);