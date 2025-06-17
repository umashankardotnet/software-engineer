using CRUD_API_Demo.Data;
using CRUD_API_Demo.Data.Entities;
using CRUD_API_Demo.Models;

namespace CRUD_API_Demo.Services
{
    public class BookService : IBookService
    {
        private readonly DBContext context;
        public BookService(DBContext dBContext)
        {
            context = dBContext;
        }
        public void CreateBook(BookModel book)
        {
            Book book1 = new Book()
            {
                Title = book.Title,
            };
            context.Books.Add(book1);
            context.SaveChanges();
        }

        public bool DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public IList<BookModel> GetAllBooks()
        {
            var books = new List<BookModel>();
            var bookList = context.Books.ToList();
            foreach (var item in bookList)
            {
                books.Add(new BookModel() { Author = item.Author, Title = item.Title });
            }

            return books;
        }

        public BookModel GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(BookModel book)
        {
            throw new NotImplementedException();
        }
    }
}
