using AutoMapper;
using ClothesRentalShop.ViewModel;
using Domain.Cloth;
using Domain.Staffs;

namespace ClothesRentalShop.Profiles
{
    public class MapperObjectToObject : IMapperObjectToObject
    {
        private readonly IMapper mapper;

        public MapperObjectToObject(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public List<ClothesViewModel> ConvertListClothes(List<Clothes> rs)
        {
            return mapper.Map<List<Clothes>, List<ClothesViewModel>>(rs);
        }
        public List<TypeClothesViewModel> ConvertListTypeClothes(List<TypeClothes> rs)
        {
            return mapper.Map<List<TypeClothes>, List<TypeClothesViewModel>>(rs);
        }
        public List<OriginViewModel> ConvertListOrigin(List<Origin> rs)
        {
            return mapper.Map<List<Origin>, List<OriginViewModel>>(rs);
        }
        public ClothesViewModel ConvertClothes(Clothes rs)
        {
            return mapper.Map<Clothes,ClothesViewModel>(rs);
        }
        public TypeClothesViewModel ConvertTypeClothes(TypeClothes rs)
        {
            return mapper.Map<TypeClothes,TypeClothesViewModel>(rs);
        }
        public OriginViewModel ConvertOrigin(Origin rs)
        {
            return mapper.Map<Origin,OriginViewModel>(rs);
        }
        public UserInfoViewModel ConvertUser(Staff rs)
        {
            return mapper.Map<Staff, UserInfoViewModel>(rs);
        }
    }
}
