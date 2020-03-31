using System;
using System.Threading.Tasks;
using GiraffeTheLogger.Domain;
using GiraffeTheLogger.Domain.Entities;

namespace GiraffeTheLogger.DbServiceContract
{
    public interface ILogService
    {
         Task<Guid> CreateAsync(MessageDto messageDto);
    }
}