using Group_2_Library;
using Microsoft.VisualBasic;
using static System.Reflection.Metadata.BlobBuilder;
using Validation;

//Validator.GetContinue();

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

//Adding days when checking out book
//DateTime addedDateTime = DateTime.Now.AddDays(14)

List<Book> bag = new List<Book>();

bool continueQ = true;
bool stillWorking = true;
bool stillSearching = true;

Console.WriteLine("Welcome to your local library!");

while (continueQ)
{
    while (stillWorking)
    {
        stillWorking = true;
        stillSearching = true;
        string choice = "";
        Console.WriteLine("You can display a list of all books, or search for a book. L to list / S to search.");
        choice = Console.ReadLine().ToLower().Trim();

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
            while (stillSearching)
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

                    //display search results
                    foreach (Book b in ourBooks.OrderBy(b => b.Author).Where(b => b.Author.ToLower().Contains(searchAuthor)))
                    {
                        Console.WriteLine(b.GetDetails());
                    }
                    Console.WriteLine();

                    //targeting book choice
                    while (true)
                    {
                        Console.WriteLine("Please enter the title you'd like to check out:");
                        string bchoice = Console.ReadLine().Trim().ToLower();
                        int indexChoice = ourBooks.FindIndex(b => b.Title.ToLower().Contains(bchoice));

                        if (indexChoice == -1 || indexChoice > ourBooks.Count)
                        {
                            Console.WriteLine("Book not found. Please re-enter title.");
                        }
                        else
                        {
                            Console.Clear();
                            if (ourBooks[indexChoice].Available == true)
                            {
                                Console.WriteLine($"You're checking out: {ourBooks[indexChoice].Title}");
                                bag.Add(ourBooks[indexChoice]);
                                ourBooks[indexChoice].UpdateDueDate();
                                //FUNCTIONALITY FOR CHECK-OUT

                                stillSearching = false;
                                stillWorking = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Sorry, {ourBooks[indexChoice].Title} is already checked out. Please select another book");
                                stillSearching = false;
                                stillWorking = false;
                                break;
                            }

                        }
                    }
                }
                else if (searchType == "k")
                {
                    //search by keyword
                    Console.Write("Please enter title keyword: ");
                    string searchKeyword = Console.ReadLine().Trim();
                    Console.Clear();
                    PrintHeader();
                    foreach (Book b in ourBooks.OrderBy(b => b.Title).Where(b => b.Title.ToLower().Contains(searchKeyword)))
                    {
                        Console.WriteLine(b.GetDetails());
                    }
                    Console.WriteLine();

                    //targeting book choice
                    while (true)
                    {
                        Console.WriteLine("Please enter the title you'd like to check out:");
                        string bchoice = Console.ReadLine().Trim().ToLower();
                        int indexChoice = ourBooks.FindIndex(b => b.Title.ToLower().Contains(bchoice));

                        if (indexChoice == -1 || indexChoice > ourBooks.Count)
                        {
                            Console.WriteLine("Book not found. Please re-enter title.");
                        }
                        else
                        {
                            Console.Clear();
                            if (ourBooks[indexChoice].Available == true)
                            {
                                Console.WriteLine($"You're checking out: {ourBooks[indexChoice].Title}");
                                bag.Add(ourBooks[indexChoice]);
                                ourBooks[indexChoice].UpdateDueDate();
                                //FUNCTIONALITY FOR CHECK-OUT

                                stillSearching = false;
                                stillWorking = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Sorry, {ourBooks[indexChoice].Title} is already checked out. Please select another book");
                                stillSearching = false;
                                stillWorking = false;
                                break;
                            }
                        }
                        stillWorking = false;
                        break;
                    }
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

    //add getcontinuecustom()
    while (continueQ)
    {
        Console.WriteLine();
        Console.WriteLine("Would you like to go again? y/n");
        string continueChoice = Console.ReadLine();

        if (continueChoice == "y")
        {
            stillWorking = true;
            continueQ = true;
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


