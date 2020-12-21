using System.Collections.Generic;
using NUnit.Framework;

namespace Day_07
{
    public class TestLuggageProcessing
    {
        [Test]
        public void It_can_parse_luggage_processing_instructions_with_one_cargo_possibility()
        {
            var instruction = "bright white bags contain 1 shiny gold bag.";
            var expectedContainer = new Bag("white", "bright");
            var expectedCargo = new Dictionary<Bag, int>(new[] {
                new KeyValuePair<Bag, int>(new Bag("gold", "shiny"), 1),
            });
            var (container, cargo) = Program.ParseLuggageProcessingInstruction(new Dictionary<(string, string), Bag>(), instruction);
            Assert.AreEqual(expectedContainer, container);
            Assert.AreEqual(expectedCargo, cargo);
        }

        [Test]
        public void It_can_parse_luggage_processing_instructions_with_multiple_cargo_possibilities()
        {
            var instruction = "light red bags contain 1 bright white bag, 2 muted yellow bags.";
            var expectedContainer = new Bag("red", "light");
            var expectedCargo = new Dictionary<Bag, int>(new[] {
                new KeyValuePair<Bag, int>(new Bag("white", "bright"), 1),
                new KeyValuePair<Bag, int>(new Bag("yellow", "muted"), 2),
            });
            var (container, cargo) = Program.ParseLuggageProcessingInstruction(new Dictionary<(string, string), Bag>(), instruction);
            Assert.AreEqual(expectedContainer, container);
            Assert.AreEqual(expectedCargo, cargo);
        }

        [Test]
        public void It_can_parse_luggage_instruction_no_contained_bags()
        {
            var instruction = "faded blue bags contain no other bags.";
            var expectedContainer = new Bag("blue", "faded");
            var expectedCargo = new Dictionary<Bag, int>();
            var (container, cargo) = Program.ParseLuggageProcessingInstruction(new Dictionary<(string, string), Bag>(), instruction);
            Assert.AreEqual(expectedContainer, container);
            Assert.AreEqual(expectedCargo, cargo);
        }

        [Test]
        public void It_reports_no_containers_correctly()
        {
            var cargo = new Bag("blue", "light");
            Assert.AreEqual(new HashSet<Bag>(), cargo.GetTransitiveContainers(cargo));
        }
    }
}