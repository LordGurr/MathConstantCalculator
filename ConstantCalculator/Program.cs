using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Text;

namespace ConstantCalculator
{
    internal class Program
    {
        private const char prefix = '?';
        private static List<Command> commands = new List<Command>();
        private static Random rng = new Random();

        private static void Main(string[] args)
        {
            commands.Add(new MathCommand());
            commands.Add(new Ping());
            commands.Add(new FlipCoin());
            commands.Add(new Help());

            //commands[0].runCommand(new object("1+2"));
            decimal phi = (decimal)(0.5 + (Math.Pow(5, 0.5)) * 0.5);
            Console.WriteLine(phi);
            decimal otherPhi = (decimal)((1 + Math.Sqrt(5)) / 2);
            Console.WriteLine(otherPhi);
            decimal e = 1;
            var a = fact(3);
            var b = fact(4);
            var c = fact(5);
            var d = fact(6);
            var f = fact((decimal)0.5);
            a = factorial(3);
            b = factorial(4);
            c = factorial(5);
            d = factorial(6);
            f = factorial(0.5);
            decimal PI = 2 * F(1, 100);

            string chamb = Champernowne(5000);
            Console.WriteLine(chamb);
            for (int i = 0; i < 20; i++)
            {
                e += fact((decimal)1 / ((decimal)i + (decimal)1));
            }
            Console.WriteLine(e);
            Console.WriteLine(PI);
            //Console.WriteLine(Eval(Console.ReadLine()));
            while (true)
            {
                CommandsAndShit(Console.ReadLine());
            }
            Console.ReadKey(true);
        }

        private static decimal fact(decimal temp)
        {
            decimal factorial = 2 > temp ? 1 : 2;
            for (int i = 3; i <= temp; i++)
            {
                factorial *= i;
            }
            return factorial;
            factorial = 2 > temp ? 1 : 2;

            factorial *= fact(factorial - 1);
        }

        private static decimal factorial(double d)
        {
            if (d == 0.0)
            {
                return (decimal)1.0;
            }

            double abs = Math.Abs(d);
            double decimalen = abs - Math.Floor(abs);
            double result = 1.0;

            for (double i = Math.Floor(abs); i > decimalen; --i)
            {
                result *= (i + decimalen);
            }
            if (d < 0.0)
            {
                result = -result;
            }

            return (decimal)result;
        }

        private decimal CalcPi(int iterations)
        {
            decimal q = 1;
            decimal r = 0;
            decimal t = 1;
            decimal k = 1;
            decimal n = 3;
            decimal l = 3;
            for (int i = 0; i < iterations; i++)
            {
                if (4 * q + r - t < n * t)
                {
                }
            }
            return q;
        }

        private static string Champernowne(int iterations)// Borde ändras igen
        {
            string temp = "0.";
            for (int i = 1; i < iterations; i++)
            {
                try
                {
                    temp += i;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
            return temp;
        }

        private static decimal F(Int64 i, int iterations)
        {
            try
            {
                if (iterations > 0)
                {
                    decimal temp = (decimal)1 + (decimal)i / ((decimal)2 * (decimal)i + (decimal)1) * F(i + 1, iterations - 1);
                    return temp;
                }

                return i;
            }
            catch (Exception)
            {
                return i;
            }
        }

        public static bool IsDigitsOnly(string str, string operators) //Den här kollar så att det bara finns nummer eller mellanslag i passwordet. Om inte för denna så skulle spelet krasha om du skrev en bokstav.
        {
            bool containsNumber = false;
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    //if (c != ' ')
                    //{
                    bool found = false;
                    foreach (char character in operators)
                    {
                        if (c == character)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        return false;
                    }
                    //}
                }
                else if (!containsNumber && c > '0' && c < '9')
                {
                    containsNumber = true;
                }
            }
            if (!containsNumber)
            {
                return false;
            }
            return true;
        }

        private const double champ = 0.123456789101112;

        private static string[] _operators = { "-", "+", "/", "*", "^", "%" };

        private static string[] mathConst = { "e", "pi", "tau", "champ", "champerowne" };

        private static Func<double, double, double>[] _operations = {
        (a1, a2) => a1 - a2,
        (a1, a2) => a1 + a2,
        (a1, a2) => a1 / a2,
        (a1, a2) => a1 * a2,
        (a1, a2) => Math.Pow(a1, a2),
        (a1, a2) => a1 % a2,
    };

        public static double Eval(string expression)
        {
            expression = expression.Replace('.', ',');
            List<string> tokens = getTokens(expression);
            Stack<double> operandStack = new Stack<double>();
            Stack<string> operatorStack = new Stack<string>();
            int tokenIndex = 0;
            MakeEverySecondOperator(tokens);

            while (tokenIndex < tokens.Count)
            {
                string token = tokens[tokenIndex];
                if (token == "(")
                {
                    string subExpr = getSubExpression(tokens, ref tokenIndex);
                    operandStack.Push(Eval(subExpr));
                    continue;
                }
                if (token == ")")
                {
                    throw new ArgumentException("Mis-matched parentheses in expression");
                }
                //If this is an operator
                if (Array.IndexOf(_operators, token) >= 0)
                {
                    while (operatorStack.Count > 0 && Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()))
                    {
                        string op = operatorStack.Pop();
                        double arg2 = operandStack.Pop();
                        double arg1 = operandStack.Pop();
                        operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                    }
                    operatorStack.Push(token);
                }
                else
                {
                    if (IsDigitsOnly(token, ","))
                    {
                        operandStack.Push(Convert.ToDouble(token));
                    }
                    else
                    {
                        operandStack.Push(ChooseConst(token));
                    }
                }
                tokenIndex += 1;
            }

            while (operatorStack.Count > 0)
            {
                string op = operatorStack.Pop();
                double arg2 = operandStack.Pop();
                double arg1 = operandStack.Pop();
                operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
            }
            return operandStack.Pop();
        }

        private static string getSubExpression(List<string> tokens, ref int index)
        {
            StringBuilder subExpr = new StringBuilder();
            int parenlevels = 1;
            index += 1;
            while (index < tokens.Count && parenlevels > 0)
            {
                string token = tokens[index];
                if (tokens[index] == "(")
                {
                    parenlevels += 1;
                }

                if (tokens[index] == ")")
                {
                    parenlevels -= 1;
                }

                if (parenlevels > 0)
                {
                    subExpr.Append(token);
                }

                index += 1;
            }

            if ((parenlevels > 0))
            {
                throw new ArgumentException("Mis-matched parentheses in expression");
            }
            return subExpr.ToString();
        }

        private static List<string> getTokens(string expression)
        {
            string operators = string.Join("", _operators); //"()^*/+-%";
            List<string> tokens = new List<string>();
            StringBuilder sb = new StringBuilder();
            string newExpress = expression.Replace(" ", string.Empty);
            for (int i = 0; i < newExpress.Length; i++)
            {
                char c = newExpress[i];
                if (operators.IndexOf(c) >= 0)
                {
                    if ((sb.Length > 0))
                    {
                        tokens.Add(sb.ToString());
                        sb.Length = 0;
                    }
                    tokens.Add(c.ToString());
                    continue;
                }
                else if (c == '(' && i > 0 && IsDigitsOnly(newExpress[i - 1].ToString(), string.Empty) || c == ')' && i + 1 < newExpress.Length && IsDigitsOnly(newExpress[i + 1].ToString(), string.Empty))
                {
                    if (c == '(' && i > 0 && IsDigitsOnly(newExpress[i - 1].ToString(), string.Empty))
                    {
                        if ((sb.Length > 0))
                        {
                            tokens.Add(sb.ToString());
                            sb.Length = 0;
                        }
                        tokens.Add("*");
                    }
                    else
                    {
                        if ((sb.Length > 0))
                        {
                            tokens.Add(sb.ToString());
                            sb.Length = 0;
                        }
                        tokens.Add(c.ToString());
                        tokens.Add("*");
                    }
                    continue;
                }
                if (c == '(' || c == ')')
                {
                    if ((sb.Length > 0))
                    {
                        tokens.Add(sb.ToString());
                        sb.Length = 0;
                    }
                    if (i > 0 && newExpress[i - 1] == ')' && c == '(')
                    {
                        tokens.Add("*");
                    }
                    tokens.Add(c.ToString());
                }
                else if (IsFirstLetterOfConstant(c))
                {
                    if ((sb.Length > 0))
                    {
                        tokens.Add(sb.ToString());
                        sb.Length = 0;
                    }
                    tokens.Add(newExpress.Substring(i, ConstantLengthFrom(c)));
                    i += ConstantLengthFrom(c) - 1;
                }
                else if (operators.IndexOf(c) < 0)
                {
                    sb.Append(c);
                }
            }

            if ((sb.Length > 0))
            {
                tokens.Add(sb.ToString());
            }
            return tokens;
        }

        private static double ChooseConst(string str)
        {
            if (str.ToLower() == "e")
            {
                return Math.E;
            }
            else if (str.ToLower() == "pi")
            {
                return Math.PI;
            }
            else if (str.ToLower() == "tau")
            {
                return 2 * Math.PI;
            }
            else if (str.ToLower() == "champ" || str.ToLower() == "champerowne")
            {
                return champ;
            }
            throw new Exception("No mathematical constant matches: " + str);
        }

        private static int ConstantLengthFrom(char c)
        {
            for (int i = 0; i < mathConst.Length; i++)
            {
                if (c == mathConst[i][0])
                {
                    return mathConst[i].Length;
                }
            }
            return 0;
        }

        private static void MakeEverySecondOperator(List<string> tokens)
        {
            string operators = string.Join("", _operators); //"()^*/+-%";

            for (int i = 0; i < tokens.Count; i++)
            {
                if (i > 0 /*&& i + 1 < tokens.Count*/)
                {
                    if ((i < 1 || operators.IndexOf(tokens[i - 1]) < 0) && (i + 1 >= tokens.Count || operators.IndexOf(tokens[i + 1]) < 0) && operators.IndexOf(tokens[i]) < 0)
                    {
                        tokens.Insert(i, "*");
                    }
                }
            }
            //return tokens;
        }

        private static bool IsFirstLetterOfConstant(char c)
        {
            for (int i = 0; i < mathConst.Length; i++)
            {
                if (c == mathConst[i][0])
                {
                    return true;
                }
            }
            return false;
        }

        private static void CommandsAndShit(string commandstring)
        {
            if (commandstring.StartsWith(prefix))
            {
                commandstring = commandstring.Remove(0, 1);
                int index = commands.FindIndex(a => commandstring.StartsWith(a.name));
                if (index >= 0)
                {
                    string sendString = null;
                    if (commands[index].name.Length < commandstring.Length)
                    {
                        sendString = commandstring.Remove(0, commands[index].name.Length);
                    }
                    else
                    {
                        sendString = null;
                    }
                    commands[index].RunCommand(sendString);
                }
                for (int i = 0; i < commands.Count; i++)
                {
                    string toCheck = commandstring.Split()[0];
                    if (commands[i].name.Length + 2 < commandstring.Length)
                    {
                        toCheck = commandstring.Substring(0, commands[i].name.Length + 3);
                    }
                    int howSimilar = Compute(toCheck.Replace(" ", ""), commands[i].name);
                    if (howSimilar <= 2 && howSimilar >= 1)
                    {
                        Console.WriteLine("Did you mean: " + commands[i].name);
                        //Console.WriteLine(commandstring + " is " + howSimilar + " similar to " + commands[i].name);
                    }
                }
            }
        }

        public static int Compute(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m];
        }

        //private enum Constanst
        //{
        //    inget,
        //    e,
        //    pi,
        //    tau,
        //    champ
        //}

        //private const int NumberOfConstants = 4;

        //private static Tuple<bool, Constanst> ContainsOneConstant(string str)
        //{
        //    for (int i = 0; i < NumberOfConstants; i++)
        //    {
        //        if (str.Contains(((Constanst)i).ToString()))
        //        {
        //            bool containsOne = true;
        //            for (int a = 0; a < NumberOfConstants; a++)
        //            {
        //                if (a != i)
        //                {
        //                    if (str.Contains(((Constanst)a).ToString()))
        //                    {
        //                        containsOne = false;
        //                        break;
        //                    }
        //                }
        //            }
        //            return Tuple.Create(containsOne, (Constanst)i);
        //        }
        //    }
        //    return Tuple.Create(false, Constanst.inget);
        //}

        private abstract class Command
        {
            public string name { get; protected set; }

            abstract public void RunCommand(string parameters);

            //public void runCommand(object[] parameters)
            //{
            //    myCommand.Invoke(new object(), parameters);
            //}
            //public void runCommand()
            //{
            //    new
            //    myCommand.Invoke(new object(), );
            //}
        }

        private class MathCommand : Command
        {
            public MathCommand()
            {
                name = "math";
            }

            public override void RunCommand(string parameters)
            {
                try
                {
                    Console.WriteLine(Eval(parameters));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private class Ping : Command
        {
            public Ping()
            {
                name = "ping";
            }

            public override void RunCommand(string parameters)
            {
                Console.WriteLine("Pong!");
            }
        }

        private class FlipCoin : Command
        {
            public FlipCoin()
            {
                name = "flipcoin";
            }

            public override void RunCommand(string parameters)
            {
                int answer = rng.Next(2);
                string[] svar = new string[2] { "Klave", "Krona" };
                Console.WriteLine(svar[answer]);
            }
        }

        private class Help : Command
        {
            public Help()
            {
                name = "help";
            }

            public override void RunCommand(string parameters)
            {
                Console.WriteLine("Listing all top level commands:");
                for (int i = 0; i < commands.Count; i++)
                {
                    Console.WriteLine(commands[i].name);
                }
            }
        }
    }
}