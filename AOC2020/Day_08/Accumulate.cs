namespace Day_08
{
    public class Accumulate : Instruction
    {
        public int Execute(ref int accumulator, ref int nextAddress, int argument)
        {
            accumulator += argument;
            ++nextAddress;
            return 0;
        }

        public override string ToString()
        {
            return "Accumulate";
        }
    }
}