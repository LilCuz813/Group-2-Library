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

Console.WriteLine("Welcome to your local library!");


while (true)
{
    Console.WriteLine("Would you like to search for a book or display a list? L to list / S to search.");
    string choice = Console.ReadLine().ToLower().Trim();

    if (choice == "l")
    {
        //list all book info (done)
        PrintHeader();
        foreach (Book b in ourBooks.OrderBy(b=>b.Title))
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
                foreach (Book b in ourBooks.OrderBy(b=>b.Author).Where(b => b.Author.ToLower().Contains(searchAuthor)))
                {
                    Console.WriteLine(b.GetDetails());
                }
                Console.WriteLine();
                Console.WriteLine("Would you like to check out a book? Y/N");
                string bchoice = Console.ReadLine ()

                break;

            }
            else if (searchType == "k")
            {
                //search by keyword
                Console.Write("Please enter title keyword: ");
                string searchKeyword = Console.ReadLine();
                Console.Clear();
                PrintHeader();
                foreach (Book b in ourBooks.OrderBy(b=>b.Title).Where(b => b.Title.ToLower().Contains(searchKeyword)))
                {
                    Console.WriteLine(b.GetDetails());
                }
                Console.WriteLine();
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

static void PrintHeader()
{
    Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-20} {3, -15} {4, -15}", $"Title", $"Author", $"Genre", $"Available", $"DueDate"));
    Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-20} {3, -15} {4, -15}", "========", "========", "========", "========", "========"));
}

Console.ReadLine();
List<Book> choiceBook = new List<Book>()
static string AddItemList(string x)
int index = -1;
if (ourBooks.ContainsKey(b))
{
    //item existing
    cart.Add(choice);
    Console.WriteLine($"{choice} was added to cart.");
}
//tryParse returns true/false. ALSO out keyword returns back the parsed number if successful
else if (int.TryParse(choice, out index))
{
    Console.WriteLine($"{menu.OrderByDescending(i => i.Value).ElementAt(index).Key} was added to cart.");
    cart.Add(menu.OrderByDescending(i => i.Value).ElementAt(index).Key);
}
else
{
    //item doesn't exist
    Console.WriteLine($"{choice} is not on the menu.");
}

//Asking to buy more
while (true)
{
    Console.WriteLine("Would you like to buy another item? y/n");
    string continueChoice = Console.ReadLine();
    if (continueChoice == "y")
    {
        Console.Clear();
        buying = true;
        break;
    }
    else if (continueChoice == "n")
    {
        buying = false;
        break;
    }
    else
    {
        Console.WriteLine("Invalid input");
    }
}