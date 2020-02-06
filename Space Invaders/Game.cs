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
        public static int windowWidth { get; private set; }
        public static int windowHeight { get; private set; }
        Form1 form;
        private List<LazerBeam> userBeams = new List<LazerBeam>();
        private AlienGroup enemies;

        public Game(int windowWidth, int windowHeight, Form1 form)
        {
            Game.windowWidth = windowWidth;
            Game.windowHeight = windowHeight;

            this.form = form;

            player = new Player();
            enemies = new AlienGroup();
        }

        public void MovePlayer(Direction direction)
        {
            player.Move(direction);          
        }

        public void FireShot()
        {
            userBeams.Add(
                new LazerBeam(player.x, player.y, player.width, player.height, LazerBeam.LazerDirection.up)
            );
        }

        public void Update()
        {
            foreach (LazerBeam beam in userBeams.ToList())
                beam.Update();

            enemies.Update();

            foreach (LazerBeam beam in userBeams.ToList())
                if (enemies.FindAlienHitByLazerBeam(beam))
                    userBeams.Remove(beam);

            if (enemies.count == 0)
                MyGlobalEvent.FireMyEvent(new EventArgs());
        }

        public void Draw(PaintEventArgs e)
        {
            Bitmap bitmap = new Bitmap(800, 600);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                enemies.Draw(g);

                foreach (LazerBeam beam in userBeams)
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
