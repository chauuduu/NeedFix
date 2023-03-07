using ClothesRentalShop.ViewModel;
using Domain.Cloth;
using Domain.Staffs;

namespace ClothesRentalShop.Profiles
{
    public interface IMapperObjectToObject
    {
        public List<ClothesViewModel> ConvertListClothes(List<Clothes> rs);
        public List<TypeClothesViewModel> ConvertListTypeClothes(List<TypeClothes> rs);
        public List<OriginViewModel> ConvertListOrigin(List<Origin> rs);
        public ClothesViewModel ConvertClothes(Clothes rs);
        public TypeClothesViewModel ConvertTypeClothes(TypeClothes rs);
        public OriginViewModel ConvertOrigin(Origin rs);
        public UserInfoViewModel ConvertUser(Staff rs);

    }
}
