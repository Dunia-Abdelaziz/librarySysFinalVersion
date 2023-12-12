using DAL.Entities;
using DAL.Repository.BookRep;
using DAL.Repository.BorrowerRep;
using DAL.Repository.LibrarianRep;
using DAL.Repository.LoanRep;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
    public class MongoRepositoryFactory : IMongoRepositoryFactory
    {
        private readonly IMongoDatabase _database;

        public MongoRepositoryFactory(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IBookRepository CreateBookRepository()
        {
            var collection = _database.GetCollection<Book>("Books");
            return new MongoBookRepository(collection);
        }

        public IBorrowerRepository CreateBorrowerRepository()
        {
            var collection = _database.GetCollection<Borrower>("Borrowers");
            return new MongoBorrowerRepository(collection);
        }
        public ILibrarianRepository CreateLibrarianRepository()
        {
            var collection = _database.GetCollection<Librarian>("Librarians");
            return new MongoLibrarianRepsitory(collection);
        }
        public ILoanRepository CreateLoanRepository()
        {
            var collection = _database.GetCollection<Loan>("Loans");
            return new MongoLoanRepository(collection);
        }

    }
}
