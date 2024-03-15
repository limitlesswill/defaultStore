using Application.Models;

namespace Application.Contracts
{
    public interface IProductRepository : IRepository<Product, int>
    {
    }
}
