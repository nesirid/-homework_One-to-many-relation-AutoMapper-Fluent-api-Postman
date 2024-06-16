using AutoMapper;
using WebApp.DTOs.Archives;
using WebApp.DTOs.Blogs;
using WebApp.DTOs.Categories;
using WebApp.DTOs.Countries;
using WebApp.DTOs.Products;
using WebApp.DTOs.Sliders;
using WebApp.Models;

namespace WebApp.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Country
            CreateMap<Country, CountryDto>();
            //.ForMember(dest => dest.FirstColumn, opt => opt.MapFrom(src => src.SecondColumn));
            CreateMap<CountryCreateDto, Country>();
            CreateMap<CountryEditDto, Country>();
            //Product
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductEditDto, Product>();
            //Category
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryEditDto, Category>();
            CreateMap<Category, CategoryDto>();
            //Slider
            CreateMap<Slider, SliderDto>();
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<SliderEditDto, Slider>();
            //Blog
            CreateMap<Blog, BlogDto>();
            CreateMap<BlogCreateDto, Blog>();
            CreateMap<BlogEditDto, Blog>();
            //Achive
            CreateMap<ArchiveCategory, ArchiveCategoryDto>();
            CreateMap<ArchiveCategoryCreateDto, ArchiveCategory>();
            CreateMap<ArchiveProduct, ArchiveProductDto>();
            CreateMap<Category, ArchiveCategory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
            CreateMap<Product, ArchiveProduct>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
