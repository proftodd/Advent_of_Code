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

            var subs = program.Select((inst, index) => (inst, index)).Where(t => t.Item1.StartsWith("jmp") || t.Item1.StartsWith("nop"));
            if (result == 0)
            {
                foreach (var (inst, i) in subs)
                {
                    var newProgram = SubstituteCommand(program, inst.StartsWith("nop") ? "jmp" : "nop", i);
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
                        System.Console.WriteLine($"\t on line {i} replaced |{inst}| with |{newProgram[i]}|");
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
