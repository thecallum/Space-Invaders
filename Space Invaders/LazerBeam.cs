using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    public class LazerBeam
    {
        public readonly int x;
        public int y { get; private set; }

        public static int speed { get; private set; } = 6;

        public int width { get; private set; } = 2;
        public int height { get; private set; } = 20;

        public enum LazerDirection
        {
            up = -1,
            down = 1,
        }

        public readonly LazerDirection direction;

        public LazerBeam(int playerX, int playerY, int playerWidth, int playerHeight, LazerDirection direction)
        {
            x = playerWidth /2 + playerX -1;
            y = playerY; 
            this.direction = direction;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Red, x, y, width, height);
        }

        public bool Update()
        {
            y += ((int) direction * LazerBeam.speed);

            // if (direction == LazerDirection.down && y > )

            if (direction == LazerDirection.up && y < -10)
                return false;

            return true;
        }
    }
}
