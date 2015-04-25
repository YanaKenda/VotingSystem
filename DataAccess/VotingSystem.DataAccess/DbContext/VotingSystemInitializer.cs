using System.Collections.Generic;

namespace VotingSystem.DataAccess.DbContext
{
	using System;
	using System.Data.Entity;
	using Entities;

	public class VotingSystemInitializer :
		DropCreateDatabaseAlways<VotingSystemContext>
	{

		//create and initialization database
		protected override void Seed(VotingSystemContext context)
		{
			base.Seed(context);

			var adminRole = new Role
			{
				Name = "Admin"
			};
			context.Roles.Add(adminRole);


			var maderatorRole = new Role
			{
				Name = "Moderator"
			};
			context.Roles.Add(maderatorRole);


			var userRole = new Role
			{
				Name = "User"
			};
			context.Roles.Add(userRole);

			var anonimRole = new Role
			{
				Name = "Ananymous"
			};
			context.Roles.Add(anonimRole);


			//users initialization
			var user1 = new User
			{
				Name = "Denis",
				Surname = "Novogrodski",
				Email = "novogrodski1992@gmail.com",
				Password = "12345",
				DateOfBirth = DateTime.Now,
				Roles = new[] { maderatorRole }
			};
			context.Users.Add(user1);


			var user2 = new User
			{
				Name = "Alex",
				Surname = "Starovoitov",
				Email = "star@gmail.com",
				Password = "iamsexy",
				DateOfBirth = DateTime.Now,
				Roles = new[] { userRole }
			};
			context.Users.Add(user2);


			var user3 = new User
			{
				Name = "Anton",
				Surname = "Zasenko",
				Email = "zasenko@gmail.com",
				Password = "654321",
				DateOfBirth = DateTime.Now,
				Roles = new[] { adminRole }
			};
			context.Users.Add(user3);
			//TODO: add data


			//answer initializing
			var answer1 = new Answer
			{

				AnswerText = "YES",
				Count = 50
			};
			context.Answers.Add(answer1);

			var answer2 = new Answer
			{
				AnswerText = "NO",
				Count = 25
			};
			context.Answers.Add(answer2);

			var answer3 = new Answer
			{
				AnswerText = "OTHER",
				Count = 25
			};
			context.Answers.Add(answer3);

			//comments initializing
			var comment1 = new Comment { Text = "Green button is so cool", User = user2, CommentTime = DateTime.Now };
			context.Comments.Add(comment1);

			//votings initializing
			context.Polls.Add(new Poll
			{
				Name = "voting1",
				Question = "Do you like this site?",
				Count = 100,
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now.AddHours(1),
				Answers = new[] { answer1 },
				User = user1,
				IsOpenResult = true,
				Comments = new[] { comment1 }
			});

			context.Polls.Add(new Poll
			{
				Name = "voting2",
				Question = "Do you like this site?",
				Count = 75,
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now.AddSeconds(40),
				Answers = new[] { answer2 },
				User = user2,
				IsOpenResult = false,
				IsPrivate = true,
				KeyWord = "qwerty",
				Comments = new List<Comment>()
			});

			context.Polls.Add(new Poll
			{
				Name = "voting3",
				Question = "Do you like this site?",
				Count = 75,
				StartDateTime = DateTime.Now,
				EndDateTime = DateTime.Now.AddHours(1),
				Answers = new[] { answer3 },
				User = user3,
				IsPrivate = true,
				KeyWord = "qwerty",
				IsOpenResult = true,
				Comments = new List<Comment>()
			});



			context.SaveChanges();
		}
	}
}
