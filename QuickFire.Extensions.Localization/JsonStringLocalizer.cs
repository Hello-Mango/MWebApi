using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using QuickFire.Extensions.Localization.Json.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using QuickFire.Extensions.Localization.Json.Caching;
using IResourceNamesCache = QuickFire.Extensions.Localization.Json.Caching.IResourceNamesCache;

namespace QuickFire.Extensions.Localization.Json
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly ConcurrentDictionary<string, object> _missingManifestCache = new ConcurrentDictionary<string, object>();
        //private readonly ConcurrentDictionary<string, string> mainfestCache = new();
        private readonly JsonResourceManager _jsonResourceManager;
        private readonly IResourceStringProvider _resourceStringProvider;
        private readonly ILogger _logger;

        private string _searchedLocation = string.Empty;

        public JsonStringLocalizer(
            JsonResourceManager jsonResourceManager,
            IResourceNamesCache resourceNamesCache,
            ILogger logger)
            : this(jsonResourceManager,
                new JsonStringProvider(resourceNamesCache, jsonResourceManager),
                logger)
        {
        }

        public JsonStringLocalizer(
            JsonResourceManager jsonResourceManager,
            IResourceStringProvider resourceStringProvider,
            ILogger logger)
        {
            _jsonResourceManager = jsonResourceManager ?? throw new ArgumentNullException(nameof(jsonResourceManager));
            _resourceStringProvider = resourceStringProvider ?? throw new ArgumentNullException(nameof(resourceStringProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public LocalizedString this[string name]
        {
            get
            {
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var value = GetStringSafely(name, null);

                return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var format = GetStringSafely(name, null);
                var value = string.Format(format ?? name, arguments);

                return new LocalizedString(name, value, resourceNotFound: format == null, searchedLocation: _searchedLocation);
            }
        }

        public virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
            GetAllStrings(includeParentCultures, CultureInfo.CurrentUICulture);

        protected virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures, CultureInfo culture)
        {
            if (culture is null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            var resourceNames = includeParentCultures
                ? GetResourceNamesFromCultureHierarchy(culture).AsEnumerable()
                : _resourceStringProvider.GetAllResourceStrings(culture, true);

            foreach (var name in resourceNames)
            {
                var value = GetStringSafely(name, culture);
                yield return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        protected string? GetStringSafely(string name, CultureInfo? culture)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }


            var keyCulture = culture ?? CultureInfo.CurrentUICulture;
            var cacheKey = $"name={name}&culture={keyCulture.Name}";

            _logger.SearchedLocation(name, _jsonResourceManager.ResourcesFilePath, keyCulture);

            if (_missingManifestCache.ContainsKey(cacheKey))
            {
                return null;
            }
            try
            {
                string? res = string.Empty;
                if (culture == null)
                {
                    res = _jsonResourceManager.GetString(name);
                }
                else
                {
                    res = _jsonResourceManager.GetString(name, culture);
                }
                return res;
            }
            catch (MissingManifestResourceException)
            {
                _missingManifestCache.TryAdd(cacheKey, string.Empty);

                return null;
            }
        }

        private HashSet<string> GetResourceNamesFromCultureHierarchy(CultureInfo startingCulture)
        {
            var currentCulture = startingCulture;
            var resourceNames = new HashSet<string>();

            while (currentCulture != currentCulture.Parent)
            {
                var cultureResourceNames = _resourceStringProvider.GetAllResourceStrings(currentCulture, false);

                if (cultureResourceNames != null)
                {
                    foreach (var resourceName in cultureResourceNames)
                    {
                        resourceNames.Add(resourceName);
                    }
                }

                currentCulture = currentCulture.Parent;
            }

            return resourceNames;
        }
    }
}