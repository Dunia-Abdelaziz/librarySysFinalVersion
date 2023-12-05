using DAL.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.LibrarianRep
{
    public interface ILibrarianRepository
    {
        Librarian GetById(ObjectId id);
        IEnumerable<Librarian> GetAll();
        void Add(Librarian librarian);
        void Update(Librarian librarian);
        void Delete(ObjectId id);
        Librarian logIn(string username, string password);
    }
}
