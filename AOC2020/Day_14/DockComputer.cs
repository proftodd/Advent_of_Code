using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_14
{
    public class DockComputer
    {
        private readonly IDictionary<long, BinaryNumber> _memory;
        private Mask mask;
        private readonly Regex instructionParser = new Regex("((\\w+)(\\[(\\d+)\\])?) = (\\w+)");

        public DockComputer()
        {
            _memory = new Dictionary<long, BinaryNumber>();
        }

        public void Run(IEnumerable<string> program)
        {
            foreach (var instruction in program)
            {
                var (command, argument, index) = ParseInstruction(instruction);
                switch (command)
                {
                    case "mask":
                        SetMask(argument);
                        break;
                    case "mem":
                        SetMemory(int.Parse(index), int.Parse(argument));
                        break;
                    default:
                        throw new ArgumentException($"Unrecognized program instruction: [{instruction}'");
                }
            }
        }

        public void RunV2(IEnumerable<string> program)
        {
            foreach (var instruction in program)
            {
                var (command, argument, index) = ParseInstruction(instruction);
                switch (command)
                {
                    case "mask":
                        SetMask(argument);
                        break;
                    case "mem":
                        var indexParam = int.Parse(index);
                        var argumentParam = int.Parse(argument);
                        var targets = mask.GenerateAddresses(indexParam);
                        foreach (var target in targets)
                        {
                            SetV2Memory(target, argumentParam);
                        }
                        break;
                    default:
                        throw new ArgumentException($"Unrecognized program instruction: [{instruction}'");
                }
            }
        }

        public long SumMemory()
        {
            return _memory.Values
                .Select(bn => bn.ToLong())
                .Sum();
        }

        private ValueTuple<string, string, string> ParseInstruction(string instruction)
        {
            var matcher = instructionParser.Match(instruction);
            var command = matcher.Groups[2].Value;
            var argument = matcher.Groups[5].Value;
            var index = matcher.Groups[4].Value;
            return (command, argument, index);
        }

        private void SetMask(string maskRep)
        {
            mask = new Mask(maskRep);
        }

        private void SetMemory(long location, long argument)
        {
            _memory[location] = mask.MaskNumber(argument);
        }

        private void SetV2Memory(long location, long argument)
        {
            _memory[location] = new BinaryNumber(argument);
        }
    }
}
