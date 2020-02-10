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
        public Alien_Bug(Rectangle position)
            : base(position, 1, 10)
        {
            Image[] imageList = new Image[] {
                Properties.Resources.bug1,
                Properties.Resources.bug2,
                Properties.Resources.bug3,
                Properties.Resources.bug4,
            };

            imageAnimation = new ImageAnimation(imageList);
        }
    }
}
