using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactManager.Common.Helper
{
    public static class Extension
    {
        public static string ToJsonString<T>(this T value)
        {
            return JsonConvert.SerializeObject(value);
        }

    }
}
