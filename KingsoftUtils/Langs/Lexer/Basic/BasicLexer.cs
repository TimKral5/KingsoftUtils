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

        List<LxToken> ILexer.RunLexer(string s, char[] e)
        {
            Dictionary<string, object> _runtimeVariables = new Dictionary<string, object>
            {
                ["lexer:vars:eot"] = e
            };

            List<LxToken> tokens = new List<LxToken>();
            char[] _t = s.ToCharArray();
            for (int i = 0; i < _t.Length; i++)
            {
                _runtimeVariables["lexer:vars:char"] = _t[i];

                foreach (var schema in Schemes)
                {
                    _runtimeVariables["lexer:vars:previous_tokens"] = tokens.ToArray();
                    LxSchemaArgs _args = new LxSchemaArgs() { Lexer = this };

                    _runtimeVariables["lexer:vars:args"] = _args;
                    _args = _runtimeVariables;

                    (bool, LxToken) res = schema.CheckSchema(new LxSchemaArgs() {
                        Lexer = this
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
