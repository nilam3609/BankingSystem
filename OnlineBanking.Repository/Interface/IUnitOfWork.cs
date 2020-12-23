using OnlineBanking.Domain;

namespace OnlineBanking.Repository.Interface
{
    public interface IUnitOfWork
    {
        IRepository<Account> Accounts { get; }

        IRepository<Bank> Banks { get; }

        void Commit();
    }
}