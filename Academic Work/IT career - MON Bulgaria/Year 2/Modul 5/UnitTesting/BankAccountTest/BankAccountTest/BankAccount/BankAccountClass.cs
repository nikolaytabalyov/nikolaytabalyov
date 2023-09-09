using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount {
    public class BankAccountClass {

        private decimal amount;

        public decimal Amount {
            get => amount; 
            set {
                if (value < 0)
                    throw new ArgumentException();
                else 
                    amount = value;
            }
        }

        public BankAccountClass(decimal amount) {
            this.Amount = amount;
        }
        public BankAccountClass() {

        }
        
        public void Deposit(decimal sum) {
            if (sum < 0) {
                throw new InvalidOperationException("Sum is negative!");
            }
            Amount += sum;
        }

        public void Withdraw(decimal amount)
        {
            Amount -= amount;
        }
    }
}
