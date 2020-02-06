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
    
        public Alien(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Update()
        {
            if (AlienGroup.direction == Direction.Right)
                x += AlienGroup.speed;
            else
                x -= AlienGroup.speed;
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
            g.FillRectangle(Brushes.Purple, x, y, width, height);
        }

    }
}
