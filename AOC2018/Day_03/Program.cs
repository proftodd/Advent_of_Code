using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day_03
{
    static class Program
    {
        static void Main(string[] args)
        {
            var sheet = ProcessClaims(args[1]);
            FindIntact(args[1], sheet);
        }

        public static Dictionary<(int, int), Char> ProcessClaims(string filename)
        {
            var sheet = new Dictionary<(int, int), Char>();
            var file = new StreamReader(filename);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var claim = ParseClaim(line);
                Console.WriteLine(ClaimToString(claim));
                for (int i = 0; i < claim.Width; ++i)
                {
                    for (int j = 0; j < claim.Height; ++j)
                    {
                        var x = i + claim.Left;
                        var y = j + claim.Top;
                        if (sheet.ContainsKey((x, y)))
                        {
                            sheet[(x, y)] = 'X';
                        }
                        else
                        {
                            sheet[(x, y)] = 'O';
                        }
                    }
                }
            }
            var overlap = sheet.Values.Count(v => v == 'X');
            Console.WriteLine($"Overlap: {overlap}");
            return sheet;
        }

        public static void FindIntact(string filename, Dictionary<(int, int), Char> sheet)
        {
            var file = new StreamReader(filename);
            string line;
            Claim foundClaim = NULL_CLAIM;
            while ((line = file.ReadLine()) != null && ClaimsAreEqual(foundClaim, NULL_CLAIM))
            {
                var claim = ParseClaim(line);
                var overlaps = false;
                for (int i = 0; i < claim.Width && !overlaps; ++i)
                {
                    for (int j = 0; j < claim.Height && !overlaps; ++j)
                    {
                        var x = i + claim.Left;
                        var y = j + claim.Top;
                        if (sheet[(x, y)] == 'X')
                        {
                            overlaps = true;
                        }
                    }
                }
                if (!overlaps)
                {
                    foundClaim = claim;
                    break;
                }
            }
            Console.WriteLine($"The intact claim is {ClaimToString(foundClaim)}");
        }

        public static Claim ParseClaim(string line)
        {
            Regex rx = new Regex(@"^#(\S+) @ (\d+),(\d+): (\d+)x(\d+)$", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(line);
            Match match = matches[0];
            int name = Int32.Parse(match.Groups[1].Value);
            int left = Int32.Parse(match.Groups[2].Value);
            int top = Int32.Parse(match.Groups[3].Value);
            int width = Int32.Parse(match.Groups[4].Value);
            int height = Int32.Parse(match.Groups[5].Value);
            return new Claim {
                Name = name,
                Left = left,
                Top = top,
                Width = width,
                Height = height,
            };
        }

        public struct Claim {
            public int Name;
            public int Left;
            public int Top;
            public int Width;
            public int Height;
        }

        public static string ClaimToString(Claim claim)
        {
            return $"Claim {claim.Name}: at |{claim.Left}|,|{claim.Top}| (|{claim.Width}| by |{claim.Height}|)";
        }

        public static bool ClaimsAreEqual(Claim c1, Claim c2)
        {
            return c1.Left == c2.Left
            && c1.Top == c2.Top
            && c1.Width == c2.Width
            && c1.Height == c2.Height;
        }

        private static readonly Claim NULL_CLAIM = new Claim
        {
            Name = 0,
            Left = -1,
            Top = -1,
            Width = 0,
            Height = 0,
        };
    }
}
