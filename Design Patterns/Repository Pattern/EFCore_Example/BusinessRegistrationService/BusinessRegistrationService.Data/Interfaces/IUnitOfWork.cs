using Microsoft.EntityFrameworkCore.Storage;

namespace BusinessRegistrationService.Data.Interfaces;

public interface IUnitOfWork
{
    IBusinessDetailRepository BusinessDetailRepository { get;  }

    Task<bool> CompleteAsync();
    Task<IDbContextTransaction> BeginDbTransaction();
}