using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.LibrarianServices
{
    public interface ILibrarianService
    {
        void AddLibrarian(LibrarianDTO user); 
        LibrarianDTO? LogIn(string username, string password);
        bool BorrowBook(string bookName, string borrowerName);
        bool ReturnBook(string bookName, string borrowerName);
    }
}
