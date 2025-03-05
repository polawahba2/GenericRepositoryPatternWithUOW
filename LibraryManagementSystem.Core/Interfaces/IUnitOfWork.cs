using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;

namespace LibraryManagementSystem.Core
{
    public interface IUnitOfWork : IDisposable
    {


       public IGenericRepository<Book> Books { get; }
       public IGenericRepository<Author> Authors { get; }
       public Task<int> SaveChangesAsync();
    }
}