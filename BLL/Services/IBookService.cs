using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IBookService
    {
        BookDTO GetBookById(string id);
        IEnumerable<BookDTO> GetAllBooks();
        void AddBook(BookDTO bookDto);
        void UpdateBook(string id, BookDTO bookDto);
        void DeleteBook(string id);
    }
}
