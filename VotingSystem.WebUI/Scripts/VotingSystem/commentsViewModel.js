(function (ko, $) {
	var modelView;

	var pollId = $("#pollId").text();

	$.ajax({
		url: "/Poll/GetCommentViewModel",
		dataType: "json",
		type: "Get",
		async: false,
		cache: false,
		data: { "pollId": pollId },
		success: function (data) {
			data.Comments.reverse();
			modelView = data;
		},
		error: function () {
			alert("error");
		}
	});

	//VIEW MODEL
	function commentsViewModel(model) {
		var self = this;
		//DATA
		self.comments = ko.observableArray(model.Comments);
		self.newCommentText = ko.observable(model.NewCommentText);

		//help success function
		function loadComments(data) {
			if (data == null) {
				return;
			}
			self.commentsCount = data.CommentsCount;
			self.comments.removeAll();
			for (var item in data.Comments) {
				self.comments.unshift(data.Comments[item]);
			}
			self.newCommentText("");
		}

		function serverError() {
			alert("error");
		}

		// event handler
		self.addComment = function () {
			if (self.newCommentText() == "") {
				return;
			}

			$.ajax({
				url: "/Poll/AddComment",
				dataType: "json",
				type: "Get",
				data: { "pollId": model.Id, "text": self.newCommentText() },
				success: loadComments,
				error: serverError
			});
		};

		self.deleteComment = function (comment) {
			if (comment != null) {
				$.ajax({
					url: "/Poll/DeleteComment",
					dataType: "json",
					type: "Get",
					data: { "pollId": model.Id, "commentId": comment.Id },
					success: loadComments,
					error: serverError
				});
			}
		};
		return self;
	};

	var viewModel = commentsViewModel(modelView);
	ko.applyBindings(viewModel);

	// functions for pooling server
	function poolServer() {
		$.ajax({
			url: "/Poll/GetCommentViewModel",
			dataType: "json",
			cache: false,
			type: "Get",
			data: { "pollId": pollId },
			success: function (data) {
				if (data == null) {
					return;
				}

				viewModel.comments.removeAll();
				for (var item in data.Comments) {
					viewModel.comments.unshift(data.Comments[item]);
				}

				viewModel.newCommentText("");
			},
			error: function () {
				alert("error");
			}
		});
	}
	setInterval(poolServer, 10000);

})(ko, jQuery);