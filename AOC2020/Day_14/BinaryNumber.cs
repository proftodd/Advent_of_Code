namespace Day_14
{
    public class BinaryNumber
    {
        int[] _binaryRepresentation;

        public BinaryNumber()
        {
            _binaryRepresentation = new int[36];
        }

        public BinaryNumber(long number) : this()
        {
            for (int i = 0; i < _binaryRepresentation.Length; ++i)
            {
                _binaryRepresentation[_binaryRepresentation.Length - i - 1] = (int)(number % 2);
                number /= 2;
            }
        }

        public long ToLong()
        {
            var ret = 0L;
            for (int i = 0; i < _binaryRepresentation.Length; ++i)
            {
                ret *= 2;
                ret += _binaryRepresentation[i];
            }
            return ret;
        }

        public int Length { get => 36; }

        public int this [int index]
        {
            get { return _binaryRepresentation[index]; }
            set { _binaryRepresentation[index] = value; }
        }
    }
}
