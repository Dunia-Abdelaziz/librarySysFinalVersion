using BLL.DTOs;
using BLL.Services;
using BLL.Services.BorrowerServices;
using DAL.Factory;
using DAL.Repository.BookRep;
using DAL.Repository.BorrowerRep;
using System;
using Microsoft.Extensions.DependencyInjection;
using BLL.Services.BookServices;

namespace LibrarySystem
{
    public static class DIContainer
    {
        private static readonly Dictionary<Type, object> _container = new Dictionary<Type, object>();

        public static void RegisterInstance<TInterface>(TInterface instance)
        {
            _container[typeof(TInterface)] = instance;
        }

        public static TInterface Resolve<TInterface>()
        {
            return (TInterface)_container[typeof(TInterface)];
        }
    }


    class Program
    {
        private static IBookService _bookService;
        private static IBorrowerServices _borrowerServices;
        public Program(IBookService bookService, IBorrowerServices borrowerServices)
        {
            _bookService = bookService;
            _borrowerServices = borrowerServices;
        }

        static void Main()
        {
            var connectionString = @"mongodb://localhost:27017/";
            var databaseName = "library";
            var serviceProvider = new ServiceCollection() // Replace YourMapperImplementation with your actual implementation
                .AddScoped<MongoRepositoryFactory>(provider => new MongoRepositoryFactory(connectionString, databaseName))
                .AddScoped<IBookRepository>(provider => provider.GetService<MongoRepositoryFactory>().CreateBookRepository())
                .AddScoped<IBorrowerRepository>(provider => provider.GetService<MongoRepositoryFactory>().CreateBorrowerRepository())
                .AddScoped<IBookService, BookService>()
                .AddScoped<IBorrowerServices, BorrowerServices>()
                .BuildServiceProvider();

            // Resolve the book service and borrower service from the container
            _bookService = serviceProvider.GetService<IBookService>();
            _borrowerServices = serviceProvider.GetService<IBorrowerServices>();
            var program = new Program(_bookService, _borrowerServices);
            Console.WriteLine("Library System Console App");
            Console.WriteLine("1. book");
            Console.WriteLine("2. borrower");
            Console.WriteLine("3. .");
            Console.WriteLine("4. .");
            Console.WriteLine("5. Exit");

            while (true)
            {
                Console.Write("Enter your choice (1-5): ");
                var entity = Console.ReadLine();
                if (entity == "1")
                {
                    // Run the application
                    program.BookRun();
                }
                else if (entity == "2")
                {
                    program.BorrowerRun();
                }

            }
        }

        //        private static IBookService _bookService;
        //        private static IBorrowerServices _borrowerServices;


        //        // Constructor for Dependency Injection
        //        public Program(IBookService bookService)
        //        {
        //            _bookService = bookService;
        //        }

        //        public Program(IBorrowerServices borrowerServices)
        //        {
        //            _borrowerServices = borrowerServices;
        //        }
        //        public Program(IBookService bookService, IBorrowerServices borrowerServices)
        //        {
        //            _bookService = bookService;
        //            _borrowerServices = borrowerServices;
        //        }

        //        static void Main()
        //        {
        //            var connectionString = @"mongodb://localhost:27017/";
        //            var databaseName = "library";


        //            var mongoRepositoryFactory = new MongoRepositoryFactory(connectionString, databaseName);

        //            // Use the factory to create the book repository
        //            var bookRepository = mongoRepositoryFactory.CreateBookRepository();
        //            var borrowerRepository = mongoRepositoryFactory.CreateBorrowerRepository();

        //            // Register instances in the DI container (if needed)
        //            DIContainer.RegisterInstance<IBookRepository>(bookRepository);
        //            DIContainer.RegisterInstance<IBookService>(new BookService(bookRepository));

        //            DIContainer.RegisterInstance<IBorrowerRepository>(borrowerRepository);
        //            DIContainer.RegisterInstance<IBorrowerServices>(new BorrowerServices(borrowerRepository));

        //            // Create an instance of the Program class (or use dependency injection)

        //            var program = new Program(DIContainer.Resolve<IBookService>(),DIContainer.Resolve<IBorrowerServices>()
        //);


        //            Console.WriteLine("Library System Console App");
        //            Console.WriteLine("1. book");
        //            Console.WriteLine("2. borrower");
        //            Console.WriteLine("3. .");
        //            Console.WriteLine("4. .");
        //            Console.WriteLine("5. Exit");
        //            while(true)
        //            {
        //                Console.Write("Enter your choice (1-5): ");
        //                var entity = Console.ReadLine();
        //                if (entity == "1")
        //                {
        //                    // Run the application
        //                    program.BookRun();
        //                }else if (entity == "2")
        //                {
        //                    program.BorrowerRun();
        //                }

        //            }


        //        }

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
                        SignIn();
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

        void SignIn()
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