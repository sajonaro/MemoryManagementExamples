using System;

namespace ConsoleContext
{
    using Tests;

    class Program
    {
        static void Main(string[] args)
        {
           var scenarios = new Scenarios();

            scenarios.ScenarioOne();
            Console.WriteLine("_______________________________________");
            Console.WriteLine("_______________________________________");
            try
            {
                //expected exception here
                scenarios.ScenarioTwo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("_______________________________________");
            Console.WriteLine("_______________________________________");

            scenarios.ScenarioTwoModified();
            Console.WriteLine("_______________________________________");
            Console.WriteLine("_______________________________________");

            scenarios.ScenarioTwoModifiedEvenMore();
            Console.WriteLine("_______________________________________");
            Console.WriteLine("_______________________________________");

            scenarios.ScenarioThree();


        }
    }
}
