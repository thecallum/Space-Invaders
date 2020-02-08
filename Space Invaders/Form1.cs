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

        public Form1()
        {
            InitializeComponent();
            
            game = new Game(ClientSize.Width, ClientSize.Height, this);

            gameTimer = new Timer();
            gameTimer.Interval = 10;
            gameTimer.Tick += new EventHandler(gameTimer_tick);

            animationTimer = new Timer();
            animationTimer.Interval = 100;
            animationTimer.Tick += new EventHandler(animationTimer_tick);

            GameEndedEvent.MyEvent += GameEnd;

            UpdateHealthEvent.MyEvent += UpdateHealthMethod;


            gameTimer.Start();
            animationTimer.Start();
        }

        private void UpdateHealthMethod(UpdateHealthEventArgs e)
        {
            this.health_label.Text = "Health: " + e.value;
        }

        private void GameEnd(object sender, EventArgs e)
        {
            gameTimer.Stop();
            animationTimer.Stop();
            this.Refresh();
            MessageBox.Show("Game Ended");
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
            game.Draw(e);
        }

        private bool spacePressed = false;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                spacePressed = true;
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
