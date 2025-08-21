using LotCom.DataAccess.Entities;

namespace LotComAPI.Services;

public interface IProcessService
{
    IEnumerable<ProcessEntity> GetAll();
    IEnumerable<ProcessEntity> GetAllFromStoredProcedure();
    ProcessEntity? Get(int id);
    ProcessEntity Create(ProcessEntity Entity);
    bool Update(int id, ProcessEntity Entity);
    void Delete(ProcessEntity Entity);
    bool Save();
}