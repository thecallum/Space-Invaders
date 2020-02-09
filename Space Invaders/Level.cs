using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    public class Level
    {
        public readonly Alien.Type alienType;
        public readonly int maxShots;
        public readonly int chanceOfShot;

        public Level(Alien.Type alienType, int maxShots, int chanceOfShot)
        {
            this.alienType = alienType;
            this.maxShots = maxShots;
            this.chanceOfShot = chanceOfShot;

            Console.WriteLine("Type: " + typeof(Alien_Bug));
        }
    }
}
