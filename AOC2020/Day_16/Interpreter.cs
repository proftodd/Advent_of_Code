using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_16
{
    public class Interpreter
    {
        private readonly List<Field> _fields;
        private readonly Ticket _ticket;
        private readonly List<Ticket> _nearbyTickets;

        public Interpreter(string filename)
        {
            _fields = new List<Field>();
            _nearbyTickets = new List<Ticket>();

            var reader = new StreamReader(filename);
            string line;
            while ((line = reader.ReadLine()) != "")
            {
                _fields.Add(new Field(line));
            }

            reader.ReadLine();
            line = reader.ReadLine();
            _ticket = new Ticket(line);
            reader.ReadLine();

            reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                _nearbyTickets.Add(new Ticket(line));
            }
        }

        public Ticket Ticket { get => _ticket; }

        public List<Field> Fields { get => _fields; }

        public List<Ticket> NearbyTickets { get => _nearbyTickets; }

        public int FieldCount { get => _fields.Count; }

        public int NearbyTicketCount { get => _nearbyTickets.Count; }

        public IEnumerable<Tuple<Ticket, int, int>> ScanErrors()
        {
            var misses = _nearbyTickets
                .Select(t => _fields.Select(f => f.Validate(t).ToList()).ToList());
            var pivoted = misses.Select(t =>
            {
                var myList = new List<List<Tuple<Ticket, int, int>>>();
                for (int i = 0; i < FieldCount; ++i)
                {
                    var thisList = new List<Tuple<Ticket, int, int>>();
                    for (int j = 0; j < FieldCount; ++j)
                    {
                        thisList.Add(t[j][i]);
                    }
                    myList.Add(thisList);
                }
                return myList;
            });
            var aggs = pivoted
                .Select(t =>
                {
                    return t.Select(tf =>
                    {
                        return tf.Aggregate((agg, tfe) =>
                        {
                            if (agg == null)
                            {
                                return null;
                            }
                            if (tfe == null)
                            {
                                return null;
                            }
                            return tfe;
                        });
                    });
                });
            var denulled = aggs
                .Select(l => l.Where(t => t != null));
            var reduced = denulled
                .SelectMany(t => t);
            return reduced;
        }
    }
}
