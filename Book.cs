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
		public DateTime? DueDate { get; set; }

		//Constructors
		public Book(string _title, string _author, string _genre)
		{
			Title = _title;
			Author = _author;
			Genre = _genre;
			Available = true;
			DueDate = null;
		}

		//Methods
		public string GetDetails()
		{
			return String.Format("{0,-40} {1,-25} {2,-20} {3, -15} {4, -15}",$"{Title}",$"{ Author}", $"{ Genre}", $"{ Available}", $"{ DueDate}");
		}

        public virtual void UpdateDueDate()
        {
            DueDate = DateTime.Now.AddDays(14);
        }

    }
}

