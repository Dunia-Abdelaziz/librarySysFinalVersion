using DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.BorrowerRep
{
    public class MongoBorrowerRepository : IBorrowerRepository
    {
        private readonly IMongoCollection<Borrower> _collection;
        private readonly string _databaseName;


        public MongoBorrowerRepository(IMongoCollection<Borrower> collection)
        {
            _collection = collection;
        }

        public Borrower GetById(ObjectId id)
        {
            return _collection.Find(borrower => borrower.Id == id).FirstOrDefault();
        }

        public IEnumerable<Borrower> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public void Add(Borrower borrower)
        {
            _collection.InsertOne(borrower);
        }

        public void Update(Borrower borrower)
        {
            _collection.ReplaceOne(b => b.Id == borrower.Id, borrower);
        }

        public void Delete(ObjectId id)
        {
            _collection.DeleteOne(book => book.Id == id);
        }

        public Borrower LogIn(string username, string password)
        {

            var filter = Builders<Borrower>.Filter.And(
                Builders<Borrower>.Filter.Eq("UserName", username),
                Builders<Borrower>.Filter.Eq("Password", password)
            );
            return _collection.Find(filter).First();

        }
        public Borrower SearchByUserName(string username)
        {
            var filter = Builders<Borrower>.Filter.Eq("UserName", username);
            return _collection.Find(filter).First();

        }
    }
}
