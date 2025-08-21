using LotCom.DataAccess.Entities;

namespace LotComAPI.Services;

public interface IPartService
{
    IEnumerable<PartEntity> GetAll();
    IEnumerable<PartEntity> GetPrintedBy(int processId);
    IEnumerable<PartEntity> GetScannedBy(int processId);
    PartEntity? Get(int id);
    PartEntity Create(PartEntity Part);
    bool Update(int id, PartEntity Part);
    void Delete(PartEntity Part);
    bool Save();
}