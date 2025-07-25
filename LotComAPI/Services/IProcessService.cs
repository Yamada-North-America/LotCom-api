using LotComAPI.Entities;

namespace LotComAPI.Services;

public interface IProcessService
{
    IEnumerable<Process> GetAll();
    Process? Get(int id);
    Process Create(Process Entity);
    void Update(int id, Process Entity);
    void Delete(Process Entity);
    bool Save();
}