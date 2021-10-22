using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingsoft.Utils.BaseSystem;
using Kingsoft.Utils.Http;
using System.Net;
using System.Net.Http;
using System.IO;

namespace Test
{
	class Program
	{
		class ObjectX
		{
			public string response { get; set; }
			public string status { get; set; }
		}

		static void Main(string[] args)
		{
			HttpServer.Init();

			HttpServer.Get("/test", (string[][] vs) => 
			{ 
				return new string[2] { "{\"status\": \"200\"}", "application/json" }; 
			});

			HttpServer.Get("/test2", (string[][] vs) => 
			{ 
				return new string[2] { File.ReadAllText("test.html"), "text/html" }; 
			});

			HttpServer.Post("/test3", (string[][] vs) => 
			{
				Dictionary<string, string> dict = vs[1].HttpQueryToDictionary();
				string res =
					$"<h3>{dict["fname"]}</h3>"+
					$"<h3>{dict["lname"]}</h3>";

				return new string[2] { res, "text/html" };
			});

			int port = 19136;

			string[] urls = new string[2]
			{
				$"http://127.0.0.1:{port}/",
				$"http://192.168.3.87:{port}/"
			};

			HttpServer.Run(urls);
        }
	}
}
