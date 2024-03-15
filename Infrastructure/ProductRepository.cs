using Application.Context;
using Application.Contracts;
using Application.Models;

namespace Infrastructure
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext _context) : base(_context)
        {

        }
    }
}
