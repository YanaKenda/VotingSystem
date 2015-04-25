namespace VotingSystem.WebUI.Models.User
{
	using System;

	public class EditModel
	{
		public int Id { get; set; }

		public String Name { get; set; }
		public String Surname { get; set; }
	    public String Email { get; set; }

	    public static EditModel FromEntity(Entities.User entityUser)
		{
			var editModelUser = new EditModel
			{
				Email = entityUser.Email,
				Id = entityUser.Id,
				Name = entityUser.Name,
				Surname = entityUser.Surname
			};

			return editModelUser;


		}

		public Entities.User ToEntity()
		{
			var entityUser = new Entities.User
			{
				Email = Email,
				Id = Id,
				Name = Name,
				Surname = Surname
			};

			return entityUser;
		}
	}
}