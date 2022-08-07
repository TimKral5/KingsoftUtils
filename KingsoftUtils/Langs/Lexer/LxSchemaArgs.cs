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
        public char Char { get; set; }
        public LxToken[] PreviousTokens { get; set; }
        public char[] EOT 
        {
            get => _eot;
            set => _eot = value;
        }
        private char[] _eot { get; set; }

        public bool CheckForEOT(char str)
        {
            bool res = false;
            for (int i = 0; i < EOT.Length; i++)
            {
                if (str == EOT[i]) res = true;
            }

            return res;
        }

        public LexerRuntimeVariables RuntimeVariables { get; set; }

        public ILexer Lexer { get; set; }
    }
}
