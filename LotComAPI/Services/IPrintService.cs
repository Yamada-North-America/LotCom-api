using LotComAPI.Entities;

namespace LotComAPI.Services;

public interface IPrintService
{
    IEnumerable<Print> GetAll();
    IEnumerable<Print> GetOnDate(DateTime Date);
    IEnumerable<Print> GetOnDateByProcess(DateTime Date, int ProcessId);
    Print? Get(int id);
    Print Create(Print Print);
    bool Update(int id, Print Print);
    void Delete(Print Print);
    bool Save();
}