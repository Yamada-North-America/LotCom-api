using LotComAPI.Entities;

namespace LotComAPI.Services;

public interface IScanService
{
    IEnumerable<Scan> GetAll();
    Scan? Get(int id);
    IEnumerable<Scan>? GetAllWithinRange(int days);
    Scan Create(Scan Entity);
    bool Update(int id, Scan Entity);
    void Delete(Scan Entity);
    bool Save();
}