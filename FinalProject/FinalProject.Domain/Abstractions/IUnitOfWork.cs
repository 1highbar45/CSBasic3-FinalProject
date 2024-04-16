using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task SaveChangeAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
