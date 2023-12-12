using DAL.Repository.BookRep;
using DAL.Repository.BorrowerRep;
using DAL.Repository.LibrarianRep;
using DAL.Repository.LoanRep;

namespace DAL.Factory
{
    public interface IMongoRepositoryFactory
    {
        IBookRepository CreateBookRepository();
        IBorrowerRepository CreateBorrowerRepository();
        ILibrarianRepository CreateLibrarianRepository();
        ILoanRepository CreateLoanRepository();
    }
}