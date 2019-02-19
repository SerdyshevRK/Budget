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
            foreach (Category category in categories)
            {
                if (category.Title.Equals(title))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AddCategory(string title)
        {
            if (title == null || title.Equals("") || ContainsCategory(title.ToUpper())) return false;
            int idx = categories.Count;
            categories.Add(new Category(idx, title.ToUpper()));
            return true;
        }

        private Category GetCategory(string title)
        {
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
            if (newTitle == null || newTitle.Equals("")) return false;
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
        public decimal Amount { get; }
        public DateTime Date { get; private set; }

        public Spending(int id, int categoryID, decimal amount) : this(id, categoryID, amount, DateTime.Today) { }

        public Spending(int id, int categoryID, decimal amount, DateTime date)
        {
            ID = id;
            CategoryID = categoryID;
            Amount = amount;
            Date = date;
        }
    }
}
