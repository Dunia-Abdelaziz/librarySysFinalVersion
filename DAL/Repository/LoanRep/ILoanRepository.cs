using DAL.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DAL.Repository.LoanRep
{
    public interface ILoanRepository
    {
        Loan GetById(ObjectId id);
        IEnumerable<Loan> GetAll();
        void Add(Loan loan);
        void Update(Loan loan);
        void Delete(ObjectId id);
        Loan GetByBorrowerAndBook(Borrower borrowers, Book books);
    }
}
