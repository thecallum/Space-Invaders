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
        public Alien_Star(Rectangle position)
            : base(position, 5, 50)
        {
            Image[] imageList = new Image[] {
                Properties.Resources.star1,
                Properties.Resources.star2,
                Properties.Resources.star3,
                Properties.Resources.star4,
            };

            imageAnimation = new ImageAnimation(imageList);
        }
    }
}
