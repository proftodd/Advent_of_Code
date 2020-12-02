using NUnit.Framework;

namespace Day_01
{
    public class Test_Day_01_Part_1
    {
        [Test]
        public void It_corrects_expense_report()
        {
            int[] input = {
                1721,
                979,
                366,
                299,
                675,
                1456
            };
            Assert.AreEqual((1721, 299, 514579), Program.CorrectExpenseReport(input));
        }
    }
}