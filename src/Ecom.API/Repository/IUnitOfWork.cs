using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecom.API.Repository
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
