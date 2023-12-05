using BLL.DTOs;
using DAL.Entities;
using DAL.Repository.BookRep;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public BookDTO GetBookById(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            Book book = _bookRepository.GetById(objectId);
            // Map Book entity to BookDTO
            // Example using AutoMapper or manual mapping
            return new BookDTO { Title = book.Title, Author = book.Author };
        }

        public IEnumerable<BookDTO> GetAllBooks()
        {
            IEnumerable<Book> books = _bookRepository.GetAll();
            // Map list of Book entities to list of BookDTOs
            // Example using AutoMapper or manual mapping
            return books.Select(book => new BookDTO { Title = book.Title, Author = book.Author });
        }

        public void AddBook(BookDTO bookDto)
        {
            // Map BookDTO to Book entity
            // Example using AutoMapper or manual mapping
            Book book = new Book { Title = bookDto.Title, Author = bookDto.Author };
            _bookRepository.Add(book);
        }

        public void UpdateBook(string id, BookDTO bookDto)
        {
            ObjectId objectId = ObjectId.Parse(id);
            Book existingBook = _bookRepository.GetById(objectId);

            if (existingBook != null)
            {
                // Update properties of existingBook with values from bookDto
                // Example using AutoMapper or manual mapping
                existingBook.Title = bookDto.Title;
                existingBook.Author = bookDto.Author;
                _bookRepository.Update(existingBook);
            }
            // Handle error if the book with the given id does not exist
        }

        public void DeleteBook(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            _bookRepository.Delete(objectId);
        }
    }
}
