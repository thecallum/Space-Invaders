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
        public readonly int points;
        protected ImageAnimation imageAnimation;

        public enum Type
        {
            Bug,
            FlyingSaucer,
            Satellite,
            SpaceShip,
            Star,
        }

        public Alien(Rectangle position, int health, int points)
            : base(position, 1, health)
        {
            this.points = points;
        }

        public static Alien Construct(Type type, int column, int row, int gap)
        {
            Rectangle position = new Rectangle(
                (column * (30 + gap)) + gap,
                (row * (30 + gap)) + gap,
                30,
                30
            );

            return ConstructAlienByType(type, position);
        }

        private static Alien ConstructAlienByType(Type type, Rectangle position)
        {
            switch (type)
            {
                case Type.Bug:
                    return new Alien_Bug(position);
                case Type.FlyingSaucer:
                    return new Alien_FlyingSaucer(position);
                case Type.Satellite:
                    return new Alien_Satellite(position);
                case Type.SpaceShip:
                    return new Alien_SpaceShip(position);
                case Type.Star:
                    return new Alien_Star(position);
                default:
                    return null;
            }
        }

        public void Update(bool directionChanged)
        {
            if (directionChanged)
                MoveDownRow();

            Move(AlienFleet.direction);
        }

        private void MoveDownRow()
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
            image = imageAnimation.Next();
        }
    } 
}
