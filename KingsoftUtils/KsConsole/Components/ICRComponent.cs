using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.KsConsole.Components
{
    public interface ICRComponent
    {
        void Render(ConsoleRenderer.Tools tools);
        int Width { get; set; }
        int Height{ get; set; }
    }
}
