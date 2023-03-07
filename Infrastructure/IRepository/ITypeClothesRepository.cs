using Domain.Cloth;

namespace Infrastructure.IRepository
{
    public interface ITypeClothesRepository
    {
        List<TypeClothes> GetList(string? key,int? pageSize, int? page);
        TypeClothes GetById(int id);
        void Add(TypeClothes typeclothes);
        void Update(int id, TypeClothes typeClothes);
        void Delete(TypeClothes typeclothes);
    }
}
