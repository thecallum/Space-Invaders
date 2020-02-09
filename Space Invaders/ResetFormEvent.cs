using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    public static class ResetFormEvent
    {
        public static event EventHandler MyEvent;

        public static void FireMyEvent()
        {
            MyEvent.Invoke(null, EventArgs.Empty);
        }
    }
}
