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
        private List<Keys> keysPressed = new List<Keys>();

        public Form1()
        {
            InitializeComponent();

            this.Width = 800;
            this.Height = 600;
            
            game = new Game(this.Width, this.Height, this);

            gameTimer = new Timer();
            gameTimer.Interval = 5;
            gameTimer.Tick += new EventHandler(gameTimer_tick);

            MyGlobalEvent.MyEvent += GameEnd;

            gameTimer.Start();

        }

        private void GameEnd(Object sender, EventArgs e)
        {
            gameTimer.Stop();
            this.Refresh();
            MessageBox.Show("Game Ended");
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

            game.Update();

            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("Painted");
            game.Draw(e);

            //e.Graphics.FillRectangle(Brushes.Red, 100, 100, 100, 100);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Space)
            {
                game.FireShot();
                return;
            }


            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);

            keysPressed.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
        }
    }
}
