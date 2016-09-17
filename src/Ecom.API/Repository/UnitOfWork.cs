using Ecom.API.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private BaseContext _context;

        public UnitOfWork(BaseContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.Commit();
        }
    }
}
