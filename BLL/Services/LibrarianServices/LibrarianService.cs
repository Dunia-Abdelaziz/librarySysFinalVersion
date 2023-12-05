using BLL.DTOs;
using DAL.Repository.BookRep;
using DAL.Repository.BorrowerRep;
using DAL.Repository.LibrarianRep;
using DAL.Repository.LoanRep;
using DAL.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.LibrarianServices
{
    public class LibrarianService : ILibrarianService
    {
        private readonly ILibrarianRepository _librarianRepository;
        private readonly IBorrowerRepository _borrowerRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;


        public LibrarianService(ILibrarianRepository librarianRepository, IBookRepository bookRepository, IBorrowerRepository borrowerRepository, ILoanRepository loanRepository)
        {
            _librarianRepository = librarianRepository;
            _bookRepository = bookRepository;
            _borrowerRepository = borrowerRepository;
            _loanRepository = loanRepository;
        }

        public void AddLibrarian(LibrarianDTO user)
        {
            Librarian Borrower = new Librarian { Name = user.Name, UserName = user.UserName, Password = user.Password };
            _librarianRepository.Add(Borrower);
        }

        public bool BorrowBook(string bookName, string borrowerName)
        {
            Console.WriteLine("inside BorrowBook in LibraryService");
            var borrower = _borrowerRepository.SearchByUserName(borrowerName);
            var book = _bookRepository.GetByTitle(bookName);

            if (book != null)
            {
                var loan = _loanRepository.GetByBorrowerAndBook(borrower, book);
                if (loan != null && !book.AllowBorrow)
                {
                    return false;
                }
                else
                {
                    _loanRepository.Add(new Loan { BorrowerId = borrower.Id,BookId = book.Id,BorrowDate=DateTime.Now});
       

                    book.AllowBorrow = false;
                    _bookRepository.Update(book);
                    return true;
                }
            }

            return false;
        }


        public bool ReturnBook(string bookName, string borrowerName)
        { 

      
        }

        public LibrarianDTO? LogIn(string username, string password)
        {
            Librarian librarian = _librarianRepository.LogIn(username, password);
            if (librarian != null)
            {
                return new LibrarianDTO { Name = librarian.Name, UserName = librarian.UserName, Password = librarian.Password };
            }
            else
            {
                return null;
            }
        }
    }
}
