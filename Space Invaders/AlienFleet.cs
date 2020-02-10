using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Space_Invaders
{
    class AlienFleet
    {
        private readonly int columnCount = 10;
        public static int gap { get; private set; } = 20;
        public static Direction direction { get; private set; } = Direction.Right;

        public int count { get {
                return row_1.Count(s => s != null) + row_2.Count(s => s != null) + row_3.Count(s => s != null);
        }}

        private readonly int maxShots;
        private readonly int chanceOfShot;

        public List<LazerBeam> shots = new List<LazerBeam>();

        private Alien[] row_1 = new Alien[10];
        private Alien[] row_2 = new Alien[10];
        private Alien[] row_3 = new Alien[10];

        private Random random = new Random();

        public bool CanShoot(int currentShotCount)
        {
            if (currentShotCount >= maxShots) return false;
            if (random.Next(chanceOfShot) == 0) return true;
            return false;
        }

        public AlienFleet(Level level)
        {
            maxShots = level.maxShots;
            chanceOfShot = level.chanceOfShot;

            FillAlienRow(row_1, 0, level.alienType);
            FillAlienRow(row_2, 1, level.alienType);
            FillAlienRow(row_3, 2, level.alienType);
        }

        private void FillAlienRow(Alien[] row, int rowNum, Alien.Type alienType)
        {
            for (int column = 0; column < columnCount; column++)
                row[column] = Alien.Construct(alienType, column, rowNum, gap);
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

        private bool RowEmpty(Alien[] row)
        {
            return row.Count(s => s != null) == 0;
        }

        public bool AliensAtBottom()
        {
            if (!RowEmpty(row_3)) return RowAtBottomBoundary(row_3);
            if (!RowEmpty(row_2)) return RowAtBottomBoundary(row_2);
            if (!RowEmpty(row_1)) return RowAtBottomBoundary(row_1);

            return false;
        }


        private bool RowAtBottomBoundary(Alien[] row)
        {
            for (int column = 0; column < row.Count(); column++)
            {
                if (row[column] == null) continue;
                if (row[column].AtBottomBoundary()) return true;
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
            for (int column = 0; column < row.Count(); column++)
            {
                if (row[column] == null) continue;
                if (row[column].AtLeftBoundary()) return true;
            }

            return false;
        }

        private bool RowAtRightBoundary(Alien[] row)
        {
            for (int column = row.Count() - 1; column > 0; column--)
            {
                if (row[column] == null) continue;
                if (row[column].AtRightBoundary()) return true;
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
            for (int column = 0; column < row.Count(); column++)
            {
                if (row[column] != null && row[column].HitByLazerBeam(beam))
                {
                    if (row[column].health == 0)
                    {
                        UpdateScoreEvent.FireMyEvent(row[column].points);
                        row[column] = null;
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
                if (row_3[i] != null)
                    firstRow.Add(row_3[i]);
                else if (row_2[i] != null)
                    firstRow.Add(row_2[i]);
                else if (row_1[i] != null)
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
