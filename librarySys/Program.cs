using BLL.DTOs;
using BLL.Services;
using DAL.Factory;
using DAL.Repository.BookRep;

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

        // Constructor for Dependency Injection
        public Program(IBookService bookService)
        {
            _bookService = bookService;
        }

        static void Main()
        {
            var connectionString = @"mongodb://library1:LRkOeXBkvRuropqJMFDk8N39lUmYdLctDtk2Y90JBMaWEqSBxpBlgwUiAp5iLX6iP8Qiwe27s7O0ACDbHnpnNw==@library1.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@library1@";
            var databaseName = "library";


            var mongoRepositoryFactory = new MongoRepositoryFactory(connectionString, databaseName);

            // Use the factory to create the book repository
            var bookRepository = mongoRepositoryFactory.CreateBookRepository();

            // Register instances in the DI container (if needed)
            DIContainer.RegisterInstance<IBookRepository>(bookRepository);
            DIContainer.RegisterInstance<IBookService>(new BookService(bookRepository));

            // Create an instance of the Program class (or use dependency injection)
            var program = new Program(DIContainer.Resolve<IBookService>());

            // Run the application
            program.Run();

        }

        void Run()
        {
            Console.WriteLine("Library System Console App");
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