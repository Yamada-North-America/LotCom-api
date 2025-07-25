using System.Globalization;
using LotCom.Types;
using LotComAPI.Entities;
using LotComAPI.Models;

namespace LotComAPI.Mappers;

public static class PrintMapper
{
    /// <summary>
    /// Converts HTTP arguments into a PrintDto object.
    /// </summary>
    /// <param name="ProcessId"></param>
    /// <param name="PartId"></param>
    /// <param name="Quantity"></param>
    /// <param name="Shift"></param>
    /// <param name="Operator"></param>
    /// <param name="ProductionDate"></param>
    /// <param name="SecondaryQuantity"></param>
    /// <param name="TertiaryQuantity"></param>
    /// <param name="SecondaryShift"></param>
    /// <param name="TertiaryShift"></param>
    /// <param name="SecondaryOperator"></param>
    /// <param name="TertiaryOperator"></param>
    /// <param name="JBKNumber"></param>
    /// <param name="LotNumber"></param>
    /// <param name="DieNumber"></param>
    /// <param name="DeburrJBKNumber"></param>
    /// <param name="ModelNumber"></param>
    /// <param name="HeatNumber"></param>
    /// <returns></returns>
    public static PrintDto HttpToDto(int ProcessId, int PartId, int Quantity, int Shift, string Operator, string ProductionDate, int? SecondaryQuantity = null, int? TertiaryQuantity = null, int? SecondaryShift = null, int? TertiaryShift = null, string? SecondaryOperator = null, string? TertiaryOperator = null, int? JBKNumber = null, string? LotNumber = null, int? DieNumber = null, int? DeburrJBKNumber = null, string? ModelNumber = null, string? HeatNumber = null)
    {
        // decode slashes and colons
        ProductionDate = ProductionDate.Replace("%2F", "/");
        ProductionDate = ProductionDate.Replace("%3A", ":");
        // convert the passed Date string to an SqlDateTime string
        try
        {
            DateTime Date = DateTime.ParseExact(ProductionDate, "MM/dd/yyyy-HH:mm:ss", CultureInfo.InvariantCulture);
            ProductionDate = new Timestamp(Date).Stamp;
        }
        catch (FormatException)
        {
            ProductionDate = new Timestamp(DateTime.MinValue).Stamp;
        }
        return new PrintDto
        (
            ProcessId,
            PartId,
            Quantity,
            SecondaryQuantity,
            TertiaryQuantity,
            Shift,
            SecondaryShift,
            TertiaryShift,
            Operator,
            SecondaryOperator,
            TertiaryOperator,
            JBKNumber,
            LotNumber,
            DieNumber,
            DeburrJBKNumber,
            ModelNumber,
            HeatNumber,
            ProductionDate
        );
    }

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
            ModelNumber: Dto.ModelNumber,
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
            ModelNumber: Entity.ModelNumber,
            HeatNumber: Entity.HeatNumber,
            ProductionDate: Entity.ProductionDate
        );
        Mapped.Id = Entity.Id;
        return Mapped;
    }
}