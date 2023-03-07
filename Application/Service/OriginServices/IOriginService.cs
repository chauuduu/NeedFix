﻿using Domain.Cloth;

namespace Application.Service.OriginsService
{
    public interface IOriginService
    {
        List<Origin> GetList(string? key, int? pageSize, int? page);
        Origin GetById(int id);
        void Add(string name,string address);
        void Update(int id, string name, string address);
        void Delete(int id);
    }
}
