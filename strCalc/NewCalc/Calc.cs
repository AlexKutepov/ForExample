using System.Collections.Generic;
using System;
using ParserMathStr;
using CalcException;
using Operations;
using System.Reflection;

namespace NewCalc {

    public class Calculator {

        public double answer = -1;

        IExpressionPolishEntry PolskEntry;

        List<Operator> TheExpression;
        List<Operator> TheOperatos;


        public Calculator(IExpressionPolishEntry polskEntry, string calcStr) {
            PolskEntry = polskEntry;
            GetTheExpressionPolishEntry(calcStr);
            TheOperatos = new List<Operator>();
         
        }

        public Calculator() {

        }

        void GetTheExpressionPolishEntry(string calcStr) {
            TheExpression = new List<Operator>();
            TheExpression = PolskEntry.GetExpressionPolishEntry(calcStr);
        }

        public void StartCalculateExpressionInPolishEntry() {
            if (TheExpression.Count == 0) {
                answer = 0;
                throw new CalculatorException("The Expression is not correct");
            } else {
                Stack<double> numbers = new Stack<double>();
                for (int i = 0; i < TheExpression.Count; i++) {
                    if (TheExpression[i].number != null)
                        numbers.Push((double)TheExpression[i].number);
                    else {
                        if (TheExpression[i].binarOp) {
                            TheExpression[i].y = numbers.Pop();
                            TheExpression[i].x = numbers.Pop();
                            numbers.Push(TheExpression[i].Operation());
                        } else {
                            TheExpression[i].x = numbers.Pop();
                            numbers.Push(TheExpression[i].Operation());
                        }
                    }
                }
                answer = numbers.Pop();
                answer = Math.Round(answer, 2);
            }
        }
    }
}
