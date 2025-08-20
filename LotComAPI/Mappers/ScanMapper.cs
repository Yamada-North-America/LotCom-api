using LotCom.DataAccess.Models;
using LotComAPI.Entities;
using LotComAPI.Models;

namespace LotComAPI.Mappers;

public static class ScanMapper
{
    /// <summary>
    /// Performs mapping of values from Models.ScanDto to Entities.ScanEntity.
    /// </summary>
    /// <param name="Dto"></param>
    /// <returns></returns>
    public static ScanEntity DtoToEntity(ScanDto Dto)
    {
        ScanEntity Entity = new ScanEntity
        (
            Dto.ScanProcessId,
            Dto.ScanDate,
            Dto.ScanAddress,
            Dto.LabelProcessId,
            Dto.PartId,
            Dto.Quantity,
            Dto.SecondaryQuantity,
            Dto.TertiaryQuantity,
            Dto.Shift,
            Dto.SecondaryShift,
            Dto.TertiaryShift,
            Dto.Operator,
            Dto.SecondaryOperator,
            Dto.TertiaryOperator,
            Dto.JBKNumber,
            Dto.LotNumber,
            Dto.DieNumber,
            Dto.DeburrJBKNumber,
            Dto.HeatNumber,
            Dto.ProductionDate
        );
        Entity.Id = Dto.Id;
        return Entity;
    }

    /// <summary>
    /// Performs mapping of values from Entities.ScanEntity to Models.ScanDto.
    /// </summary>
    /// <param name="Entity"></param>
    /// <returns></returns>
    public static ScanDto EntityToDto(ScanEntity Entity)
    {
        ScanDto Dto = new ScanDto
        (
            Entity.ScanProcessId,
            Entity.ScanDate,
            Entity.ScanAddress,
            Entity.LabelProcessId,
            Entity.PartId,
            Entity.Quantity,
            Entity.SecondaryQuantity,
            Entity.TertiaryQuantity,
            Entity.Shift,
            Entity.SecondaryShift,
            Entity.TertiaryShift,
            Entity.Operator,
            Entity.SecondaryOperator,
            Entity.TertiaryOperator,
            Entity.JBKNumber,
            Entity.LotNumber,
            Entity.DieNumber,
            Entity.DeburrJBKNumber,
            Entity.HeatNumber,
            Entity.ProductionDate
        );
        Dto.Id = Entity.Id;
        return Dto;
    }

    /// <summary>
    /// Maps New properties to the corresponding Original properties (in essence, updating the object).
    /// </summary>
    /// <param name="Original"></param>
    /// <param name="New"></param>
    /// <returns></returns>
    public static void EntityToEntity(ScanEntity Original, ScanEntity New)
    {
        Original.ScanProcessId = New.ScanProcessId;
        Original.ScanDate = New.ScanDate;
        Original.ScanAddress = New.ScanAddress;
        Original.LabelProcessId = New.LabelProcessId;
        Original.PartId = New.PartId;
        Original.Quantity = New.Quantity;
        Original.SecondaryQuantity = New.SecondaryQuantity;
        Original.TertiaryQuantity = New.TertiaryQuantity;
        Original.Shift = New.Shift;
        Original.SecondaryShift = New.SecondaryShift;
        Original.TertiaryShift = New.TertiaryShift;
        Original.Operator = New.Operator;
        Original.SecondaryOperator = New.SecondaryOperator;
        Original.TertiaryOperator = New.TertiaryOperator;
        Original.JBKNumber = New.JBKNumber;
        Original.LotNumber = New.LotNumber;
        Original.DieNumber = New.DieNumber;
        Original.DeburrJBKNumber = New.DeburrJBKNumber;
        Original.HeatNumber = New.HeatNumber;
        Original.ProductionDate = New.ProductionDate;
    }

    /// <summary>
    /// Maps values from a Data Access Object (DAO) to an Entities.ScanEntity object.
    /// </summary>
    /// <param name="Dao"></param>
    /// <returns></returns>
    public static ScanEntity DaoToEntity(ScanDao Dao)
    {
        return new ScanEntity
        (
            Dao.ScanProcessId,
            Dao.ScanDate
                .Replace("%2F", "/")
                .Replace("%3A", ":"),
            Dao.ScanAddress,
            Dao.LabelProcessId,
            Dao.PartId,
            Dao.Quantity,
            Dao.SecondaryQuantity,
            Dao.TertiaryQuantity,
            Dao.Shift,
            Dao.SecondaryShift,
            Dao.TertiaryShift,
            Dao.Operator,
            Dao.SecondaryOperator,
            Dao.TertiaryOperator,
            Dao.JBKNumber,
            Dao.LotNumber,
            Dao.DieNumber,
            Dao.DeburrJBKNumber,
            Dao.HeatNumber,
            Dao.ProductionDate
                .Replace("%2F", "/")
                .Replace("%3A", ":")
        );
    }
}