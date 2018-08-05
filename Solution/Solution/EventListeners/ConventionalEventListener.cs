namespace Solution.EventListeners
{
    using System;
    using System.Diagnostics;
    using Solution.EventSources;

    /// <summary>
    /// will work incorrectly in GC scenario
    /// </summary>
    public class ConventionalEventListener
    {
        private readonly Guid Id;

        public void OnEvent(object source, EventArgs args)
        {
            Debug.WriteLine("EventListener received event. " + this.Id );
            Console.WriteLine("EventListener received event. " + this.Id);
        }

        public ConventionalEventListener(SmartEventSource source)
        {
            source.Event += this.OnEvent;

            Id = Guid.NewGuid();
        }

        public ConventionalEventListener(EventSource source)
        {
            source.Event += this.OnEvent;
            Id = Guid.NewGuid();
        }

        public ConventionalEventListener()
        {
            Id = Guid.NewGuid();
        }

        ~ConventionalEventListener()
        {
            Debug.WriteLine("ConventionalEventListener finalized. " + this.Id);
            Console.WriteLine("ConventionalEventListener finalized."+ this.Id);
        }
    }
}
