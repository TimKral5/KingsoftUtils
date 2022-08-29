using System;
using System.Net;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Http
{
    public class TWebClient : WebClient
    {
        //10 secs default
        public int Timeout { get; set; } = 10000;

        protected override WebRequest GetWebRequest(Uri uri)
        {
            var w = base.GetWebRequest(uri);
            w.Timeout = Timeout; //10 seconds timeout
            return w;
        }

        public async Task<string> TDownloadStringTaskAsync(Uri address)
        {
            var t = DownloadStringTaskAsync(address);
            if (await Task.WhenAny(t, Task.Delay(Timeout)) != t)
            {
                CancelAsync();
            }
            return await t;
        }

        public async Task<string> TUploadStringTaskAsync(Uri address, string data)
        {
            var t = UploadStringTaskAsync(address, data);
            if (await Task.WhenAny(t, Task.Delay(Timeout)) != t)
            {
                CancelAsync();
            }
            return await t;
        }
    }
}
