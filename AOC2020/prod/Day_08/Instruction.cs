namespace Day_08
{
    public interface Instruction
    {
        public int Execute(ref int accumulator, ref int nextAddress, int argument);
    }
}