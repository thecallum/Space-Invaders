using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    public abstract class Movable
    {
        public Rectangle position { get; protected set; }
        public int health { get; protected set; }
        public readonly int speed;
        protected Image image;

        public Movable(Rectangle position, int speed, int health)
        {
            this.position = position;
            this.speed = speed;
            this.health = health;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, position);
        }

        public bool AtRightBoundary()
        {
            return position.X + position.Width > Game.windowWidth - 10;
        }

        public bool AtLeftBoundary()
        {
            return position.X < 10;
        }

        public virtual void Move(Direction direction)
        {
            Rectangle newPosition = this.position;

            if (direction == Direction.Right)
                newPosition.X += speed;
            else
                newPosition.X -= speed;

            this.position = newPosition;
        }

        public bool HitByLazerBeam(LazerBeam beam)
        {
            if (!beam.position.IntersectsWith(this.position))
                return false;

            this.health--;
            return true;
        }
    }
}
