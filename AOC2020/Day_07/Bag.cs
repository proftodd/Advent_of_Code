using System;
using System.Collections.Generic;

namespace Day_07
{
    public class Bag : IEquatable<Bag>, IComparable<Bag>
    {
        private readonly IDictionary<Bag, int> _canContain;
        private readonly ISet<Bag> _canBeIn;

        public Bag(string color, string modifier)
        {
            Color = color;
            Modifier = modifier;
            _canContain = new Dictionary<Bag, int>();
            _canBeIn = new HashSet<Bag>();
        }

        public string Color { get; set; }

        public string Modifier { get; set; }

        public void AddCargo(Bag bag, int number)
        {
            _canContain.Add(bag, number);
        }

        public IDictionary<Bag, int> GetCargo()
        {
            return _canContain;
        }

        public void SetContainedBy(Bag bag)
        {
            _canBeIn.Add(bag);
            // if (_canBeIn.Add(bag))
            // {
            //     Console.WriteLine($"{bag} can contain {this}");
            // }
            // else
            // {
            //     Console.WriteLine($"{bag} already present as a container for {this}");
            // }
            // Console.WriteLine($"{this} can be directly contained by {_canBeIn.Count} other bags");
        }

        public ISet<Bag> GetContainers()
        {
            return _canBeIn;
        }

        public bool CanContain(Bag bag)
        {
            return _canContain.ContainsKey(bag);
        }

        public bool CanBeIn(Bag bag)
        {
            return _canBeIn.Contains(bag);
        }

        public override string ToString()
        {
            return $"{Modifier} {Color} bag";
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            if (obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            return this.Equals((Bag) obj);
        }

        public bool Equals(Bag other)
        {
            return this.Color == other.Color
                && this.Modifier == other.Modifier;
        }

        public int CompareTo(Bag other)
        {
            int colorComparison = this.Color.CompareTo(other.Color);
            if (colorComparison == 0)
            {
                return this.Modifier.CompareTo(other.Modifier);
            }
            {
                return colorComparison;
            }
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Color, Modifier).GetHashCode();
        }

        public ISet<Bag> GetTransitiveContainers(Bag baseBag)
        {
            var ret = new HashSet<Bag>();
            if (this != baseBag)
            {
                // Console.WriteLine($"{baseBag} can be contained by {this}");
                ret.Add(this);
            }
            foreach (var myContainer in _canBeIn)
            {
                // var tc = myContainer.GetTransitiveContainers(baseBag);
                // foreach (var mc in tc)
                // {
                //     Console.WriteLine($"\t{this} can also be contained by {mc}");
                // }
                ret.UnionWith(myContainer.GetTransitiveContainers(baseBag));
            }
            return ret;
        }

        public int GetTransitiveContentCount()
        {
            var transitiveCount = 0;
            foreach (var kvp in _canContain)
            {
                var bag = kvp.Key;
                var count = kvp.Value;
                transitiveCount += count;
                transitiveCount += count * bag.GetTransitiveContentCount();
            }
            return transitiveCount;
        }
    }
}