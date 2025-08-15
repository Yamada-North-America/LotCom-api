using LotComAPI.Entities;

namespace LotComAPI.Services;

public interface IProcessService
{
    IEnumerable<Process> GetAll();
    IEnumerable<Process> GetAllFromStoredProcedure();
    Process? Get(int id);
    Process Create(Process Entity);
    bool Update(int id, Process Entity);
    void Delete(Process Entity);
    bool Save();
}