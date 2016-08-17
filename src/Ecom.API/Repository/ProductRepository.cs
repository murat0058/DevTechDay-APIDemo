using Ecom.API.Repository.Abstract;
using Ecom.API.Entities;
using Ecom.API.Contexts;

namespace Ecom.API.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(BaseContext context)
            :base(context)
        {

        }
    }
}
