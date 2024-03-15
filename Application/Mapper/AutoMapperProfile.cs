using Application.Models;
using AutoMapper;
using DTOs.Product;

namespace Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateOrUpdateProductDTO, Product>().ReverseMap();
            CreateMap<CreateOrUpdateProductDTO, Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<Product>>().ReverseMap();
            CreateMap<Product, Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<CreateOrUpdateProductDTO>>().ReverseMap();
            CreateMap<GetAllProductDTO, Product>().ReverseMap();
        }
    }
}
