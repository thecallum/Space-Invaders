using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    public class UserLazerBeam : LazerBeam
    {
        public UserLazerBeam(Rectangle userPosition)
            : base(userPosition, Direction.up, Brushes.Red)
        {

        }
    }

    public class EnemyLazerBeam : LazerBeam
    {
        public EnemyLazerBeam(Rectangle userPosition)
            :  base(userPosition, Direction.down, Brushes.Yellow)
        {

        }
    }
    
    public abstract class LazerBeam
    {
        public readonly int x;
        public int y { get; private set; }

        public static int speed { get; private set; } = 12;

        public readonly int width = 2;
        public readonly int height = 20;
        public readonly Direction direction;
        public readonly Brush color;

        public enum Direction
        {
            up = -1,
            down = 1,
        }

        public LazerBeam(Rectangle userPosition, Direction direction, Brush color)
        {
            x = userPosition.Width / 2 + userPosition.X - 1;
            y = userPosition.Y;

            this.direction = direction;
            this.color = color;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(color, x, y, width, height);
        }

        public void Update(List<LazerBeam> beams)
        {
            y += (int) direction * speed;
            
            if (direction == Direction.up && y < -height)
                beams.Remove(this);

            if (direction == Direction.down && y > Game.windowHeight - height)
                beams.Remove(this);
        }
    }
}
