using System;
using System.Threading.Tasks;
using Datadog.Trace.ClrProfiler;
using StackExchange.Redis;

namespace Samples.RedisCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            var c = program.Test2("111", 1, 1);
            Console.WriteLine(c);

            RunStackExchange("StackExchange").Wait();

            Console.ReadLine();
        }

        private static string Host()
        {
            return Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost";
        }

        private string Test2(string a, int? b, int c)
        {
            return "1";
        }

        public string Test3(string a, int? b, int c)
        {
            try
            {
                Console.WriteLine("try");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("finally");
            }
            return "1";
        }

        private static async Task RunStackExchange(string prefix)
        {
            prefix += "StackExchange.Redis.";

            Console.WriteLine($"Testing StackExchange.Redis {prefix}");
            using (var redis = ConnectionMultiplexer.Connect(Host() + ",allowAdmin=true"))
            {
                redis.Configure(Console.Out);

                var db = redis.GetDatabase(1);
                var n = await db.StringSetAsync($"{prefix}INCR", "0");
                Console.WriteLine(n);

                n = db.StringSet($"{prefix}INCR", "0");
                Console.WriteLine(n);

                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                //for (int i = 0; i < 10000; i++)
                //{
                //    await db.StringSetAsync($"{prefix}INCR{i}", "0");
                //}
                //sw.Stop();
                //Console.WriteLine(sw.ElapsedMilliseconds);
            }

            Console.ReadLine();
        }
    }
}
