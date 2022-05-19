using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Kingsoft.Utils.Graphics
{
    public class GraphicsHandler : Form
    {
        public void CreateWindow(string title, int height, int width)
        {
            Application.Run(this);
            SuspendLayout();
            ClientSize = new Size(width, height);
            Name = title;
            ResumeLayout(false);
        }
    }
}
