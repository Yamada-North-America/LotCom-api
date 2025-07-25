using LotComAPI.Entities;

namespace LotComAPI.Services;

public interface IPrintService
{
    IEnumerable<Print> GetAll();
    Print? Get(int id);
    Print Create(Print Print);
    void Update(int id, Print Print);
    void Delete(Print Print);
    bool Save();
}