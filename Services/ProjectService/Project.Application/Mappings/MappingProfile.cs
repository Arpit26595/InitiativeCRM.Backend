using AutoMapper;

namespace Project.Application.Mappings;

/// <summary>
/// Base mapping profile that can be extended by specific entity profiles
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Apply all mappings from specific profiles
        ApplyMappingsFromAssembly();
    }

    private void ApplyMappingsFromAssembly()
    {
        // This will automatically discover and apply all IMapFrom<T> implementations
        var assembly = typeof(MappingProfile).Assembly;

        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}