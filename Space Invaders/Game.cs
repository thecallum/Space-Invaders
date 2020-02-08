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
        Form1 form;
        Player player;

        private int score = 0;
        private List<LazerBeam> userShots = new List<LazerBeam>();
        private List<LazerBeam> enemyShots = new List<LazerBeam>();

        private AlienGroup enemies;

        public static int windowWidth { get; private set; }
        public static int windowHeight { get; private set; }

        private int updatesBeforeNextShot = 0;
        
        public Game(int windowWidth, int windowHeight, Form1 form)
        {
            Game.windowWidth = windowWidth;
            Game.windowHeight = windowHeight;

            this.form = form;

            UpdateScoreEvent.MyEvent += UpdateScoreMethod;

            player = new Player();
            enemies = new AlienGroup();

        }

        public void MovePlayer(Direction direction)
        {
            player.Move(direction);          
        }

        private void UpdateScoreMethod(UpdateScoreEventArgs e)
        {
            score += e.value;
            form.UpdateScore(score);
        }

        public void FireShot()
        {
            if (updatesBeforeNextShot == 0) {
                updatesBeforeNextShot = 10;

                Rectangle userPosition = new Rectangle(player.x, player.y, player.width, player.height);

                userShots.Add(new UserLazerBeam(userPosition));
            }
        }

        public void Update()
        {
            if (updatesBeforeNextShot > 0) updatesBeforeNextShot--;

            foreach (LazerBeam beam in userShots.ToArray())
                beam.Update(userShots);

            enemies.Update();

            foreach (LazerBeam beam in userShots.ToList())
                if (enemies.FindAlienHitByLazerBeam(beam))
                    userShots.Remove(beam);

            if (enemies.CanShoot(enemyShots.Count))
                enemies.FireShot(enemyShots);

            foreach (LazerBeam beam in enemyShots.ToArray())
                beam.Update(enemyShots);
               
            if (enemies.count == 0) GameEndedEvent.FireMyEvent();
        }

        public void Draw(PaintEventArgs e)
        {
            Bitmap bitmap = new Bitmap(800, 600);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                enemies.Draw(g);

                foreach (LazerBeam beam in userShots)
                    beam.Draw(g);

                foreach (LazerBeam beam in enemyShots)
                    beam.Draw(g);

                player.Draw(g);
            }

            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
        }

        public void ToggleAnimation()
        {
            enemies.ToggleAnimation();
        }
    }
}
