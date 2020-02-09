using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    class AlienGroup
    {
        private readonly int columnCount = 10;
        public static int gap { get; private set; } = 20;
        public static Direction direction { get; private set; } = Direction.Right;

        public int count { get {
                return row_1.Count(s => s != null) + row_2.Count(s => s != null) + row_3.Count(s => s != null);
        }}

        // private readonly 
        private readonly int maxShots;
        private readonly int chanceOfShot;

        private Alien[] row_1 = new Alien[10];
        private Alien[] row_2 = new Alien[10];
        private Alien[] row_3 = new Alien[10];

        private Random random = new Random();


        public Alien ConstructAlien(Rectangle position, int column, Alien.Type alienType)
        {
            switch(alienType)
            {
                case Alien.Type.Bug:
                    return new Alien_Bug(position, column);
                case Alien.Type.FlyingSaucer:
                    return new Alien_FlyingSaucer(position, column);
                case Alien.Type.Satellite:
                    return new Alien_Satellite(position, column);
                case Alien.Type.SpaceShip:
                    return new Alien_SpaceShip(position, column);
                case Alien.Type.Star:
                    return new Alien_Star(position, column);
                default:
                    return null;
            }
        }
       


        public bool CanShoot(int currentShotCount)
        {
            if (currentShotCount >= maxShots) return false;
            if (random.Next(chanceOfShot) == 0) return true;
            return false;
        }

        public AlienGroup(Level level)
        {
            maxShots = level.maxShots;
            chanceOfShot = level.chanceOfShot;

            FillAlienRow(row_1, 0, level.alienType);
            FillAlienRow(row_2, 1, level.alienType);
            FillAlienRow(row_3, 2, level.alienType);
        }

        private void FillAlienRow(Alien[] row, int rowNum, Alien.Type alienType)
        {
            for (int i = 0; i < columnCount; i++)
            {
                Rectangle position = new Rectangle(
                    (i * (30 + gap)) + gap,
                    (rowNum * (30 + gap)) + gap,
                    30,
                    30
                );

                row[i] = ConstructAlien(position, i, alienType);
            }
        }

        public void Update()
        {
            bool directionChanged = GroupAtBoundary();
            if (directionChanged) ToggleDirection();

            UpdateRow(row_1, directionChanged);
            UpdateRow(row_2, directionChanged);
            UpdateRow(row_3, directionChanged);

        }


        private void UpdateRow(Alien[] row, bool directionChanged)
        {
            for (int i = 0; i < row.Count(); i++)
                if (row[i] != null)
                    row[i].Update(directionChanged);
        }

        public bool AliensAtBottom()
        {
            if (row_3.Count(s => s != null) > 0)
            {
                if (RowAtBottom(row_3))
                    return true;
                return false;
            }

            if (row_2.Count(s => s != null) > 0)
            {
                if (RowAtBottom(row_2))
                    return true;
                return false;
            }

            if (row_1.Count(s => s != null) > 0)
            {
                if (RowAtBottom(row_1))
                    return true;
                return false;
            }

            return false;
        }


        private bool RowAtBottom(Alien[] row)
        {
            for (int i = 0; i < row.Count(); i++)
            {
                if (row[i] == null) continue;
                if (row[i].AtBottomBoundary()) return true;
            }
            return false;
        }

        private void ToggleDirection()
        {
            if (direction == Direction.Right)
                direction = Direction.Left;
            else
                direction = Direction.Right;
        }

        private bool GroupAtBoundary()
        {
            if (direction == Direction.Right) {
                if (RowAtRightBoundary(row_1)) return true;
                if (RowAtRightBoundary(row_2)) return true;
                if (RowAtRightBoundary(row_3)) return true;
            } else {
                if (RowAtLeftBoundary(row_1)) return true;
                if (RowAtLeftBoundary(row_2)) return true;
                if (RowAtLeftBoundary(row_3)) return true;
            }
            return false;
         }

        private bool RowAtLeftBoundary(Alien[] row)
        {
            // find furthest left alien
            for (int i = 0; i < row.Count(); i++)
            {
                if (row[i] == null) continue;
                if (row[i].AtLeftBoundary()) return true;
            }

            return false;
        }

        private bool RowAtRightBoundary(Alien[] row)
        {
            // find furthest right alien
            for (int i = row.Count() - 1; i > 0; i--)
            {
                if (row[i] == null) continue;
                if (row[i].AtRightBoundary()) return true;
            }

            return false;
        }

        public bool FindAlienHitByLazerBeam(LazerBeam beam)
        {
            if (FindAlienHitByLazerBeamInRow(row_3, beam)) return true;
            if (FindAlienHitByLazerBeamInRow(row_2, beam)) return true;
            if (FindAlienHitByLazerBeamInRow(row_1, beam)) return true;

            return false;
        }

        private bool FindAlienHitByLazerBeamInRow(Alien[] row, LazerBeam beam)
        {
            for (int i = 0; i < row.Count(); i++)
            {
                if (row[i] != null && row[i].HitByLazerBeam(beam))
                {
                    if (row[i].health == 0)
                    {
                        UpdateScoreEvent.FireMyEvent(row[i].points);
                        row[i] = null;
                    }
                    return true;
                }
            }

            return false;
        }

        public void Draw(Graphics g)
        {
            DrawRow(row_1, g);
            DrawRow(row_2, g);
            DrawRow(row_3, g);
        }

        private void DrawRow(Alien[] row, Graphics g)
        {
            for (int i = 0; i < row.Count(); i++)
                if (row[i] != null)
                    row[i].Draw(g);
        }

        public void ToggleAnimation()
        {
            ToggleAnimationRow(row_1);
            ToggleAnimationRow(row_2);
            ToggleAnimationRow(row_3);
        }

        private void ToggleAnimationRow(Alien[] row)
        {
            for (int i = 0; i < row.Count(); i++)
                if (row[i] != null)
                    row[i].ToggleImage();
        }

        public void FireShot(List<LazerBeam> enemyShots)
        {
            if (count == 0) return;

            List<Alien> firstRow = new List<Alien>();

            for (int i = 0; i < 10; i++)
            {
                if (row_3[i] != null && row_3[i].column == i)
                    firstRow.Add(row_3[i]);
                else if (row_2[i] != null && row_2[i].column == i)
                    firstRow.Add(row_2[i]);
                else if (row_1[i] != null && row_1[i].column == i)
                    firstRow.Add(row_1[i]);
                else
                    firstRow.Add(null);
            }

            firstRow.RemoveAll(item => item == null);

            Alien selectedEnemy = firstRow[random.Next(firstRow.Count)];

            enemyShots.Add(new EnemyLazerBeam(selectedEnemy.position));
        }
    }
}
