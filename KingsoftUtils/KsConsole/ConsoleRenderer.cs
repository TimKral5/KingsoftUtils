using Kingsoft.Utils.TypeExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.KsConsole
{
    public static class ConsoleRenderer
    {

        public class Tools
        {

            public void Draw(int x, int y, ConsoleColor color)
            {
                Console.SetCursorPosition(x, y);
                ConsoleColor _col = Console.BackgroundColor;
                Console.BackgroundColor = color;
                Console.Write(" ");
                Console.BackgroundColor = _col;
            }
        }

        public static void Render(Components.ICRComponent component)
        {
            component.Render(new ConsoleRenderer.Tools());
            Console.CursorVisible = false;
            Console.CursorLeft = 0;
        }
    }
}
