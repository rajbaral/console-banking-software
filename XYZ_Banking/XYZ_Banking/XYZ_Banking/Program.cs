using System;
using System.Linq;

namespace XYZ_Banking
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n************************************");
            Console.WriteLine("WELCOME TO XYZ BANKING CORPORATION");
            Console.WriteLine("************************************\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Test User Detials");
            Console.WriteLine("\n1. Ishwor Ojha\n\nusername: user1\npassword: password1\n\n2. Raj Baral\n\nusername: user2\npassword: password2");
            Console.ResetColor();

            /* Test username and password:
             ----------------------------
            | Id | username  | password  |
            |----------------------------|
            | 1  | user1     | password1 |
            | 2  | user2     | password2 |
            -----------------------------
            */


            var bankName = "";
            var bank = new Bank(bankName);
            var currentPersonName = "";
            var personBalance = 0;
            var currentUser = new Person(currentPersonName);
            var currentAmount = new Money(personBalance);

            var loggedInUserAct = new Account(currentAmount, currentUser);

            var commands = new ICommand[]
            {
                            new CreateNewAccountCommand(),
                            new LoginCommand(),
                            new CheckBalanceCommand(),
                            new DepositCommand(),
                            new WithdrawCommand(),
                            new TransectionDetailsCommand(),
                            new LogoutCommand(),
                            new ExitCommand()
            };

            while (true)
            {
                try
                {
                    Console.WriteLine("\nWhat would you like to do today?\n");

                    // This loop creates a list of commands:
                    for (int i = 0; i < commands.Length; i++)
                    {
                        Console.WriteLine("{0}. {1}", i + 1, commands[i].Description);
                    }

                    // Read until the input is valid.
                    var userChoice = string.Empty;
                    var commandIndex = -1;
                    do
                    {
                        userChoice = Console.ReadLine();
                        int selectedOption;
                        if (int.TryParse(userChoice, out selectedOption))
                        {
                            if (selectedOption == 1)
                            {
                                //create new account
                                Console.WriteLine("Awesome! Let's create new account for you..!");
                                Console.WriteLine("\nYour Full Name:");
                                var personName = Console.ReadLine();

                                Console.WriteLine("\nYour Email Address:");
                                var personEmail = Console.ReadLine();

                                Console.WriteLine("\nThe amount you would like to deposit?");
                                var amount = Convert.ToDecimal(Console.ReadLine());

                                Console.WriteLine("\nSelect your user name?");
                                var userName = Console.ReadLine();

                                Console.WriteLine("\nSelect your password?");
                                var password = Console.ReadLine();

                                var person = new Person(personName);
                                person.UserName = userName;
                                person.Password = password;
                                person.Email = personEmail;

                                var money = new Money(amount);
                                person.Money = money;
                                var activityType = "create new account";
                                var createdAccount = bank.CreateAccount(person, money);

                                var bankTransection = new BankTransection(activityType);
                                createdAccount.TransectionType.Add(bankTransection);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Congratulation {0} your account has been created successfully!\nCurrent Balance Available: {1}\nAccount Name: {2}\nAccount Owner: {3}\nUser name: {4}\nPassword: {5}",
                                    createdAccount.Owner.Name, createdAccount.Money.Value, createdAccount.Name, createdAccount.Owner.Name, createdAccount.Owner.UserName, createdAccount.Owner.Password);
                                Console.ResetColor();
                                Console.WriteLine(" ");
                            }
                            else if (selectedOption == 2)
                            {
                                // log in
                                // check if user already logged in
                                if (loggedInUserAct.Owner.UserName != null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("you were already logged in!");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    //Login Attempts counter
                                    int loginAttempts = 0;

                                    //Simple iteration upto three times
                                    for (int i = 0; i < 3; i++)
                                    {
                                        Console.WriteLine("Enter username");
                                        string username = Console.ReadLine();
                                        Console.WriteLine("Enter password");
                                        string password = Console.ReadLine();

                                        loggedInUserAct = bank._inMemoryDb.Find(a => a.Owner.UserName == username && a.Owner.Password == password);
                                        if (loggedInUserAct == null)
                                        {
                                            Console.WriteLine("Invalid username or passpord.");
                                            loginAttempts++;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                                            Console.WriteLine("\nWelcome {0}", loggedInUserAct.Owner.Name);
                                            Console.ResetColor();
                                            break;
                                        }


                                    }

                                    //Display the result
                                    if (loginAttempts > 2)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("We are not able to verify your identity! Please reset your password or try later");
                                        Console.ResetColor();
                                        Console.ReadKey();
                                    }
                                }

                            }
                            else if (selectedOption == 3)
                            {
                                // check balance
                                if (loggedInUserAct.Owner.UserName != null)
                                {
                                    var checkBalance = bank.BalanceInquiry(loggedInUserAct.Owner);

                                    foreach (var item in checkBalance)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Your available balance in account {0} is: ${1}\n", item.Name, string.Format("{0:#.00}", Convert.ToDecimal(item.Money.Value)));
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("you need to login first");
                                    Console.ResetColor();
                                }


                            }
                            else if (selectedOption == 4)
                            {
                                // Deposit
                                if (loggedInUserAct.Owner.UserName != null)
                                {
                                    Console.WriteLine("Choose the account you would like to deposit");
                                    decimal depositAmt = Convert.ToDecimal(Console.ReadLine());

                                    var moneyDeposit = new Money(depositAmt);
                                    var account = new Money(loggedInUserAct.Money.Value);
                                    var accountActivity = "Deposit";

                                    bank.Deposit(loggedInUserAct, moneyDeposit);

                                    // record deposit activity
                                    var bankTransection = new BankTransection(accountActivity);
                                    loggedInUserAct.AddTransections(bankTransection);

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Deposit completed! The amount of ${0} has been added to your account!", string.Format("{0:#.00}", Convert.ToDecimal(depositAmt)));
                                    Console.ResetColor();
                                    Console.WriteLine(" ");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("you need to login first");
                                    Console.ResetColor();
                                }
                            }
                            else if (selectedOption == 5)
                            {
                                // Withdraw
                                if (loggedInUserAct.Owner.UserName != null)
                                {
                                    Console.WriteLine("How much would you like to withdraw?");
                                    decimal withdrawAmt = Convert.ToDecimal(Console.ReadLine());

                                    var moneyToWithdraw = new Money(withdrawAmt);
                                    var account = new Money(loggedInUserAct.Money.Value);
                                    var accountActivityType = "Withdraw";

                                    bank.Withdraw(loggedInUserAct, moneyToWithdraw);

                                    // record deposit activity
                                    var bankTransection = new BankTransection(accountActivityType);
                                    loggedInUserAct.AddTransections(bankTransection);

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Withdraw completed! Your account deducted by: ${0}", string.Format("{0:#.00}", Convert.ToDecimal(withdrawAmt)));
                                    Console.ResetColor();
                                    Console.WriteLine(" ");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("you need to login first");
                                    Console.ResetColor();
                                }
                            }
                            else if (selectedOption == 6)
                            {
                                // transection history
                                if (loggedInUserAct.Owner.UserName != null)
                                {
                                    var accountTransection = bank.BalanceInquiry(loggedInUserAct.Owner);

                                    foreach (var i in accountTransection.SelectMany(k => k.TransectionType))
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("You did {0} on {1}\n", i.TransectionType, i.TransectionDate);
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("you need to login first");
                                    Console.ResetColor();
                                }
                            }
                            else if (selectedOption == 7)
                            {
                                if (loggedInUserAct.Owner.UserName != null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nYou are successfully log out!");
                                    Console.ResetColor();
                                    Repeat();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("you need to login first");
                                    Console.ResetColor();
                                }

                            }
                            else if (selectedOption == 8)
                            {
                                Environment.Exit(0);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Menu option 1 to 8 only allowed!");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Selection! Please select number 1, 2 . . . 8\n");
                            Console.ResetColor();
                        }


                    }
                    while (!int.TryParse(userChoice, out commandIndex) || commandIndex > commands.Length);
                    commands[commandIndex - 1].Execute(bank);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error occured: " + ex.Message);
                    Console.ResetColor();
                }


            }

        }

        static void Repeat()
        {
            Main(null);
        }

    }

    interface ICommand
    {
        string Description { get; }
        void Execute(Bank bank);
    }

    class CreateNewAccountCommand : ICommand
    {
        public string Description => "Create a new account.";
        public void Execute(Bank bank) { }
    }

    class LoginCommand : ICommand
    {
        public string Description => "Login to Bank.";
        public void Execute(Bank bank) { }
    }

    class CheckBalanceCommand : ICommand
    {
        public string Description => "Check my balance.";
        public void Execute(Bank bank) { }
    }

    class DepositCommand : ICommand
    {
        public string Description => "Deposit Money.";
        public void Execute(Bank bank) { }
    }

    class WithdrawCommand : ICommand
    {
        public string Description => "Withdraw Money.";
        public void Execute(Bank bank) { }
    }

    class TransectionDetailsCommand : ICommand
    {
        public string Description => "Activity Details";
        public void Execute(Bank bank) { }
    }

    class TransferCommand : ICommand
    {
        public string Description => "Transfer Money.";
        public void Execute(Bank bank) { }
    }

    class LogoutCommand : ICommand
    {
        public string Description => "Logout.";
        public void Execute(Bank bank) { }
    }

    class ExitCommand : ICommand
    {
        public string Description => "Exit.";
        public void Execute(Bank bank) { Environment.Exit(0); }
    }
}
