using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.IRepository;

namespace Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext _context;
        private IGenericRepository<Country> _contries;
        private IGenericRepository<Hotel> _hotels;
        public UnitOfWork(DataBaseContext context)
        {
            _context = context;
        }
        public IGenericRepository<Country> Countries => _contries ??= new GenericRepository<Country>(_context);
        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}