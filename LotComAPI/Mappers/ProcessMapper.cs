using LotCom.DataAccess.Models;
using LotComAPI.Entities;
using LotComAPI.Models;

namespace LotComAPI.Mappers;

public static class ProcessMapper
{
    /// <summary>
    /// Performs mapping of values from Models.ProcessDto to Entities.ProcessEntity.
    /// </summary>
    /// <param name="Dto"></param>
    /// <returns></returns>
    public static ProcessEntity DtoToEntity(ProcessDto Dto)
    {
        ProcessEntity Entity = new ProcessEntity
        (
            Dto.LineCode,
            Dto.LineName,
            Dto.Title,
            Dto.Serialization,
            Dto.Type,
            Dto.Origination,
            Dto.PassThroughType,
            Dto.DoesPrint,
            Dto.DoesScan,
            Dto.UsesJBKNumber,
            Dto.UsesLotNumber,
            Dto.UsesDieNumber,
            Dto.UsesDeburrJBKNumber,
            Dto.UsesHeatNumber,
            Dto.Previous1,
            Dto.Previous2
        );
        Entity.Id = Dto.Id;
        return Entity;
    }

    /// <summary>
    /// Performs mapping of values from Entities.ProcessEntity to Models.ProcessDto.
    /// </summary>
    /// <param name="Entity"></param>
    /// <returns></returns>
    public static ProcessDto EntityToDto(ProcessEntity Entity)
    {
        ProcessDto Dto = new ProcessDto
        (
            Entity.LineCode,
            Entity.LineName,
            Entity.Title,
            Entity.Serialization,
            Entity.Type,
            Entity.Origination,
            Entity.PassThroughType,
            Entity.DoesPrint,
            Entity.DoesScan,
            Entity.UsesJBKNumber,
            Entity.UsesLotNumber,
            Entity.UsesDieNumber,
            Entity.UsesDeburrJBKNumber,
            Entity.UsesHeatNumber,
            Entity.Previous1,
            Entity.Previous2
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
    public static void EntityToEntity(ProcessEntity Original, ProcessEntity New)
    {
        Original.LineCode = New.LineCode;
        Original.LineName = New.LineName;
        Original.Title = New.Title;
        Original.Serialization = New.Serialization;
        Original.Type = New.Type;
        Original.Origination = New.Origination;
        Original.PassThroughType = New.PassThroughType;
        Original.DoesPrint = New.DoesPrint;
        Original.DoesScan = New.DoesScan;
        Original.UsesJBKNumber = New.UsesJBKNumber;
        Original.UsesLotNumber = New.UsesLotNumber;
        Original.UsesDieNumber = New.UsesDieNumber;
        Original.UsesDeburrJBKNumber = New.UsesDeburrJBKNumber;
        Original.UsesHeatNumber = New.UsesHeatNumber;
        Original.Previous1 = New.Previous1;
        Original.Previous2 = New.Previous2;
    }

    /// <summary>
    /// Maps values from a Data Access Object (DAO) to an Entities.ProcessEntity object.
    /// </summary>
    /// <param name="Dao"></param>
    /// <returns></returns>
    public static ProcessEntity DaoToEntity(ProcessDao Dao)
    {
        return new ProcessEntity
        (
            Dao.LineCode,
            Dao.LineName,
            Dao.Title,
            Dao.Serialization,
            Dao.Type,
            Dao.Origination,
            Dao.PassThroughType,
            Dao.DoesPrint,
            Dao.DoesScan,
            Dao.UsesJBKNumber,
            Dao.UsesLotNumber,
            Dao.UsesDieNumber,
            Dao.UsesDeburrJBKNumber,
            Dao.UsesHeatNumber,
            Dao.Previous1,
            Dao.Previous2
        );
    }
}