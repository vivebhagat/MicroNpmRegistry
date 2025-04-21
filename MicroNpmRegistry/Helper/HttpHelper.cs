using System.Text;

namespace MicroNpmRegistry.Helper
{
    public static class HttpHelper
    {
        internal static async Task<string> GetRequestBodyAsync(HttpRequest httpRequest)
        {
            // Enable buffering so we can read the request body multiple times
            httpRequest.EnableBuffering();

            // Read the raw body string
            string body;
            using (var reader = new StreamReader(httpRequest.Body, Encoding.UTF8, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
            }

            // Reset the stream position so other middleware can still read it
            httpRequest.Body.Position = 0;

            return body;
        }
    }
}
