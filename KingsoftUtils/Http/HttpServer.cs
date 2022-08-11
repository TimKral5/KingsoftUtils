using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kingsoft.Utils.TypeExtensions.Arrays;
using Kingsoft.Utils.TypeExtensions.Dictionaries;
using Newtonsoft.Json;

namespace Kingsoft.Utils.Http
{

    public static class StandartErrorCodes
    {
        
    }

    public class HttpServer
    {

        public static class Utils
        {
            public static byte[] StringToData(string s)
            {
                return Encoding.UTF8.GetBytes(s);
            }

            public static Dictionary<string, string> DecodeQuery(string query)
            {
                Dictionary<string, string> res = new Dictionary<string, string>();
                string[] pairs = query.Split('&');
                pairs.Each(x => res.Add(x.Split('=')));
                return res;
            }

            public static Dictionary<string, string> GetFPostBody(HttpListenerRequest request)
            {
                System.IO.Stream body = request.InputStream;
                Encoding encoding = request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                if (string.IsNullOrEmpty(s)) return new Dictionary<string, string>();
                return DecodeQuery(s);
            }

            public static Dictionary<string, string> GetPostBody(HttpListenerRequest request)
            {
                if (!request.HasEntityBody)
                {
                    throw new Exception("No client data was sent with the request.");
                }
                System.IO.Stream body = request.InputStream;
                Encoding encoding = request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                

                string s = reader.ReadToEnd();

                string boundary = request.ContentType.Split('=')[1];
                string[] paramChunks = s.Split(new string[] { $"--{boundary}" }, StringSplitOptions.None);

                Dictionary<string, string> _body = new Dictionary<string, string>();
                paramChunks.Each((j, chunk) =>
                {
                    if (j == 0 || j == paramChunks.Length - 1) return;
                    string key = chunk.Split('\n')[1].Split(new string[] { $"name=\"" }, StringSplitOptions.None)[1].Split('"')[0];
                    string value = "";
                    chunk.Split('\n').Each((i, l) => value += chunk.Split('\n').Length - 1 > i && i > 2 ? l + (i != chunk.Split('\n').Length - 2 ? "\n" : "") : "");
                    _body[key] = value.Split('\r')[0];
                });

                body.Close();
                reader.Close();
                return _body;
            }
        }

        public Delegates.Templates.DelegateTemplates._4_O1<string[], HttpServer,
            Delegates.Templates.DelegateTemplates._2_Void<HttpServer, (byte[], Encoding, string, long)>,
            HttpListenerRequest, byte[]> Error_404 = (pathArgs, _self, res, req) =>
        {
            byte[] data = Utils.StringToData("<h1>404 - Page not found</h1>");
            res(_self, (data, Encoding.UTF8, "text/html", data.LongLength));
            return data;
        };

        private byte[] _Data;
        private string _ContentType;
        private Encoding _ContentEncoding;
        private long _ContentLength64;
        private Delegates.Templates.DelegateTemplates._2_Void<HttpServer, (byte[], Encoding, string, long)> WriteResponse =
            (self, _data) =>
        {
            (byte[] data, Encoding enc, string contentType, long contentLenght64) = _data;
            self._ContentEncoding = enc;
            self._Data = data;
            self._ContentType = contentType;
            self._ContentLength64 = contentLenght64;
        };

        public HttpServer()
        {
            GeneralRoutes = new List<Route>();
            GetRoutes = new List<Route>();
            PostRoutes = new List<Route>();
            PutRoutes = new List<Route>();
            DeleteRoutes = new List<Route>();
            ErrorCodes = new Dictionary<int, Route>();
            UrlArgs = new List<string>();
        }

        public List<Route> GeneralRoutes { get; set; }
        public List<Route> GetRoutes { get; set; }
        public List<Route> PostRoutes { get; set; }
        public List<Route> PutRoutes { get; set; }
        public List<Route> DeleteRoutes { get; set; }
        public Dictionary<int,Route> ErrorCodes { get; set; }
        public List<string> UrlArgs { get; set; }

        private HttpListener listener;
        private string url;


        public async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                _Data = Utils.StringToData("<h1>500</h1>");
                _ContentType = "text/html";
                _ContentLength64 = _Data.LongLength;
                _ContentEncoding = Encoding.UTF8;

                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse res = ctx.Response;

                //Console.WriteLine(req.RawUrl);
                //Console.WriteLine(req.HttpMethod);
                //Console.WriteLine(req.UserHostName);
                //Console.WriteLine(req.UserAgent);
                //Console.WriteLine();

                bool resultFound = false;

                void iterate(List<Route> routes)
                {
                    routes.ToArray().Each(route =>
                    {
                        UrlArgs = new List<string>();
                        if (resultFound) return;
                        bool testBool = true;
                        string[] Url = req.RawUrl.Split('/');
                        string[] RouteUrl = route.RoutePath.Split('/');
                        if (Url.Length != RouteUrl.Length) return;
                        RouteUrl.Each((x, path) => 
                        {
                            if (!testBool) return;
                            string UrlPart = Url[x];
                            //if(x < Url.Length) UrlPart = Url[x];
                            if (x == Url.Length - 1) UrlPart = UrlPart.Split('?')[0];

                            // scenario-table =
                            // 1: true, 2: true => stop loop; goto next route.
                            // 1: true, 2: false => ignore testBool; add Url to list
                            // 1: false, 2: true => do nothing; continue
                            // 1: false, 2: false => ignore testBool; add Url to list
                            if (path != UrlPart && path != "<var>" ) testBool = false;
                            if (path == "<var>") UrlArgs.Add(UrlPart);
                        });

                        if (testBool)
                        {
                            _Data = route.Function(UrlArgs.ToArray(), this, WriteResponse, req);
                            resultFound = true;
                        }
                    });
                }

                iterate(GeneralRoutes);

                switch (req.HttpMethod.ToLower())
                {
                    case "get":
                        iterate(GetRoutes);
                        break;
                    case "post":
                        iterate(GetRoutes);
                        break;
                    case "put":
                        iterate(PutRoutes);
                        break;
                    case "delete":
                        iterate(DeleteRoutes);
                        break;
                    default:
                        break;
                }

                if (!resultFound) _Data = (ErrorCodes.TryGetValue(404, out Route _route) ? _route.Function : Error_404)
                        (UrlArgs.ToArray(), this, WriteResponse, req);

                res.ContentType = _ContentType;
                res.ContentEncoding = _ContentEncoding;
                res.ContentLength64 = _ContentLength64;

                await res.OutputStream.WriteAsync(_Data, 0, _Data.Length);
                res.Close();
            }
        }


        public void RunServer(int port)
        {
            url = $"http://+:{port}/";
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            listener.Close();
        }
    }
}
