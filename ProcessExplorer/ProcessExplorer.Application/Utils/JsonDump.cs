using Newtonsoft.Json;
using System;

namespace ProcessExplorer.Application.Utils
{
    public static class JsonDump
    {
        public static void Dump(object obj)
        {
            if (obj == null)
                return;

            Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
        }
    }
}
