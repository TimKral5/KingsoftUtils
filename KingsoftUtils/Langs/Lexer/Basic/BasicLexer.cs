using Kingsoft.Utils.TypeExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Langs.Lexer.Basic
{
    public class BasicLexer : ILexer
    {
        private List<ILxSchema> Schemes { get; set; }

        public BasicLexer()
        {
            Schemes = new List<ILxSchema>();
        }

        ILexer ILexer.RegisterSchema(ILxSchema schema)
        {
            Schemes.Add(schema);
            return this;
        }

        List<LxToken> ILexer.RunLexer(string s)
        {
            LexerRuntimeVariables _runtimeVariables = new LexerRuntimeVariables();

            List<LxToken> tokens = new List<LxToken>();
            char[] _t = s.ToCharArray();
            for (int i = 0; i < _t.Length; i++)
            {
                foreach (var schema in Schemes)
                {
                    (bool, LxToken) res = schema.CheckSchema(new LxSchemaArgs() { 
                        Char = _t[i],
                        PreviousTokens = tokens.ToArray(),
                        RuntimeVariables = _runtimeVariables
                    });

                    if (res.Item1) 
                        tokens.Add(res.Item2);

                }
            }

            Console.WriteLine(Schemes.json());
            return tokens;
        }
    }
}
