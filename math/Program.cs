using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace math
{
    public class Program
    {
        /*
         * Write a command line program in the language of your choice that will take operations on fractions as an input and produce a fractional result.
            Legal operators shall be *, /, +, - (multiply, divide, add, subtract)
            Operands and operators shall be separated by one or more spaces
            Mixed numbers will be represented by whole_numerator/denominator. e.g. "3_1/4"
            Improper fractions and whole numbers are also allowed as operands 
            Example run:
            ? 1/2 * 3_3/4
            = 1_7/8

            ? 2_3/8 + 9/8
            = 3_1/2
           */
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                //Start Interactive Mode
                Console.WriteLine("Math: Interactive Mode. Type exit to exit");
                while (true)
                {
                    String input = Console.ReadLine();
                    if (input.Equals("exit")){ break; }
                    Console.WriteLine("= " + DoMath(input.Split(" ")));
                }
            }else
            Console.WriteLine(DoMath(args));
        }

        public static string DoMath(string[] input)
        {
            int i = 1;
            if (input.Length > 0)
            {
                Fraction ret = new Fraction(input[0]);
                while (i < input.Length)
                {
                    if (input.Length > i + 1)
                    {
                        Fraction f2 = new Fraction(input[i + 1]);
                        switch (input[i])
                        {
                            case "+": ret.Add(f2); break;
                            case "-": ret.Subtract(f2);break;
                            case "*": ret.Multiply(f2);break;
                            case "/": ret.Divide(f2);break;
                            default: return "syntax error";
                        }
                    }
                    else return "syntax error";
                    i += 2;
                }
                return ret.ToString();
            }
            else return ".";
        }
        public class Fraction
        {
            public int Whole { get; set; }
            public int Numerator { get; set; }
            public int Denominator { get; set; }

            //regex to match numbers. Match groups used for:
            //whole + fraction : $1, $2, $3
            //whole : $1
            //fraction: $1, $3
            public static Regex rx = new Regex("(\\d*)_?(\\d*)/?(\\d*)");

            public Fraction(String s)
            {
                var matches = rx.Match(s);
                if(matches.Success)
                {
                    if (matches.Groups[2].Length > 0) // All 3 elements present
                    {
                        SetFraction(int.Parse(matches.Groups[1].Value), int.Parse(matches.Groups[2].Value), int.Parse(matches.Groups[3].Value));
                    }
                    else if (matches.Groups[3].Length > 0) // Whole number missing
                    {
                        SetFraction(0, int.Parse(matches.Groups[1].Value), int.Parse(matches.Groups[3].Value));
                    }
                    else //fraction missing
                    {
                        SetFraction(int.Parse(matches.Groups[1].Value), 0, 1);
                    }
                }
                else
                {
                    throw new Exception("Expected Input " + s + " is not a valid number. Correct format is #_#/#");
                }

            }

            public Fraction(int whole, int num, int denom)
            {
                SetFraction(whole, num, denom);
            }

            public void Simple()
            {
                Numerator += Whole * Denominator;
                Whole = 0;
            }

            public void Add(Fraction f)
            {
                Whole += f.Whole;
                Numerator *= f.Denominator;
                f.Numerator *= Denominator;
                Numerator += f.Numerator;
                Denominator *= f.Denominator;
            }

            public void Subtract(Fraction f)
            {
                Whole -= f.Whole;
                Numerator *= f.Denominator;
                f.Numerator *= Denominator;
                Numerator -= f.Numerator;
                Denominator *= f.Denominator;
            }

            public void Multiply(Fraction f)
            {
                Simple();
                f.Simple();
                Numerator *= f.Numerator;
                Denominator *= f.Denominator;
            }

            public void Divide(Fraction f)
            {
                Simple();
                f.Simple();
                Numerator *= f.Denominator;
                Denominator *= f.Numerator;
            }

            void SetFraction(int whole, int num, int denom)
            {
                Whole = whole + num / denom;
                Numerator = num % denom;
                Denominator = denom;
            }

            void Compound()
            {
                // Reset Whole Number (And this bit of funkyness is to catch negtive numbers in the numerator)
                Numerator += Whole * Denominator;
                Whole = Numerator / Denominator;
                Numerator %= Denominator;

                // Simplify Fraction

                if(Numerator != 0)
                {
                    //Find Primes on Denom:
                    int i = 2;
                    LinkedList<int> primes = new LinkedList<int>();
                    int t = Denominator;
                    while( i < t)
                    {
                        if(t % i == 0)
                        {
                            primes.AddLast(i);
                            t /= i;
                        }
                        else {
                            i++;
                        }
                        
                    } 

                    //Use find and apply compatable primes
                    foreach (int prime in primes) {
                        if(Numerator % prime == 0)
                        {
                            Numerator /= prime;
                            Denominator /= prime;
                        }
                    }

                }

            }
            
            override public string ToString()
            {
                Compound();
                if (Whole != 0)
                {
                    if(Numerator == 0)
                    {
                        return "" + Whole;
                    }
                    else
                    {
                        return Whole + "_" + Numerator + "/" + Denominator;
                    }
                }
                else
                {
                    if(Numerator == 0)
                    {
                        return "0";
                    }
                    else
                    {
                        return Numerator + "/" + Denominator;
                    }
                }
            }
        }
    }
}
