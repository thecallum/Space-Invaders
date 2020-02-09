using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    class Alien_Bug : Alien
    {
        public Alien_Bug(Rectangle position, int column)
            : base(position, column, 1, 10)
        {
            imageList = new List<Image>();

            imageList.Add(Properties.Resources.bug1);
            imageList.Add(Properties.Resources.bug2);
            imageList.Add(Properties.Resources.bug3);
            imageList.Add(Properties.Resources.bug4);

            ToggleImage();
        }
    }
}
