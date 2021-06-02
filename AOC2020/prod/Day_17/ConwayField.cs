using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_17
{
    public class ConwayField
    {
        private IDictionary<(int, int, int), char> _population;
        private int AbsMax;

        private ConwayField()
        {
            _population = new Dictionary<(int, int, int), char>();
        }

        public ConwayField(string[] lines) : this()
        {
            var zLayers = PartitionLines(lines);
            int numRows, numCols, maxX, maxY, xCoord, yCoord;

            foreach (var zCoord in zLayers.Keys)
            {
                var theseLines = zLayers[zCoord];
                numRows = theseLines.Length;
                numCols = theseLines[0].Length;
                maxX = numRows / 2;
                maxY = numCols / 2;
                for (int i = 0; i < numRows; ++i)
                {
                    var thisLine = theseLines[i];
                    xCoord = maxX - numRows + i + 1;
                    for (int j = 0; j < numCols; ++j)
                    {
                        yCoord = maxY - numRows + j + 1;
                        if (thisLine[j] == '#')
                        {
                            _population.Add((xCoord, yCoord, zCoord), thisLine[j]);
                        }
                    }
                }
            }
            AbsMax = _population.Keys.SelectMany(t => new[] { t.Item1, t.Item2, t.Item3 })
                .Select(coord => Math.Abs(coord))
                .Max();
        }

        public char this [(int, int, int) coord]
        {
            get { return _population.TryGetValue(coord, out var value) ? value : '.'; }
            private set
            {
                if (value == '#')
                {
                    if (Math.Abs(coord.Item1) > AbsMax)
                    {
                        AbsMax = Math.Abs(coord.Item1);
                    }
                    if (Math.Abs(coord.Item2) > AbsMax)
                    {
                        AbsMax = Math.Abs(coord.Item2);
                    }
                    if (Math.Abs(coord.Item3) > AbsMax)
                    {
                        AbsMax = Math.Abs(coord.Item3);
                    }
                    _population[coord] = value;
                }
            }
        }

        public int MinX => -AbsMax;

        public int MaxX => AbsMax;

        public int MinY => -AbsMax;

        public int MaxY => AbsMax;

        public int MinZ => -AbsMax;

        public int MaxZ => AbsMax;

        public int Population => _population.Values.Count();

        public int NeighborCount(int xCoord, int yCoord, int zCoord)
        {
            int neighborCount = 0;
            for (int z = zCoord - 1; z <= zCoord + 1; ++z)
            {
                for (int y = yCoord - 1; y <= yCoord + 1; ++y)
                {
                    for (int x = xCoord - 1; x <= xCoord + 1; ++x)
                    {
                        if (x == xCoord && y == yCoord && z == zCoord)
                        {
                            continue;
                        }
                        if (this[(x, y, z)] == '#')
                        {
                            ++neighborCount;
                        }
                    }
                }
            }
            return neighborCount;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            var other = (ConwayField)obj;
            var thisActive = this._population.Where(kvp => kvp.Value == '#').ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var otherActive = other._population.Where(kvp => kvp.Value == '#').ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            if (thisActive.Keys.Except(otherActive.Keys).Any())
            {
                return false;
            }

            if (otherActive.Keys.Except(thisActive.Keys).Any())
            {
                return false;
            }

            return true;
        }

        public ConwayField Iterate()
        {
            var ret = new ConwayField();
            for (int z = MinZ - 1; z <= MaxZ + 1; ++z)
            {
                for (int y = MinY - 1; y <= MaxY + 1; ++y)
                {
                    for (int x = MinX - 1; x <= MaxX + 1; ++x)
                    {
                        int neighborCount = NeighborCount(x, y, z);
                        if (this[(x, y, z)] == '#')
                        {
                            if (neighborCount == 2 || neighborCount == 3)
                            {
                                ret[(x, y, z)] = '#';
                            }
                        }
                        else
                        {
                            if (neighborCount == 3)
                            {
                                ret[(x, y, z)] = '#';
                            }
                        }
                    }
                }
            }
            return ret;
        }

        private static IDictionary<int, string[]> PartitionLines(string[] lines)
        {
            var ret = new Dictionary<int, string[]>();
            List<string> group = new List<string>();
            var headerRegex = new Regex("^z=(-?\\d+)$");
            Match match;
            int zCoord = -1;

            for (int i = 0; i < lines.Length; ++i)
            {
                match = headerRegex.Match(lines[i]);
                if (match.Success)
                {
                    zCoord = int.Parse(match.Groups[1].Value);
                }
                else if (lines[i] == "")
                {
                    ret.Add(zCoord, group.ToArray());
                    group = new List<string>();
                } else
                {
                    group.Add(lines[i]);
                }
            }
            ret.Add(zCoord, group.ToArray());

            return ret;
        }
    }
}
