using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    public abstract class Alien : Movable
    {
        public readonly int column;
        public readonly int points;

        private int imagePosition = 0;
        private bool imageDirection = true;

        protected List<Image> imageList;

        public enum Type
        {
            Bug,
            FlyingSaucer,
            Satellite,
            SpaceShip,
            Star,
        }

        public Alien(Rectangle position, int column, int health, int points)
            : base(position, 1, health)
        {
            this.column = column;
            this.points = points;
        }

        public void Update(bool directionChanged)
        {
            if (directionChanged)
                MoveDown();

            Move(AlienGroup.direction);
        }

        private void MoveDown()
        {
            Rectangle newPosition = position;
            newPosition.Y += 20;
            position = newPosition;
        }

        public bool AtBottomBoundary()
        {
            return position.Y + position.Height > Game.windowHeight;
        }

        public void ToggleImage()
        {
            image = imageList[imagePosition];

            if (imageDirection) {
                imagePosition++;
                if (imagePosition == imageList.Count - 1)
                    imageDirection = false;
            } else {
                imagePosition--;
                if (imagePosition == 0)
                    imageDirection = true;
            }
        }

    }
}
