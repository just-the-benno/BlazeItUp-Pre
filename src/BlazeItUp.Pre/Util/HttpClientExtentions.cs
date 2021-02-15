using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazeItUp.Pre.Util
{
    public static class HttpClientExtentions
    {
        public async static Task<String> GetAsStringAsync(this HttpClient client, String url)
        {
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            String content = await response.Content.ReadAsStringAsync();
            return content;
        }

    }
}
