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

    class Game
    {
        Player player;
        private int score = 0;

        private int width;
        private int height;

        Form1 form;

        private List<LazerBeam> userBeams = new List<LazerBeam>();


        private List<Alien> enemies = new List<Alien>();


        public Game(int width, int height, Form1 form)
        {
            this.width = width;
            this.height = height;

            this.form = form;

            player = new Player();

            AddEnemies();
        }

        private void AddEnemies()
        {
            int gap = 20;

            for (int y = 0; y < 3; y ++)
                for (int x = 0; x < 10; x++)
                {
                    int alienX = (x * (40 + gap)) + gap;
                    int alienY = (y * (40 + gap)) + gap;

                    enemies.Add(new Alien(alienX, alienY));
                }
        }

        public void MovePlayer(Direction direction)
        {
            Console.WriteLine("Player Move: " + direction);
            player.Move(direction);
           
        }

        public void FireShot()
        {
            userBeams.Add(
                new LazerBeam(player.x, player.y, LazerBeam.LazerDirection.up)
            );
        }

        public void Update()
        {
            foreach (LazerBeam beam in userBeams.ToList())
                beam.Update();

            foreach (Alien enemy in enemies.ToList())
            {
                LazerBeam beamThatHitEnemy = enemy.HitWithLazerBeam(userBeams);
                if (beamThatHitEnemy != null)
                {
                    enemies.Remove(enemy);
                    userBeams.Remove(beamThatHitEnemy);
                }
            }

            if (enemies.Count() == 0)
            {
                MyGlobalEvent.FireMyEvent(new EventArgs());
            }
        }

        public void Draw(PaintEventArgs e)
        {
            Bitmap bitmap = new Bitmap(800, 600);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                player.Draw(g);

                foreach (Alien enemy in enemies)
                    enemy.Draw(g);

                foreach (LazerBeam beam in userBeams)
                    beam.Draw(g);
            }

            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
        }
    }
}
