using System.Collections.Generic;
using System.Globalization;

namespace QuickFireApi.Extensions.Localization.Json.Internal;

public interface IResourceStringProvider
{
    IList<string> GetAllResourceStrings(CultureInfo culture, bool throwOnMissing);
}
