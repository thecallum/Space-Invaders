using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    class Alien : Movable
    {
        public readonly int column;
        public readonly int points;

        private int imageState = 0;

        public Alien(Rectangle position, int column)
            : base(position, 1, 1)
        {
            this.column = column;

            points = 10;
            ToggleImage();
        }

        public void Update()
        {
            Move(AlienGroup.direction);
        }

        public void ToggleImage()
        {
            imageState++;

            switch(imageState)
            {
                case 1:
                    image = Properties.Resources.bug1;
                    break;
                case 2:
                    image = Properties.Resources.bug2;
                    break;
                case 3:
                    image = Properties.Resources.bug3;
                    break;
                case 4:
                    image = Properties.Resources.bug4;
                    break;
                case 5:
                    image = Properties.Resources.bug3;
                    break;
                default:
                    image = Properties.Resources.bug2;
                    imageState = 0;
                    break;
            }
        }

    }
}
