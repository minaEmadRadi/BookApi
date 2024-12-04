using BookRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace BookRepo.Repo
{
    public class BookRepo : IBook
    {
        private readonly BookDbContext _db;
        public BookRepo(BookDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Book b = _db.books.Find(id);
                _db.books.Remove(b);
                var res= await _db.SaveChangesAsync();
                return res > 0;
            }catch(Exception ex)
            {
                throw new Exception("Invalid Book Id");
            }
            
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _db.books.AsNoTracking().ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await _db.books.FindAsync(id);
        }

        public async Task<Book> Update(int id, Book book)
        {
            var existingBook = await _db.books.FindAsync(id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
            existingBook.title = book.title;
            existingBook.author = book.author;
            existingBook.publishYear = book.publishYear;
            existingBook.genre = book.genre;
            _db.books.Update(existingBook);
            await _db.SaveChangesAsync();
            return existingBook;
        }
        public async Task<Book> Add(Book book)
        {
            try
            {
                await _db.books.AddAsync(book);
                await _db.SaveChangesAsync();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the book.", ex);
            }
        }

    }
}
