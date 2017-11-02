using System;

namespace Solution
{
    public class SmartEventSource
    {
        private readonly WeakCollection<EventHandler<EventArgs>> eventHandlers = new WeakCollection<EventHandler<EventArgs>>();

        public event EventHandler<EventArgs> Event
        {
            add => this.eventHandlers.Add(value);
            remove => this.eventHandlers.Remove(value);
        }

        public void RaiseEvent(EventArgs args)
        {
            foreach (var handler in this.eventHandlers.GetLiveItems())
            {
                handler(this, args);
            }
        }

    }
}
