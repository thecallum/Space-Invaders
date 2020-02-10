using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    public class EnemyLazerBeam : LazerBeam
    {
        public EnemyLazerBeam(Rectangle userPosition)
            : base(userPosition, Direction.down, Brushes.Yellow)
        {

        }
    }
}
