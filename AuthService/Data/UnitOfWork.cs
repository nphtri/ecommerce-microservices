using System;
using AuthService.Models;

namespace AuthService.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepo<Account> AccountRepo { get; }
        IGenericRepo<Role> RoleRepo { get; }
        int Complete();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private bool disposed = false;
        private IGenericRepo<Account> _accountRepo;
        private IGenericRepo<Role> _roleRepo;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IGenericRepo<Account> AccountRepo
        {
            get { return _accountRepo ??= new GenericRepo<Account>(_context); }
        }

        public IGenericRepo<Role> RoleRepo
        {
            get { return _roleRepo ??= new GenericRepo<Role>(_context); }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }
    }
}