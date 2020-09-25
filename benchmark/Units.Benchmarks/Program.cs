using System.Linq;
using BenchmarkDotNet.Running;

namespace Units.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if (!args.Any())
            {
                args = new[] {"--filter", "*"};
            }

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}