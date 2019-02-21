using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model
{
    public class BudgetModel
    {
        private Balance balance;
        private List<Category> categories;
        private List<Spending> spendings;

        public BudgetModel()
        {
            balance = Balance.GetInstance();
            categories = new List<Category>();
            spendings = new List<Spending>();
        }

        public decimal ShowBalanceAmount()
        {
            return balance.Amount;
        }

        public void AddIncome(decimal value)
        {
            if (value <= 0) return;
            balance.Amount += value;
        }

        private bool ContainsCategory(string title)
        {
            bool result = false;
            foreach (Category category in categories)
            {
                if (category.Title.Equals(title))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool AddCategory(string title)
        {
            if (title == null || title.Length == 0 || ContainsCategory(title.ToUpper())) return false;
            int idx = categories.Count;
            categories.Add(new Category(idx, title.ToUpper()));
            return true;
        }

        private Category GetCategory(string title)
        {
            if (title == null || title.Length == 0) return null;
            Category result = null;
            foreach (Category category in categories)
            {
                if (category.Title.Equals(title.ToUpper()))
                {
                    result = category;
                }
            }
            return result;
        }

        private int GetCategoryID(string title)
        {
            Category category = GetCategory(title);
            return category == null ? -1 : category.ID;
        }

        public bool EditCategory(string oldTitle, string newTitle)
        {
            if (newTitle == null || newTitle.Length == 0 || oldTitle == null || oldTitle.Length == 0) return false;
            Category category = GetCategory(oldTitle);
            if (category == null) return false;
            category.UpdateTitle(newTitle);
            return true;
        }

        public string[] GetAllCategories()
        {
            int count = categories.Count;
            string[] result = new string[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = categories[i].Title;
            }
            return result;
        }

        private Spending GetSpending(int categoryID)
        {
            Spending spending = null;            
            foreach (Spending s in spendings)
            {
                if (s.CategoryID == categoryID && s.Date.Equals(DateTime.Today))
                {
                    spending = s;
                    break;
                }
            }
            return spending;
        }

        public bool AddSpending(string category, decimal amount)
        {
            if (category == null || category.Length == 0 || amount <= 0) return false;
            int categoryID = GetCategoryID(category);
            if (categoryID < 0) return false;
            Spending spending = GetSpending(categoryID);
            if (spending != null)
            {
                spending.IncreaseAmount(amount);
                balance.Amount -= amount;
                return true;
            }
            int id = spendings.Count;            
            spendings.Add(new Spending(id, categoryID, amount));
            balance.Amount -= amount;
            return true;
        }

        public decimal ShowSpendingsInCategoryByDay(string category, DateTime day)
        {
            return ShowSpendingsInCategoryByInterval(category, day, day);
        }

        public decimal ShowSpendingsInCategoryByInterval(string category, DateTime start, DateTime end)
        {
            if (category == null || category.Length == 0) return 0;
            Spending spending = null;
            int catID = GetCategoryID(category);
            foreach (Spending s in spendings)
            {
                if (s.CategoryID == catID && s.Date.CompareTo(start) >= 0 && s.Date.CompareTo(end) <= 0)
                {
                    spending = s;
                    break;
                }
            }
            return spending == null ? 0 : spending.Amount;
        }

        public decimal ShowAllSpendingsByDay(DateTime day)
        {
            return ShowAllSpendingsByInterval(day, day);
        }

        public decimal ShowAllSpendingsByInterval(DateTime start, DateTime end)
        {
            decimal result = 0;
            foreach (Spending spending in spendings)
            {
                if (spending.Date.CompareTo(start) >= 0 && spending.Date.CompareTo(end) <= 0)
                {
                    result += spending.Amount;
                }
            }
            return result;
        }
    }

    class Balance
    {
        private static Balance instance;
        public decimal Amount { get; set; }

        private Balance() { }

        public static Balance GetInstance()
        {
            if (instance == null)
            {
                instance = new Balance();
            }
            return instance;
        }
    }

    class Category
    {
        public int ID { get; }
        public string Title { get; private set; }

        public Category(int id, string title)
        {
            ID = id;
            Title = title;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }
    }

    class Spending
    {
        public int ID { get; }
        public int CategoryID { get; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; }

        public Spending(int id, int categoryID, decimal amount) : this(id, categoryID, amount, DateTime.Today) { }

        public Spending(int id, int categoryID, decimal amount, DateTime date)
        {
            ID = id;
            CategoryID = categoryID;
            Amount = amount;
            Date = date;
        }

        public void IncreaseAmount(decimal value)
        {
            Amount += value;
        }
    }
}
