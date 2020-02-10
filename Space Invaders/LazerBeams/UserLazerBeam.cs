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
}
