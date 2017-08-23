using MonoDragons.Core.Common;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MonoDragons.Core.Networking
{
    public class MyIP
    {
        public Optional<string> CachedIPAddress { get; private set; } = new Optional<string>();

        public async void StartGetIPAddress()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://myexternalip.com/raw");
            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            {
                var stream = response.GetResponseStream();
                var x = Encoding.UTF8.GetString(ExtractResponse(response.ContentLength, response.GetResponseStream()));
                CachedIPAddress = new Optional<string>(x.Substring(0, x.IndexOf("\n")));
            }
        }

        public async Task<string> GetIPAddress()
        {
            if (CachedIPAddress.HasValue)
                return CachedIPAddress.Value;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://myexternalip.com/raw");
            request.Method = "GET";
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            {
                var stream = response.GetResponseStream();
                var x = Encoding.UTF8.GetString(ExtractResponse(response.ContentLength, response.GetResponseStream()));
                CachedIPAddress = new Optional<string>(x.Substring(0, x.IndexOf("/n")));
            }
            return CachedIPAddress.Value;
        }

        private byte[] ExtractResponse(long length, Stream stream)
        {
            byte[] data;
            using (var mstrm = new MemoryStream())
            {
                var tempBuffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = stream.Read(tempBuffer, 0, tempBuffer.Length)) != 0)
                    mstrm.Write(tempBuffer, 0, bytesRead);
                mstrm.Flush();
                data = mstrm.GetBuffer();
            }
            return data;
        }
    }
}
