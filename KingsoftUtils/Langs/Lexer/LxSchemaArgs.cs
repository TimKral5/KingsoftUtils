using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kingsoft.Utils.Langs.Lexer.Basic.Schemes;

namespace Kingsoft.Utils.Langs.Lexer
{
    public class LxSchemaArgs
    {
        public LxSchemaArgs()
        {
            runtimeVariables = new Dictionary<string, object>();
        }

        private Dictionary<string, object> runtimeVariables { get; set; }

        public object this[string index]
        {
            get => runtimeVariables.ContainsKey(index) ? runtimeVariables[index] : null;
            //get => runtimeVariables[index];
            set => runtimeVariables[index] = value;
        }

        public bool CheckForEOT(char str)
        {
            bool res = false;
            char[] eot = (char[]) runtimeVariables["lexer:vars:eot"];

            for (int i = 0; i < eot.Length; i++)
            {
                if (str == eot[i]) res = true;
            }

            return res;
        }

        public static implicit operator LxSchemaArgs(Dictionary<string, object> dict)
        {
            LxSchemaArgs res = (LxSchemaArgs) dict["lexer:vars:args"];
            res.runtimeVariables = dict;
            return res;
        }

        public ILexer Lexer { get; set; }
    }
}
