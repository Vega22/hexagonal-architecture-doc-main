using System.Threading.Tasks;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Unit Of Work. Should only be used by Use Cases.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Begins a new transaction scope.
        /// </summary>
        /// <returns>Task.</returns>
        Task BeginTransaction();

        /// <summary>
        /// Commits the current transaction.
        /// </summary>
        /// <returns>Task.</returns>
        Task CommitTransaction();

        /// <summary>
        /// Rolls back the current transaction.
        /// </summary>
        /// <returns>Task.</returns>
        Task RollbackTransaction();

        /// <summary>
        /// Applies all database changes.
        /// </summary>
        /// <returns>Number of affected rows.</returns>
        Task<int> Save();
    }
}
