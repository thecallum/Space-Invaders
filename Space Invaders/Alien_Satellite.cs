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
        public Alien_Satellite(Rectangle position, int column)
            : base(position, column, 3, 30)
        {
            imageList = new List<Image>();

            imageList.Add(Properties.Resources.satellite1);
            imageList.Add(Properties.Resources.satellite2);
            imageList.Add(Properties.Resources.satellite3);
            imageList.Add(Properties.Resources.satellite4);

            ToggleImage();
        }
    }
}
