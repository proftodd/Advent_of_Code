namespace Day_08
{
    public class Jump : Instruction
    {
        public int Execute(ref int accumulator, ref int nextAddress, int argument)
        {
            nextAddress += argument;
            return 0;
        }

        public override string ToString()
        {
            return "Jump";
        }
    }
}