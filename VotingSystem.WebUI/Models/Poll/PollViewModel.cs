namespace VotingSystem.WebUI.Models.Poll
{
	using System;
	using System.Collections.ObjectModel;
	using System.ComponentModel.DataAnnotations;
	using Entities;

	public class PollViewModel
	{
		public string Question { get; set; }

		[Required(ErrorMessage = "*")]
		public string Name { get; set; }

		public string[] Answers { get; set; }

		public bool IsPrivate { get; set; }

		public bool IsOpenResult { get; set; }

		public  string KeyWord { get; set; }

		public DateTime EndDateTime { get; set; }

		public Poll ToEntities()
		{
			var poll = new Poll
			{
				Name = Name,
				Question = Question,
				IsPrivate = IsPrivate,
				IsOpenResult = IsOpenResult,
				KeyWord = KeyWord,
				EndDateTime = EndDateTime,
				StartDateTime = DateTime.Now,
				Answers = new Collection<Answer>()
			};

			foreach (var answer in Answers)
			{
				poll.Answers.Add(new Answer{AnswerText = answer});
			}

			return poll;
		}
	}
}