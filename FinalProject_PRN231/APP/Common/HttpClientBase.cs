using System.Text;

namespace APP.Common
{
    public class HttpClientBase
    {
        HttpClient client;
        public HttpClientBase()
        {
            client = new HttpClient();
        }
        public async Task<string> SendRequest(string uri, HttpMethod method, string content = null)
        {
            try
            {
                var request = new HttpRequestMessage();
                request.Method = method;
                request.RequestUri = new Uri(uri);
                if (content != null)
                {
                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                }
                HttpResponseMessage response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"An occured error when get data. Code status: {response.StatusCode}");
                }
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
    }
}
