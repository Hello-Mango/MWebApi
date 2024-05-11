using Microsoft.Extensions.Localization;

namespace QuickFireApi.Extensions.Localization.Json;

public class JsonLocalizationOptions : LocalizationOptions
{
    public ResourcesType ResourcesType { get; set; } = ResourcesType.TypeBased;
}