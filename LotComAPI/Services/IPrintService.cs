using LotCom.Database.Entities;

namespace LotComAPI.Services;

public interface IPrintService
{
    IEnumerable<PrintEntity> GetAll();
    IEnumerable<PrintEntity> GetOnDate(DateTime Date);
    IEnumerable<PrintEntity> GetOnDateByProcess(DateTime Date, int ProcessId);
    IEnumerable<PrintEntity>? GetWithSerialNumber(int serialNumber);
    PrintEntity? Get(int id);
    PrintEntity Create(PrintEntity Print);
    bool Update(int id, PrintEntity Print);
    void Delete(PrintEntity Print);
    bool Save();
}