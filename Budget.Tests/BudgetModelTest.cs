using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Budget.Model;

namespace Budget.Tests
{
    [TestFixture]
    class BudgetModelTest
    {
        [Test]
        public void AddIncome_InputIsStandardValue_IncreaseBalanceAmountByValue()
        {
            BudgetModel model = new BudgetModel();
            decimal value = 10;
            decimal expected = model.ShowBalanceAmount() + value;

            model.AddIncome(value);
            decimal actual = model.ShowBalanceAmount();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void AddIncome_InputIsNegativeValue_NoChangesToBalanceAmount()
        {
            BudgetModel model = new BudgetModel();
            decimal expected = model.ShowBalanceAmount();
            decimal value = -10;

            model.AddIncome(value);
            decimal actual = model.ShowBalanceAmount();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
