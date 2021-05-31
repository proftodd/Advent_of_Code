using System.IO;
using NUnit.Framework;

namespace Day_14
{
    public class TestDockComputer
    {
        [Test]
        public void It_runs_test_program_correctly()
        {
            var computer = new DockComputer();
            var program = File.ReadLines("test.txt");
            computer.Run(program);
            Assert.AreEqual(165, computer.SumMemory());
        }

        [Test]
        public void It_runs_v2_test_program_correctly()
        {
            var computer = new DockComputer();
            var program = File.ReadAllLines("test2.txt");
            computer.RunV2(program);
            Assert.AreEqual(208, computer.SumMemory());
        }
    }
}
