using BLL.DTOs;
using BLL.Services.BorrowerServices;
using BLL.Services.LibrarianServices;
using BLL.Services.WeatherService;
using BLL.Services;
using BLL.weaterFactory;
using DAL.Factory;
using DAL.Repository.BookRep;
using DAL.Repository.BorrowerRep;
using DAL.Repository.LibrarianRep;
using DAL.Repository.LoanRep;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarySys
{
    public class PresntationLayarServices 
    {
        private readonly IBookService _bookService;
        private readonly IBorrowerServices _borrowerServices;
        private readonly ILibrarianService _librarianService;
        private readonly IWeatherService _weatherService;

        public PresntationLayarServices(IBookService bookService, IBorrowerServices borrowerServices, ILibrarianService librarianService, IWeatherService weatherService)
        {
            _bookService = bookService;
            _borrowerServices = borrowerServices;
            _librarianService = librarianService;
            _weatherService = weatherService;
        }
        public async Task RunAsync()
        {
            string cityName = "London";
            try
            {
                // Use the weather service to fetch and process weather data
                var weatherDTO = await _weatherService.GetWeatherDataAsync("London");

                // Display the city name and temperature on the console
                Console.WriteLine($"City: {weatherDTO.CityName}");
                Console.WriteLine($"Temperature: {weatherDTO.TemperatureCelsius}°C\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Console.WriteLine("Library System Console App");
            Console.WriteLine("1. book");
            Console.WriteLine("2. borrower");
            Console.WriteLine("3. Exit");

            while (true)
            {
                Console.Write("Enter your choice (1-3): ");
                var entity = Console.ReadLine();
                if (entity == "1")
                {
                    // Run the application
                    BookRun();
                }
                else if (entity == "2")
                {
                    BorrowerRun();
                }

            }
        }

        private void LibrarianRun(BorrowerDTO b)
        {
            Console.WriteLine("Library System <Librarian>");
            Console.WriteLine("1. SignIn");
            Console.WriteLine("2. SignUp");
            Console.WriteLine("3. Exit");
            while (true)
            {
                Console.Write("Enter your choice (1-3): ");
                var LibrarianChoice = Console.ReadLine();
                switch (LibrarianChoice)
                {
                    case "1":
                        LibrarianSignIn(b);
                        break;
                    case "2":
                        AddNewLibrarian();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                        break;
                }
            }

        }

        private void AddNewLibrarian()
        {
            Console.Write("Enter Name: ");
            var name = Console.ReadLine();

            Console.Write("Enter UserName: ");
            var username = Console.ReadLine();

            Console.Write("Enter Password: ");
            var password = Console.ReadLine();

            var LibrarianDto = new LibrarianDTO { Name = name, UserName = username, Password = password };
            _librarianService.AddLibrarian(LibrarianDto);

            Console.WriteLine("librarian added successfully.");
        }

        private void LibrarianSignIn(BorrowerDTO b)
        {
            Console.Write("Enter UserName: ");
            var username = Console.ReadLine();

            Console.Write("Enter Password: ");
            var password = Console.ReadLine();

            var l = _librarianService.LogIn(username, password);
            if (l == null)
            {
                Console.WriteLine("user name or password is incorrect");
            }
            else
            {
                Console.WriteLine("Borrower added successfully.");
            }

            Console.WriteLine("now you can acces to books");
            Console.WriteLine("1. Return Book");
            Console.WriteLine("2. Borrow Book");
            Console.WriteLine("3. Exit");
            while (true)
            {
                Console.Write("Enter your choice (1-3): ");
                var LibrarianChoice = Console.ReadLine();
                switch (LibrarianChoice)
                {
                    case "1":
                        RetunBook(b);
                        break;
                    case "2":
                        BorrowBook(b);
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                        break;
                }
            }

        }

        private void BorrowBook(BorrowerDTO b)
        {
            ViewAllBooks();
            Console.WriteLine("enter title of book");
            string title = Console.ReadLine();
            if (_librarianService.BorrowBook(title, b.UserName))
            {
                Console.WriteLine("succesfull borrow");
            }
            else
            {
                Console.WriteLine("I am sorry but this book has been borrowed");
            }
        }

        private void RetunBook(BorrowerDTO b)
        {

            Console.WriteLine("enter title of book");
            string title = Console.ReadLine();

            if (_librarianService.ReturnBook(title, b.UserName))
            {
                Console.WriteLine("succesfull return");
            }
            else
            {
                Console.WriteLine("please insure about book name");
            }

        }

        void BorrowerRun()
        {
            Console.WriteLine("Library System <Borrower>");
            Console.WriteLine("1. SignIn");
            Console.WriteLine("2. SignUp");
            Console.WriteLine("3. Exit");
            while (true)
            {
                Console.Write("Enter your choice (1-3): ");
                var BorrowerChoice = Console.ReadLine();
                switch (BorrowerChoice)
                {
                    case "1":
                        BorrowerSignIn();
                        break;
                    case "2":
                        AddNewBorrower();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
                        break;
                }
            }

        }

        void AddNewBorrower()
        {
            Console.Write("Enter Name: ");
            var name = Console.ReadLine();

            Console.Write("Enter UserName: ");
            var username = Console.ReadLine();

            Console.Write("Enter Password: ");
            var password = Console.ReadLine();

            var BorrowerDto = new BorrowerDTO { Name = name, UserName = username, Password = password };
            _borrowerServices.AddBorrower(BorrowerDto);

            Console.WriteLine("Borrower added successfully.");
        }

        void BorrowerSignIn()
        {
            Console.Write("Enter UserName: ");
            var username = Console.ReadLine();

            Console.Write("Enter Password: ");
            var password = Console.ReadLine();

            var b = _borrowerServices.LogIn(username, password);
            if (b == null)
            {
                Console.WriteLine("user name or password is incorrect");

            }
            else
            {
                Console.WriteLine("Borrower added successfully.");
                LibrarianRun(b);
            }


        }


        void BookRun()
        {
            Console.WriteLine("Library System <Books>");
            Console.WriteLine("1. View All Books");
            Console.WriteLine("2. Add New Book");
            Console.WriteLine("3. Update Book");
            Console.WriteLine("4. Delete Book");
            Console.WriteLine("5. Exit");

            while (true)
            {
                Console.Write("Enter your choice (1-5): ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewAllBooks();
                        break;
                    case "2":
                        AddNewBook();
                        break;
                    case "3":
                        UpdateBook();
                        break;
                    case "4":
                        DeleteBook();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }
        }

        void ViewAllBooks()
        {
            var books = _bookService.GetAllBooks();

            Console.WriteLine("All Books:");
            foreach (var book in books)
            {
                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}");
            }
            Console.WriteLine();
        }

        void AddNewBook()
        {
            Console.Write("Enter Title: ");
            var title = Console.ReadLine();

            Console.Write("Enter Author: ");
            var author = Console.ReadLine();

            var bookDto = new BookDTO { Title = title, Author = author };
            _bookService.AddBook(bookDto);

            Console.WriteLine("Book added successfully.");
        }

        void UpdateBook()
        {
            Console.Write("Enter Book Id to Update: ");
            var id = Console.ReadLine();

            Console.Write("Enter New Title: ");
            var title = Console.ReadLine();

            Console.Write("Enter New Author: ");
            var author = Console.ReadLine();

            var bookDto = new BookDTO { Title = title, Author = author };
            _bookService.UpdateBook(id, bookDto);

            Console.WriteLine("Book updated successfully.");
        }

        void DeleteBook()
        {
            Console.Write("Enter Book Id to Delete: ");
            var id = Console.ReadLine();

            _bookService.DeleteBook(id);

            Console.WriteLine("Book deleted successfully.");
        }
    }

}


//using BLL.DTOs;
//using BLL.Services;
//using BLL.Services.BorrowerServices;
//using DAL.Factory;
//using DAL.Repository.BookRep;
//using DAL.Repository.BorrowerRep;
//using System;
//using Microsoft.Extensions.DependencyInjection;
//using BLL.Services.LibrarianServices;
//using DAL.Repository.LibrarianRep;
//using DAL.Repository.LoanRep;
//using BLL.Services.WeatherService;
//using BLL.weaterFactory;
//using LibrarySystem;

//namespace LibrarySystem
//{

//    class Program
//    {
//        private static IBookService _bookService;
//        private static IBorrowerServices _borrowerServices;
//        private static ILibrarianService _librarianService;
//        private static IWeatherService _weatherService;
//        public Program(IBookService bookService, IBorrowerServices borrowerServices, ILibrarianService librarianService, IWeatherService weatherService)
//        {
//            _bookService = bookService;
//            _borrowerServices = borrowerServices;
//            _librarianService = librarianService;
//            _weatherService = weatherService;
//        }

//        static async Task Main()
//        {
//            var connectionString = @"mongodb://localhost:27017/";
//            var databaseName = "library";
//            // Replace "YOUR_API_KEY" with your actual OpenWeatherMap API key
//            string apiKey = "958a0cca7d6e90effe76435c6f3c35d7";
//            // Replace "London" with the desired city name
//            string cityName = "London";

//            var serviceProvider = new ServiceCollection() // Replace YourMapperImplementation with your actual implementation
//                .AddScoped<MongoRepositoryFactory>(provider => new MongoRepositoryFactory(connectionString, databaseName))
//                .AddScoped<IBookRepository>(provider => provider.GetService<MongoRepositoryFactory>().CreateBookRepository())
//                .AddScoped<IBorrowerRepository>(provider => provider.GetService<MongoRepositoryFactory>().CreateBorrowerRepository())
//                .AddScoped<ILibrarianRepository>(provider => provider.GetService<MongoRepositoryFactory>().CreateLibrarianRepository())
//                .AddScoped<ILoanRepository>(provider => provider.GetService<MongoRepositoryFactory>().CreateLoanRepository())
//                .AddScoped<IBookService, BookService>()
//                .AddScoped<IBorrowerServices, BorrowerServices>()
//                .AddScoped<ILibrarianService, LibrarianService>()
//                .AddHttpClient()
//                    .AddSingleton<IWeatherServiceFactory, OpenWeatherMapServiceFactory>(provider =>
//                    {
//                        var httpClient = provider.GetRequiredService<HttpClient>();
//                        return new OpenWeatherMapServiceFactory(httpClient, apiKey);
//                    })
//                    .AddSingleton<IWeatherService>(provider =>
//                    {
//                        var factory = provider.GetRequiredService<IWeatherServiceFactory>();
//                        return factory.CreateWeatherService();
//                    })
//                    .BuildServiceProvider();

//            // Resolve the book service and borrower service from the container
//            _weatherService = serviceProvider.GetRequiredService<IWeatherService>();
//            _bookService = serviceProvider.GetService<IBookService>();
//            _borrowerServices = serviceProvider.GetService<IBorrowerServices>();
//            _librarianService = serviceProvider.GetService<ILibrarianService>();

//            var program = new Program(_bookService, _borrowerServices, _librarianService, _weatherService);
//            try
//            {
//                // Use the weather service to fetch and process weather data
//                var weatherDTO = await _weatherService.GetWeatherDataAsync(cityName);

//                // Display the city name and temperature on the console
//                Console.WriteLine($"City: {weatherDTO.CityName}");
//                Console.WriteLine($"Temperature: {weatherDTO.TemperatureCelsius}°C\n\n");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An error occurred: {ex.Message}");
//            }
//            Console.WriteLine("Library System Console App");
//            Console.WriteLine("1. book");
//            Console.WriteLine("2. borrower");
//            Console.WriteLine("3. Exit");

//            while (true)
//            {
//                Console.Write("Enter your choice (1-3): ");
//                var entity = Console.ReadLine();
//                if (entity == "1")
//                {
//                    // Run the application
//                    program.BookRun();
//                }
//                else if (entity == "2")
//                {
//                    program.BorrowerRun();
//                }

//            }
//        }

//        private void LibrarianRun(BorrowerDTO b)
//        {
//            Console.WriteLine("Library System <Librarian>");
//            Console.WriteLine("1. SignIn");
//            Console.WriteLine("2. SignUp");
//            Console.WriteLine("3. Exit");
//            while (true)
//            {
//                Console.Write("Enter your choice (1-3): ");
//                var LibrarianChoice = Console.ReadLine();
//                switch (LibrarianChoice)
//                {
//                    case "1":
//                        LibrarianSignIn(b);
//                        break;
//                    case "2":
//                        AddNewLibrarian();
//                        break;
//                    case "3":
//                        Environment.Exit(0);
//                        break;
//                    default:
//                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
//                        break;
//                }
//            }

//        }

//        private void AddNewLibrarian()
//        {
//            Console.Write("Enter Name: ");
//            var name = Console.ReadLine();

//            Console.Write("Enter UserName: ");
//            var username = Console.ReadLine();

//            Console.Write("Enter Password: ");
//            var password = Console.ReadLine();

//            var LibrarianDto = new LibrarianDTO { Name = name, UserName = username, Password = password };
//            _librarianService.AddLibrarian(LibrarianDto);

//            Console.WriteLine("librarian added successfully.");
//        }

//        private void LibrarianSignIn(BorrowerDTO b)
//        {
//            Console.Write("Enter UserName: ");
//            var username = Console.ReadLine();

//            Console.Write("Enter Password: ");
//            var password = Console.ReadLine();

//            var l = _librarianService.LogIn(username, password);
//            if (l == null)
//            {
//                Console.WriteLine("user name or password is incorrect");
//            }
//            else
//            {
//                Console.WriteLine("Borrower added successfully.");
//            }

//            Console.WriteLine("now you can acces to books");
//            Console.WriteLine("1. Return Book");
//            Console.WriteLine("2. Borrow Book");
//            Console.WriteLine("3. Exit");
//            while (true)
//            {
//                Console.Write("Enter your choice (1-3): ");
//                var LibrarianChoice = Console.ReadLine();
//                switch (LibrarianChoice)
//                {
//                    case "1":
//                        RetunBook(b);
//                        break;
//                    case "2":
//                        BorrowBook(b);
//                        break;
//                    case "3":
//                        Environment.Exit(0);
//                        break;
//                    default:
//                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
//                        break;
//                }
//            }

//        }

//        private void BorrowBook(BorrowerDTO b)
//        {
//            ViewAllBooks();
//            Console.WriteLine("enter title of book");
//            string title = Console.ReadLine();
//            if (_librarianService.BorrowBook(title, b.UserName))
//            {
//                Console.WriteLine("succesfull borrow");
//            }
//            else
//            {
//                Console.WriteLine("I am sorry but this book has been borrowed");
//            }
//        }

//        private void RetunBook(BorrowerDTO b)
//        {

//            Console.WriteLine("enter title of book");
//            string title = Console.ReadLine();

//            if (_librarianService.ReturnBook(title, b.UserName))
//            {
//                Console.WriteLine("succesfull return");
//            }
//            else
//            {
//                Console.WriteLine("please insure about book name");
//            }

//        }

//        void BorrowerRun()
//        {
//            Console.WriteLine("Library System <Borrower>");
//            Console.WriteLine("1. SignIn");
//            Console.WriteLine("2. SignUp");
//            Console.WriteLine("3. Exit");
//            while (true)
//            {
//                Console.Write("Enter your choice (1-3): ");
//                var BorrowerChoice = Console.ReadLine();
//                switch (BorrowerChoice)
//                {
//                    case "1":
//                        BorrowerSignIn();
//                        break;
//                    case "2":
//                        AddNewBorrower();
//                        break;
//                    case "3":
//                        Environment.Exit(0);
//                        break;
//                    default:
//                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 3.");
//                        break;
//                }
//            }

//        }

//        void AddNewBorrower()
//        {
//            Console.Write("Enter Name: ");
//            var name = Console.ReadLine();

//            Console.Write("Enter UserName: ");
//            var username = Console.ReadLine();

//            Console.Write("Enter Password: ");
//            var password = Console.ReadLine();

//            var BorrowerDto = new BorrowerDTO { Name = name, UserName = username, Password = password };
//            _borrowerServices.AddBorrower(BorrowerDto);

//            Console.WriteLine("Borrower added successfully.");
//        }

//        void BorrowerSignIn()
//        {
//            Console.Write("Enter UserName: ");
//            var username = Console.ReadLine();

//            Console.Write("Enter Password: ");
//            var password = Console.ReadLine();

//            var b = _borrowerServices.LogIn(username, password);
//            if (b == null)
//            {
//                Console.WriteLine("user name or password is incorrect");

//            }
//            else
//            {
//                Console.WriteLine("Borrower added successfully.");
//                LibrarianRun(b);
//            }


//        }


//        void BookRun()
//        {
//            Console.WriteLine("Library System <Books>");
//            Console.WriteLine("1. View All Books");
//            Console.WriteLine("2. Add New Book");
//            Console.WriteLine("3. Update Book");
//            Console.WriteLine("4. Delete Book");
//            Console.WriteLine("5. Exit");

//            while (true)
//            {
//                Console.Write("Enter your choice (1-5): ");
//                var choice = Console.ReadLine();

//                switch (choice)
//                {
//                    case "1":
//                        ViewAllBooks();
//                        break;
//                    case "2":
//                        AddNewBook();
//                        break;
//                    case "3":
//                        UpdateBook();
//                        break;
//                    case "4":
//                        DeleteBook();
//                        break;
//                    case "5":
//                        Environment.Exit(0);
//                        break;
//                    default:
//                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
//                        break;
//                }
//            }
//        }

//        void ViewAllBooks()
//        {
//            var books = _bookService.GetAllBooks();

//            Console.WriteLine("All Books:");
//            foreach (var book in books)
//            {
//                Console.WriteLine($"Title: {book.Title}, Author: {book.Author}");
//            }
//            Console.WriteLine();
//        }

//        void AddNewBook()
//        {
//            Console.Write("Enter Title: ");
//            var title = Console.ReadLine();

//            Console.Write("Enter Author: ");
//            var author = Console.ReadLine();

//            var bookDto = new BookDTO { Title = title, Author = author };
//            _bookService.AddBook(bookDto);

//            Console.WriteLine("Book added successfully.");
//        }

//        void UpdateBook()
//        {
//            Console.Write("Enter Book Id to Update: ");
//            var id = Console.ReadLine();

//            Console.Write("Enter New Title: ");
//            var title = Console.ReadLine();

//            Console.Write("Enter New Author: ");
//            var author = Console.ReadLine();

//            var bookDto = new BookDTO { Title = title, Author = author };
//            _bookService.UpdateBook(id, bookDto);

//            Console.WriteLine("Book updated successfully.");
//        }

//        void DeleteBook()
//        {
//            Console.Write("Enter Book Id to Delete: ");
//            var id = Console.ReadLine();

//            _bookService.DeleteBook(id);

//            Console.WriteLine("Book deleted successfully.");
//        }
//    }

//}
