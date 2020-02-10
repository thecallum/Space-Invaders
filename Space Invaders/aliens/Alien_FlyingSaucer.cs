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
        public Alien_FlyingSaucer(Rectangle position)
            : base(position, 2, 20)
        {
            Image[] imageList = new Image[] {
                Properties.Resources.flyingsaucer1,
                Properties.Resources.flyingsaucer2,
                Properties.Resources.flyingsaucer3,
                Properties.Resources.flyingsaucer4,
            };

            imageAnimation = new ImageAnimation(imageList);
        }
    }
}
