using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kingsoft.Utils.BaseSystem;

namespace Kingsoft.Utils.Http
{

    public class MethodArgs
	{
        public string[] UriArgs { get; set; }
        public Dictionary<string, string> RequestBody { get; set; }
	}

    public static class HttpServer
    {
        public static void Init()
		{
            GET_Paths = new Dictionary<string, Func<MethodArgs, string[]>>();
            PUT_Paths = new Dictionary<string, Func<MethodArgs, string[]>>();
            POST_Paths = new Dictionary<string, Func<MethodArgs, string[]>>();
            PATCH_Paths = new Dictionary<string, Func<MethodArgs, string[]>>();
            DELETE_Paths = new Dictionary<string, Func<MethodArgs, string[]>>();
        }

        public static void Run(string[] urls)
		{
            listener = new HttpListener();
			for (int i = 0; i < urls.Length; i++)
			{
                listener.Prefixes.Add(urls[i]);
            }
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", urls[0]);

            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();
            
			listener.Close();
        }

        public static HttpListener listener;

        public static Dictionary<string, Func<MethodArgs, string[]>> GET_Paths { get; set; }
        public static Dictionary<string, Func<MethodArgs, string[]>> PUT_Paths { get; set; }
        public static Dictionary<string, Func<MethodArgs, string[]>> POST_Paths { get; set; }
        public static Dictionary<string, Func<MethodArgs, string[]>> PATCH_Paths { get; set; }
        public static Dictionary<string, Func<MethodArgs, string[]>> DELETE_Paths { get; set; }

        public static string errorData (string error)
		{
            return "<!DOCTYPE>" +
                "<html>" +
                "  <head>" +
                "    <title>Kingsoft-HTTP: 404</title>" +
                "  </head>" +
                "  <body>" +
                $"    <h2>{error}</h2>" +
                "  </body>" +
                "</html>";
        }

        private static Func<MethodArgs, string[]> Method(Dictionary<string, Func<MethodArgs, string[]>> dict, string path, 
            Func<MethodArgs, string[]> func=null)
		{
            dict.Add(path, func);
            return func;
		}

        public static Func<MethodArgs, string[]> Get(string path, Func<MethodArgs, string[]> func = null) => 
            Method(GET_Paths, path, func);
        public static Func<MethodArgs, string[]> Post(string path, Func<MethodArgs, string[]> func = null) => 
            Method(POST_Paths, path, func);
        public static Func<MethodArgs, string[]> Patch(string path, Func<MethodArgs, string[]> func = null) => 
            Method(PATCH_Paths, path, func);
        public static Func<MethodArgs, string[]> Delete(string path, Func<MethodArgs, string[]> func = null) => 
            Method(DELETE_Paths, path, func);

        public static string[] GetPath(string inp, Dictionary<string, Func<MethodArgs, string[]>> dict, string[] body)
		{
			foreach (KeyValuePair<string, Func<MethodArgs, string[]>> item in dict)
			{
                bool isCorrect = true;
                string[] aItem = item.Key.Split('/');
                string[] aInp = inp.Split('/');
                int x = 
                    aItem.Length < aInp.Length
                    ? aInp.Length - aItem.Length + aInp.Length
                    : aItem.Length - aInp.Length + aItem.Length;
				string[] aArgs = new string[x];
				for (int i = 0; i < x && isCorrect; i++)
				{
                    if (aItem[i] != "[<var>]") { if (aItem[i] != aInp[i]) isCorrect = false; }
                    else aArgs[i] = aInp[i];
                }

                if (isCorrect) return item.Value(
                    new MethodArgs { UriArgs = aArgs, RequestBody = body.HttpQueryToDictionary() }); ;
			}
            return new string[2] { errorData("null"), "text/html" };
		}
        public static string[] GetRequestData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
            string s = reader.ReadToEnd();
            body.Close();
            reader.Close();
            return s.Split('&');
        }

        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine();

				Dictionary<string, Func<MethodArgs, string[]>> reqMethod;
				switch (req.HttpMethod)
				{
                    case "POST":
                        reqMethod = POST_Paths;

                        break;
                    case "GET":
					default:
                        reqMethod = GET_Paths;
						break;
				}

                string[] result = GetPath(req.Url.AbsolutePath, reqMethod, GetRequestData(req));

                byte[] data = Encoding.UTF8.GetBytes(result[0]);
                resp.ContentType = result[1];
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }
    }
}
