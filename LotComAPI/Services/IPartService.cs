using LotComAPI.Entities;

namespace LotComAPI.Services;

public interface IPartService
{
    IEnumerable<Part> GetAll();
    IEnumerable<Part> GetPrintedBy(int processId);
    IEnumerable<Part> GetScannedBy(int processId);
    Part? Get(int id);
    Part Create(Part Part);
    bool Update(int id, Part Part);
    void Delete(Part Part);
    bool Save();
}