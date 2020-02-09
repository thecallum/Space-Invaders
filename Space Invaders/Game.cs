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

        Queue<Level> levels;

        private int score = 0;
        private List<LazerBeam> userShots = new List<LazerBeam>();
        private List<LazerBeam> enemyShots = new List<LazerBeam>();

        private AlienGroup enemies;

        public static int windowWidth { get; private set; }
        public static int windowHeight { get; private set; }

        private int updatesBeforeNextShot = 0;

        private readonly int beforeLevelFrames = 100;
        private int beforeLevelFrameCount = 100;
        private bool beforeLevel = true;

        private int round = 1;

        // private bool levelStarted = true;
        
        public Game(int windowWidth, int windowHeight, Form1 form)
        {
            Game.windowWidth = windowWidth;
            Game.windowHeight = windowHeight;

            this.form = form;

            levels = new Queue<Level>();
            SetupLevels();

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

            enemies = new AlienGroup(levels.Dequeue());
        }
        
        private void SetupLevels()
        {
            levels.Enqueue(new Level(Alien.Type.Bug, 5, 50));
            levels.Enqueue(new Level(Alien.Type.FlyingSaucer, 5, 50));
            levels.Enqueue(new Level(Alien.Type.Satellite, 5, 50));
            levels.Enqueue(new Level(Alien.Type.SpaceShip, 5, 50));
            levels.Enqueue(new Level(Alien.Type.Star, 5, 50));
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
            if (beforeLevel) return;

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
               
            if (enemies.count == 0) ToggleNextLevel();
        }

        private void ToggleNextLevel()
        {
            if (levels.Count() == 0) {
                GameEndedEvent.FireMyEvent();
                return;
            }



            round++;
            beforeLevel = true;

            Level nextLevel = levels.Dequeue();
            enemies = new AlienGroup(nextLevel);
            enemyShots.Clear();
            userShots.Clear();
        }

        public void Draw(PaintEventArgs e)
        {
            Bitmap bitmap = new Bitmap(800, 600);

            using (Graphics g = Graphics.FromImage(bitmap))
            {

                if (beforeLevel) {
                    DrawBeforeLevel(g);
                } else {
                    foreach (LazerBeam beam in userShots)
                        beam.Draw(g);

                    foreach (LazerBeam beam in enemyShots)
                        beam.Draw(g);

                    enemies.Draw(g);
                    player.Draw(g);
                }
            }

            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
        }

        private void DrawBeforeLevel(Graphics g)
        {
            beforeLevelFrameCount--;

            if (beforeLevelFrameCount == 0)
            {
                beforeLevel = false;
                beforeLevelFrameCount = beforeLevelFrames;
            }

            string message = "Round " + round;
            Font messageFont = new Font("Arial", 42);

            Brush messageColor = Brushes.White;

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            g.DrawString(message, messageFont, messageColor, Game.windowWidth /2, Game.windowHeight /2 - 50, drawFormat);
        }

        public void ToggleAnimation()
        {
            enemies.ToggleAnimation();
        }
    }
}
