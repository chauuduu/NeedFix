using AutoMapper;
using ClothesRentalShop.ViewModel;
using Domain.Cloth;
using Domain.Staffs;

namespace ClothesRentalShop.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TypeClothes, TypeClothesViewModel>();
            CreateMap<Clothes, ClothesViewModel>();
            CreateMap<Origin,OriginViewModel>();
            CreateMap<Staff, UserInfoViewModel>();
        }
    }
}