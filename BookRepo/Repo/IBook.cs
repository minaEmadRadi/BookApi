using BookRepo.Models;

namespace BookRepo.Repo
{
    public interface IBook
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<bool> Delete(int id);
        Task<Book> Add(Book book);
        Task<Book> Update(int id, Book book);
    }
}
