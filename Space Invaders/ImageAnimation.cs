using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    public class ImageAnimation
    {
        private Image[] images;
        private int position = 0;
        private Direction direction = Direction.Right;

        private enum Direction
        {
            Left,
            Right,
        }

        public ImageAnimation(Image[] imageList)
        {
            images = imageList;
        }

        public Image Next()
        {
            Image image = images[position];

            if (direction == Direction.Right) {
                position++;
                if (AtEndOfList()) direction = Direction.Left;
            } else {
                position--;
                if (AtStartOfList()) direction = Direction.Right;
            }

            return image;
        }

        private bool AtEndOfList()
        {
            return position == images.Count() - 1;
        }

        private bool AtStartOfList()
        {
            return position == 0;
        }
    }
}
