using Application.Contracts;
using Application.Models;
using AutoMapper;
using DTOs.Product;
using DTOs.ResultView;

namespace Application.Services
{
    public interface IProductService
    {
        public Task<List<CreateOrUpdateProductDTO>> GetAll();
        public Task<CreateOrUpdateProductDTO> GetOne(int id);
        public Task<ResultView<CreateOrUpdateProductDTO>> Create(CreateOrUpdateProductDTO product);
        public Task<CreateOrUpdateProductDTO> Update(CreateOrUpdateProductDTO product);
        public Task<ResultView<CreateOrUpdateProductDTO>> Delete(CreateOrUpdateProductDTO product);
        public Task<CreateOrUpdateProductDTO> Save();
    }
}
