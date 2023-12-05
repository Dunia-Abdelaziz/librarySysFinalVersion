using DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.LoanRep
{
    public class MongoLoanRepository : ILoanRepository
    {
        private readonly IMongoCollection<Loan> _collection;
        private readonly string _databaseName;
        public MongoLoanRepository(IMongoCollection<Loan> collection, string databaseName)
        {
            _collection = collection;
            _databaseName = databaseName;
        }

        public void Add(Loan loan)
        {
            _collection.InsertOne(loan);
        }

        public void Delete(ObjectId id)
        {
            _collection.DeleteOne(loan => loan.Id == id);
        }

        public IEnumerable<Loan> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }

        public Loan GetByBorrowerAndBook(Borrower borrowers, Book books)
        {
            var filter = Builders<Loan>.Filter.Eq(x => x.BorrowerId, borrowers.Id) &
              Builders<Loan>.Filter.Eq(x => x.BookId, books.Id);

            return _collection.Find(filter).FirstOrDefault();
        }

        public Loan GetById(ObjectId id)
        {
            return _collection.Find(loan => loan.Id == id).FirstOrDefault();
        }

        public void Update(Loan loan)
        {
            _collection.ReplaceOne(b => b.Id == loan.Id, loan);
        }
    }
}
