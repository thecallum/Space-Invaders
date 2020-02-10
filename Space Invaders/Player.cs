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
        public readonly static int width = 40;
        public readonly static int height = 40;

        private int updatesBeforeNextShot = 0;

        public List<LazerBeam> shots = new List<LazerBeam>();

        private Player(Rectangle position)
            : base(position, 8, 3)
        {
            image = Properties.Resources.player;
        }

        public static Player Setup()
        {
            Rectangle position = new Rectangle(
               (Game.windowWidth + width) / 2,
               Game.windowHeight - height - 10,
               width,
               height
           );

            return new Player(position);
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

        public bool CanShoot()
        {
            return updatesBeforeNextShot == 0;
        }

        public void ResetUpdatesBeforeNextShot()
        {
            updatesBeforeNextShot = 10;
        }

        public void DecrementUpdatesBeforeNextShot()
        {
            updatesBeforeNextShot--;
        }

        public void Shoot()
        {
            LazerBeam shot = new UserLazerBeam(position);
            shots.Add(shot);
        }
    }
}
