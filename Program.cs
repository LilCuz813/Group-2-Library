using Group_2_Library;
using Microsoft.VisualBasic;
using static System.Reflection.Metadata.BlobBuilder;
using Validation;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;

//Book list
List<Book> ourBooks = TextReader();

List<Book> bag = new List<Book>();

bool continueQ = true;
bool taskSelection = true;
bool stillBrowsing = true;
bool stillSearching = true;
bool repeatBorrowing = true;

Console.WriteLine("Welcome to your local library!");

while (continueQ)
{
    while (taskSelection)
    {
        Console.WriteLine("Would you like to return books, or borrow? R for return / B for borrow.");
        string userTask = Console.ReadLine().ToLower().Trim();

        if (userTask == "r")
        {
            stillBrowsing = false;
            continueQ = false;
            taskSelection = false;
            repeatBorrowing = false;
            
        }
        else if (userTask == "b")
        {
            stillBrowsing = true;
            taskSelection = false;
        }
        else
        {
            Console.WriteLine("Invalid input. R for return / B for borrow.");
        }
    }


    while (stillBrowsing)
    {
        stillBrowsing = true;
        stillSearching = true;
        string choice = "";
        Console.WriteLine("You can display a list of all books, or search for a book. L to list / S to search.");
        choice = Console.ReadLine().ToLower().Trim();

        if (choice == "l")
        {
            //list all book info
            PrintHeader();
            foreach (Book b in ourBooks.OrderBy(b => b.mediaType).ThenBy(b => b.Title))
            {
                Console.WriteLine(b.GetDetails());
            }

            int selectedBook = BookSelection(ourBooks);
            bag.Add(ourBooks[selectedBook]);
            ourBooks[selectedBook].UpdateDueDate();
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
                    string searchAuthor = Console.ReadLine().Trim().ToLower();
                    //Console.Clear();

                    List<Book> authorResults = ourBooks.OrderBy(b => b.Author).Where(b => b.Author.ToLower().Contains(searchAuthor)).ToList();

                    if (authorResults.Count > 0 && (authorResults.Any(b => b.Available == true)))
                    {
                        PrintHeader();
                        foreach (Book b in authorResults)
                        {
                            Console.WriteLine(b.GetDetails());
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("No books available.");
                        break;
                    }

                    //targeting book choice + checkout
                    int selectedBook = BookSelection(ourBooks);
                    bag.Add(ourBooks[selectedBook]);
                    ourBooks[selectedBook].UpdateDueDate();

                    stillSearching = false;
                    stillBrowsing = false;
                }
                else if (searchType == "k")
                {
                    //search by keyword
                    Console.Write("Please enter title keyword: ");
                    string searchKeyword = Console.ReadLine().Trim().ToLower();

                    List<Book> keywordResults = ourBooks.OrderBy(b => b.Title).Where(b => b.Title.ToLower().Contains(searchKeyword)).ToList();


                    if (keywordResults.Count > 0 && (keywordResults.Any(b => b.Available == true)))
                    {
                        PrintHeader();
                        foreach (Book b in keywordResults)
                        {
                            Console.WriteLine(b.GetDetails());
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("No search results");
                        break;
                    }

             

                    //targeting book choice + checkout
                    int selectedBook = BookSelection(ourBooks);
                    bag.Add(ourBooks[selectedBook]);
                    ourBooks[selectedBook].UpdateDueDate();

                    stillSearching = false;
                    stillBrowsing = false;
                }

                else
                {
                    //validate choice - search by author or keyword
                    Console.WriteLine("Invalid input, please try again.");
                }
            }
        }
        else //validate choice - list or search
        {
            Console.WriteLine("Invalid input. L to list / S to search.");
        }
    }

        // Still want to borrow books?

    while(repeatBorrowing)
    {
    Console.WriteLine();
    Console.WriteLine("Would you like to borrow another book? Y to borrow a book / N to complete visit.");
    string continueChoice = Console.ReadLine().ToLower().Trim();

    if (continueChoice == "y")
    {
        stillBrowsing = true;
            continueQ = true;
        Console.WriteLine();
            break;
    }
    else if (continueChoice == "n")
    {
        stillBrowsing = false;
        continueQ = false;
        Console.WriteLine();
            break;
    }
    else
    {
        Console.WriteLine("Invalid input. Y to borrow a book / N to finish borrowing.");
    }

        break;
}

}

bool stillReturning = true;
//Return books

List<Book> bookDrop = new List<Book>();

    while (stillReturning)
    {
        Console.WriteLine("Return a book? Y to return a book / N to complete visit.");
        string returnOption = Console.ReadLine().ToLower().Trim();

        if (returnOption == "y")
        {
            Console.WriteLine("Which book would you like to return?");
            string bookToReturn = Console.ReadLine().Trim().ToLower();
            int returnIndex = ourBooks.FindIndex(b => b.Title.ToLower().Contains(bookToReturn));

            try// When the user types in a book that doesn't exist or is not in their bag, it will return an exception
            {
                if (ourBooks[returnIndex].Available == false)
                {
                   ourBooks[returnIndex].Return();
                   int bagReturnIndex = bag.FindIndex(b => b.Title.ToLower().Contains(bookToReturn));

                   

                   if (bagReturnIndex != -1)
                   {

                    bookDrop.Add(bag[bagReturnIndex]);
                    bag.Remove(bag[bagReturnIndex]);

                   }
                    else
                    {
                    bookDrop.Add(ourBooks[returnIndex]);
                     }
                   

                  Console.WriteLine($"You have returned {ourBooks[returnIndex].Title}");

                }
                else
                {
                    Console.WriteLine("Cannot return - this book was already available.");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Cannot return this item.");
            }
        }
        else if (returnOption == "n")
        {
            stillReturning = false;
        }
        else
        {
            Console.WriteLine("Invalid input. Y to return a book / N to complete visit.");
        }
    }

//Summary of activity
Console.Clear();
if (bag.Any(b => b.Title == "Fahrenheit 451"))
{
    TorchIt();
}
if (bag.Count <= 0)
{
    Console.WriteLine("You aren't taking any books home today.");
}
else
{
    Console.WriteLine("These are the books you are checking out:\n");
    PrintHeader();

    foreach (Book b in bag.Where(b => b.Available == false))
    {
        Console.WriteLine(b.GetDetails());
    }
}

if (bookDrop.Count <= 0)
{
    Console.WriteLine("\nYou did not return any books today.");
}
else
{
    Console.WriteLine("\nYou returned:\n");
    PrintHeader();

    foreach (Book b in bookDrop)
    {
        Console.WriteLine(b.GetDetails());
    }
}


Console.WriteLine("\nGoodbye.");

TextWriter(ourBooks);


//Methods
static int BookSelection(List<Book>books)
{
    
    while (true)
    {
        Console.WriteLine("Please enter the title you'd like to check out:");
        string bchoice = Console.ReadLine().Trim().ToLower();
        int indexChoice = books.FindIndex(b => b.Title.ToLower().Contains(bchoice));

        if (indexChoice == -1 || indexChoice > books.Count)
        {
            Console.WriteLine("Book not found. Please re-enter title.");
        }
        else if (books[indexChoice].Available == false)
        {
            Console.WriteLine("Book already checked out.");
        }
        else
        {
            Console.WriteLine($"You're checking out: {books[indexChoice].Title}");
            return indexChoice;
            
        }
         
    }
    return -1;
    
}


static void PrintHeader()
{
    Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-20} {3, -15} {4, -15} {5, -15}", $"Title", $"Author", $"Genre", $"Media Type", $"Available", $"DueDate"));
    Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-20} {3, -15} {4, -15} {5, -15}", "========", "========", "========", "========", "========", "========"));
}


static List<Book> TextReader()
{
    string filePath = "../../../LibraryList.txt";

    //StreamWriter writer = new StreamWriter(filePath);

    if (File.Exists(filePath) == false)
    {
        StreamWriter tempWriter = new StreamWriter(filePath);
        tempWriter.WriteLine("The Catcher in the Rye|J. D. Salinger|Fiction|Book");
        tempWriter.WriteLine("To Kill a Mockingbird|Harper Lee|Fiction|Book");
        tempWriter.WriteLine("Great Expectations|Charles Dickens|Fiction|Book");
        tempWriter.WriteLine("The Great Gatsby|F. Scott Fitzgerald|Fiction|Book");
        tempWriter.WriteLine("Murder on the Orient Express|Agatha Christie|Mystery|Book");
        tempWriter.WriteLine("The Hound of the Baskervilles|Arthur Conan Doyle|Mystery|Book");
        tempWriter.WriteLine("Gone with the Wind|Margaret Mitchell|Romance|Book");
        tempWriter.WriteLine("Jane Eyre|Jane Austen|Romance|Book");
        tempWriter.WriteLine("Dune|Frank Herbert|Science fiction|Book");
        tempWriter.WriteLine("Ender's Game|Orson Scott Card|Science fiction|Book");
        tempWriter.WriteLine("The Hobbit|J. R. R. Tolkien|Fantasy|Book");
        tempWriter.WriteLine("The Lion, the Witch and the Wardrobe|C. S. Lewis|Fantasy|Book");
        tempWriter.WriteLine("Spider - Man: Miles Morales|Sony|Adventure|Game");
        tempWriter.WriteLine("NBA 2K23|2K Games|Sports|Game");
        tempWriter.WriteLine("Call of Duty MW2|Treyarch|FPS|Game");
        tempWriter.WriteLine("Knockout City|Velan Studios|Sports|Game");
        tempWriter.WriteLine("Fall Guys|Epic Games|Adventure|Game");
        tempWriter.WriteLine("Fahrenheit 451|CBJE Productions|Mayhem|Game");
        tempWriter.WriteLine("Gotham Knights|Warner Bros|Adventure|Game");
        tempWriter.WriteLine("PubG|PubG Studios|Battle Royale|Game");
        tempWriter.WriteLine("Mario Kart 8|Nintendo|Racing|Game");
        tempWriter.WriteLine("Brawlhalla|Ubisoft|Fighting|Game");
        tempWriter.WriteLine("Rocket League|Psyonix|Sports|Game");
        tempWriter.WriteLine("Apex Legends|EA|Battle Royale|Game");

    tempWriter.Close();
    }

    List<Book> booklist = new List<Book> { };
    
    //Collecting all students in the file
    StreamReader reader = new StreamReader(filePath);
    while (true)
    {
        string line = reader.ReadLine();
        if (line == null)//if line is empty
        {
            break;
        }
        else//if line is not empty
        {
            string[] parts = line.Split("|");
            //parts[0] Title
            //parts[1] Author
            //parts[2] Genre
            Book book;
            if (parts.Length <= 4)
            {
                book = new Book(parts[0], parts[1], parts[2], parts[3]);
            }
            else
            {
                DateOnly? duedate = (parts[5].Length < 1) ? null : DateOnly.Parse(parts[5]);
                book = new Book(parts[0], parts[1], parts[2], parts[3],bool.Parse(parts[4]), duedate);
            }
            booklist.Add(book);
        }
    }
    reader.Close();//always CLOSE when done
    return booklist;
}

static void TextWriter(List<Book>booklist)
{
    string filePath = "../../../LibraryList.txt";
    StreamWriter writer = new StreamWriter(filePath);
    foreach (Book book in booklist)
    {
        writer.WriteLine($"{book.Title}|{book.Author}|{book.Genre}|{book.mediaType}|{book.Available}|{book.DueDate}");
    }
    writer.Close();//always CLOSE
}

static void TorchIt()
{
    Console.Clear();
    Console.WriteLine("YOU HAVE BURNED DOWN THE LIBRARY....Love, CBJE Productions");
    Console.ReadLine();
    //Environment.Exit(0);
}


Console.ReadLine();