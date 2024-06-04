using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using QuickFire.Extensions.Localization.Json.Internal;
using QuickFire.Extensions.Localization.Json.Caching;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using IResourceNamesCache = QuickFire.Extensions.Localization.Json.Caching.IResourceNamesCache;
using ResourceNamesCache = QuickFire.Extensions.Localization.Json.Caching.ResourceNamesCache;

namespace QuickFire.Extensions.Localization.Json
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IResourceNamesCache _resourceNamesCache = new ResourceNamesCache();
        private readonly ConcurrentDictionary<string, JsonStringLocalizer> _localizerCache = new ConcurrentDictionary<string, JsonStringLocalizer>();
        private readonly string _resourcesRelativePath;
        private readonly ResourcesType _resourcesType = ResourcesType.TypeBased;
        private readonly ILoggerFactory _loggerFactory;

        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> localizationOptions, ILoggerFactory loggerFactory)
        {
            if(localizationOptions is null)
            {
                throw new ArgumentNullException(nameof(localizationOptions));
            }

            _resourcesRelativePath = localizationOptions.Value.ResourcesPath ?? string.Empty;
            _resourcesType = localizationOptions.Value.ResourcesType;
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource is null)
            {
                throw new ArgumentNullException(nameof(resourceSource));
            }

            string resourcesPath = string.Empty;

            var typeInfo = resourceSource.GetTypeInfo();
            var assembly = typeInfo.Assembly;
            var assemblyName = resourceSource.Assembly.GetName().Name;
            var resourceName = $"{assemblyName}.{typeInfo.Name}" == typeInfo.FullName
                ? typeInfo.Name
                : TrimPrefix(typeInfo.FullName!, assemblyName + ".");

            resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _resourcesRelativePath);

            return _localizerCache.GetOrAdd($"culture={CultureInfo.CurrentUICulture.Name}, typeName={resourceName}", _ => CreateJsonStringLocalizer(resourcesPath, resourceName));
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            if (baseName is null)
            {
                throw new ArgumentNullException(nameof(baseName));
            }
            if (location is null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return _localizerCache.GetOrAdd($"baseName={baseName},location={location}", _ =>
            {
                var assemblyName = new AssemblyName(location);
                var assembly = Assembly.Load(assemblyName);
                var resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _resourcesRelativePath);
                string? resourceName = null;
                if (baseName == string.Empty)
                {
                    resourceName = baseName;

                    return CreateJsonStringLocalizer(resourcesPath, resourceName);
                }

                if (_resourcesType == ResourcesType.TypeBased)
                {
                    resourceName = TrimPrefix(baseName, location + ".");
                }

                return CreateJsonStringLocalizer(resourcesPath, resourceName!);
            });
        }

        protected virtual JsonStringLocalizer CreateJsonStringLocalizer(
            string resourcesPath,
            string resourceName)
        {
            var resourceManager = _resourcesType == ResourcesType.TypeBased
                ? new JsonResourceManager(resourcesPath, resourceName)
                : new JsonResourceManager(resourcesPath);
            var logger = _loggerFactory.CreateLogger<JsonStringLocalizer>();

            return new JsonStringLocalizer(resourceManager, _resourceNamesCache, logger);
        }

        private static string TrimPrefix(string name, string prefix)
        {
            if (name.StartsWith(prefix, StringComparison.Ordinal))
            {
                return name[prefix.Length..];
            }
            return name;
        }
    }
}