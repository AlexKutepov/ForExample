using System.Collections.Generic;
using System;
using ParserMathStr;
using CalcException;

namespace NewCalc {

    public class Calculator {

        public double answer = 0;

        IExpressionPolishEntry PolskEntry;

        List<string> TheExpression;

        public Calculator(IExpressionPolishEntry polskEntry, string calcStr) {
            PolskEntry = polskEntry;
            GetTheExpressionPolishEntry(calcStr);
        }

        public Calculator() {

        }

        void GetTheExpressionPolishEntry(string calcStr) {
            TheExpression = new List<string>();
            TheExpression = PolskEntry.GetExpressionPolishEntry(calcStr);
        }

        public void StartCalculateExpressionInPolishEntry() {
            if (TheExpression.Count == 0) {
                answer = 0;
                throw new CalculatorException("The Expression is not correct");
            } else { 
                Stack<double> numbers = new Stack<double>();
                double x = 0, y = 0;
                double num = 0;
                for (int i = 0; i < TheExpression.Count; i++) {
                    if (UniversalDoubleParserFromStr.ParseToDoubleAnyWay(TheExpression[i], out num))
                        numbers.Push(num);
                    else {
                        if (OperationIndexOneVariable(TheExpression[i]) == -1) {
                            y = numbers.Pop();
                            x = numbers.Pop();
                            try {
                                numbers.Push(OperationListXandY[OperationIndexTwoVariable(TheExpression[i])](x, y));
                            } catch (CalculatorException) {
                                answer = 0;
                                throw new CalculatorException("Division on null");
                            }
                        } else {
                            x = numbers.Pop();
                            numbers.Push(OperationListX[OperationIndexOneVariable(TheExpression[i])](x));
                        }
                    }
                }
                answer = numbers.Pop();
                answer = Math.Round(answer, 2);
            }
        }

        delegate double CalcOperationTwoVariable(double x = 0, double y = 0);

        //сюда добавлять новые методы операций двух переменных
        readonly List<CalcOperationTwoVariable> OperationListXandY = new List<CalcOperationTwoVariable> {
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
        readonly List<CalcOperationOneVariable> OperationListX = new List<CalcOperationOneVariable> {
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
                throw new CalculatorException();
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

}
