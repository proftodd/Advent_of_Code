using System;

namespace Day_08
{
    public class Console
    {
        Instruction Accumulate;
        Instruction Jump;
        Instruction Noop;

        string[] instructions { get; }

        int address;

        int accumulator;

        public int Address
        {
            get { return address; }
        }

        public int Accumulator
        {
            get { return accumulator; }
        }

        public Console(string[] instructions)
        {
            Accumulate = new Accumulate();
            Noop = new Noop();
            Jump = new Jump();
            this.instructions = instructions;
            address = 0;
            accumulator = 0;
        }

        public int Step()
        {
            if (address >= instructions.Length)
            {
                return accumulator;
            }
            var instruction = instructions[address];
            var (inst, arg) = ParseInstruction(instruction);
            // System.Console.WriteLine($"executing |{instruction}| address {address} accumulator {accumulator} inst {inst} arg {arg}");
            return inst.Execute(ref accumulator, ref address, arg);
        }

        public ValueTuple<Instruction, int> ParseInstruction(string instruction)
        {
            var pair = instruction.Split();
            var arg = int.Parse(pair[1]);
            var inst = pair[0] == "acc" ? Accumulate
                     : pair[0] == "jmp" ? Jump
                     : pair[0] == "nop" ? Noop
                     : null;
            return (inst, arg);
        }
    }
}