namespace NowSoft.Application.Contracts;

public interface IUnitOfWork
{
    IAccountService Account { get; }
}

