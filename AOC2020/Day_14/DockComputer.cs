using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_14
{
    public class DockComputer
    {
        private readonly IDictionary<int, BinaryNumber> _memory;
        private Mask mask;

        public DockComputer()
        {
            _memory = new Dictionary<int, BinaryNumber>();
        }

        public void Run(IEnumerable<string> program)
        {
            foreach (var instruction in program)
            {
                RunInstruction(instruction);
            }
        }

        public long SumMemory()
        {
            return _memory.Values
                .Select(bn => bn.ToLong())
                .Sum();
        }

        private void RunInstruction(string instruction)
        {
            var regex = new Regex("((\\w+)(\\[(\\d+)\\])?) = (\\w+)");
            var matcher = regex.Match(instruction);
            var command = matcher.Groups[2].Value;
            var argument = matcher.Groups[5].Value;
            switch (command)
            {
                case "mask":
                    SetMask(argument);
                    break;
                case "mem":
                    var value = int.Parse(argument);
                    var index = int.Parse(matcher.Groups[4].Value);
                    SetMemory(index, value);
                    break;
                default:
                    throw new ArgumentException($"Unrecognized program instruction: [{instruction}'");
            }
        }

        private void SetMask(string maskRep)
        {
            mask = new Mask(maskRep);
        }

        private void SetMemory(int location, long argument)
        {
            _memory[location] = mask.MaskNumber(argument);
        }
    }
}
