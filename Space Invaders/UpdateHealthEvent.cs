using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    public class UpdateHealthEventArgs : EventArgs
    {
        public readonly int value;

        public UpdateHealthEventArgs(int value)
        {
            this.value = value;
        }
    }

    public static class UpdateHealthEvent
    {
        public delegate void UpdateHealthDeledate(UpdateHealthEventArgs e);
        public static event UpdateHealthDeledate MyEvent;

        public static void FireMyEvent(int value)
        {
            MyEvent.Invoke(new UpdateHealthEventArgs(value));
        }
    }
}