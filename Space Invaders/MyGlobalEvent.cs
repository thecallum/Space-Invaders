using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    public static class MyGlobalEvent
    {
        public static event EventHandler MyEvent;

        public static void FireMyEvent(EventArgs args)
        {
            MyEvent.Invoke(null, args);
        }
    }
}
