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

			HttpServer.Get("/", (MethodArgs vs) => 
			{ 
				return new string[2] { "{\"status\": \"200\"}", "application/json" }; 
			});

			int port = 1111;

			string[] urls = new string[1]
			{
				$"http://127.0.0.1:{port}/"
			};

			HttpServer.Run(urls);
        }
	}
}
