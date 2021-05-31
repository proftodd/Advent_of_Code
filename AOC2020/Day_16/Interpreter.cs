using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_16
{
    public class Interpreter
    {
        private readonly List<Field> _fields;
        private readonly Ticket _ticket;
        private readonly List<Ticket> _nearbyTickets;
        private List<Ticket> _goodTickets;

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

        public List<Ticket> GoodTickets
        {
            get
            {
                if (_goodTickets == null)
                {
                    _goodTickets = NearbyTickets.Where(t => !ScanErrors().Any(bt => bt.Item1 == t)).ToList();
                }
                return _goodTickets;
            }
        }

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

        public Scanner CreateScanner()
        {
            var dict = new Dictionary<string, int>();
            List<Field>[] possibleMatches = new List<Field>[_fields.Count];
            for (int i = 0; i < _fields.Count; ++i)
            {
                possibleMatches[i] = new List<Field>();
                for (int j = 0; j < _fields.Count; ++j)
                {
                    if (GoodTickets.All(t => _fields[j].Validate(t.Fields[i])))
                    {
                        possibleMatches[i].Add(_fields[j]);
                    }
                }
            }

            while (possibleMatches.Any(pm => pm.Count > 1))
            {
                for (int i = 0; i < _fields.Count; ++i)
                {
                    if (possibleMatches[i].Count == 1)
                    {
                        for (int j = 0; j < _fields.Count; ++j)
                        {
                            if (i == j)
                            {
                                continue;
                            }
                            possibleMatches[j].Remove(possibleMatches[i][0]);
                        }
                    }
                }
            }

            for (int i = 0; i < _fields.Count; ++i)
            {
                dict.Add(possibleMatches[i][0].Name, i);
            }

            return new Scanner(dict);
        }
    }
}
