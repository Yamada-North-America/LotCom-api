using LotCom.DataAccess.Models;
using LotComAPI.Entities;
using LotComAPI.Models;

namespace LotComAPI.Mappers;

public static class PartMapper
{
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

    /// <summary>
    /// Maps New properties to the corresponding Original properties (in essence, updating the object).
    /// </summary>
    /// <param name="Original"></param>
    /// <param name="New"></param>
    /// <returns></returns>
    public static void EntityToEntity(Part Original, Part New)
    {
        Original.Number = New.Number;
        Original.PrintedBy = New.PrintedBy;
        Original.ScannedBy = New.ScannedBy;
        Original.Name = New.Name;
        Original.ModelCode = New.ModelCode;
    }

    /// <summary>
    /// Maps values from a Data Access Object (DAO) to a Entities.Part object.
    /// </summary>
    /// <param name="Dao"></param>
    /// <returns></returns>
    public static Part DaoToEntity(PartDao Dao)
    {
        return new Part
        (
            Dao.Number,
            Dao.PrintedBy,
            Dao.ScannedBy,
            Dao.Name,
            Dao.ModelCode
        );
    }
}