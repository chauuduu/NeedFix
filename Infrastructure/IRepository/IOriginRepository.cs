using Domain.Cloth;

namespace Infrastructure.IRepository
{
    public interface IOriginRepository
    {
        List<Origin> GetList(string? key,int? pageSize, int? page);
        Origin GetById(int id);
        void Add(Origin origin);
        void Update(int id, Origin origin);
        void Delete(Origin origin);
    }
}
