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
            
            game = new Game(this.ClientSize.Width, this.ClientSize.Height);

            gameTimer = new Timer();
            gameTimer.Interval = 10;
            gameTimer.Tick += new EventHandler(gameTimer_tick);

            animationTimer = new Timer();
            animationTimer.Interval = 100;
            animationTimer.Tick += new EventHandler(animationTimer_tick);

            MyGlobalEvent.MyEvent += GameEnd;

            gameTimer.Start();
            animationTimer.Start();
        }

        private void GameEnd(Object sender, EventArgs e)
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
                // game.FireShot();
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
                // game.FireShot();
                spacePressed = false;
                return;
            }

            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
        }
    }
}
