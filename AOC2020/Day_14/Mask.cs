using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_14
{
    public class Mask
    {
        readonly char[] characters;

        public Mask(string rep)
        {
            characters = rep.ToCharArray();
        }

        public BinaryNumber MaskNumber(long input)
        {
            var rawInput = new BinaryNumber(input);
            var output = new BinaryNumber();
            for (int i = 0; i < BinaryNumber.Length; ++i)
            {
                output[i] = characters[i] switch
                {
                    'X' => rawInput[i],
                    '1' => 1,
                    '0' => 0,
                    _ => throw new ArgumentException(nameof(characters), $"Unexpected mask value at position [{i}]: {characters[i]}"),
                };
            }
            return output;
        }

        public IEnumerable<long> GenerateAddresses(int target)
        {
            var generatedTargets = new List<BinaryNumber>();
            generatedTargets.Add(new BinaryNumber());

            var binaryTarget = new BinaryNumber(target);
            for (int i = 0; i < BinaryNumber.Length; ++i)
            {
                if (characters[i] == '0')
                {
                    foreach (var gt in generatedTargets)
                    {
                        gt[i] = binaryTarget[i];
                    }
                }
                else if (characters[i] == '1')
                {
                    foreach (var gt in generatedTargets)
                    {
                        gt[i] = 1;
                    }
                }
                else if (characters[i] == 'X')
                {
                    var newTargets = new List<BinaryNumber>();
                    foreach (var gt in generatedTargets)
                    {
                        var copy = gt.Clone();
                        gt[i] = 1;
                        copy[i] = 0;
                        newTargets.Add(gt);
                        newTargets.Add(copy);
                    }
                    generatedTargets = newTargets;
                }
            }
            return generatedTargets.Select(gt => gt.ToLong());
        }
    }
}
