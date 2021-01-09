using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ParserMathStrTests {

    [TestClass]
    public class ParserTests {

        //Что нужно: отрицательные,с десятками, со скобками, сложные выражения, функции, исключения

        [TestMethod]
        public void GetExpressionPolishEntry_TestEasyExpression() {
            //arrange
            string str = "1 / 2";
            Operations.Expression newExpression = new Operations.Expression();
            ParserMathStr.ParserMathOperationInPolishEntry parserMathStr =
                new ParserMathStr.ParserMathOperationInPolishEntry(newExpression);
            List<Operations.Operator> expected = new List<Operations.Operator>() {
                new Operations.Number {
                    number = 1
                },
                new Operations.Number {
                    number = 2
                },
                new Operations.DivisionSing {
                    symbol = "/",
                    prior=1,
                    binarOp = true,
                }
            };
            // Act
            List<Operations.Operator> actual =
            parserMathStr.GetExpressionPolishEntry(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void GetPolishEntryListElements_TestEasyExpression() {
            //arrange
            string str = "1 / 2";
            ParserMathStr.ParserMathStrInPolishEntry parserMathStr =
                new ParserMathStr.ParserMathStrInPolishEntry();
            List<string> expected = new List<string>() { "1","2","/" };
            // Act
            List<string> actual =
            parserMathStr.GetExpressionPolishEntry(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPolishEntryListElements_TestEasyPlusExpression() {
            //arrange
            string str = "7 + (5 - 2) * 4";
            ParserMathStr.ParserMathStrInPolishEntry parserMathStr = 
                new ParserMathStr.ParserMathStrInPolishEntry();
           
            List<string> expected = new List<string>() { "7", "5", "2", "-", "4", "*", "+" };
            // Act
            List<string> actual =
            parserMathStr.GetExpressionPolishEntry(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPolishEntryListElements_TestBarr() {
            //arrange
            ParserMathStr.ParserMathStrInPolishEntry parserMathStr = 
                new ParserMathStr.ParserMathStrInPolishEntry();
            string str = "(((10-10)20*10)(40(10-25)))";
            List<string> expected = new List<string>() {
              "10","10","-","20","10","*","40","10","25","-"
            };
            // Act
            List<string> actual =
            parserMathStr.GetExpressionPolishEntry(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPolishEntryListElements_TestNewElement() {
            //arrange
            ParserMathStr.ParserMathStrInPolishEntry parserMathStr = 
                new ParserMathStr.ParserMathStrInPolishEntry();
            string str = "sin(1)^cos(0)";
            List<string> expected = new List<string>() {
                "1","sin","0","cos","^"
            };
            // Act
            List<string> actual =
            parserMathStr.GetExpressionPolishEntry(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPolishEntryListElements_TestNotCorrectValue() {
            //arrange
            ParserMathStr.ParserMathStrInPolishEntry parserMathStr =
                new ParserMathStr.ParserMathStrInPolishEntry();
            string str = "йцук-123(йцу)йцу+";
            List<string> expected = new List<string>() {
            };
            // Act
            List<string> actual =
            parserMathStr.GetExpressionPolishEntry(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringListElementsParsing_TestEasyExpression() {
            //arrange
            ParserMathStr.ParserMathString parserMathStr = new ParserMathStr.ParserMathString();
            string str = "1 / 2";
            List<string> expected = new List<string> {
                "1",
                "/",
                "2"
            };
            // Act
            List<string> actual =
            parserMathStr.GetExpression(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringListElementsParsing_TestMiddleExpression() {
            //arrange
            ParserMathStr.ParserMathString parserMathStr = new ParserMathStr.ParserMathString();
            string str = "2 + 11 /4-  1*5  + 4";
            List<string> expected = new List<string>() {
                 "2","+","11","/","4","-","1","*","5","+","4"
            };
            // Act
            List<string> actual =
            parserMathStr.GetExpression(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
       
        [TestMethod]
        public void GetStringListElementsParsing_TestHardExpression() {
            //arrange
            ParserMathStr.ParserMathString parserMathStr = new ParserMathStr.ParserMathString();
            string str = "(13+1-5+1000/45)   *2+11/4-1*5+4*9*(1-10+100/555*(23+12))";
            List<string> expected = new List<string>() {
                "(","13","+","1","-","5","+","1000","/","45",
                ")","*","2","+","11","/","4","-","1","*","5",
                "+","4","*","9","*","(","1","-","10","+","100",
                "/","555","*","(","23","+","12",")",")"
            };
            // Act
            List<string> actual =
            parserMathStr.GetExpression(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringListElementsParsing_TestNewElement() {
            //arrange
            ParserMathStr.ParserMathString parserMathStr = new ParserMathStr.ParserMathString();
            string str = "sin(1)^cos(0)";
            List<string> expected = new List<string>() {
               "sin","(","1",")","^","cos","(","0",")"
            };
            // Act
            List<string> actual =
            parserMathStr.GetExpression(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringListElementsParsing_TestMinusValue() {
            //arrange
            ParserMathStr.ParserMathString parserMathStr = new ParserMathStr.ParserMathString();
            string str = "-1+-2*(-2+1*-5)";
            List<string> expected = new List<string>() {
               "-1","+","-2","*","(","-2","+","1","*","-5",")"
            };
            // Act
            List<string> actual =
            parserMathStr.GetExpression(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringListElementsParsing_TestBarr() {
            //arrange
            ParserMathStr.ParserMathString parserMathStr = new ParserMathStr.ParserMathString();
            string str = "(((10-10)20*10)(40(10-25)))";
            List<string> expected = new List<string>() {
               "(","(","(","10","-","10",")","20","*","10",")","(","40","(","10","-","25",")",")",")"
            };
            // Act
            List<string> actual =
            parserMathStr.GetExpression(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringListElementsParsing_Test_Floating_Point_Number() {
            //arrange
            ParserMathStr.ParserMathString parserMathStr = new ParserMathStr.ParserMathString();
            string str = "2.53+73,2+0,1+-0,4";
            List<string> expected = new List<string>() {
                "2.53","+","73,2","+","0,1","+","-0,4"
            };
            // Act
            List<string> actual =
             parserMathStr.GetExpression(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStringListElementsParsing_TestNotCorrectValue() {
            //arrange
            ParserMathStr.ParserMathString parserMathStr = new ParserMathStr.ParserMathString();
            string str = "fsa+123+eqw";
            List<string> expected = new List<string>();
            // Act
            List<string> actual =
             parserMathStr.GetExpression(str);
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
