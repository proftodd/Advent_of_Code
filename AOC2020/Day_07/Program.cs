using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day_07
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(args[0]);
            Dictionary<(string, string), Bag> bagList = new Dictionary<(string, string), Bag>();
            foreach (var line in input)
            {
                var (container, cargo) = ParseLuggageProcessingInstruction(bagList, line);
                // Console.WriteLine($"{container} can contain {cargo.Keys.Count} other bags");
                // Console.WriteLine($"{container}");
                bagList.TryAdd((container.Color, container.Modifier), container);
                foreach (var c in cargo.Keys)
                {
                    bagList.TryAdd((c.Color, c.Modifier), c);
                    container.AddCargo(c, cargo[c]);
                    c.SetContainedBy(container);
                    // Console.WriteLine($"\tcan contain {c}");
                    // foreach (var cc in container.GetCargo().Keys)
                    // {
                    //     Console.WriteLine($"\t\t\tcan contain {cc}");
                    // }
                    // Console.WriteLine($"\t\t{container} can contain {c}: {container.CanContain(c)}");
                    // foreach (var cc in c.GetContainers())
                    // {
                    //     Console.WriteLine($"\t\t\t{c} can be contained by {cc}");
                    // }
                    // Console.WriteLine($"\t\t{c} can be in {container}: {c.CanBeIn(container)}");
                }
            }
            var shinyGold = bagList[("gold", "shiny")];
            // var brightWhite = bagList[("white", "bright")];
            // var mutedYellow = bagList[("yellow", "muted")];
            // var darkOrange = bagList[("orange", "dark")];
            // var lightRed = bagList[("red", "light")];
            // Console.WriteLine($"{shinyGold}");
            // foreach (var b in shinyGold.GetContainers())
            // {
            //     Console.WriteLine($"\tcan be contained by {b}");
            // }
            // Console.WriteLine($"{brightWhite}");
            // foreach (var b in brightWhite.GetContainers())
            // {
            //     Console.WriteLine($"\tcan be contained by {b}");
            // }
            // Console.WriteLine($"{mutedYellow}");
            // foreach (var b in mutedYellow.GetContainers())
            // {
            //     Console.WriteLine($"\tcan be contained by {b}");
            // }
            // Console.WriteLine($"{darkOrange}");
            // foreach (var b in darkOrange.GetContainers())
            // {
            //     Console.WriteLine($"\tcan be contained by {b}");
            // }
            // Console.WriteLine($"{lightRed}");
            // foreach (var b in lightRed.GetContainers())
            // {
            //     Console.WriteLine($"\tcan be contained by {b}");
            // }
            var transitiveContainers = shinyGold.GetTransitiveContainers(shinyGold);
            var transitiveContainerCount = shinyGold.GetTransitiveContentCount();
            Console.WriteLine($"{shinyGold} can be contained by {transitiveContainers.Count} other bags");
            Console.WriteLine($"{shinyGold} can contain {transitiveContainerCount} bags");
        }

        public static ValueTuple<Bag, Dictionary<Bag, int>> ParseLuggageProcessingInstruction(Dictionary<(string, string), Bag> bagList, string instruction)
        {
            // Console.WriteLine(instruction);
            var processor = new Regex(@"^(\w+) (\w+) bag(?:s)? contain ((?<noCargo>no other bags)|((\d+) (\w+) (\w+) bag(?:s)?(, (?<count>\d+) (?<mod>\w+) (?<color>\w+) bag(?:s)?)*))\.$");
            var match = processor.Match(instruction);
            var color = match.Groups[2].Value;
            var modifier = match.Groups[1].Value;
            int count;
            Bag container;
            Bag cargo;
            if (!bagList.TryGetValue((color, modifier), out container))
            {
                container = new Bag(color, modifier);
                bagList.Add((color, modifier), container);
            }
            // Console.WriteLine($"container: {container}");
            var cargoList = new Dictionary<Bag, int>();

            if (!match.Groups["noCargo"].Success)
            {
                color = match.Groups[7].Value;
                modifier = match.Groups[6].Value;
                count = int.Parse(match.Groups[5].Value);
                // Console.WriteLine($"\tcargo: {new Bag(color, modifier)}, count: {count}");
                if (!bagList.TryGetValue((color, modifier), out cargo))
                {
                    cargo = new Bag(color, modifier);
                    bagList.Add((color, modifier), cargo);
                }
                cargoList.Add(cargo, count);
                var countCaptures = match.Groups["count"].Captures;
                var modCaptures = match.Groups["mod"].Captures;
                var colorCaptures = match.Groups["color"].Captures;
                // Console.WriteLine($"count count {countCaptures.Count} mod count {modCaptures.Count} color count {colorCaptures.Count}");
                for (int i = 0; i < Math.Min(countCaptures.Count, Math.Min(modCaptures.Count, colorCaptures.Count)); ++i)
                {
                    color = colorCaptures[i].Value;
                    modifier = modCaptures[i].Value;
                    count = int.Parse(countCaptures[i].Value);
                    // Console.WriteLine($"\tcargo: {new Bag(color, modifier)}, count: {count}");
                    if (!bagList.TryGetValue((color, modifier), out cargo))
                    {
                        cargo = new Bag(color, modifier);
                        bagList.Add((color, modifier), cargo);
                    }
                    cargoList.Add(cargo, count);
                }
            }
            return (container, cargoList);
        }
    }
}
