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
        public Alien_SpaceShip(Rectangle position, int column)
            : base(position, column, 4, 40)
        {
            imageList = new List<Image>();

            imageList.Add(Properties.Resources.spaceship1);
            imageList.Add(Properties.Resources.spaceship2);
            imageList.Add(Properties.Resources.spaceship3);
            imageList.Add(Properties.Resources.spaceship4);

            ToggleImage();
        }
    }
}
