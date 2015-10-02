using JsonParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = File.ReadAllText(@".....");

            Stopwatch watch = new Stopwatch();
            for (int i = 0; i < 20; i++)
            {
                watch.Start();
                Newtonsoft.Json.Linq.JObject jObj = Newtonsoft.Json.Linq.JObject.Parse(data);
                watch.Stop();
                Console.WriteLine(watch.Elapsed);
                watch.Reset();
            }
            Console.WriteLine(" ");

            GC.Collect();

            for (int i = 0; i < 20; i++)
            {
                watch.Start();
                JParser parser = new JParser();
                JToken token = parser.Parse(data);
                watch.Stop();
                Console.WriteLine(watch.Elapsed);
                watch.Reset();
            }
            Console.WriteLine(" ");
        }
    }
}
