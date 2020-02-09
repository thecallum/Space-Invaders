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

        private int round = 0;
        private bool gameEnded = false;
        
        public Game(int windowWidth, int windowHeight, Form1 form)
        {
            Game.windowWidth = windowWidth;
            Game.windowHeight = windowHeight;

            this.form = form;

            SetupLevels();
            SetupPlayer();

            // Sets up enemies
            ToggleNextLevel();
            InitializeFormLabels();

            UpdateScoreEvent.MyEvent += UpdateScoreMethod;
        }

        private void InitializeFormLabels()
        {
            form.UpdateScore(0);
            form.UpdateHealth(player.health);
        }

        private void SetupPlayer()
        {
            Rectangle playerPosition = new Rectangle(
                (Game.windowWidth + Player.width) / 2,
                Game.windowHeight - Player.height - 10,
                Player.width,
                Player.height
            );

            player = new Player(playerPosition);
        }

        private void SetupLevels()
        {
            levels = new Queue<Level>();

            levels.Enqueue(new Level(Alien.Type.Bug, 5, 50));
            levels.Enqueue(new Level(Alien.Type.FlyingSaucer, 10, 40));
            levels.Enqueue(new Level(Alien.Type.Satellite, 15, 30));
            levels.Enqueue(new Level(Alien.Type.SpaceShip, 20, 20));
            levels.Enqueue(new Level(Alien.Type.Star, 25, 20));
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
            if (beforeLevel || gameEnded) return;

            if (updatesBeforeNextShot > 0) updatesBeforeNextShot--;

            foreach (LazerBeam beam in userShots.ToArray())
                beam.Update(userShots);

            enemies.Update();

            if (enemies.AliensAtBottom()) {
                ToggleGameEnd();
                return;
            }

            foreach (LazerBeam beam in userShots.ToList())
                if (enemies.FindAlienHitByLazerBeam(beam))
                    userShots.Remove(beam);

            foreach (LazerBeam beam in enemyShots.ToList())
                if (player.HitByLazerBeam(beam))
                {
                    enemyShots.Remove(beam);
                    form.UpdateHealth(player.health);
                    if (player.health == 0) ToggleGameEnd();
                }

            if (enemies.CanShoot(enemyShots.Count))
                enemies.FireShot(enemyShots);

            foreach (LazerBeam beam in enemyShots.ToArray())
                beam.Update(enemyShots);
               
            if (enemies.count == 0) ToggleNextLevel();
        }

        private void ToggleGameEnd()
        {
            Console.WriteLine("Game Ended");
            gameEnded = true;
            ResetFormEvent.FireMyEvent();
        }

        private void ToggleNextLevel()
        {
            if (levels.Count() == 0) {
                ToggleGameEnd();
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
            int width = 800;
            int height = 600;

            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                if (gameEnded) {
                    DisplayGameEndedMessage(g);
                } else if (beforeLevel) {
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

        private void DisplayMessage(Graphics g, string message)
        {
            Font messageFont = new Font("Arial", 42);
            Brush messageColor = Brushes.White;
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            g.DrawString(message, messageFont, messageColor, Game.windowWidth / 2, Game.windowHeight / 2 - 200, drawFormat);
        }

        private void DisplayGameEndedMessage(Graphics g)
        {
            string message = "";

            message += "Game Ended\r\n";
            message += "Score : " + score + "\r\n";
            message += "Round : " + round + "\r\n";

            DisplayMessage(g, message);
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
            DisplayMessage(g, message);
        }

        public void ToggleAnimation()
        {
            enemies.ToggleAnimation();
        }
    }
}
