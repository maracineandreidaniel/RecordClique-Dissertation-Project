namespace RecordClique_DataAccess.Repository.Abstraction
{
    public interface IUnitOfWork
    {
        int CommitChanges();
        Task<int> CommitChangesAsync();
        void CreateTransaction();
        void RollbackTransaction();
        void CommitTransaction();
    }
}
