using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class MyCustomException : Exception
    {
        public static readonly MyCustomException IsNotANumber = new("is NaN");
        public static readonly MyCustomException IsNotABoolean = new("is not a bool");
        public static readonly MyCustomException QuestionNotFound = new("question does not found");
        public static readonly MyCustomException NegativeNumber = new("The number can not be negative");

        private MyCustomException(string message) : base(message)
        {
        }
    }

    internal static class Program
    {
        private static readonly Dictionary<string, Action> Questions = new()
        {
            {"0", Exit},
            {"1", Question1},
            {"2", Question2},
            {"3", Question3},
            {"4", Question4},
            {"5", Question5},
            {"6", Question6},
            {"7", Question7}
        };

        private static bool _working = true;

        private static void Main(string[] args)
        {
            while (_working)
            {
                try
                {
                    //Console.Clear();
                    Console.Write("Enter the number (1-7) for the question to run or 0 to exit: ");
                    var question = Console.ReadLine() ?? string.Empty;

                    if (!Questions.TryGetValue(question, out var action))
                        throw MyCustomException.QuestionNotFound;

                    Console.WriteLine($"You entered: {question}");
                    Console.WriteLine("------------");
                    action.Invoke();
                }
                catch (MyCustomException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void Exit()
        {
            _working = false;
        }

        private static void Question1()
        {
            Console.Write("Please type yours first name: ");
            var firstName = Console.ReadLine();

            Console.Write("Please type yours middle name: ");
            var middleName = Console.ReadLine();

            Console.Write("Please type yours first name: ");
            var lastName = Console.ReadLine();

            Console.WriteLine($"{firstName} {lastName}");
            Console.WriteLine($"{firstName} {middleName} {lastName}");
            Console.WriteLine($"{lastName}, {firstName} {middleName}");
        }

        private static void Question2()
        {
            Console.Write("First number: ");
            if (!int.TryParse(Console.ReadLine(), out var firstNumber))
                throw MyCustomException.IsNotANumber;

            Console.Write("Second number: ");
            if (!int.TryParse(Console.ReadLine(), out var secondNumber))
                throw MyCustomException.IsNotANumber;

            Console.WriteLine("Sum: {0}", firstNumber + secondNumber);
            Console.WriteLine("Difference: {0}", Math.Abs(firstNumber - secondNumber));
        }

        private static void Question3()
        {
            Console.Write("Radius: ");
            if (!double.TryParse(Console.ReadLine(), out var radius)) throw MyCustomException.IsNotANumber;
            if (radius < 0) throw MyCustomException.NegativeNumber;

            Console.WriteLine("Circle area is {0}", Math.PI * Math.Pow(radius, 2));
        }

        private static void Question4()
        {
            Console.Write("Input a single character: ");
            Console.WriteLine("\nYou entered: {0}", Console.ReadKey().KeyChar);
        }

        private static void Question5()
        {
            Console.Write("Does C# looks like Java? ");
            if (!bool.TryParse(Console.ReadLine(), out var answer))
                throw MyCustomException.IsNotABoolean;

            Console.WriteLine("Your answer was {0}", answer);
        }

        private static void Question6()
        {
            Console.Write("How many siblings do you have? ");
            if (!int.TryParse(Console.ReadLine(), out var siblings))
                throw MyCustomException.IsNotANumber;
            if (siblings < 0) throw MyCustomException.NegativeNumber;

            Console.WriteLine("I also have {0} siblings", siblings);
        }

        private static void Question7()
        {
            const double adultTicket = 3.75;
            const double childTicket = 2.25;

            Console.Write("How many adult tickets are you going to buy? ");
            if (!int.TryParse(Console.ReadLine(), out var adultAmount))
                throw MyCustomException.IsNotANumber;
            
            Console.Write("How many child tickets are you going to buy? ");
            if (!int.TryParse(Console.ReadLine(), out var childAmount))
                throw MyCustomException.IsNotANumber;
    
            if (adultAmount < 0 || childAmount < 0)
                throw MyCustomException.NegativeNumber;

            Console.WriteLine("Result is : {0}$", adultAmount * adultTicket + childAmount * childTicket);
        }
    }
}