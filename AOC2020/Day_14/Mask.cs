using System;

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
            for (int i = 0; i < rawInput.Length; ++i)
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
    }
}
