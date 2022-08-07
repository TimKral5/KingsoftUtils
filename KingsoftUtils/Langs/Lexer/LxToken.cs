using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Langs.Lexer
{
    public struct LxToken
    {
        public LxToken(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; set; }
        public string Value { get; set; }
    }
}
