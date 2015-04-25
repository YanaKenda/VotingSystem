namespace VotingSystem.WebUI.Models.Poll
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using Entities;

	public class CommentViewModel
	{
		public int Id { get; set; }

		public int Count { get; set; }

		public List<CommentModel> Comments { get; set; }

		public int UserId { get; set; }

		public string NewCommentText { get; set; }

		public void AddComment(Comment newComment)
		{
			Comments.Add(new CommentModel { Id = newComment.Id, Text = newComment.Text, UserName = newComment.User.Name , CommentTime = DateTime.Now.ToString(CultureInfo.InvariantCulture)});
		}

		public CommentViewModel(Poll poll)
		{
			Id = poll.Id;
			Comments = new List<CommentModel>();

			foreach (var comment in poll.Comments.OrderBy(c => c.CommentTime))
			{
				Comments.Add(new CommentModel { Id = comment.Id, Text = comment.Text, UserName = comment.User.Name, CommentTime = comment.CommentTime.ToString(CultureInfo.InvariantCulture) });
			}

			Count = poll.Count;
			UserId = poll.User.Id;
			NewCommentText = "";
		}
	}

	public class CommentModel
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Text { get; set; }
		public string CommentTime { get; set; }
	}
}