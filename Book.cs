using System;
namespace Group_2_Library
{
	public class Book
	{
		//Properties
		public string Title { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
        public string mediaType { get; set; }
        public bool Available { get; set; }
		public DateOnly? DueDate { get; set; }


		//Constructors
		public Book(string _title, string _author, string _genre, string _mediatype)
		{
			Title = _title;
			Author = _author;
			Genre = _genre;
            mediaType = _mediatype;
			Available = true;
			DueDate = null;
		}

        public Book(string _title, string _author, string _genre, string _mediatype, bool _available, DateOnly? _duedate)
        {
            Title = _title;
            Author = _author;
            Genre = _genre;
            mediaType = _mediatype;
            Available = _available;
            DueDate = _duedate;
        }

        //Methods
        public string GetDetails()
		{
			return String.Format("{0,-40} {1,-25} {2,-20} {3, -15} {4, -15} {5, -15}",
            $"{Title}",$"{ Author}", $"{ Genre}", $"{mediaType}",$"{ Available}", $"{ DueDate}");
		}

        public virtual void UpdateDueDate()
        {
            DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(14);
            Available = false;
        }
        public virtual void Return()
        {
            DueDate = null;
            Available = true;
        }
    }
}

