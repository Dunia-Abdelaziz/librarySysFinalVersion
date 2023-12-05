using DAL.Entities;
using DAL.Repository.BookRep;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
    public class MongoRepositoryFactory
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
            return new MongoBookRepository(collection, _database.DatabaseNamespace.DatabaseName);
        }


    }
}
