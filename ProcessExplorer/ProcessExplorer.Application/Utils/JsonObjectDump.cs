using Newtonsoft.Json;
using System;

namespace ProcessExplorer.Application.Utils
{
    public static class JsonObjectDump
    {
        public static string Dump(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
