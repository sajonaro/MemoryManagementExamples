using System;

namespace ConsoleContext
{
    using Tests;

    class Program
    {
        static void Main(string[] args)
        {
           var scenarios = new Scenarios();

            scenarios.AllConventionalAndFails();
            Console.WriteLine("_______________________________________");
            Console.WriteLine("_______________________________________");
            try
            {
                //expected exception here
                scenarios.ModifiedSmartSourceWithClosure();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("_______________________________________");
            Console.WriteLine("_______________________________________");

            scenarios.SmartEventSourceMethodGroup();
            Console.WriteLine("_______________________________________");
            Console.WriteLine("_______________________________________");

            scenarios.ScenarioWithSmartEventSourceNoRoot();
            Console.WriteLine("_______________________________________");
            Console.WriteLine("_______________________________________");

            scenarios.ScenarioThree();


        }
    }
}
