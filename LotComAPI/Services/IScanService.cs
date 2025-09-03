using LotCom.Database.Entities;

namespace LotComAPI.Services;

public interface IScanService
{
    IEnumerable<ScanEntity> GetAll();
    ScanEntity? Get(int id);
    IEnumerable<ScanEntity>? GetAllWithinRange(int days);
    IEnumerable<ScanEntity>? GetWithSerialNumber(int serialNumber);
    ScanEntity Create(ScanEntity Entity);
    bool Update(int id, ScanEntity Entity);
    void Delete(ScanEntity Entity);
    bool Save();
}