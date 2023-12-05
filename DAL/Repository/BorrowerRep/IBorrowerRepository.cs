using DAL.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.BorrowerRep
{
    public interface IBorrowerRepository
    {
        Borrower GetById(ObjectId id);
        IEnumerable<Borrower> GetAll();
        void Add(Borrower borrower);
        void Update(Borrower borrower);
        void Delete(ObjectId id);
        Borrower LogIn(string username, string password);
        Borrower SearchByUserName(string username);
    }
}
