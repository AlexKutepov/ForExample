using System.Collections.Generic;
using System;
using ParserMathStr;

namespace NewCalc {

    public class Calculator {

        public double answer = 0;

        StringForTheCalculator stringForTheCalculator = new StringForTheCalculator();

        public void Calculate(string calcStr) {
            List<string> PolskEntry = stringForTheCalculator.GetPolskEntry(calcStr);
            if (PolskEntry.Count != 0)
                StartCalculateExpressionInPolishEntry(PolskEntry);
            else {
                Console.WriteLine("Syntaxis error");
                answer = 0;
            }
        }
       
        void StartCalculateExpressionInPolishEntry(List<string> PolskEntry) {
            Stack<double> numbers = new Stack<double>();
            double x = 0, y = 0;
            double num = 0;
            for (int i = 0; i < PolskEntry.Count; i++) {
                if (stringForTheCalculator.ParseToDoubleAnyWay(PolskEntry[i], out num))
                    numbers.Push(num);
                else {
                    if (OperationIndexOneVariable(PolskEntry[i]) == -1) {
                        y = numbers.Pop();
                        x = numbers.Pop();
                        try {
                            numbers.Push(OperationListXandY[OperationIndexTwoVariable(PolskEntry[i])](x, y));
                        } catch (DivideByZeroException) {
                            answer = 0;
                            Console.WriteLine("Attempted divide by zero.");
                            return;
                        }
                    } else {
                        x = numbers.Pop();
                        numbers.Push(OperationListX[OperationIndexOneVariable(PolskEntry[i])](x));
                    }
                }
            }
            answer = numbers.Pop();
            answer = Math.Round(answer, 2);
        }

        delegate double CalcOperationTwoVariable(double x = 0, double y = 0);
        //сюда добавлять новые методы операций двух переменных
        List<CalcOperationTwoVariable> OperationListXandY = new List<CalcOperationTwoVariable> {
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Pow,
            Mod
        };

        //index операции двух переменных
        int OperationIndexTwoVariable(string operation) {
            switch (operation) {
                case "+": return 0;
                case "-": return 1;
                case "*": return 2;
                case "/": return 3;
                case "^": return 4;
                case "%": return 5;
            }
            return -1;
        }

        delegate double CalcOperationOneVariable(double x = 0);
        //сюда - одной
        List<CalcOperationOneVariable> OperationListX = new List<CalcOperationOneVariable> {
            Cos,
            Sin
        };

        //index функции, операции 1 переменной
        int OperationIndexOneVariable(string operation) {
            switch (operation) {
                case "cos": return 0;
                case "sin": return 1;
            }
            return -1;
        }

        static double Addition(double x, double y) {
            return x + y;
        }

        static double Subtraction(double x, double y) {
            return x - y;
        }

        static double Multiplication(double x, double y) {
            return x * y;
        }

        static double Division(double x, double y) {
            if (y == 0)
                throw new DivideByZeroException();
            return x / y;
        }

        static double Cos(double x) {
            return Math.Cos(x);
        }

        static double Sin(double x) {
            return Math.Sin(x);
        }

        static double Pow(double x, double y) {
            return Math.Pow(x, y);
        }

        static double Mod(double x, double y) {
            return x % y;
        }
    }

    class StringForTheCalculator : ParserMathStrInPolishEntry {

        public List<string> GetPolskEntry(string calcStr) {
            return GetPolishEntryListElements(calcStr);
        }

    }

}
