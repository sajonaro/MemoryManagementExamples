namespace Solution.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Implementation of WeakEventManager pattern for .net Core
    /// </summary>
    /// <typeparam name="TEventSource">Type of event source</typeparam>
    /// <typeparam name="TEventArguments">tye of eventargs of the event</typeparam>
    public class WeakEventManager<TEventSource, TEventArguments> where TEventArguments : EventArgs
    {
        private static readonly Lazy<WeakEventManager<TEventSource, TEventArguments>> InstanceHolder = new Lazy<WeakEventManager<TEventSource, TEventArguments>>(() => new WeakEventManager<TEventSource, TEventArguments>());
        private readonly IDictionary<Type, WeakCollection<EventHandler<TEventArguments>>> eventHandlers;
        private WeakEventManager()
        {
            this.eventHandlers = new Dictionary<Type, WeakCollection<EventHandler<TEventArguments>>>();
        }

        /// <summary>
        /// singleton instance
        /// </summary>
        /// <returns></returns>
        public static WeakEventManager<TEventSource, TEventArguments> Current => InstanceHolder.Value;

        /// <summary>
        /// Add a handler for the given source's event.
        /// </summary>
        public void AddHandler(TEventSource source, string eventName, EventHandler<TEventArguments> eventHandler)
        {
            var sourceType = source.GetType();

            if (!this.eventHandlers.ContainsKey(sourceType))
            {
                var collection = new WeakCollection<EventHandler<TEventArguments>>();
                collection.Add(eventHandler);
                this.eventHandlers.Add(sourceType, collection );
            }
            else this.eventHandlers[sourceType].Add(eventHandler);

            this.WireUpEventWithHandlers(source,eventName);
        }

        private  void WireUpEventWithHandlers(TEventSource source, string eventName)
        {
            Type sourceType = source.GetType();
            MethodInfo method = this.GetType().GetMethod("RaiseChainOfEvents", BindingFlags.NonPublic | BindingFlags.Instance);
            EventInfo eventInfo = sourceType.GetEvent(eventName);
            Type handlerType = eventInfo.EventHandlerType;
            Delegate handler = Delegate.CreateDelegate(handlerType, this, method);
            eventInfo.AddEventHandler(source, handler);
        }


        public  void AddHandler(TEventSource source, Func<TEventSource, string> eventNameFunc, EventHandler<TEventArguments> eventHandlerAction)
        {
            this.AddHandler(source, eventNameFunc(source), eventHandlerAction);
        }

        private void RaiseChainOfEvents(object sender, TEventArguments arguments)
        {
            foreach (var handler in this.eventHandlers[sender.GetType()].GetLiveItems())
                handler(this, arguments);
        }

   
    }
}