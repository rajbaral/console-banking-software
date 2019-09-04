using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace XYZ_Banking
{

    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void PersonCreate()
        {
            // Setup test variables
            var testUser = new Person("Test User");

            // Perform test
            Assert.AreEqual(testUser.Name, "Test User");
        }
    }

    [TestClass]
    public class MoneyTest
    {
        [TestMethod]
        public void MoneyCreate()
        {
            // Setup test variables
            decimal testAmount = 1000;

            // Setup test objects
            var testMoney = new Money(testAmount);

            // Perform test
            Assert.AreEqual(testMoney.Value, testAmount);
        }
    }

    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void AccountCreate()
        {
            // Setup test variables
            decimal testAmount = 1000;
            var testUserName = "Test User";

            // Setup test objects
            var testUser = new Person(testUserName);
            var testMoney = new Money(testAmount);
            var testAccount = new Account(testMoney, testUser);

            // Perform test
            Assert.AreEqual(testAccount.Money.Value, testMoney.Value);
        }
    }

    [TestClass]
    public class BankTest
    {

        private const string _testBankName = "XYZ";
        private const string _testPerson1Name = "Ishwor Ojha";
        private const string _testPerson2Name = "RAJ Baral";
        private const decimal _testMoneyAccount1Amount = 2500;
        private const decimal _testMoneyAccount2Amount = 1850;

        [TestMethod]
        public void XYZBankConstructor()
        {
            // Setup test objects
            var testBank = new Bank(_testBankName);

            // Assert
            Assert.AreEqual(_testBankName, testBank.BankName);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BankCreateAccountNegativeValue()
        {
            // Setup test variables
            decimal testMoneyPersonAmount = 1600;
            decimal testMoneyAccountAmount = -1000;

            // Setup test objects
            var testBank = new Bank(_testBankName);
            var testPerson = new Person(_testPerson1Name);
            var testMoneyAccount = new Money(testMoneyAccountAmount);
            var testMoneyPerson = new Money(testMoneyPersonAmount);

            // Perform test
            testPerson.Money = testMoneyPerson;

            // Assert
            // Expected to throw ArgumentException
            testBank.CreateAccount(testPerson, testMoneyAccount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BankCreateAccountInsufficientFunds()
        {
            // Setup test variables
            decimal testMoneyPersonAmount = 800;

            // Setup test objects
            var testBank = new Bank(_testBankName);
            var testPerson = new Person(_testPerson1Name);
            var testMoneyAccount = new Money(_testMoneyAccount1Amount);
            var testMoneyPerson = new Money(testMoneyPersonAmount);

            // Perform test
            testPerson.Money = testMoneyPerson;

            // Assert
            // Expected to throw ArgumentException
            testBank.CreateAccount(testPerson, testMoneyAccount);
        }

        [TestMethod]
        public void BankAccountWithdrawPass()
        {
            // Setup test variables
            decimal testMoneyWithdrawAmount = 300;

            // Setup test objects
            var testBank = new Bank(_testBankName);
            var testPerson = new Person(_testPerson1Name);
            var testMoneyAccount = new Money(_testMoneyAccount1Amount);
            var testMoneyWithdraw = new Money(testMoneyWithdrawAmount);
            var testAccount = new Account(testMoneyAccount, testPerson);

            // Perform test
            testBank.Withdraw(testAccount, testMoneyWithdraw);

            // Assert
            Assert.AreEqual(
                testAccount.Money.Value,
                _testMoneyAccount1Amount - testMoneyWithdrawAmount
                );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BankAccountWithdrawNegativeValue()
        {
            // Setup test variables
            decimal testMoneyWithdrawAmount = -300;

            // Setup test objects
            var testBank = new Bank(_testBankName);
            var testPerson = new Person(_testPerson1Name);
            var testMoneyAccount = new Money(_testMoneyAccount1Amount);
            var testMoneyWithdraw = new Money(testMoneyWithdrawAmount);
            var testAccount = new Account(testMoneyAccount, testPerson);

            // Perform test
            // Expected to throw ArgumentException
            testBank.Withdraw(testAccount, testMoneyWithdraw);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BankAccountWithdrawInsufficientFunds()
        {
            // Setup test variables
            decimal testMoneyWithdrawAmount = 3000;

            // Setup test objects
            var testBank = new Bank(_testBankName);
            var testPerson = new Person(_testPerson1Name);
            var testMoneyAccount = new Money(_testMoneyAccount1Amount);
            var testMoneyWithdraw = new Money(testMoneyWithdrawAmount);
            var testAccount = new Account(testMoneyAccount, testPerson);

            // Perform test
            // Expected to throw ArgumentException
            testBank.Withdraw(testAccount, testMoneyWithdraw);

        }

        [TestMethod]
        public void BankAccountDepositPass()
        {
            // Setup test variables
            decimal testMoneyDepositAmount = 300;

            // Setup test objects
            var testBank = new Bank(_testBankName);
            var testPerson = new Person(_testPerson1Name);
            var testMoneyDeposit = new Money(testMoneyDepositAmount);
            var testMoneyAccount = new Money(_testMoneyAccount1Amount);
            var testAccount = new Account(testMoneyAccount, testPerson);

            // Perform test
            testBank.Deposit(testAccount, testMoneyDeposit);

            // Assert
            Assert.AreEqual(
                testAccount.Money.Value,
                _testMoneyAccount1Amount + testMoneyDepositAmount
                );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BankAccountDepositNegativeValue()
        {
            // Setup test variables
            decimal testMoneyDepositAmount = -300;

            // Setup test objects
            var testBank = new Bank(_testBankName);
            var testPerson = new Person(_testPerson1Name);
            var testMoneyDeposit = new Money(testMoneyDepositAmount);
            var testMoneyAccount = new Money(_testMoneyAccount1Amount);
            var testAccount = new Account(testMoneyAccount, testPerson);

            // Perform test
            // Expected to throw ArgumentException
            testBank.Deposit(testAccount, testMoneyDeposit);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BankAccountTransferNegativeValue()
        {
            // Setup test variables
            decimal testMoneyTransferAmount = -200;

            // Setup test objects
            var testBank = new Bank(_testBankName);

            var testPerson1 = new Person(_testPerson1Name);
            var testPerson2 = new Person(_testPerson2Name);

            var testMoneyAccount1 = new Money(_testMoneyAccount1Amount);
            var testMoneyAccount2 = new Money(_testMoneyAccount2Amount);
            var testMoneyTransfer = new Money(testMoneyTransferAmount);

            var testAccount1 = new Account(testMoneyAccount1, testPerson1);
            var testAccount2 = new Account(testMoneyAccount2, testPerson2);

            // Perform test
            // Transfer FROM testAccount1 TO testAccount2 WITH testMoneyTransfer
            // Expected to throw ArgumentException
            testBank.Transfer(testAccount1, testAccount2, testMoneyTransfer);
        }

    }
}