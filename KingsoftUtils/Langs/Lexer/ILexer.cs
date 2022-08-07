using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Langs.Lexer
{
    public interface ILexer
    {
        ILexer RegisterSchema(ILxSchema schema);
        List<LxToken> RunLexer(string str);
    }
}
