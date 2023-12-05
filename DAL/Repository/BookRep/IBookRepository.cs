using DAL.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.BookRep
{
    public interface IBookRepository
    {
        Book GetById(ObjectId id);
        IEnumerable<Book> GetAll();
        void Add(Book book);
        void Update(Book book);
        void Delete(ObjectId id);
    }
}
