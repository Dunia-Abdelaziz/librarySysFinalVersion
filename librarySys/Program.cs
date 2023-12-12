using BLL.DTOs;
using BLL.Services;
using BLL.Services.BorrowerServices;
using DAL.Factory;
using DAL.Repository.BookRep;
using DAL.Repository.BorrowerRep;
using System;
using Microsoft.Extensions.DependencyInjection;
using BLL.Services.LibrarianServices;
using DAL.Repository.LibrarianRep;
using DAL.Repository.LoanRep;
using BLL.Services.WeatherService;
using BLL.weaterFactory;
using System.Net;
using Microsoft.Extensions.Hosting;
using librarySys;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ThreeTierLibraryMamagmentSystem
{
    class Program
    {
        private static IBookService _bookService;
        private static IBorrowerServices _borrowerServices;
        private static ILibrarianService _librarianService;
        private static IWeatherService _weatherService;

        static async Task Main(string[] args)
        {
            var connectionString = @"mongodb://localhost:27017/";
            var databaseName = "library";

            var serviceProvider = new ServiceCollection()
                .AddScoped<IMongoRepositoryFactory>(provider => new MongoRepositoryFactory(connectionString, databaseName))
                .AddScoped<IBookRepository>(provider => provider.GetRequiredService<IMongoRepositoryFactory>().CreateBookRepository())
                .AddScoped<IBorrowerRepository>(provider => provider.GetRequiredService<IMongoRepositoryFactory>().CreateBorrowerRepository())
                .AddScoped<ILibrarianRepository>(provider => provider.GetRequiredService<IMongoRepositoryFactory>().CreateLibrarianRepository())
                .AddScoped<ILoanRepository>(provider => provider.GetRequiredService<IMongoRepositoryFactory>().CreateLoanRepository())
                .AddScoped<IBookService, BookService>()
                .AddScoped<IBorrowerServices, BorrowerServices>()
                .AddScoped<ILibrarianService, LibrarianService>()
                .AddHttpClient()
                .AddSingleton<IWeatherServiceFactory, OpenWeatherMapServiceFactory>(provider =>
                {
                    var httpClient = provider.GetRequiredService<HttpClient>();
                    return new OpenWeatherMapServiceFactory(httpClient, "958a0cca7d6e90effe76435c6f3c35d7");
                })
                .AddSingleton<IWeatherService>(provider =>
                {
                    var factory = provider.GetRequiredService<IWeatherServiceFactory>();
                    return factory.CreateWeatherService();
                })
                    .BuildServiceProvider();

            _weatherService = serviceProvider.GetRequiredService<IWeatherService>();
            _bookService = serviceProvider.GetRequiredService<IBookService>();
            _borrowerServices = serviceProvider.GetRequiredService<IBorrowerServices>();
            _librarianService = serviceProvider.GetRequiredService<ILibrarianService>();

            // Create an instance of the RepresentationLayer and pass the services
            var representationLayer = new PresntationLayarServices(_bookService, _borrowerServices, _librarianService, _weatherService);

            // Run the client/user logic
            await representationLayer.RunAsync();
        }
    }
}
