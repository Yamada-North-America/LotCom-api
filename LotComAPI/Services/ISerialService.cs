namespace LotComAPI.Services;

public interface ISerialService
{
    int? GetNextJBK(int PartId);
    int? GetNextLot(int PartId);
    bool Save();
}