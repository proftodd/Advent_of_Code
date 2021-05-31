using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_16
{
    public struct Range
    {
        public int Min;
        public int Max;

        public Range(int min, int max) : this()
        {
            Min = min;
            Max = max;
        }
    }

    public class Field
    {
        private static Regex parser = new Regex("^([^:]*): ((\\d+)-(\\d+)+) or ((\\d+)-(\\d+))$");
        private string _name;
        private Range[] _validator;

        public Field(string line)
        {
            var match = parser.Match(line);
            _name = match.Groups[1].Value;
            _validator = new[]
            {
                new Range(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)),
                new Range(int.Parse(match.Groups[6].Value), int.Parse(match.Groups[7].Value))
            };
        }

        public string Name
        {
            get => _name;
        }

        public Range[] Validator
        {
            get => _validator;
        }

        public bool Validate(int value)
        {
            return _validator.Any(v => v.Min <= value && value <= v.Max);
        }

        public IEnumerable<Tuple<Ticket, int, int>> Validate(Ticket ticket)
        {
            return ticket.Fields
                .Select((value, index) => (Value: value, Index: index))
                .Select(tf =>
                {
                    if (Validate(tf.Value))
                    {
                        return null;
                    }
                    else
                    {
                        return new Tuple<Ticket, int, int>(ticket, tf.Index, tf.Value);
                    }
                });
        }
    }
}
