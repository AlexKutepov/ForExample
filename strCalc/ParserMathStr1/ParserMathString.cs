﻿using System.Collections.Generic;
using System;
using Operations;

namespace ParserMathStr {


    public class ParserMathStringOperation {

        IExpression operators_get;

        public List<Operator> Operators;

        public ParserMathStringOperation(IExpression Expression) {
            operators_get = Expression;
            Operators = new List<Operator>();
            Operators = operators_get.GetOperators();
        }

        public List<string> GetExpression(string stringForPars) {
            List<string> elements = new List<string>();
            stringForPars = stringForPars.Replace(" ", "");
            stringForPars = stringForPars.ToLower();
            string numberTmp = "";
            int tmpIndex = -1;
            for (int i = 0; i < stringForPars.Length; i++) {
                if (i == 0) {
                    if (!char.IsNumber(stringForPars[0])) {
                        if (CheckExistNegativeNumber(stringForPars[0].ToString())) {
                            numberTmp += stringForPars[0].ToString();
                            continue;
                        }
                    }
                }
                if (CheckExistBrackets(stringForPars[i].ToString())) {
                    if (numberTmp != "") {
                        elements.Add(numberTmp);
                    }
                    numberTmp = "";
                    elements.Add(stringForPars[i].ToString());
                    if (CheckExistLeftBracket(stringForPars[i].ToString())) tmpIndex = i;
                    continue;
                }
                if (OperationPriority(stringForPars[i].ToString()) != -1) {
                    if (numberTmp != "") {
                        elements.Add(numberTmp);
                    }
                    numberTmp = "";
                    if (tmpIndex == i - 1) {
                        if (CheckExistNegativeNumber(stringForPars[i].ToString())) {
                            numberTmp += stringForPars[i].ToString();
                            continue;
                        }
                    } else {
                        elements.Add(stringForPars[i].ToString());
                        tmpIndex = i;
                        continue;
                    }
                }
                numberTmp += stringForPars[i].ToString();
                if (i == stringForPars.Length - 1) {
                    elements.Add(numberTmp);
                }
            }
            if (СorrectCheckElements(elements))
                return elements;
            else return new List<string>();
        }

        bool CheckExistNegativeNumber(string operation) {
            if (operation == "-") {
                return true;
            }
            return false;
        }

        bool СorrectCheckElements(List<string> elements) {
            double num = 0;
            for (int i = 0; i < elements.Count; i++) {
                if (!UniversalDoubleParserFromStr.ParseToDoubleAnyWay(elements[i], out num)) {
                    if (OperationPriority(elements[i]) == -1) {
                        if (!CheckExistBrackets(elements[i])) {
                            return false;
                        } else { continue; }
                    } else { continue; }
                } else { continue; }
            }
            return true;
        }

        public bool CheckExistBrackets(string mathElements) {
            if (mathElements == "(" || mathElements == ")") {
                return true;
            }
            return false;
        }

        public bool CheckExistLeftBracket(string mathElements) {
            if (mathElements == "(") {
                return true;
            }
            return false;
        }

        public bool CheckExistRightBracket(string mathElements) {
            if (mathElements == ")") {
                return true;
            }
            return false;
        }

        public int OperationPriority(string operation) {
            for (int i=0;i< Operators.Count; i++) {
                if (operation == Operators[i].symbol) {
                    return Operators[i].prior;
                }
            }
            return -1;
        }

        public bool UnarOperation(int index) {
            if (Operators[index].unarOp) {
                return true;
            } else return false;
        }

        public Operator GetOperator(string operation) {
            for (int i = 0; i < Operators.Count; i++) {
                if (operation == Operators[i].symbol) {
                    return Operators[i];
                }
            }
            return null;
        }
    }

    //реализация парсинга польской записи
    public class ParserMathOperationInPolishEntry : ParserMathStringOperation, IExpressionPolishEntry {

        public ParserMathOperationInPolishEntry(IExpression Expression) : base(Expression) {

        }

        List<Operator> result = new List<Operator>();

        public List<Operator> GetExpressionPolishEntry(string stringForPars) {
            Stack<string> operationsStack = new Stack<string>();
            string lastOperation = "";
            List<string> elements = GetExpression(stringForPars);
            if (elements.Count != 0) {
                double num = 0;
                for (int i = 0; i < elements.Count; i++) {
                    if (UniversalDoubleParserFromStr.ParseToDoubleAnyWay(elements[i], out num)) {
                        result.Add(new Number {
                            number = num
                        });
                        continue;
                    }
                    if (OperationPriority(elements[i]) != -1) {
                        if (operationsStack.Count != 0)
                            lastOperation = operationsStack.Peek();
                        else {
                            operationsStack.Push(elements[i]);
                            continue;
                        }
                        if (OperationPriority(lastOperation) < OperationPriority(elements[i])) {
                            operationsStack.Push(elements[i]);
                            continue;
                        } else {
                            if (OperationPriority(lastOperation) >= OperationPriority(elements[i])) {
                                AddOperation(operationsStack.Pop());
                                operationsStack.Push(elements[i]);
                                continue;
                            }
                        }
                    }
                    if (CheckExistLeftBracket(elements[i])) {
                        operationsStack.Push(elements[i]);
                        continue;
                    }
                    if (CheckExistRightBracket(elements[i])) {
                        while (operationsStack.Peek() != "(") {
                            AddOperation(operationsStack.Pop());
                        }
                        operationsStack.Pop();
                    }
                }
                while (operationsStack.Count != 0) {
                    AddOperation(operationsStack.Pop());
                }
                return result;
            }
            return new List<Operator>();
        }

        void AddOperation(string operationStack) {
            var op = GetOperator(operationStack);
            result.Add(op);
        }
    }

    public static class UniversalDoubleParserFromStr {

        public static bool ParseToDoubleAnyWay(string value, out double num) {
            value = value.Trim();
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.GetCultureInfo("ru-RU"), out num)) {
                if (!double.TryParse(value, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.GetCultureInfo("en-US"), out num)) {
                    num = 0;
                    return false;
                }
            }
            return true;
        }
    }

    //устаревшие методы, есть в тестирование
    
    [Obsolete]
    public class ParserMathString : IExpressionPars {

        public List<string> GetExpression(string stringForPars) {

            List<string> elements = new List<string>();
            stringForPars = stringForPars.Replace(" ", "");
            stringForPars = stringForPars.ToLower();
            string numberTmp = "";
            int tmpIndex = -1;
            for (int i = 0; i < stringForPars.Length; i++) {
                if (i == 0) {
                    if (!char.IsNumber(stringForPars[0])) {
                        if (CheckExistNegativeNumber(stringForPars[0].ToString())) {
                            numberTmp += stringForPars[0].ToString();
                            continue;
                        }
                    }
                }
                if (CheckExistBrackets(stringForPars[i].ToString())) {
                    if (numberTmp != "") {
                        elements.Add(numberTmp);
                    }
                    numberTmp = "";
                    elements.Add(stringForPars[i].ToString());
                    if (CheckExistLeftBracket(stringForPars[i].ToString())) tmpIndex = i;
                    continue;
                }
                if (OperationPriority(stringForPars[i].ToString()) != -1) {
                    if (numberTmp != "") {
                        elements.Add(numberTmp);
                    }
                    numberTmp = "";
                    if (tmpIndex == i - 1) {
                        if (CheckExistNegativeNumber(stringForPars[i].ToString())) {
                            numberTmp += stringForPars[i].ToString();
                            continue;
                        }
                    } else {
                        elements.Add(stringForPars[i].ToString());
                        tmpIndex = i;
                        continue;
                    }
                }
                numberTmp += stringForPars[i].ToString();
                if (i == stringForPars.Length - 1) {
                    elements.Add(numberTmp);
                }
            }
            if (СorrectCheckElements(elements))
                return elements;
            else return new List<string>();
        }

        bool СorrectCheckElements(List<string> elements) {
            double num = 0;
            for (int i = 0; i < elements.Count; i++) {
                if (!UniversalDoubleParserFromStr.ParseToDoubleAnyWay(elements[i], out num)) {
                    if (OperationPriority(elements[i]) == -1) {
                        if (!CheckExistBrackets(elements[i])) {
                            return false;
                        } else { continue; }
                    } else { continue; }
                } else { continue; }
            }
            return true;
        }

        public bool CheckExistBrackets(string mathElements) {
            if (mathElements == "(" || mathElements == ")") {
                return true;
            }
            return false;
        }

        public bool CheckExistLeftBracket(string mathElements) {
            if (mathElements == "(") {
                return true;
            }
            return false;
        }

        public bool CheckExistRightBracket(string mathElements) {
            if (mathElements == ")") {
                return true;
            }
            return false;
        }

        bool CheckExistNegativeNumber(string operation) {
            if (operation == "-") {
                return true;
            }
            return false;
        }

        // сюда добавлять операции для парсинга и приоритеты, 
        // 0- самый низкий приоритет.
        public int OperationPriority(string operation) {
            switch (operation) {
                case "+": return 0;
                case "-": return 0;
                case "*": return 1;
                case "/": return 1;
                case "^": return 2;
                case "%": return 2;
                case "cos": return 3;
                case "sin": return 3;
            }
            return -1;
        }
        
    }

    [Obsolete]
    public class ParserMathStrInPolishEntry : ParserMathString {

        //алгоритм польской записи
        public List<string> GetExpressionPolishEntry(string stringForPars) {
            Stack<string> operationsStack = new Stack<string>();
            string lastOperation = "";
            List<string> elements = GetExpression(stringForPars);
            if (elements.Count != 0) {
                List<string> result = new List<string>();
                double num = 0;
                for (int i = 0; i < elements.Count; i++) {
                    if (UniversalDoubleParserFromStr.ParseToDoubleAnyWay(elements[i], out num)) {
                        result.Add(elements[i]);
                        continue;
                    }
                    if (OperationPriority(elements[i]) != -1) {
                        if (operationsStack.Count != 0)
                            lastOperation = operationsStack.Peek();
                        else {
                            operationsStack.Push(elements[i]);
                            continue;
                        }
                        if (OperationPriority(lastOperation) < OperationPriority(elements[i])) {
                            operationsStack.Push(elements[i]);
                            continue;
                        } else {
                            if (OperationPriority(lastOperation) >= OperationPriority(elements[i])) {
                                result.Add(operationsStack.Pop());
                                operationsStack.Push(elements[i]);
                                continue;
                            }
                        }
                    }
                    if (CheckExistLeftBracket(elements[i])) {
                        operationsStack.Push(elements[i]);
                        continue;
                    }
                    if (CheckExistRightBracket(elements[i])) {
                        while (operationsStack.Peek() != "(") {
                            result.Add(operationsStack.Pop());
                        }
                        operationsStack.Pop();
                    }
                }
                while (operationsStack.Count != 0) {
                    result.Add(operationsStack.Pop());
                }
                return result;
            }
            return new List<string>();
        }
    }

 

}
