using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Model
{
    public class BudgetModel
    {

    }

    class Balance
    {
        private static Balance instance;
        public decimal Amount { get; set; }

        private Balance() { }

        public Balance GetInstance()
        {
            if (instance == null)
            {
                instance = new Balance();
            }
            return instance;
        }
    }
}
