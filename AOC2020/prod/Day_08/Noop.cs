namespace Day_08
{
    public class Noop : Instruction
    {
        public int Execute(ref int accumulator, ref int nextAddress, int argument)
        {
            ++nextAddress;
            return 0;
        }

        public override string ToString()
        {
            return "Noop";
        }
    }
}