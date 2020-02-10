using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class LevelManager
    {
        private Queue<Level> levels;

        public uint round { get; private set; }

        public LevelManager()
        {
            levels = new Queue<Level>();

            levels.Enqueue(new Level(Alien.Type.Bug, 5, 50));
            levels.Enqueue(new Level(Alien.Type.FlyingSaucer, 10, 40));
            levels.Enqueue(new Level(Alien.Type.Satellite, 15, 30));
            levels.Enqueue(new Level(Alien.Type.SpaceShip, 20, 20));
            levels.Enqueue(new Level(Alien.Type.Star, 25, 20));
        }

        public AlienFleet NextLevel()
        {
            if (!MoreLevels()) return null;

            round++;
            Level nextLevel = levels.Dequeue();
            return new AlienFleet(nextLevel);
        }

        public bool MoreLevels()
        {
            return levels.Count() > 0;
        }
    }
}
