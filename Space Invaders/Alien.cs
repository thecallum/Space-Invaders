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
        private int width = 40;
        private int height = 40;

        public Alien(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Purple, x, y, width, height);
        }

        public LazerBeam HitWithLazerBeam(List<LazerBeam> beams)
        {
            foreach (LazerBeam beam in beams)
            {
                if (beam.x - beam.width > this.x && 
                    beam.x < this.x + this.width &&
                    beam.y + beam.height >= this.y &&
                    beam.y  <= this.y + this.height)
                    return beam;

            }
            return null;

        }
    }
}
