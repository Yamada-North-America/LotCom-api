using System.ComponentModel.DataAnnotations;

namespace LotComAPI.Entities;

/// <summary>
/// Defines the Database entity structure of a Scan.
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
/// <param name="HeatNumber"></param>
/// <param name="ProductionDate"></param>
public class Scan(string Date, string Address, int ProcessId, int PartId, int Quantity, int? SecondaryQuantity, int? TertiaryQuantity, int Shift, int? SecondaryShift, int? TertiaryShift, string Operator, string? SecondaryOperator, string? TertiaryOperator, int? JBKNumber, string? LotNumber, string? DieNumber, int? DeburrJBKNumber, string? HeatNumber, string ProductionDate)
{
    // protected properties (not transfered by DTO Layer)
    [MaxLength(20)]
    public string Created { get; set; } = "";

    [MaxLength(20)]
    public string Updated { get; set; } = "";

    // open properties (transfered by DTO Layer)
    [Key]
    public int Id { get; set; } = 0;

    [Required]
    [MaxLength(20)]
    public string Date { get; set; } = Date;

    [Required]
    [MaxLength(15)]
    public string Address { get; set; } = Address;

    [Required]
    public int ProcessId { get; set; } = ProcessId;

    [Required]
    public int PartId { get; set; } = PartId;

    [Required]
    public int Quantity { get; set; } = Quantity;

    public int? SecondaryQuantity { get; set; } = SecondaryQuantity;

    public int? TertiaryQuantity { get; set; } = TertiaryQuantity;

    [Required]
    public int Shift { get; set; } = Shift;

    public int? SecondaryShift { get; set; } = SecondaryShift;

    public int? TertiaryShift { get; set; } = TertiaryShift;

    [Required]
    [MaxLength(3)]
    public string Operator { get; set; } = Operator;

    [MaxLength(3)]
    public string? SecondaryOperator { get; set; } = SecondaryOperator;

    [MaxLength(3)]
    public string? TertiaryOperator { get; set; } = TertiaryOperator;

    public int? JBKNumber { get; set; } = JBKNumber;

    [MaxLength(3)]
    public string? LotNumber { get; set; } = LotNumber;

    [MaxLength(3)]
    public string? DieNumber { get; set; } = DieNumber;

    public int? DeburrJBKNumber { get; set; } = DeburrJBKNumber;

    public string? HeatNumber { get; set; } = HeatNumber;

    [Required]
    public string ProductionDate { get; set; } = ProductionDate;
}