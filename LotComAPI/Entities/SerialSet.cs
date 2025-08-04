using System.ComponentModel.DataAnnotations;

namespace LotComAPI.Entities;

/// <summary>
/// Defines the Database entity structure of a Serial Set for a Part.
/// </summary>
/// <param name="PartId"></param>
/// <param name="NextJBK"></param>
/// <param name="NextLot"></param>
public class SerialSet(int PartId, int NextJBK, int NextLot)
{
    [Key]
    public int PartId { get; set; } = PartId;

    [Required]
    public int NextJBK { get; set; } = NextJBK;

    [Required]
    public int NextLot { get; set; } = NextLot;
}