using System.Linq;

namespace Day_16
{
    public class Ticket
    {
        private int[] _fields;

        public Ticket(string line)
        {
            _fields = line.Split(',').Select(int.Parse).ToArray();
        }

        public int[] Fields
        {
            get => _fields;
        }

        public override string ToString()
        {
            return string.Join(',', _fields);
        }
    }
}
