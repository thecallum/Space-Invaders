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
        private Image image;

        public int width { get; private set; } = 80;
        public int height { get; private set; } = 80;
        public int health { get; private set; } = 3;

        private readonly int speed = 8;

        public int x { get; private set; }
        public int y { get; private set; }

                public Player()
        {
            x = (Game.windowWidth + width) /2;
            y = Game.windowHeight - height - 10;

            image = ResizeImage(Properties.Resources.player, new Size(80, 80));
        
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, x, y, width, height);
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
