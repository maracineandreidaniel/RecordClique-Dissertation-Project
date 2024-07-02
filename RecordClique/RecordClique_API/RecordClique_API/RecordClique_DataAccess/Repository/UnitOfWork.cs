using Microsoft.EntityFrameworkCore.Storage;
using RecordClique_DataAccess.Context;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_API.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RecordCliqueContext _dbContext;
        private IDbContextTransaction _transaction;

        public UnitOfWork(RecordCliqueContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CommitChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void CreateTransaction()
        {
            if (_transaction == null)
            {
                _transaction = _dbContext.Database.BeginTransaction();
            }
            else
            {
                throw new InvalidOperationException("Transaction already exists! Use Rollback or Commit to close it.");
            }
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
            _transaction = null;
        }
    }
}
