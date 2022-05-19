using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Http
{
    public class TWebClient : WebClient
    {
        //10 secs default
        public int Timeout { get; set; } = 10000;

        //for sync requests
        protected override WebRequest GetWebRequest(Uri uri)
        {
            var w = base.GetWebRequest(uri);
            w.Timeout = Timeout; //10 seconds timeout
            return w;
        }

        //the above will not work for async requests :(
        //let's create a workaround by hiding the method
        //and creating our own version of DownloadStringTaskAsync
        public async Task<string> TDownloadStringTaskAsync(Uri address)
        {
            var t = DownloadStringTaskAsync(address);
            if (await Task.WhenAny(t, Task.Delay(Timeout)) != t) //time out!
            {
                CancelAsync();
            }
            return await t;
        }

        public async Task<string> TUploadStringTaskAsync(Uri address, string data)
        {
            var t = UploadStringTaskAsync(address, data);
            if (await Task.WhenAny(t, Task.Delay(Timeout)) != t) //time out!
            {
                CancelAsync();
            }
            return await t;
        }
    }
}
