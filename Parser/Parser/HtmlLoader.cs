using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parser
{
    class HtmlLoader
    {
        HttpClient client = new HttpClient();
        string url = "http://www.tavriav.ua";
        string sourse = null;

        public async Task<string> GetSourse(string prefix)
        {
            var response = await client.GetAsync(url + prefix);

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                sourse = await response.Content.ReadAsStringAsync();
            }
            return sourse;
        }

    }
}
