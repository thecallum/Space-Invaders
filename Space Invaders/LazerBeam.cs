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

        public int width { get; private set; } = 6;
        public int height { get; private set; } = 18;

        public enum LazerDirection
        {
            up = -1,
            down = 1,
        }

        public readonly LazerDirection direction;

        public LazerBeam(int x, int y, LazerDirection direction)
        {
            this.x = x + 50 - 3;
            this.y = y;
            this.direction = direction;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Blue, x, y, width, height);
        }

        public bool Update()
        {
            y += ((int) direction * 6);

            // if (direction == LazerDirection.down && y > )

            if (direction == LazerDirection.up && y < -10)
                return false;

            return true;
        }
    }
}
