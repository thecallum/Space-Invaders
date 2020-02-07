using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    class Alien
    {
        public int x { get; private set; }
        public int y { get; private set; }

        public static int width { get; private set; } = 40;
        public static int height { get; private set; } = 40;

        public int health { get; private set; }

        private int imageState = 0;

        private Image image;
    
        public Alien(int x, int y)
        {
            this.x = x;
            this.y = y;
            health = 1;
            ToggleImage();
        }

        public void Update()
        {
            if (AlienGroup.direction == Direction.Right)
                x += AlienGroup.speed;
            else
                x -= AlienGroup.speed;
        }

        public bool HitByLazerBeam(LazerBeam beam)
        {
            if (beam.x - beam.width > this.x &&
                    beam.x < this.x + Alien.width &&
                    beam.y + beam.height >= this.y &&
                    beam.y <= this.y + Alien.height)
            {
                this.health--;
                return true;
            }

            return false;
        }

        public bool AtRightBoundary()
        {
            return x + width > Game.windowWidth - 10;
        }

        public bool AtLeftBoundary()
        {
            return x < 10;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, x, y, width, height);
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
