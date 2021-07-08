using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace InstagramDraw.Helpers
{
    public static class JsonConverterHelper
    {
        /// <summary>
        /// Converts the json.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static async Task<dynamic> ConvertJson(string path)
        {
            using (var httpClient = new HttpClient())
            {
                var json = await httpClient.GetStringAsync(path);

                dynamic data = JObject.Parse(json);
                return data;
            }
        }

    }
}