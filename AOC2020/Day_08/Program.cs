using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_08
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = File.ReadAllLines(args[0]);
            var console = new Console(program);
            var addresses = new Dictionary<int, int>();
            int result = 0;
            while (!addresses.ContainsKey(console.Address))
            {
                addresses.Add(console.Address, console.Accumulator);
                console.Step();
            }
            System.Console.WriteLine($"console repeats address {console.Address} with accumulator {console.Accumulator}");

            var jumps = program.Select((inst, index) => (inst, index)).Where(t => t.Item1.StartsWith("jmp"));
            var noops = program.Select((inst, index) => (inst, index)).Where(t => t.Item1.StartsWith("nop"));
            if (result == 0)
            {
                foreach (var (inst, i) in jumps)
                {
                    var newProgram = SubstituteCommand(program, "nop", i);
                    console = new Console(newProgram);
                    addresses.Clear();
                    result = 0;
                    while (!addresses.ContainsKey(console.Address) && result == 0)
                    {
                        addresses.Add(console.Address, console.Accumulator);
                        result = console.Step();
                    }
                    if (result != 0)
                    {
                        System.Console.WriteLine($"program terminated with accumulator {console.Accumulator}");
                        break;
                    }
                }
            }
            if (result == 0)
            {
                foreach (var (inst, i) in noops)
                {
                    var newProgram = SubstituteCommand(program, "jmp", i);
                    console = new Console(newProgram);
                    addresses.Clear();
                    result = 0;
                    while (!addresses.ContainsKey(console.Address) && result == 0)
                    {
                        addresses.Add(console.Address, console.Accumulator);
                        result = console.Step();
                    }
                    if (result != 0)
                    {
                        System.Console.WriteLine($"program terminated with accumulator {console.Accumulator}");
                        break;
                    }
                }
            }
            if (result == 0)
            {
                System.Console.WriteLine("Could not find a substitution that would allow console to run to completion");
            }
        }

        public static string[] SubstituteCommand(string[] program, string replacement, int index)
        {
            return program.Select((inst, i) => {
                return i == index ? replacement + inst.Substring(3)
                                  : inst;
            })
            .ToArray();
        }
    }
}
