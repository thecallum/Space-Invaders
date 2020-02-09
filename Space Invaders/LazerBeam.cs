using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    public abstract class LazerBeam
    {
        public Rectangle position { get; protected set; }
        public static int speed { get; private set; } = 6;
        public readonly Direction direction;
        public readonly Brush color;

        public enum Direction
        {
            up = -1,
            down = 1,
        }

        public LazerBeam(Rectangle userPosition, Direction direction, Brush color)
        {
            this.position = new Rectangle(
                userPosition.Width / 2 + userPosition.X - 1,
                userPosition.Y,
                2,
                12    
            );

            this.direction = direction;
            this.color = color;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(color, position);
        }

        public void Update(List<LazerBeam> beams)
        {
            Rectangle newPosition = position;

            newPosition.Y += (int) direction * speed;
            
            if (direction == Direction.up && newPosition.Y < -newPosition.Height)
                beams.Remove(this);

            if (direction == Direction.down && newPosition.Y > Game.windowHeight - newPosition.Height)
                beams.Remove(this);

            this.position = newPosition;
        }
    }
}
