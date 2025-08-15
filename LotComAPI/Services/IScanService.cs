using LotComAPI.Entities;

namespace LotComAPI.Services;

public interface IScanService
{
    IEnumerable<Scan> GetAll();
    Scan? Get(int id);
    Scan Create(Scan Entity);
    bool Update(int id, Scan Entity);
    void Delete(Scan Entity);
    bool Save();
}