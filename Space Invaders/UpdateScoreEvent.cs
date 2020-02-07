using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    public class UpdateScoreEventArgs : EventArgs
    {
        public readonly int value;

        public UpdateScoreEventArgs(int value)
        {
            this.value = value;
        }
    }

    public static class UpdateScoreEvent
    {
        public delegate void UpdateScoreDeledate(UpdateScoreEventArgs e);
        public static event UpdateScoreDeledate MyEvent;

        public static void FireMyEvent(int value)
        {
            MyEvent.Invoke(new UpdateScoreEventArgs(value));
        }
    }
}