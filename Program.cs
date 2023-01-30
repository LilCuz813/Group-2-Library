﻿using Group_2_Library;
using Microsoft.VisualBasic;
using static System.Reflection.Metadata.BlobBuilder;
using Validation;
using System.ComponentModel;

//Book list
List<Book> ourBooks = TextReader();

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
            //list all book info
            PrintHeader();
            foreach (Book b in ourBooks.OrderBy(b => b.Title))
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
                    Console.Clear();
                    PrintHeader();

                    //display search results
                    foreach (Book b in ourBooks.OrderBy(b => b.Author).Where(b => b.Author.ToLower().Contains(searchAuthor)))
                    {
                        Console.WriteLine(b.GetDetails());
                    }
                    Console.WriteLine();

                    //targeting book choice + checkout

                    int selectedBook = BookSelection(ourBooks);
                    bag.Add(ourBooks[selectedBook]);
                    ourBooks[selectedBook].UpdateDueDate();

                    stillSearching = false;
                    stillWorking = false;
                }
                else if (searchType == "k")
                {
                    //search by keyword
                    Console.Write("Please enter title keyword: ");
                    string searchKeyword = Console.ReadLine().Trim().ToLower();
                    Console.Clear();
                    PrintHeader();
                    foreach (Book b in ourBooks.OrderBy(b => b.Title).Where(b => b.Title.ToLower().Contains(searchKeyword)))
                    {
                        Console.WriteLine(b.GetDetails());
                    }
                    Console.WriteLine();

                    //targeting book choice + checkout

                    int selectedBook = BookSelection(ourBooks);
                    bag.Add(ourBooks[selectedBook]);
                    ourBooks[selectedBook].UpdateDueDate();

                    stillSearching = false;
                    stillWorking = false;
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
            Console.WriteLine("Invalid input, please try again.");
        }
    }
// Asking to continue
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

//Return books
while (true)
{
    Console.WriteLine("Do you have books to return? Y/N");
    string returnOption = Console.ReadLine();

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
                //bag.Remove(bag[returnIndex]);
                Console.WriteLine($"You have return {ourBooks[returnIndex].Title}");
                
            }
            else
            {
                Console.WriteLine("Unable to return.");
            }
        }  
        catch(ArgumentOutOfRangeException) 
        {
            Console.WriteLine("Book was not found checked out.");
        }
        
        break;
    }
    else if (returnOption == "n")
    {
        Console.WriteLine("not returning");
        break;
    }
    else
    {
        Console.WriteLine("Wrong input. Returning Y/N");
    }
}

Console.WriteLine("These are the books you are checking out:\n");
PrintHeader();

foreach (Book b in bag.Where(b => b.Available == false))
{
    Console.WriteLine(b.GetDetails());
}

Console.WriteLine("\nGoodbye.");
Console.ReadLine();

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
    Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-20} {3, -15} {4, -15}", $"Title", $"Author", $"Genre", $"Available", $"DueDate"));
    Console.WriteLine(String.Format("{0,-40} {1,-25} {2,-20} {3, -15} {4, -15}", "========", "========", "========", "========", "========"));
}


static List<Book> TextReader()
{
    string filePath = "../../../LibraryList.txt";

    //StreamWriter writer = new StreamWriter(filePath);

    if (File.Exists(filePath) == false)
    {
        StreamWriter tempWriter = new StreamWriter(filePath);
        tempWriter.WriteLine("The Catcher in the Rye|J. D. Salinger|Fiction");
        tempWriter.WriteLine("To Kill a Mockingbird|Harper Lee|Fiction");
        tempWriter.WriteLine("Great Expectations|Charles Dickens|Fiction");
        tempWriter.WriteLine("The Great Gatsby|F. Scott Fitzgerald|Fiction");
        tempWriter.WriteLine("Murder on the Orient Express|Agatha Christie|Mystery");
        tempWriter.WriteLine("The Hound of the Baskervilles|Arthur Conan Doyle|Mystery");
        tempWriter.WriteLine("Gone with the Wind|Margaret Mitchell|Romance");
        tempWriter.WriteLine("Jane Eyre|Jane Austen|Romance");
        tempWriter.WriteLine("Dune|Frank Herbert|Science fiction");
        tempWriter.WriteLine("Ender's Game|Orson Scott Card|Science fiction");
        tempWriter.WriteLine("The Hobbit|J. R. R. Tolkien|Fantasy");
        tempWriter.WriteLine("The Lion, the Witch and the Wardrobe|C. S. Lewis|Fantasy");
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
            if (parts.Length <= 3)
            {
                book = new Book(parts[0], parts[1], parts[2]);
            }
            else
            {
                DateOnly? duedate = (parts[4].Length < 1) ? null : DateOnly.Parse(parts[4]);
                book = new Book(parts[0], parts[1], parts[2], bool.Parse(parts[3]), duedate);
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
        writer.WriteLine($"{book.Title}|{book.Author}|{book.Genre}|{book.Available}|{book.DueDate}");
    }
    writer.Close();//always CLOSE
}

