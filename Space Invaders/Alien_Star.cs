using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    class Alien_Star : Alien
    {
        public Alien_Star(Rectangle position, int column)
            : base(position, column, 5, 50)
        {
            imageList = new List<Image>();

            imageList.Add(Properties.Resources.star1);
            imageList.Add(Properties.Resources.star2);
            imageList.Add(Properties.Resources.star3);
            imageList.Add(Properties.Resources.star4);

            ToggleImage();
        }
    }
}
