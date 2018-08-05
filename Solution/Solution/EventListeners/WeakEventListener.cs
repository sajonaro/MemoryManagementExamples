namespace Solution.EventListeners
{
    using System;
    using System.Diagnostics;
    using Solution.EventSources;
    using Solution.Tools;

    /// <summary>
    /// Event listener using WeakEventManager implementation
    /// </summary>
    public class WeakEventListener
    {
        private void OnEvent(object source, EventArgs args)
        {
            Debug.WriteLine("WeakEventListener received event.");
            Console.WriteLine("WeakEventListener received event.");
        }

        public WeakEventListener(EventSource source)
        {
            //WeakEventManager<EventSource, EventArgs>.Current.AddHandler(source, "Event", this.OnEvent);
            WeakEventManager<EventSource, EventArgs>.Current.AddHandler(source, s => nameof(s.Event), this.OnEvent);
        }

        ~WeakEventListener()
        {
            Debug.WriteLine("WeakEventListener finalized.");
            Console.WriteLine("WeakEventListener finalized.");
        }
    }
}
