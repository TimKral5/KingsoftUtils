using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Kingsoft.Utils.Graphics;
using Kingsoft.Utils.Math;
using Kingsoft.Utils.Math.Physics;
using Kingsoft.Utils.Math.Physics.Newton;
using Newtonsoft.Json;
using Kingsoft.Utils.TypeExtensions;
using Kingsoft.Utils.Langs.Lexer;
using Kingsoft.Utils.Langs.Lexer.Basic.Schemes;
using System.Text.RegularExpressions;
using Kingsoft.Utils.Langs.Lexer.Basic;
using System.Threading;
using Kingsoft.Utils.Http;
using Kingsoft.Utils.KsConsole;
using Kingsoft.Utils.KsConsole.Components;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            new ConsoleRenderer.Tools().Draw(20, 0, ConsoleColor.DarkBlue);
            Console.SetCursorPosition(10, 0);
            Console.ReadKey(true);

            while (true) { }
        }
    }
}
