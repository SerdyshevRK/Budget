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

        [Test]
        public void AddCategory_InputIsStandardString_AddNewCategoryToCategoriesListReturnTrue()
        {
            BudgetModel model = new BudgetModel();
            string categoryTitle = "Food";

            bool result = model.AddCategory(categoryTitle);

            Assert.IsTrue(result);
        }

        [Test]
        public void AddCategory_InputIsStandardStringCategoryAlreadyExists_NoChangesToCategoryListReturnFalse()
        {
            BudgetModel model = new BudgetModel();
            string categoryTitle = "Food";
            string categoryTitle2 = "fOod";

            model.AddCategory(categoryTitle);
            bool result = model.AddCategory(categoryTitle2);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddCategory_InputIsEmptyString_NoChangesToCategoriesListReturnFalse()
        {
            BudgetModel model = new BudgetModel();
            string categoryName = "";

            bool result = model.AddCategory(categoryName);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddCategory_InputIsNull_NoChangesToCategoriesListReturnFalse()
        {
            BudgetModel model = new BudgetModel();
            string categoryTitle = null;

            bool result = model.AddCategory(categoryTitle);

            Assert.IsFalse(result);
        }

        [Test]
        public void EditCategory_InputStandardStringCategoryExists_ChangeTitleOfCategoryReturnTrue()
        {
            BudgetModel model = new BudgetModel();
            string categoryTitle = "food";
            string newCategoryTitle = "clothes";

            model.AddCategory(categoryTitle);
            bool result = model.EditCategory(categoryTitle, newCategoryTitle);

            Assert.IsTrue(result);
        }

        [Test]
        public void EditCategory_InputStandardStringCategoryDoNotExists_NoChangesToCategoriesListReturnFalse()
        {
            BudgetModel model = new BudgetModel();
            string categoryTitle = "food";
            string newCategoryTitle = "clothes";

            bool result = model.EditCategory(categoryTitle, newCategoryTitle);

            Assert.IsFalse(result);
        }

        [Test]
        public void EditCategory_InputEmptyString_NoChangesToCategoriesListReturnFalse()
        {
            BudgetModel model = new BudgetModel();
            string categoryTitle = "food";
            string newCategoryTitle = "";

            model.AddCategory(categoryTitle);
            bool result = model.EditCategory(categoryTitle, newCategoryTitle);

            Assert.IsFalse(result);
        }

        [Test]
        public void EditCategory_InputIsNull_NoChangesToCategoriesListReturnFalse()
        {
            BudgetModel model = new BudgetModel();
            string categoryTitle = "food";
            string newCategoryTitle = null;

            model.AddCategory(categoryTitle);
            bool result = model.EditCategory(categoryTitle, newCategoryTitle);

            Assert.IsFalse(result);
        }

        [Test]
        public void EditCategory_InputOldCategoryIsNull_NoChangesToCategoriesListReturnFalse()
        {
            BudgetModel model = new BudgetModel();
            string categoryTitle = null;
            string newCategoryTitle = "food";

            model.AddCategory(categoryTitle);
            bool result = model.EditCategory(categoryTitle, newCategoryTitle);

            Assert.IsFalse(result);
        }

        [Test]
        public void GetAllCategories_NoScenario_ReturnAllCategoryTitles()
        {
            BudgetModel model = new BudgetModel();
            string[] expected = { "FOOD", "CLOTHES", "OTHER" };
            for (int i = 0; i < expected.Length; i++)
            {
                model.AddCategory(expected[i]);
            }

            string[] actual = model.GetAllCategories();
            bool result = expected.SequenceEqual(actual);

            Assert.IsTrue(result);
        }

        [Test]
        public void AddSpending_StandardInput_AddNewSpendingReturnTrue()
        {
            BudgetModel model = new BudgetModel();
            model.AddIncome(100);
            model.AddCategory("food");
            bool result = model.AddSpending("food", 50);

            Assert.IsTrue(result);
        }

        [Test]
        public void AddSpending_CategoryDoNotExists_NoChangesToSpendingListReturnFasle()
        {
            BudgetModel model = new BudgetModel();
            model.AddIncome(100);
            bool result = model.AddSpending("food", 50);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddSpending_SpendingAmountIsNegativeNumberOrZero_NoChangesToSpendingListReturnFasle()
        {
            BudgetModel model = new BudgetModel();
            model.AddIncome(100);
            model.AddCategory("food");
            bool result = model.AddSpending("food", -10);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddSpending_CategoryIsNull_NoChangesToSpendingListReturnFasle()
        {
            BudgetModel model = new BudgetModel();
            model.AddIncome(100);
            model.AddCategory("food");
            bool result = model.AddSpending(null, 10);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddSpending_CategoryIsEmptyString_NoChangesToSpendingListReturnFasle()
        {
            BudgetModel model = new BudgetModel();
            model.AddIncome(100);
            model.AddCategory("food");
            bool result = model.AddSpending("", 10);

            Assert.IsFalse(result);
        }
    }
}
