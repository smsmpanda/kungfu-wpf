using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KungfuWpf.Game
{
    public class SnakePart
    {
        public bool IsHead { get; set; }

        public UIElement UiElement { get; set; }

        public Point Position { get; set; }
    }
}
