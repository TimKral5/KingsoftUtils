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
using Kingsoft.Utils.Langs.Lexer;
using Kingsoft.Utils.Langs.Lexer.Basic.Schemes;
using System.Text.RegularExpressions;
using Kingsoft.Utils.Langs.Lexer.Basic;

namespace Test
{
    class StringSchema : ILxSchema
    {
        private bool openNumber = false;
        private string currentNumber = "";
        private Regex invokeRegex = new Regex(@"^[0-9]$");
        private Regex continueRegex = new Regex(@"^-?[0-9][0-9,\.]+$");

        (bool, LxToken) ILxSchema.CheckSchema(LxSchemaArgs args)
        {
            (bool, LxToken) result = (false, new LxToken());

            

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ILexer lexer = new BasicLexer();

            lexer.RegisterSchema(new BasicKeywordSchema("var", "var")); // keyword to create a new variable
            lexer.RegisterSchema(new BasicKeywordSchema("=", "equals")); // assignment operator
            lexer.RegisterSchema(new BasicKeywordSchema(";", "eos")); // (e)nd (o)f (s)tatement (semicolon)
            lexer.RegisterSchema(new BasicKeywordSchema("(", "bovc")); // (b)egining (o)f a (v)ariable-(c)ontainer
            lexer.RegisterSchema(new BasicKeywordSchema(")", "eovc")); // (e)nd (o)f a (v)ariable-(c)ontainer
            lexer.RegisterSchema(new BasicStringSchema());
            lexer.RegisterSchema(new BasicNumberSchema());
            //lexer.RegisterSchema(new StringSchema());

            string input = string.Join("\n",
                "var test = '5.5';",
                "print(\"test\");"
                );

            List<LxToken> res = lexer.RunLexer(input);

            Console.WriteLine(res.json(Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine(input);
            Console.ReadLine();
        }
    }
}
