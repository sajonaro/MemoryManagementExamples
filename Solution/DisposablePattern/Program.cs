using System;

namespace DisposablePattern
{
    using System.IO;
    using System.Net;
    using System.Reflection.Metadata;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello " + "World!");
            const string FileNme = "test.txt";

            using (File.CreateText(FileNme))
            {
            }

            //proper disposing of disposable class
            //using (new DisposableClass(FileNme))
            //{
            //    Console.WriteLine("let the rumble begin");
            //}

            //incorrect usage of disposable class
            var manager = new DisposableClass(FileNme);
            Console.WriteLine("let the rumble begin");

            Console.ReadLine();
            File.Delete(FileNme);
        }
    }
}
