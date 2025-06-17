using CRUD_API_Demo.Models;

namespace CRUD_API_Demo.Services
{
    public interface IBookService
    {
        BookModel GetBookById(int id);

        void CreateBook(BookModel book);

        bool DeleteBook(int id);

        IList<BookModel> GetAllBooks();

        void UpdateBook(BookModel book);
    }
}
