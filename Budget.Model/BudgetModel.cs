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
        private Dictionary<int, string> categories;

        public BudgetModel()
        {
            balance = Balance.GetInstance();
            categories = new Dictionary<int, string>();
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
            return categories.ContainsValue(title);
        }

        public bool AddCategory(string title)
        {
            if (title == null || title.Equals("") || ContainsCategory(title.ToUpper())) return false;
            int key = categories.Count;
            categories.Add(key, title.ToUpper());
            return true;
        }

        public bool EditCategory(string oldTitle, string newTitle)
        {
            foreach(int key in categories.Keys)
            {
                if (categories[key].Equals(oldTitle.ToUpper()))
                {
                    categories[key] = newTitle.ToUpper();
                    return true;
                }
            }
            return false;
        }

        public string[] GetAllCategories()
        {
            return categories.Values.ToArray();
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
}
