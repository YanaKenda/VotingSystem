using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using VotingSystem.WebUI.Models;
using VotingSystem.WebUI.Models.User;

namespace VotingSystem.WebUI.HtmlHelpers
{
	public static class PagingHelper
	{
		public static MvcHtmlString PollGrid(this AjaxHelper ajax,
			VotingsListViewModel votingList, bool aproved)
		{
			var div = new TagBuilder("div");
			div.GenerateId("target");

			var grid = new WebGrid(votingList.Votings,
				new[] { "Name","EndDateTime", "Count" },
				defaultSort: "Name", ajaxUpdateContainerId: "target",
				canSort: true, rowsPerPage: 2);

			var columns = new List<WebGridColumn>();
			columns.AddRange(grid.Columns(
				grid.Column("Name",
					format: item =>
					{
						var a = new TagBuilder("a");
						a.SetInnerText(item.Name);
						a.MergeAttribute("href", "/Poll/Details/?pollId=" + item.Id);
						a.AddCssClass("text-info");
						return MvcHtmlString.Create(a.ToString());
					}),
				grid.Column("EndDateTime", "Finish"),
				grid.Column("Count", "People Voted")));

			if (aproved)
			{
				columns.Add(grid.Column("Ban", format: o =>
				{
					var bunContainer = new TagBuilder("div");
					bunContainer.GenerateId("bunContainer" + o.Id);
					if (o.IsBlocked)
					{
						bunContainer.InnerHtml = ajax.ActionLink("Unban", "Index", "PollManage", new { pollid = o.Id }, new AjaxOptions { UpdateTargetId = "bunContainer"+ o.Id},
						new { @class = "label label-success", id = "bunButton" + o.Id }).ToHtmlString();
					}
					else
					{
						bunContainer.InnerHtml = ajax.ActionLink("Ban", "Index", "PollManage", new { pollid = o.Id }, new AjaxOptions { UpdateTargetId = "bunContainer" + o.Id },
						new { @class = "label label-danger", id = "bunButton" + o.Id}).ToHtmlString();
					}
					return MvcHtmlString.Create(bunContainer.ToString());
				}));
			}

			div.InnerHtml = grid.GetHtml(tableStyle: "table table-striped table-bordered table-hover", columns: columns.ToArray()).ToHtmlString();
			return MvcHtmlString.Create(div.ToString());
		}


		public static MvcHtmlString UserGrid(this AjaxHelper ajax, IndexModel userList)
		{
			var div = new TagBuilder("div");
			var MessageBox = new TagBuilder("div");

			MessageBox.GenerateId("messageBox");
			div.GenerateId("target");

			var grid = new WebGrid(userList.Users, new[] { "Name", "Surname", "Email", "Role" },
				defaultSort: "Name", ajaxUpdateContainerId: "target", canSort: true, rowsPerPage: userList.ItemsPerPage);

			var columns = new List<WebGridColumn>();
			columns.AddRange(grid.Columns(
					grid.Column("Name",
					format:
						item =>
						{
							var a = new TagBuilder("a");
							a.SetInnerText(item.Name);
							a.MergeAttribute("href", "/User/Profile/?id=" + item.Id);
							a.AddCssClass("text-info");
							return MvcHtmlString.Create(a.ToString());
						}),
					grid.Column("Surname", header: "Surname"),
					grid.Column("Email"),
					grid.Column("Role",
						format: u =>
						{
							var span = new TagBuilder("span");
							span.GenerateId("roleUser" + u.Id);
							span.SetInnerText(u.Role[0]);
							return MvcHtmlString.Create(span.ToString());
						}),
					grid.Column("Action with Role",
						format: u =>
						{
							var dropDownDiv = new TagBuilder("div");
							dropDownDiv.MergeAttribute("class", "dropdown");

							var dropDownA = new TagBuilder("a");
							dropDownA.MergeAttribute("class", "dropdown-toggle");
							dropDownA.MergeAttribute("data-toggle", "dropdown");
							dropDownA.MergeAttribute("href", "#");
							dropDownA.SetInnerText("Set role");

							var selectBlock = new TagBuilder("ul");
							selectBlock.MergeAttribute("class", "dropdown-menu");
							selectBlock.MergeAttribute("roll", "menu");
							selectBlock.MergeAttribute("aria-labelledby", "dLable");
							var optionBlock = new TagBuilder("li")
							{
								InnerHtml = ajax.ActionLink("Make as Moderator", "SetRole", "User",
									new { id = u.Id, roleName = "moderator" }, new AjaxOptions() { UpdateTargetId = "roleUser" + u.Id }).ToHtmlString()
							};

							var optionBlock2 = new TagBuilder("li")
							{
								InnerHtml = ajax.ActionLink("Make as Regular User", "SetRole", "User",
									new { id = u.Id, roleName = "user" }, new AjaxOptions() { UpdateTargetId = "roleUser" + u.Id }).ToHtmlString()
							};

							var optionBlock3 = new TagBuilder("li")
							{
								InnerHtml = ajax.ActionLink("Make as Admin", "SetRole", "User",
									new { id = u.Id, roleName = "admin" }, new AjaxOptions() { UpdateTargetId = "roleUser" + u.Id }).ToHtmlString()
							};

							selectBlock.InnerHtml = optionBlock.ToString() + optionBlock2.ToString() + optionBlock3.ToString();
							dropDownDiv.InnerHtml = dropDownA.ToString() + selectBlock.ToString();
							return MvcHtmlString.Create(dropDownDiv.ToString());
						})));


			div.InnerHtml = MessageBox.ToString() + grid.GetHtml(tableStyle: "table table-striped table-bordered table-hover",
				columns: columns).ToHtmlString();
			return MvcHtmlString.Create(div.ToString());
		}

	}
}
