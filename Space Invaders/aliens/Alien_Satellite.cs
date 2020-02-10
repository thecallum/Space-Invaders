using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    class Alien_Satellite : Alien
    {
        public Alien_Satellite(Rectangle position)
            : base(position, 3, 30)
        {
            Image[] imageList = new Image[] {
                Properties.Resources.satellite1,
                Properties.Resources.satellite2,
                Properties.Resources.satellite3,
                Properties.Resources.satellite4,
            };

            imageAnimation = new ImageAnimation(imageList);
        }
    }
}
