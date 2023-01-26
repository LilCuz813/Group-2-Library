using System;
namespace Group_2_Library
{
	public class Book
	{
		//Properties
		public string Title { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
		public bool Available { get; set; }
		public DateTime DueDate { get; set; }

		//Constructors
		public Book(string _title, string _author, string _genre)
		{
			Title = _title;
			Author = _author;
			Genre = _genre;
			Available = true;
			DueDate = DateTime.Today;
		}

		//Methods
		public string GetDetails()
		{
			return $"Title: {Title}\tAuthor: {Author}\tGenre: {Genre}\tStatus: {Available}\tDueDate: {DueDate}";
		}
	}
}

