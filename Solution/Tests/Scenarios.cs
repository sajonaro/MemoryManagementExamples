using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using System;
    using System.Diagnostics;
    using Solution.EventListeners;
    using Solution.EventSources;

    [TestClass]
    public class Scenarios
    {
        /// <summary>
        /// Wrong assumtion!
        /// This should demonstrate that it is impossible to 
        /// reclaim event listener  
        /// </summary>
        [TestMethod]
        public void AllConventionalAndFails()
        {
            Debug.WriteLine("!!! Start diagnostics !!! ");

            EventSource source = new EventSource();

            var listener = new ConventionalEventListener(source);

            source.Raise();

            Debug.WriteLine("Setting listener to null.");
            listener = null;

            AttemptToTriggerGC();

            source.Raise();

            Debug.WriteLine("Setting source to null.");
            source = null;

            AttemptToTriggerGC();
        }


        /// <summary>
        /// scenario with smart event source
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ModifiedSmartSourceWithClosure()
        {
            Debug.WriteLine("!!! Start diagnostics !!! ");

            var source = new SmartEventSource();;

            var listener = new ConventionalEventListener();
            source.Event += (s,args) => listener.OnEvent(s,args);

            source.RaiseEvent(EventArgs.Empty);

            Debug.WriteLine("Setting listener to null.");
            listener = null;

            AttemptToTriggerGC();

            source.RaiseEvent(EventArgs.Empty);

            Debug.WriteLine("Setting source to null.");
            source = null;

            AttemptToTriggerGC();
        }


        /// <summary>
        /// scenario with smart event source
        /// </summary>
        [TestMethod]
        public void SmartEventSourceMethodGroup()
        {
            Debug.WriteLine("!!! Start diagnostics !!! ");

            var source = new SmartEventSource(); ;

            var listener = new ConventionalEventListener();
            source.Event += listener.OnEvent;

            source.RaiseEvent(EventArgs.Empty);

            Debug.WriteLine("Setting listener to null.");
            listener = null;

            AttemptToTriggerGC();

            source.RaiseEvent(EventArgs.Empty);

            Debug.WriteLine("Setting source to null.");
            source = null;

            AttemptToTriggerGC();
        }

        /// <summary>
        /// scenario with smart event source
        /// </summary>
        [TestMethod]
        public void ScenarioWithSmartEventSourceNoRoot()
        {
            Debug.WriteLine("!!! Start diagnostics !!! ");

            var source = new SmartEventSource(); ;
            var listener = new ConventionalEventListener(source);

            source.RaiseEvent(EventArgs.Empty);

            Debug.WriteLine("Setting listener to null.");
            listener = null;

            AttemptToTriggerGC();

            source.RaiseEvent(EventArgs.Empty);

            Debug.WriteLine("Setting source to null.");
            source = null;

            AttemptToTriggerGC();
        }

        /// <summary>
        /// Improved scenario two
        /// </summary>
        [TestMethod]
        public void ScenarioThree()
        {

            Debug.WriteLine("!!! Start diagnostics !!! ");

            var source = new EventSource(); ;

            var listener = new WeakEventListener(source);

            source.Raise();

            Debug.WriteLine("Setting listener to null.");
            listener = null;

            AttemptToTriggerGC();

            source.Raise();

            Debug.WriteLine("Setting source to null.");
            source = null;

            AttemptToTriggerGC();
        }

        private static void AttemptToTriggerGC()
        {
            Debug.WriteLine("Starting GC.");
            Console.WriteLine("Starting GC.");

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Debug.WriteLine("GC finished.");
            Console.WriteLine("GC finished.");
        }
    }
}
