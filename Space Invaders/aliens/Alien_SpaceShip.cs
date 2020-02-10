using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    class Alien_SpaceShip : Alien
    {
        public Alien_SpaceShip(Rectangle position)
            : base(position, 4, 40)
        {
            Image[] imageList = new Image[] {
                Properties.Resources.spaceship1,
                Properties.Resources.spaceship2,
                Properties.Resources.spaceship3,
                Properties.Resources.spaceship4,
            };

            imageAnimation = new ImageAnimation(imageList);
        }
    }
}
