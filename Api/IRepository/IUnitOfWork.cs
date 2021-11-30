using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;

namespace Api.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries{get;set;}

        IGenericRepository<Hotel> Hotels{get;set;}

        Task Save();
    }
}