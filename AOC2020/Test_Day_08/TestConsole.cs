using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Day_08
{
    public class TestConsole
    {
        [Test]
        public void It_steps_correctly()
        {
            string[] program = {
                "nop +0",
                "nop +0",
                "nop +0"
            };
            var resultList = new List<int>();
            var console = new Console(program);
            resultList.Add(console.Step());
            resultList.Add(console.Step());
            resultList.Add(console.Step());
            Assert.IsTrue(resultList.All(r => r == 0));
            Assert.AreEqual(0, console.Accumulator);
            Assert.AreEqual(3, console.Address);
        }
    }
}