using AutoMapper;

namespace Project.Application.Mappings;

/// <summary>
/// Generic interface for mapping configuration
/// Implement this interface in DTOs to define their mapping rules
/// </summary>
/// <typeparam name="T">The source type to map from</typeparam>
public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}