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
        public static int speed { get; private set; } = 1;
        public int count { get { return row_1.Count + row_2.Count + row_3.Count; } }

        private List<Alien> row_1;
        private List<Alien> row_2;
        private List<Alien> row_3;

        public AlienGroup()
        {
            row_1 = new List<Alien>();
            row_2 = new List<Alien>();
            row_3 = new List<Alien>();

            FillAlienRow(row_1, 0);
            FillAlienRow(row_2, 1);
            FillAlienRow(row_3, 2);
        }

        private void FillAlienRow(List<Alien> row, int rowNum)
        {
            for (int i = 0; i < columnCount; i++)
            {
                int alienX = (i * (Alien.width + AlienGroup.gap)) + AlienGroup.gap;
                int alienY = (rowNum * (Alien.height + AlienGroup.gap)) + AlienGroup.gap;
                row.Add(new Alien(alienX, alienY));
            }
        }

        public void Update()
        {
            // Check if alien furthest to right

            if (GroupAtBoundary())
            {
                if (AlienGroup.direction == Direction.Right)
                    AlienGroup.direction = Direction.Left;
                else
                    AlienGroup.direction = Direction.Right;
            }

            UpdateRow(row_1);
            UpdateRow(row_2);
            UpdateRow(row_3);
        }

        private bool GroupAtBoundary()
        {
            if (AlienGroup.direction == Direction.Right) {
                // check all furthest right aliens
                if (row_1.Count > 0 && row_1[row_1.Count - 1].AtRightBoundary()) return true;
                if (row_2.Count > 0 && row_2[row_2.Count - 1].AtRightBoundary()) return true;
                if (row_3.Count > 0 && row_3[row_3.Count - 1].AtRightBoundary()) return true;
            } else {
                // check all furthest left aliens
                if (row_1.Count > 0 && row_1[0].AtLeftBoundary()) return true;
                if (row_2.Count > 0 && row_2[0].AtLeftBoundary()) return true;
                if (row_3.Count > 0 && row_3[0].AtLeftBoundary()) return true;
            }
            return false;
         }

        private void UpdateRow(List<Alien> row)
        {
            foreach (Alien alien in row)
                alien.Update();
        }

        public bool FindAlienHitByLazerBeam(LazerBeam beam)
        {
            if (FindAlienHitByLazerBeamInRow(row_3, beam) != null)
                return true;

            if (FindAlienHitByLazerBeamInRow(row_2, beam) != null)
                return true;

            if (FindAlienHitByLazerBeamInRow(row_1, beam) != null)
                return true;

            return false;
        }

        private LazerBeam FindAlienHitByLazerBeamInRow(List<Alien> row, LazerBeam beam)
        {
            foreach (Alien alien in row.ToArray())
            {
                if (beam.x - beam.width > alien.x &&
                    beam.x < alien.x + Alien.width &&
                    beam.y + beam.height >= alien.y &&
                    beam.y <= alien.y + Alien.height)
                {
                    row.Remove(alien);
                    return beam;
                }
            }
                return null;
        }

        public void Draw(Graphics g)
        {
            DrawRow(row_1, g);
            DrawRow(row_2, g);
            DrawRow(row_3, g);
        }

        private void DrawRow(List<Alien> row, Graphics g)
        {
            foreach (Alien alien in row)
                alien.Draw(g);
        }

        public void ToggleAnimation()
        {
            ToggleAnimationRow(row_1);
            ToggleAnimationRow(row_2);
            ToggleAnimationRow(row_3);
        }

        private void ToggleAnimationRow(List<Alien> row)
        {
            foreach (Alien alien in row)
                alien.ToggleImage();
        }
    }
}
