﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace QuickFire.Extensions.Localization.Json.Internal
{

    public class JsonResourceManager
    {
        private readonly JsonFileWatcher _jsonFileWatcher;
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _resourcesCache = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
        private readonly ConcurrentDictionary<string, string> _hitKeyCache = new ConcurrentDictionary<string, string>();


        public JsonResourceManager(string resourcesPath, string? resourceName = null)
        {
            ResourcesPath = resourcesPath;
            ResourceName = resourceName;

            _jsonFileWatcher = new JsonFileWatcher(resourcesPath);
            _jsonFileWatcher.Changed += RefreshResourcesCache;
        }

        public string? ResourceName { get; }

        public string ResourcesPath { get; }

        public string ResourcesFilePath { get; private set; }

        public virtual ConcurrentDictionary<string, string>? GetResourceSet(CultureInfo culture, bool tryParents)
        {
            TryLoadResourceSet(culture);

            var key = $"{ResourceName}.{culture.Name}";
            if (!_resourcesCache.ContainsKey(key))
            {
                return null;
            }

            if (tryParents)
            {
                var allResources = new ConcurrentDictionary<string, string>();
                do
                {
                    if (_resourcesCache.TryGetValue(key, out var resources))
                    {
                        foreach (var entry in resources)
                        {
                            allResources.TryAdd(entry.Key, entry.Value);
                        }
                    }

                    culture = culture.Parent;
                } while (culture != CultureInfo.InvariantCulture);

                return allResources;
            }
            else
            {
                _resourcesCache.TryGetValue(key, out var resources);

                return resources;
            }
        }

        public virtual string? GetString(string name)
        {
            var culture = CultureInfo.CurrentUICulture;
            var key = $"{ResourceName}.{culture.Name}";
            if (_hitKeyCache.ContainsKey(key))
            {
                return _hitKeyCache[key];
            }
            if (!_resourcesCache.ContainsKey(key))
            {
                TryLoadResourceSet(culture);
            }
            if (_resourcesCache.IsEmpty)
            {
                return null;
            }
            do
            {
                if (_resourcesCache.TryGetValue(key, out var resources))
                {
                    if (resources.TryGetValue(name, out var value))
                    {
                        _hitKeyCache.TryAdd(key, value.ToString());
                        return value.ToString();
                    }
                }
                culture = culture.Parent;
            } while (culture != culture.Parent);

            return null;
        }

        public virtual string? GetString(string name, CultureInfo culture)
        {
            var key = $"{ResourceName}.{culture.Name}";
            if (_hitKeyCache.ContainsKey(key))
            {
                return _hitKeyCache[key];
            }
            if (!_resourcesCache.ContainsKey(key))
            {
                TryLoadResourceSet(culture);
            }

            if (_resourcesCache.IsEmpty)
            {
                return null;
            }

            if (!_resourcesCache.TryGetValue(key, out var resources))
            {
                return null;
            }
            bool flag = resources.TryGetValue(name, out string? value);
            if (flag)
            {
                _hitKeyCache.TryAdd(key, value!.ToString());
                return value.ToString();
            }
            else
            {
                return null;
            }
        }

        private void TryLoadResourceSet(CultureInfo culture)
        {
            if (string.IsNullOrEmpty(ResourceName))
            {
                var file = Path.Combine(ResourcesPath, $"{culture.Name}.json");

                TryAddResources(file);
            }
            else
            {
                var resourceFiles = Enumerable.Empty<string>();
                var rootCulture = culture.Name[..2];
                if (ResourceName.Contains('.'))
                {
                    resourceFiles = Directory.EnumerateFiles(ResourcesPath, $"{ResourceName}.{rootCulture}*.json");

                    if (!resourceFiles.Any())
                    {
                        resourceFiles = GetResourceFiles(rootCulture);
                    }
                }
                else
                {
                    resourceFiles = GetResourceFiles(rootCulture);
                }

                foreach (var file in resourceFiles)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    var cultureName = fileName[(fileName.LastIndexOf('.') + 1)..];

                    culture = CultureInfo.GetCultureInfo(cultureName);

                    TryAddResources(file);
                }
            }

            IEnumerable<string> GetResourceFiles(string culture)
            {
                var resourcePath = ResourceName.Replace('.', Path.AltDirectorySeparatorChar);
                var resourcePathLastDirectorySeparatorIndex = resourcePath.LastIndexOf(Path.AltDirectorySeparatorChar);
                var resourceName = resourcePath[(resourcePathLastDirectorySeparatorIndex + 1)..];
                var resourcesPath = resourcePathLastDirectorySeparatorIndex == -1
                    ? ResourcesPath
                    : Path.Combine(ResourcesPath, resourcePath[..resourcePathLastDirectorySeparatorIndex]);

                return Directory.Exists(resourcesPath)
                    ? Directory.EnumerateFiles(resourcesPath, $"{resourceName}.{culture}*.json")
                    : new List<string>();
            }

            void TryAddResources(string resourceFile)
            {
                var key = $"{ResourceName}.{culture.Name}";
                if (!_resourcesCache.ContainsKey(key))
                {
                    var resources = JsonResourceLoader.Load(resourceFile);

                    _resourcesCache.TryAdd(key, new ConcurrentDictionary<string, string>(resources));
                }
            }
        }

        private void RefreshResourcesCache(object sender, FileSystemEventArgs e)
        {
            var key = Path.GetFileNameWithoutExtension(e.FullPath);
            if (_resourcesCache.TryGetValue(key, out var resources))
            {
                if (!resources.IsEmpty)
                {
                    resources.Clear();
                    _hitKeyCache.Clear();
                    var freshResources = JsonResourceLoader.Load(e.FullPath);

                    foreach (var item in freshResources)
                    {
                        _resourcesCache[key].TryAdd(item.Key, item.Value);
                    }
                }
            }
        }
    }
}
