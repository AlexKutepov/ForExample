using System;
using System.Collections.Generic;
using CalcException;

namespace Operations {
    
    public class Expression : IExpression {

        public List<Operator> OperationesInExpression;

        static List<Operator> Operators = new List<Operator> {
            new PlusSign {
                symbol = "+",
                prior=0,
                binarOp = true,
            },
            new MinusSing {
                symbol = "-",
                prior=0,
                binarOp = true,
            },
            new MultiplicationSing {
                symbol = "*",
                prior=1,
                binarOp = true,
            },
            new DivisionSing {
                symbol = "/",
                prior=1,
                binarOp = true,
            },
            new CosSing {
                symbol = "cos",
                prior=3,
                unarOp = true,
            },
            new SinSing {
                symbol = "sin",
                prior=3,
                unarOp = true,
            },
            new PowSing {
                symbol = "^",
                prior=2,
                binarOp = true,
            },
            new ModSing {
                symbol = "%",
                prior=1,
                binarOp = true,
            }
        };

        public void AddOperators(Operator newOperator) {
            Operators.Add(newOperator);
        }

        public List<Operator> GetOperators() {
            return Operators;
        }

        public List<Operator> GetExpression() {
            return OperationesInExpression;
        }

        public void SetExpressio(List<Operator> operationesInExpression) {
            OperationesInExpression = operationesInExpression;
        }
    }

    abstract public class Operator {

        public double? number = null;
        public double x;
        public double y;
        public string symbol;
        public int prior;
        public bool unarOp;
        public bool binarOp;
      
        public override bool Equals(object obj) {
            var Operator = obj as Operator;
            return Operator != null &&
                   x == Operator.x && y == Operator.y &&
                   number == Operator.number &&
                   symbol == Operator.symbol &&
                   prior == Operator.prior &&
                   unarOp == Operator.unarOp &&
                   binarOp == Operator.binarOp;
        }

        public override int GetHashCode() {
            var hashCode = 1290039854;
            hashCode = hashCode * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(number);
            hashCode = hashCode * -1521134295 + EqualityComparer<double>.Default.GetHashCode(x);
            hashCode = hashCode * -1521134295 + EqualityComparer<double>.Default.GetHashCode(y);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(symbol);
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(prior);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(unarOp);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(binarOp);
             return hashCode;
        }
        public abstract double Operation();
    }

    public class Number: Operator {
        public override double Operation() {
            return (double) number;
        }
    }

    public class PlusSign : Operator {
        public override double Operation() {
            return x + y;
        }
    }

    public class MinusSing : Operator {
        public override double Operation() { 
                return x - y;
        }
    }

    public class MultiplicationSing : Operator {
        public override double Operation() {
            return x * y;
        }
    }

    public class DivisionSing : Operator {
        public override double Operation() {
            if (y == 0) throw new CalculatorException("Division on null");
            return x / y;
        }
    }

    public class CosSing : Operator {
        public override double Operation() {
            return Math.Cos(x);
        }
    }

    public class SinSing : Operator {
        public override double Operation() {
            return Math.Sin(x);
        }
    }

    public class PowSing : Operator {
        public override double Operation() {
            return Math.Pow(x, y);
        }
    }

    public class ModSing : Operator {
        public override double Operation() {
            return x % y;
        }
    }

}
