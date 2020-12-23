using OnlineBanking.Domain;
using OnlineBanking.Repository.Interface;

namespace OnlineBanking.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private BankContext _bankContext;
        private Repository<Account> _accounts;
        private Repository<Bank> _banks;
        public UnitOfWork(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public IRepository<Account> Accounts
        {
            get
            {
                return _accounts ?? (_accounts = new Repository<Account>(_bankContext));
            }
        }

        public IRepository<Bank> Banks
        {
            get
            {
                return _banks ?? (_banks = new Repository<Bank>(_bankContext));
            }
        }

        public void Commit()
        {
            _bankContext.SaveChanges();
        }

    }
}