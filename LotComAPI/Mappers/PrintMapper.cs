using LotCom.DataAccess.Models;
using LotComAPI.Entities;
using LotComAPI.Models;

namespace LotComAPI.Mappers;

public static class PrintMapper
{
    /// <summary>
    /// Performs mapping of values from Models.PrintDto to Entities.Print. 
    /// </summary>
    /// <param name="Dto"></param>
    /// <returns></returns>
    public static Print DtoToEntity(PrintDto Dto)
    {
        Print Mapped = new Print
        (
            ProcessId: Dto.ProcessId,
            PartId: Dto.PartId,
            Quantity: Dto.Quantity,
            SecondaryQuantity: Dto.SecondaryQuantity,
            TertiaryQuantity: Dto.TertiaryQuantity,
            Shift: Dto.Shift,
            SecondaryShift: Dto.SecondaryShift,
            TertiaryShift: Dto.TertiaryShift,
            Operator: Dto.Operator,
            SecondaryOperator: Dto.SecondaryOperator,
            TertiaryOperator: Dto.TertiaryOperator,
            JBKNumber: Dto.JBKNumber,
            LotNumber: Dto.LotNumber,
            DieNumber: Dto.DieNumber,
            DeburrJBKNumber: Dto.DeburrJBKNumber,
            HeatNumber: Dto.HeatNumber,
            ProductionDate: Dto.ProductionDate
        );
        Mapped.Id = Dto.Id;
        return Mapped;
    }

    /// <summary>
    /// Performs mapping of values from Entities.Print to Models.PrintDto.
    /// </summary>
    /// <param name="Entity"></param>
    /// <returns></returns>
    public static PrintDto EntityToDto(Print Entity)
    {
        PrintDto Mapped = new PrintDto
        (
            ProcessId: Entity.ProcessId,
            PartId: Entity.PartId,
            Quantity: Entity.Quantity,
            SecondaryQuantity: Entity.SecondaryQuantity,
            TertiaryQuantity: Entity.TertiaryQuantity,
            Shift: Entity.Shift,
            SecondaryShift: Entity.SecondaryShift,
            TertiaryShift: Entity.TertiaryShift,
            Operator: Entity.Operator,
            SecondaryOperator: Entity.SecondaryOperator,
            TertiaryOperator: Entity.TertiaryOperator,
            JBKNumber: Entity.JBKNumber,
            LotNumber: Entity.LotNumber,
            DieNumber: Entity.DieNumber,
            DeburrJBKNumber: Entity.DeburrJBKNumber,
            HeatNumber: Entity.HeatNumber,
            ProductionDate: Entity.ProductionDate
        );
        Mapped.Id = Entity.Id;
        return Mapped;
    }

    /// <summary>
    /// Maps New properties to the corresponding Original properties (in essence, updating the object).
    /// </summary>
    /// <param name="Original"></param>
    /// <param name="New"></param>
    /// <returns></returns>
    public static void EntityToEntity(Print Original, Print New)
    {
        Original.ProcessId = New.ProcessId;
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
    /// Maps values from a Data Access Object (DAO) to an Entities.Print object.
    /// </summary>
    /// <param name="Dao"></param>
    /// <returns></returns>
    public static Print DaoToEntity(PrintDao Dao)
    {
        return new Print
        (
            Dao.ProcessId,
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