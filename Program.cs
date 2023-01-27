using Group_2_Library;
using Microsoft.VisualBasic;
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

List<Book> bag = new List<Book>();

bool stillWorking = true;
bool continueQ = true;

Console.WriteLine("Welcome to your local library!");

while (continueQ)
{
    while (stillWorking)
    {
        Console.WriteLine("You can display a list of all books, or search for a book. L to list / S to search.");
        string choice = Console.ReadLine().ToLower().Trim();

        if (choice == "l")
        {
            //list all book info (done)
            PrintHeader();
            foreach (Book b in ourBooks.OrderBy(b => b.Title))
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
                    string searchAuthor = Console.ReadLine().Trim();
                    Console.Clear();
                    PrintHeader();
                    foreach (Book b in ourBooks.OrderBy(b => b.Author).Where(b => b.Author.ToLower().Contains(searchAuthor)))
                    {
                        Console.WriteLine(b.GetDetails());
                        bag.Add(b);
                    }
                    Console.WriteLine();

                    //Console.WriteLine("Please enter the title you'd like to check out:");
                    //string bchoice = Console.ReadLine().Trim();
                    //int indexChoice = ourBooks.FindIndex(b => b.Contains(bchoice));
                    //int indexChoice = ourBooks.IndexOf(bchoice);
                    //int val = ourBooks(b => b.Title == bchoice);

                    //if (bchoice => ")
                    //{
                    //    Console.WriteLine("Which book do you want to check out? Please type in an author.");
                    //}
                    stillWorking = false;
                    break;

                }
                else if (searchType == "k")
                {
                    //search by keyword
                    Console.Write("Please enter title keyword: ");
                    string searchKeyword = Console.ReadLine();
                    Console.Clear();
                    PrintHeader();
                    foreach (Book b in ourBooks.OrderBy(b => b.Title).Where(b => b.Title.ToLower().Contains(searchKeyword)))
                    {
                        Console.WriteLine(b.GetDetails());
                        bag.Add(b);
                    }
                    Console.WriteLine();
                    stillWorking = false;
                    break;
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

    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("Would you like to go again? y/n");
        string continueChoice = Console.ReadLine();

        if (continueChoice == "y")
        {
            stillWorking = true;
            Console.WriteLine();
            break;
        }
        else if (continueChoice == "n")
        {
            stillWorking = false;
            continueQ = false;
            Console.WriteLine();
            break;
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }
}

Console.Clear();
Console.WriteLine("These are the books you are checking out:\n");
PrintHeader();

foreach (Book b in bag)
{
    Console.WriteLine(b.GetDetails());
}

Console.WriteLine("\nGoodbye.");
Console.ReadLine();








static void PrintHeader()
{
    Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-20} {3, -15} {4, -15}", $"Title", $"Author", $"Genre", $"Available", $"DueDate"));
    Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-20} {3, -15} {4, -15}", "========", "========", "========", "========", "========"));
}


