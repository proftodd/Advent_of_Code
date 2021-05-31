using System.Collections.Generic;

namespace Day_16
{
    public class Scanner
    {
        private readonly IDictionary<string, int> _fields;

        public Scanner(IDictionary<string, int> fields)
        {
            _fields = fields;
        }

        public int Scan(Ticket ticket, string fieldName)
        {
            return ticket.Fields[_fields[fieldName]];
        }
    }
}
