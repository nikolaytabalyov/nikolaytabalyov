using BankAccount;

namespace BankAccountTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DepositShouldAddMoney() {
            BankAccountClass account = new();

            account.Deposit(50);
            account.Deposit(50);

            Assert.AreEqual(100, account.Amount);

        }

        [Test]
        public void DepositShouldAddMoney3() {
            BankAccountClass account = new();

            account.Deposit(50);
            account.Deposit(50);

            Assert.AreEqual(100, account.Amount);

        }

        [TestCase (100)]
        [TestCase (500)]
        public void DepositShouldAddMoney2(decimal sum)
        {
            BankAccountClass account = new();

            account.Deposit(sum);
            account.Deposit(sum);

            Assert.AreEqual(2 * sum, account.Amount);

        }

        [Test]
        public void DepositShouldThrowException() {
            BankAccountClass account = new();


            Assert.Throws<InvalidOperationException>(() => account.Deposit(-50));

        }
        [Test]
        public void DepositShouldThrowExceptionMessage() {
            BankAccountClass account = new();


            var ex = Assert.Throws<InvalidOperationException>(() => account.Deposit(-50));

            Assert.That(ex.Message == "Sum is negative!");

        }

        [Test]
        public void WithdrawShoulRemoveMoney() {
            BankAccountClass account = new(200);

            account.Withdraw(50);

            Assert.AreEqual(150, account.Amount);
        }
        
        [Test]
        public void WithdrawShoulRemoveMoneyExecution() {
            BankAccountClass account = new(200);

            Assert.Throws<ArgumentException>(() => account.Withdraw(250));
        }

        [Test]
        public void AccountInitWithPositiveValue() {
            BankAccountClass account = new(2000);
            Assert.AreEqual(2000, account.Amount);
        }

        [Test]
        public void AccountInitWithNegativeValue() {
            Assert.Throws<ArgumentException>(() => new BankAccountClass(-200));
        }
    }
}