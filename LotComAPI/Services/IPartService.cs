using LotComAPI.Entities;

namespace LotComAPI.Services;

public interface IPartService
{
    IEnumerable<Part> GetAll();
    Part? Get(int id);
    Part Create(Part Part);
    void Update(int id, Part Part);
    void Delete(Part Part);
    bool Save();
}