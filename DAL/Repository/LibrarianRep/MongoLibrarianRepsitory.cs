using DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.LibrarianRep
{
    public class MongoLibrarianRepsitory : ILibrarianRepository
    {
        private readonly IMongoCollection<Librarian> _collection;
        private readonly string _databaseName;
        public MongoLibrarianRepsitory(IMongoCollection<Librarian> collection, string databaseName)
        {
            _collection = collection;
            _databaseName = databaseName;
        }

        public Librarian GetById(ObjectId id)
        {
            return _collection.Find(librarian => librarian.Id == id).FirstOrDefault();

        }
        public void Add(Librarian librarian)
        {
            _collection.InsertOne(librarian);

        }

        public void Delete(ObjectId id)
        {
            _collection.DeleteOne(Librarian => Librarian.Id == id);

        }

        public IEnumerable<Librarian> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public Librarian logIn(string username, string password)
        {
            var filter = Builders<Librarian>.Filter.And(
                Builders<Librarian>.Filter.Eq("UserName", username),
                Builders<Librarian>.Filter.Eq("Password", password)
            );

            return _collection.Find(filter).First();
        }

        public void Update(Librarian librarian)
        {
            _collection.ReplaceOne(l => l.Id == librarian.Id, librarian);

        }
    }

}
