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
    public partial class Form1 : Form
    {
        private Game game;
        private Timer gameTimer;
        private Timer animationTimer;
        private List<Keys> keysPressed = new List<Keys>();

        private bool gameStarted = false;

        public Form1()
        {
            InitializeComponent();
            ResetFormEvent.MyEvent += ResetFormMethod;
        }

        private void start_game_btn_Click_1(object sender, EventArgs e)
        {
            if (gameStarted) return;
            StartNewGame();
        }

        private void StartNewGame()
        {
            gameStarted = true;
            game = new Game(ClientSize.Width, ClientSize.Height, this);

            start_game_btn.Visible = false;

            SetupGameTimer();
            SetupAnimationTimer();

            gameTimer.Start();
            animationTimer.Start();
        }

        private void SetupGameTimer()
        {
            gameTimer = new Timer();
            gameTimer.Interval = 10;
            gameTimer.Tick += new EventHandler(gameTimer_tick);
        }

        private void SetupAnimationTimer()
        {
            animationTimer = new Timer();
            animationTimer.Interval = 100;
            animationTimer.Tick += new EventHandler(animationTimer_tick);
        }

        public void UpdateHealth(int value)
        {
            this.health_label.Text = "Health: " + value;
        }

        private void ResetFormMethod(object sender, EventArgs e)
        {
            if (!gameStarted)
                return;

            animationTimer.Stop();
            gameTimer.Stop();
            start_game_btn.Visible = true;

            this.Refresh();

            gameStarted = false;
        }

        private void animationTimer_tick(object sender, EventArgs e)
        {
            game.ToggleAnimation();
        }

        public void UpdateScore(int value)
        {
            this.score_label.Text = "Score: " + value;
        }

        private void gameTimer_tick(object sender, EventArgs e)
        {
            int keysPressedLength = keysPressed.Count();

            if (keysPressedLength > 0)
            {
                switch (keysPressed[keysPressedLength -1])
                {
                    case Keys.Left:
                        game.MovePlayer(Direction.Left);
                        break;
                    case Keys.Right:
                        game.MovePlayer(Direction.Right);
                        break;
                }
            }

            if (spacePressed)
                game.FireShot();

            game.Update();
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (game == null) return;
            game.Draw(e);
        }

        private bool spacePressed = false;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                spacePressed = true;
                e.Handled = true;
                return;
            }

            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);

            keysPressed.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                spacePressed = false;
                return;
            }

            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
        }

    }
}
