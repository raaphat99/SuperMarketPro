using AutoMapper;
using Domain.Models;
using Infrastructure.Identity;
using Presentation.ViewModels;

namespace Presentation.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();

            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore())
                .ForMember(dest => dest.ImageData, opt => opt.Ignore())
                .ForMember(dest => dest.ImageContentType, opt => opt.Ignore());

            CreateMap<ProductViewModel, Product>()
                .ForMember(dest => dest.ImageData, opt => opt.Ignore())
                .ForMember(dest => dest.ImageContentType, opt => opt.Ignore());

            CreateMap<Transaction, TransactionSearchViewModel>();
        }


    }
}
