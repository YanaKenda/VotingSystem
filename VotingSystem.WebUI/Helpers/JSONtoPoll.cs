namespace VotingSystem.WebUI.Helpers
{
	using System.Collections.Generic;
	using System.Linq;
	using Newtonsoft.Json.Linq;

	public class JSONtoPoll
	{
		public string Question { get; set; }
		public IList<string> Answers { get; set; }

		public JSONtoPoll(string json)
		{
			JObject o = JObject.Parse(json);

			Question = (string)o["question"];

			Answers = o["answers"].Select(a => (string)a["text"]).ToList();
		}
	}
}