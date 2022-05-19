using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Http
{
    public class Route
    {
        public Route(string path, Delegates.Templates.DelegateTemplates._4_O1<string[], HttpServer,
            Delegates.Templates.DelegateTemplates._2_Void<HttpServer, (byte[], Encoding, string, long)>,
            HttpListenerRequest,
            byte[]> func)
        {
            RoutePath = path;
            Function = func;
        }
        public string RoutePath { get; set; }
        public Delegates.Templates.DelegateTemplates._4_O1<string[], HttpServer,
            Delegates.Templates.DelegateTemplates._2_Void<HttpServer, (byte[], Encoding, string, long)>, 
            HttpListenerRequest, 
            byte[]> Function { get; set; }
    }
}
