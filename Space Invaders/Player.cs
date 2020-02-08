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
    class Player : Movable
    {
        public Player(Rectangle position)
            : base(position, 8, 3)
        {
            image = Properties.Resources.player;
        }

        public override void Move(Direction direction)
        {
            Rectangle newPosition = this.position;

            if (direction == Direction.Right)
                newPosition.X += speed;
            else
                newPosition.X -= speed;

            if (AtRightBoundary())
                newPosition.X = Game.windowWidth - newPosition.Width -10;

            if (AtLeftBoundary())
                newPosition.X = 10;

            this.position = newPosition;
        }
    }
}
