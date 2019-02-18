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
        public void ShowBalanceAmount_NoScenario_ReturnBalanceAmount()
        {
            BudgetModel model = new BudgetModel();
            decimal expected = 0;

            decimal actual = model.ShowBalanceAmount();

            Assert.That(expected, Is.EqualTo(actual));
        }
    }
}
