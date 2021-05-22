using System.Collections.Generic;

namespace Day_15
{
    public class Sequence
    {
        private int[] _seed;
        private IDictionary<int, int> _sequence;

        public Sequence(int[] seed)
        {
            _seed = seed;
            _sequence = new Dictionary<int, int>();
            for (int i = 0; i < seed.Length - 1; ++i)
            {
                _sequence.Add(seed[i], i + 1);
            }
            LastSaid = seed[^1];
            NumberSaid = seed.Length;
        }

        public int NumberSaid { get; private set; }

        public int LastSaid { get; private set; }

        public int NextValue()
        {
            int ret;
            if (_sequence.TryGetValue(LastSaid, out var prevSaid))
            {
                ret = NumberSaid - prevSaid;
            }
            else
            {
                ret = 0;
            }
            _sequence[LastSaid] = NumberSaid;
            ++NumberSaid;
            LastSaid = ret;
            return ret;
        }

        public int FindNthNumber(int n)
        {
            if (n <= _seed.Length)
            {
                return _seed[n - 1];
            }
            while (NumberSaid < n)
            {
                NextValue();
            }
            return LastSaid;
        }
    }
}
