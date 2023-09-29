using System;
using System.Collections.Generic;
using System.Linq; // Added for LINQ support

public class cardHolder
{
    string cardNum;
    int pin;
    string firstName;
    string lastName;
    double balance;

    public cardHolder(string cardNum, int pin, string firstName, string lastName, double balance)
    {
        this.cardNum = cardNum;
        this.pin = pin;
        this.firstName = firstName;
        this.lastName = lastName;
        this.balance = balance;
    }

    public string getNum()
    {
        return cardNum;
    }

    public int getPin()
    {
        return pin;
    }

    public string getFirstName()
    {
        return firstName;
    }

    public string getLastName()
    {
        return lastName;
    }

    public double getBalance()
    {
        return balance;
    }

    public void setNum(string newCardNum)
    {
        cardNum = newCardNum;
    }

    public void setPin(int newPin)
    {
        pin = newPin;
    }

    public void setFirstName(string newFirstName)
    {
        firstName = newFirstName;
    }

    public void setLastName(string newLastName)
    {
        lastName = newLastName;
    }

    public void setBalance(double newBalance)
    {
        balance = newBalance;
    }

    static void showBalance(cardHolder currentUser)
    {
        Console.WriteLine("Current balance: " + currentUser.getBalance());
    }

    public static void Main(string[] args)
    {
        List<cardHolder> cardHolders = new List<cardHolder>();
        cardHolders.Add(new cardHolder("1234567890123456", 1234, "Alice", "Johnson", 150.31));
        cardHolders.Add(new cardHolder("9876543210987654", 5678, "Bob", "Smith", 321.21));
        cardHolders.Add(new cardHolder("4567890123456789", 2345, "Carol", "Brown", 105.31));
        cardHolders.Add(new cardHolder("5678901234567890", 6789, "David", "Lee", 950.51));
        cardHolders.Add(new cardHolder("7890123456789012", 7890, "Emily", "Davis", 50.56));

        Console.WriteLine("Welcome to FastATM");
        Console.WriteLine("Please insert your debit card: ");
        string debitCardNum = "";
        cardHolder currentUser = null;

        while (true)
        {
            try
            {
                debitCardNum = Console.ReadLine();
                // Check against our database using LINQ
                currentUser = cardHolders.FirstOrDefault(a => a.cardNum == debitCardNum);
                if (currentUser != null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Card not recognized. Please try again.");
                }
            }
            catch
            {
                Console.WriteLine("Card not recognized. Please try again.");
            }
        }

        Console.WriteLine("Please enter your pin: ");
        int userPin = 0;

        while (true)
        {
            try
            {
                userPin = int.Parse(Console.ReadLine());
                // Check against our database
                if (currentUser.getPin() == userPin)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect pin. Please try again.");
                }
            }
            catch
            {
                Console.WriteLine("Incorrect pin. Please try again.");
            }
        }

        Console.WriteLine("Welcome " + currentUser.getFirstName());

        int option = 0;

        do
        {
            printOptions();
            try
            {
                option = int.Parse(Console.ReadLine());
            }
            catch
            {
            }

            if (option == 1)
            {
                deposit(currentUser);
            }
            else if (option == 2)
            {
                withdraw(currentUser);
            }
            else if (option == 3)
            {
                showBalance(currentUser);
            }
            else if (option == 4)
            {
                break;
            }
            else
            {
                option = 0;
            }
        }
        while (option != 4);

        Console.WriteLine("Thank you! Have a nice day!");
    }

    static void printOptions()
    {
        Console.WriteLine("Please choose from one of the following options...");
        Console.WriteLine("1. Deposit");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Show Balance");
        Console.WriteLine("4. Exit");
    }

    static void deposit(cardHolder currentUser)
    {
        Console.WriteLine("How much $$ would you like to deposit: ");
        double depositAmount = double.Parse(Console.ReadLine());
        if (depositAmount < 0)
        {
            Console.WriteLine("Invalid deposit amount. Please enter a positive value.");
        }
        else
        {
            currentUser.setBalance(currentUser.getBalance() + depositAmount);
            Console.WriteLine("Thank you for your deposit. Your new balance is: " + currentUser.getBalance());
        }
    }

    static void withdraw(cardHolder currentUser)
    {
        Console.WriteLine("How much $$ would you like to withdraw: ");
        double withdrawalAmount = double.Parse(Console.ReadLine());
        if (withdrawalAmount < 0)
        {
            Console.WriteLine("Invalid withdrawal amount. Please enter a positive value.");
        }
        else if (currentUser.getBalance() >= withdrawalAmount)
        {
            currentUser.setBalance(currentUser.getBalance() - withdrawalAmount);
            Console.WriteLine("Withdrawal successful. Your new balance is: " + currentUser.getBalance());
        }
        else
        {
            Console.WriteLine("Insufficient balance.");
        }
    }
}
