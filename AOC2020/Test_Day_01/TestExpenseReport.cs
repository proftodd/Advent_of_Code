using NUnit.Framework;

namespace Day_01
{
    public class Test_Day_01_Part_1
    {
        int[] input = {
            1721,
            979,
            366,
            299,
            675,
            1456
        };

        [Test]
        public void It_corrects_expense_report()
        {
            Assert.AreEqual((1721, 299, 514579), Program.CorrectExpenseReport(input));
        }

        [Test]
        public void It_finds_bonus_coin()
        {
            Assert.AreEqual((979, 366, 675, 241861950), Program.FindBonus(input));
        }
    }
}