using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    class Alien_FlyingSaucer : Alien
    {
        public Alien_FlyingSaucer(Rectangle position, int column)
            : base(position, column, 2, 20)
        {
            imageList = new List<Image>();

            imageList.Add(Properties.Resources.flyingsaucer1);
            imageList.Add(Properties.Resources.flyingsaucer2);
            imageList.Add(Properties.Resources.flyingsaucer3);
            imageList.Add(Properties.Resources.flyingsaucer4);

            ToggleImage();
        }
    }
}
