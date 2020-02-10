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

        LevelManager levelManager;

        private int score = 0;
        private AlienFleet enemies;

        public static int windowWidth { get; private set; }
        public static int windowHeight { get; private set; }

        private readonly int beforeLevelFrames = 100;
        private int beforeLevelFrameCount = 100;
        private bool displayBetweenLevelMessage = true;

        private bool gameEnded = false;
        
        public Game(int windowWidth, int windowHeight, Form1 form)
        {
            Game.windowWidth = windowWidth;
            Game.windowHeight = windowHeight;

            this.form = form;

            levelManager = new LevelManager();

            player = Player.Setup();

            InitializeFormLabels();
            ToggleNextLevel();

            UpdateScoreEvent.MyEvent += UpdateScoreMethod;
        }

        private void InitializeFormLabels()
        {
            form.UpdateScore(0);
            form.UpdateHealth(player.health);
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
            if (player.CanShoot()) {
                player.ResetUpdatesBeforeNextShot();
                player.Shoot();
            }
        }
    
        public void Update()
        {
            if (displayBetweenLevelMessage || gameEnded) return;
            if (!player.CanShoot()) player.DecrementUpdatesBeforeNextShot();

            foreach (LazerBeam shot in player.shots.ToArray())
                shot.Update(player.shots);

            enemies.Update();

            if (enemies.AliensAtBottom()) {
                ToggleGameEnd();
                return;
            }

            foreach (LazerBeam beam in player.shots.ToList())
                if (enemies.FindAlienHitByLazerBeam(beam))
                    player.shots.Remove(beam);

            foreach (LazerBeam beam in enemies.shots.ToList())
                if (player.HitByLazerBeam(beam))
                {
                    enemies.shots.Remove(beam);
                    form.UpdateHealth(player.health);
                    if (player.health == 0) ToggleGameEnd();
                }

            if (enemies.CanShoot(enemies.shots.Count))
                enemies.FireShot(enemies.shots);

            foreach (LazerBeam beam in enemies.shots.ToArray())
                beam.Update(enemies.shots);
               
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
            if (!levelManager.MoreLevels()) {
                ToggleGameEnd();
                return;
             }

            displayBetweenLevelMessage = true;
            enemies = levelManager.NextLevel();
            player.shots.Clear();
        }

        public void Draw(PaintEventArgs e)
        {
            int width = 800;
            int height = 600;

            Bitmap bitmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                if (gameEnded)
                    DisplayGameEndedMessage(g);
                else if (displayBetweenLevelMessage)
                    DisplayBetweenLevelMessage(g);
                else
                    DrawGame(g);    
            }

            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
        }

        private void DrawGame(Graphics g)
        {
            foreach (LazerBeam shot in player.shots)
                shot.Draw(g);

            foreach (LazerBeam beam in enemies.shots)
                beam.Draw(g);

            enemies.Draw(g);
            player.Draw(g);
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
            message += "Round : " + levelManager.round + "\r\n";

            DisplayMessage(g, message);
        }

        private void DisplayBetweenLevelMessage(Graphics g)
        {
            beforeLevelFrameCount--;

            if (beforeLevelFrameCount == 0)
            {
                displayBetweenLevelMessage = false;
                beforeLevelFrameCount = beforeLevelFrames;
            }

            string message = "Round " + levelManager.round;
            DisplayMessage(g, message);
        }

        public void ToggleAnimation()
        {
            enemies.ToggleAnimation();
        }
    }
}
