namespace LotComAPI.Models;

/// <summary>
/// Defines the publicly-available entity structure of a Scan.
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
public class ScanDto(string Date, string Address, int ProcessId, int PartId, int Quantity, int? SecondaryQuantity, int? TertiaryQuantity, int Shift, int? SecondaryShift, int? TertiaryShift, string Operator, string? SecondaryOperator, string? TertiaryOperator, int? JBKNumber, string? LotNumber, int? DieNumber, int? DeburrJBKNumber, string? ModelNumber, string? HeatNumber, string ProductionDate)
{
    public int Id { get; set; } = 0;

    public string Date { get; set; } = Date;

    public string Address { get; set; } = Address;

    public int ProcessId { get; set; } = ProcessId;

    public int PartId { get; set; } = PartId;

    public int Quantity { get; set; } = Quantity;

    public int? SecondaryQuantity { get; set; } = SecondaryQuantity;

    public int? TertiaryQuantity { get; set; } = TertiaryQuantity;

    public int Shift { get; set; } = Shift;

    public int? SecondaryShift { get; set; } = SecondaryShift;

    public int? TertiaryShift { get; set; } = TertiaryShift;

    public string Operator { get; set; } = Operator;

    public string? SecondaryOperator { get; set; } = SecondaryOperator;

    public string? TertiaryOperator { get; set; } = TertiaryOperator;

    public int? JBKNumber { get; set; } = JBKNumber;

    public string? LotNumber { get; set; } = LotNumber;

    public int? DieNumber { get; set; } = DieNumber;

    public int? DeburrJBKNumber { get; set; } = DeburrJBKNumber;

    public string? ModelNumber { get; set; } = ModelNumber;

    public string? HeatNumber { get; set; } = HeatNumber;

    public string ProductionDate { get; set; } = ProductionDate;
}