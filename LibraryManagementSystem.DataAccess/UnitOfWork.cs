using System.Collections.Concurrent;
using LibraryManagementSystem.Core;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.DataAccess.Repositories;

namespace LibraryManagementSystem.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;

        public IGenericRepository<Book> Books { get; private set; }
        public IGenericRepository<Author> Authors { get; private set; }



        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            Books = new GenericRepository<Book>(_context);
            Authors = new GenericRepository<Author>(_context);
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}