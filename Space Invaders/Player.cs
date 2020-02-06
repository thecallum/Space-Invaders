using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders
{
    class Player
    {
        // private Bitmap image = new Bitmap(Properties.Resources.player);

        private int width = 100;
        private int height = 100;

        private readonly int speed = 10;

        public int x { get; private set; }
        public int y { get; private set; }

        public Player()
        {
            x = (Game.windowWidth + width) /2;
            y = Game.windowHeight - height - 10;
        }

        public void Draw(Graphics g)
        {
           //  Console.WriteLine("Draw: " + x + ", " + y);
            //pictureBox.Location = new Point(x, y);

                g.FillRectangle(Brushes.Red, x, y, width, height);

            /*
            Bitmap bm = new Bitmap(100, 100);

            using (Graphics g = Graphics.FromImage(bm))
            {
                // do normal rendering
            }

            e.Graphics.DrawImage(bm);

    */
        }

        public void Move(Direction direction)
        {
            if (direction == Direction.Right)
                x += speed;
            else
                x -= speed;

            if (x > Game.windowWidth - width -10)
                x = Game.windowWidth - width -10;

            if (x < 10)
                x = 10;
        }

    }
}
