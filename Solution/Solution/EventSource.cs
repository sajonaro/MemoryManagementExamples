using System;

namespace Solution
{
    /// <summary>
    /// Conventional event source
    /// </summary>
    public class EventSource
    {
        public event EventHandler<EventArgs> Event = delegate { };

        public void Raise()
        {
            Event(this, EventArgs.Empty);
        }
    }
}
