using LotComAPI.Entities;
using LotComAPI.Models;

namespace LotComAPI.Mappers;

public static class PartMapper
{
    /// <summary>
    /// Converts HTTP arguments into a PartDto object.
    /// </summary>
    /// <param name="Number"></param>
    /// <param name="PrintedBy"></param>
    /// <param name="ScannedBy"></param>
    /// <param name="Name"></param>
    /// <param name="ModelCode"></param>
    /// <returns></returns>
    public static PartDto HttpToDto(string Number, int PrintedBy, int ScannedBy, string Name, string ModelCode)
    {
        return new PartDto(Number, PrintedBy, ScannedBy, Name, ModelCode);
    }

    /// <summary>
    /// Performs mapping of values from Models.PartDto to Entities.Part. 
    /// </summary>
    /// <param name="Dto"></param>
    /// <returns></returns>
    public static Part DtoToEntity(PartDto Dto)
    {
        Part Entity = new Part
        (
            Dto.Number,
            Dto.PrintedBy,
            Dto.ScannedBy,
            Dto.Name,
            Dto.ModelCode
        );
        Entity.Id = Dto.Id;
        return Entity;
    }

    /// <summary>
    /// Performs mapping of values from Entities.Part to Models.PartDto.
    /// </summary>
    /// <param name="Entity"></param>
    /// <returns></returns>
    public static PartDto EntityToDto(Part Entity)
    {
        PartDto Dto = new PartDto
        (
            Entity.Number,
            Entity.PrintedBy,
            Entity.ScannedBy,
            Entity.Name,
            Entity.ModelCode
        );
        Dto.Id = Entity.Id;
        return Dto;
    }
}