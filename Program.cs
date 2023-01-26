using Group_2_Library;
using static System.Reflection.Metadata.BlobBuilder;

//Book list
List<Book> ourBooks = new List<Book>()
{
    new Book("The Catcher in the Rye", "J. D. Salinger", "Fiction"),
    new Book("To Kill a Mockingbird", "Harper Lee", "Fiction"),
    new Book("Great Expectations", "Charles Dickens", "Fiction"),
    new Book("The Great Gatsby", "F. Scott Fitzgerald", "Fiction"),
    new Book("Murder on the Orient Express", "Agatha Christie", "Mystery"),
    new Book("The Hound of the Baskervilles", "Arthur Conan Doyle", "Mystery"),
    new Book("Gone with the Wind", "Margaret Mitchell", "Romance"),
    new Book("Jane Eyre", "Jane Austen", "Romance"),
    new Book("Dune", "Frank Herbert", "Science fiction"),
    new Book("Ender's Game", "Orson Scott Card", "Science fiction"),
    new Book("The Hobbit", "J. R. R. Tolkien", "Fantasy"),
    new Book("The Lion, the Witch and the Wardrobe", "C. S. Lewis", "Fantasy")
};

Console.WriteLine("Welcome to your local library!");


while (true)
{
    Console.WriteLine("Would you like to search for a book or display a list? L to list / S to search.");
    string choice = Console.ReadLine().ToLower().Trim();

    if (choice == "l")
    {
        //list all book info (done)
        Console.WriteLine("Title\t\tAuthor\tGenre\tAvailability\tDue Date");
        foreach (Book b in ourBooks)
        {
            Console.WriteLine(b.GetDetails());
        }
        break;
    }
    else if (choice == "s")
    {
        //search
        while (true)
        {
            Console.WriteLine("You can search by author or title keyword. A for author / K for keyword.");
            string searchType = Console.ReadLine().ToLower().Trim();

            if (searchType == "a")
            {
                //search by author
                Console.Write("Please enter author name: ");
                string searchAuthor = Console.ReadLine();

            }
            else if (searchType == "k")
            {
                //search by keyword
                Console.Write("Please enter author name: ");
                string searchKeyword = Console.ReadLine();
            }
            else
            {
                //validate choice - search by author or keyword (done)
                Console.WriteLine("Invalid input, please try again.");
            }
        }

    }
    else //validate choice - list or search (done)
    {
        Console.WriteLine("Invalid input, please try again.");
    }
}

Console.ReadLine();

