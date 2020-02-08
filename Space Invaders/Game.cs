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

            int playerWidth = 80;
            int playerHeight = 80;

            Rectangle playerPosition = new Rectangle(
                (Game.windowWidth + playerWidth) / 2, 
                Game.windowHeight - playerHeight - 10,
                playerWidth,
                playerHeight
            );
        
            player = new Player(playerPosition);
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
            if (updatesBeforeNextShot > 0) return;

            updatesBeforeNextShot = 10;
            userShots.Add(new UserLazerBeam(player.position));
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

            foreach (LazerBeam beam in enemyShots.ToList())
                if (player.HitByLazerBeam(beam))
                {
                    enemyShots.Remove(beam);
                    UpdateHealthEvent.FireMyEvent(player.health);
                    if (player.health == 0) GameEndedEvent.FireMyEvent();
                }

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
