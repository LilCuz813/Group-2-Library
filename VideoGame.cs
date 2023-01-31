using System;
namespace Group_2_Library
{
	public class VideoGame : Book
	{

		public string rating { get; set; }

		public VideoGame(string _title, string _author, string _genre, string _mediatype, bool _available, DateOnly? _duedate, string _rating)
			:base(_title, _author, _genre, _mediatype, _available, _duedate)
		{
			rating = _rating;
		}
	}
}

