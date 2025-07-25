using System.ComponentModel.DataAnnotations;

namespace LotComAPI.Entities;

/// <summary>
/// Defines the Database entity structure of a Print.
/// </summary>
public class Print(int ProcessId, int PartId, int Quantity, int? SecondaryQuantity, int? TertiaryQuantity, int Shift, int? SecondaryShift, int? TertiaryShift, string Operator, string? SecondaryOperator, string? TertiaryOperator, int? JBKNumber, string? LotNumber, int? DieNumber, int? DeburrJBKNumber, string? ModelNumber, string? HeatNumber, string ProductionDate)
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
    public int ProcessId { get; set; } = ProcessId;

    [Required]
    public int PartId { get; set; } = PartId;

    [Required]
    public int Quantity { get; set; } = Quantity;

    public int? SecondaryQuantity { get; set; } = SecondaryQuantity;

    public int? TertiaryQuantity { get; set; } = TertiaryQuantity;

    [Required]
    [MaxLength(1)]
    public int Shift { get; set; } = Shift;

    [MaxLength(1)]
    public int? SecondaryShift { get; set; } = SecondaryShift;

    [MaxLength(1)]
    public int? TertiaryShift { get; set; } = TertiaryShift;

    [Required]
    [MaxLength(3)]
    public string Operator { get; set; } = Operator;

    [MaxLength(3)]
    public string? SecondaryOperator { get; set; } = SecondaryOperator;

    [MaxLength(3)]
    public string? TertiaryOperator { get; set; } = TertiaryOperator;

    [MaxLength(3)]
    public int? JBKNumber { get; set; } = JBKNumber;

    [MaxLength(3)]
    public string? LotNumber { get; set; } = LotNumber;

    [MaxLength(3)]
    public int? DieNumber { get; set; } = DieNumber;

    [MaxLength(3)]
    public int? DeburrJBKNumber { get; set; } = DeburrJBKNumber;

    [MaxLength(3)]
    public string? ModelNumber { get; set; } = ModelNumber;

    public string? HeatNumber { get; set; } = HeatNumber;

    [Required]
    public string ProductionDate { get; set; } = ProductionDate;
}