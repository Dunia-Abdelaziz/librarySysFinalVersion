using DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.BookRep
{
    public class MongoBookRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _collection;
        private readonly string _databaseName;


        public MongoBookRepository(IMongoCollection<Book> collection, string databaseName)
        {
            _collection = collection;
            _databaseName = databaseName;
        }

        public Book GetById(ObjectId id)
        {
            return _collection.Find(book => book.Id == id).FirstOrDefault();
        }

        public IEnumerable<Book> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public void Add(Book book)
        {
            _collection.InsertOne(book);
        }

        public void Update(Book book)
        {
            _collection.ReplaceOne(b => b.Id == book.Id, book);
        }

        public void Delete(ObjectId id)
        {
            _collection.DeleteOne(book => book.Id == id);
        }

        public Book GetByTitle(string bookName)
        {
            return _collection.Find(book => book.Title == bookName).FirstOrDefault();
        }
    }
}
