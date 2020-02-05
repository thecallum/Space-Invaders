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

        public int x { get; private set; }
        public int y { get; private set; }

        private readonly int MIN_X = 10;
        private readonly int MAX_X = 680;

        public Player()
        {
            x = (800 / 2) - (width / 2);
            y = 600 - height - 50;
        }

        public void Draw(Graphics g)
        {
            Console.WriteLine("Draw: " + x + ", " + y);
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
            Console.WriteLine("Move");
            if (direction == Direction.Right)
                x += 10;
            else
                x -= 10;

            if (x > MAX_X) x = MAX_X;
            if (x < MIN_X) x = MIN_X;
        }

    }
}
