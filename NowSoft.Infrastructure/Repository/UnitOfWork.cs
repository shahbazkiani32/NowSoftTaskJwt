using NowSoft.Application.Contracts;

namespace NowSoft.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IAccountService accountService)
        {
            Account = accountService;
        }

        public IAccountService Account { get; set; }
    }
}
