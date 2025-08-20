using LotComAPI.Entities;

namespace LotComAPI.Services;

public interface IPrintService
{
    IEnumerable<PrintEntity> GetAll();
    IEnumerable<PrintEntity> GetOnDate(DateTime Date);
    IEnumerable<PrintEntity> GetOnDateByProcess(DateTime Date, int ProcessId);
    PrintEntity? Get(int id);
    PrintEntity Create(PrintEntity Print);
    bool Update(int id, PrintEntity Print);
    void Delete(PrintEntity Print);
    bool Save();
}