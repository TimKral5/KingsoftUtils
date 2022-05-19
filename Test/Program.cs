using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Kingsoft.Utils.Graphics;
using Kingsoft.Utils.Math.Physics;
using Kingsoft.Utils.Math.Physics.Newton;
using Newtonsoft.Json;
using Kingsoft.Utils.TypeExtensions;
using Kingsoft.Utils.KConsole;

namespace Test
{
	class Program
	{

		static void Main(string[] args)
		{
			HtmlEngine.RunHtml(@""+
"<main>"+
"	<chart size='60' lbl='Hello World !!'>"+
"		<item name='hello' value='15' color='{\"r\":0,\"g\":0,\"b\":255}'></item>" +
"		<item name='world' value='12'></item>"+
"	</chart>"+
"	<text markup=''>test... LOL!!</text>" +
"	<chart size='60' lbl='Hello World !!'>" +
"		<item name='hello' value='15' color='{\"r\":0,\"g\":0,\"b\":255}'></item>" +
"		<item name='world' value='12'></item>" +
"	</chart>" +
"</main>");
			Console.ReadLine();
		}
	}
}
