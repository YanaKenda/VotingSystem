(function (ko,$) {
	
	$('#dp3').datepicker();

	function isNullString(str) {
		for (var i = 0; i < str.length; i++) {
			if (str[i] != " ") {
				return false;
			}
		}
		return true;
	}
	


	function Answer(answerText) {
		this.text = ko.observable(answerText);
	}

	function VotingViewModel() {
		//Data
		var self = this;

		self.name = ko.observable();
		self.newAnswerText = ko.observable();
		self.question = ko.observable();
		self.answers = ko.observableArray([]);
		self.isPrivate = ko.observable(false);
		self.keyWord = ko.observable();
		self.isOpenResult = ko.observable(false);

		//Operations
		self.addAnswer = function () {
			if (!isNullString(self.newAnswerText())) {
				self.answers.push(new Answer(self.newAnswerText()));
				self.newAnswerText("");
			}
		};

		self.removeAnswer = function (answer) {
			self.answers.remove(answer);
		};

		self.save = function () {
			
			var answers = [];

			var endDate = $('#dp3').attr("data-date");

			var dmy = endDate.split("-");

			var date = new Date(dmy[2] - 0, dmy[1] - 0, dmy[0] - 0, 0, 0, 0, 0);

			for (var i = 0; i < self.answers().length; i++) {
				answers.push(self.answers()[i].text());
			}

			if (answers.length <= 0) {
				console.log("Error");
				return;
			}

			$.ajax({
				type: "POST",
				url: "/UserActivity/AddVoting",
				contentType: 'application/json',
				data: JSON.stringify({
					Question: self.question(),
					Answers: answers,
					Name: self.question(),
					IsPrivate: self.isPrivate(),
					IsOpenResult: self.isOpenResult(),
					EndDateTime: date
				}),
				success: function (response) {
					window.location.href = response;
				},
				error: function() {
					console.log("Error");
				}
				});
		};
		

	};

	ko.applyBindings(new VotingViewModel());
})(ko,jQuery);