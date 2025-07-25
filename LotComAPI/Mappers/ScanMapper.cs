using System.Globalization;
using LotComAPI.Entities;
using LotComAPI.Models;

namespace LotComAPI.Mappers;

public static class ScanMapper
{
    /// <summary>
    /// Converts HTTP arguments into a ProcessDto object.
    /// </summary>
    /// <param name="Date"></param>
    /// <param name="Address"></param>
    /// <param name="ProcessId"></param>
    /// <param name="PartId"></param>
    /// <param name="Quantity"></param>
    /// <param name="SecondaryQuantity"></param>
    /// <param name="TertiaryQuantity"></param>
    /// <param name="Shift"></param>
    /// <param name="SecondaryShift"></param>
    /// <param name="TertiaryShift"></param>
    /// <param name="Operator"></param>
    /// <param name="SecondaryOperator"></param>
    /// <param name="TertiaryOperator"></param>
    /// <param name="JBKNumber"></param>
    /// <param name="LotNumber"></param>
    /// <param name="DieNumber"></param>
    /// <param name="DeburrJBKNumber"></param>
    /// <param name="ModelNumber"></param>
    /// <param name="HeatNumber"></param>
    /// <param name="ProductionDate"></param>
    /// <returns></returns>
    public static ScanDto HttpToDto(string Date, string Address, int ProcessId, int PartId, int Quantity, int? SecondaryQuantity, int? TertiaryQuantity, int Shift, int? SecondaryShift, int? TertiaryShift, string Operator, string? SecondaryOperator, string? TertiaryOperator, int? JBKNumber, string? LotNumber, int? DieNumber, int? DeburrJBKNumber, string? ModelNumber, string? HeatNumber, string ProductionDate)
    {
        // decode slashes and colons
        ProductionDate = ProductionDate.Replace("%2F", "/");
        ProductionDate = ProductionDate.Replace("%3A", ":");
        // validate the passed Date string
        try
        {
            DateTime DateObject = DateTime.ParseExact(ProductionDate, "MM/dd/yyyy-HH:mm:ss", CultureInfo.InvariantCulture);
            ProductionDate = new LotCom.Types.Timestamp(DateObject).Stamp;
        }
        catch (FormatException)
        {
            ProductionDate = new LotCom.Types.Timestamp(DateTime.MinValue).Stamp;
        }
        return new ScanDto
        (
            Date,
            Address,
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
    /// Performs mapping of values from Models.ScanDto to Entities.Scan.
    /// </summary>
    /// <param name="Dto"></param>
    /// <returns></returns>
    public static Scan DtoToEntity(ScanDto Dto)
    {
        Scan Entity = new Scan
        (
            Dto.Date,
            Dto.Address,
            Dto.ProcessId,
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
            Dto.ModelNumber,
            Dto.HeatNumber,
            Dto.ProductionDate
        );
        Entity.Id = Dto.Id;
        return Entity;
    }

    /// <summary>
    /// Performs mapping of values from Entities.Scan to Models.ScanDto.
    /// </summary>
    /// <param name="Entity"></param>
    /// <returns></returns>
    public static ScanDto EntityToDto(Scan Entity)
    {
        ScanDto Dto = new ScanDto
        (
            Entity.Date,
            Entity.Address,
            Entity.ProcessId,
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
            Entity.ModelNumber,
            Entity.HeatNumber,
            Entity.ProductionDate
        );
        Dto.Id = Entity.Id;
        return Dto;
    }
}