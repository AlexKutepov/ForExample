using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NewCalcTests {

    [TestClass]
    public class CalcTests {

        //Что нужно: отрицательные(в т.ч. первое число - отрицательное), 
        //с десятками, со скобками, сложные выражения, функции

        [TestMethod]
        public void Calculate_Test_1plus2_ans3() {
            //arrange
            string str = "1 +  2";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            double expected = 3;
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual =calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Test_min5plus2_Minus10_plus4_ans3() {
            //arrange
            string str = "-5+2-10+4";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            double expected = -9;
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Test_Min5MinusMin1Plus2_ansMin2() {
            //arrange
            string str = "-5--1+2";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            double expected = -2;
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Test_2Abs2() {
            //arrange
            string str = "2^2";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            double expected = 4;
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_TestCos_6() {
            //arrange
            string str = "Cos(6)";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            double expected = 0.96;
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void Calculate_TestSin_1() {
            //arrange
            string str = "sin(1)";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            double expected = 0.84;
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_TestInBarrs2Multiplication2_Abs2() {
            //arrange
            string str = "(2*2)^2";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            double expected =16;
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Tes_Abs2_InBarrs2Mutiplication2() {
            //arrange
            string str = "2^(2*2)";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            double expected = 16;
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Test_10Mod2() {
            //arrange
            string str = "123413%14232";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            double expected = 9557;
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Test_10Mod2_PlusSin1_PlusCos1_MinusBarrs2_Plu110() {
            //arrange
            string str = "123413%14232+Sin(1)+Cos(1)-(2.3+-110)";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            double expected = 9666.08;
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_TestBarr() {
            //arrange
            string str = "((10-10)+20*10)-40-(10-25)";
            double expected = 175;
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_TestEasyAplusExpression() {
            //arrange
            string str = "1+2/10+2*5";
            double expected = 11.2;
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Test_MiddleExpressio() {
            //arrange
            string str = "174 + (10.32+16) * 9.1^4  ";
            double expected = 180663.3;
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Calculate_Test_HardExpressio() {
            //arrange
            string str = "(((123+1.23/13)-123.213)*13^2/13-(31+145.1)/31*-31)+10%3";
            double expected = 21.39*(-1);
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStrInPolishEntry = new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            NewCalc.Calculator calc = new NewCalc.Calculator(parserMathStrInPolishEntry, newExpression, str);
            // Act
            calc.StartCalculateExpressionInPolishEntry();
            double actual = calc.answer;
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }

}
    