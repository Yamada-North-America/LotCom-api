using LotComAPI.Entities;
using LotComAPI.Models;

namespace LotComAPI.Mappers;

public static class ProcessMapper
{
    /// <summary>
    /// Converts HTTP arguments into a ProcessDto object.
    /// </summary>
    /// <param name="LineCode"></param>
    /// <param name="LineName"></param>
    /// <param name="Title"></param>
    /// <param name="Serialization"></param>
    /// <param name="Type"></param>
    /// <param name="Origination"></param>
    /// <param name="PassThroughType"></param>
    /// <param name="DoesPrint"></param>
    /// <param name="DoesScan"></param>
    /// <param name="UsesJBKNumber"></param>
    /// <param name="UsesLotNumber"></param>
    /// <param name="UsesDieNumber"></param>
    /// <param name="UsesDeburrJBKNumber"></param>
    /// <param name="UsesHeatNumber"></param>
    /// <param name="Previous1"></param>
    /// <param name="Previous2"></param>
    /// <returns></returns>
    public static ProcessDto HttpToDto(int LineCode, string LineName, string Title, string? Serialization, string Type, int OriginationBit, string? PassThroughType, int DoesPrintBit, int DoesScanBit, int UsesJBKNumberBit, int UsesLotNumberBit, int UsesDieNumberBit, int UsesDeburrJBKNumberBit, int UsesHeatNumberBit, int? Previous1, int? Previous2)
    {
        // convert from bits to bools (0!=0 -> false or x!=0 -> true)
        // consumes all non-zero digits as true
        bool Origination = OriginationBit != 0;
        bool DoesPrint = DoesPrintBit != 0;
        bool DoesScan = DoesScanBit != 0;
        bool UsesJBKNumber = UsesJBKNumberBit != 0;
        bool UsesLotNumber = UsesLotNumberBit != 0;
        bool UsesDieNumber = UsesDieNumberBit != 0;
        bool UsesDeburrJBKNumber = UsesDeburrJBKNumberBit != 0;
        bool UsesHeatNumber = UsesHeatNumberBit != 0;
        // construct and return Dto
        return new ProcessDto
        (
            LineCode,
            LineName,
            Title,
            Serialization,
            Type,
            Origination,
            PassThroughType,
            DoesPrint,
            DoesScan,
            UsesJBKNumber,
            UsesLotNumber,
            UsesDieNumber,
            UsesDeburrJBKNumber,
            UsesHeatNumber,
            Previous1,
            Previous2
        );
    }

    /// <summary>
    /// Performs mapping of values from Models.ProcessDto to Entities.Process.
    /// </summary>
    /// <param name="Dto"></param>
    /// <returns></returns>
    public static Process DtoToEntity(ProcessDto Dto)
    {
        Process Entity = new Process
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
    /// Performs mapping of values from Entities.Process to Models.ProcessDto.
    /// </summary>
    /// <param name="Entity"></param>
    /// <returns></returns>
    public static ProcessDto EntityToDto(Process Entity)
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
}