using System;
using System.Collections.Generic;
using CalcException;

namespace NewCalc {
    
    //TODO новая архитектура

    public class Expression : IExpression {

        public List<Operator> OperationesInExpression;

        List<Operator> Operators = new List<Operator> {

             new PlusSign {
                id = 0,
                symbol = "+",
                prior=0,
                binarOp = true
            },
            new MinusSing {
                id = 1,
                symbol = "-",
                prior=0,
                binarOp = true
            },
            new MultiplicationSing {
                id = 2,
                symbol = "*",
                prior=1,
                binarOp = true
            },
            new DivisionSing {
                id = 3,
                symbol = "/",
                prior=1,
                binarOp = true
            },
            new CosSing {
                id = 4,
                symbol = "cos",
                prior=3,
                unarOp = true
            },
            new SinSing {
                id = 5,
                symbol = "sin",
                prior=3,
                unarOp = true
            },
            new PowSing {
                id = 6,
                symbol = "^",
                prior=2,
                unarOp = true
            },
            new ModSing {
                id = 7,
                symbol = "%",
                prior=1,
                unarOp = true
            },
        };

        List<Operator> IExpression.GetExpression() {
            return OperationesInExpression;
        }

        void IExpression.SetExpressio() {
            throw new NotImplementedException();
        }

    }

    public class Operator {
        public int id;
        public string symbol;
        public int prior;
        public bool unarOp;
        public bool binarOp;
    }

    public class PlusSign : Operator, IOperationBinary {
        double IOperationBinary.Operation(double x, double y) {
            return x + y;
        }

    }

    public class MinusSing : Operator, IOperationBinary {
        double IOperationBinary.Operation(double x, double y) {
            return x - y;
        }
    }

    public class MultiplicationSing : Operator, IOperationBinary {
        double IOperationBinary.Operation(double x, double y) {
            return x * y;
        }
    }

    public class DivisionSing : Operator, IOperationBinary {
        double IOperationBinary.Operation(double x, double y) {
            if (y == 0) throw new CalculatorException("Division on null");
            return x / y;
        }
    }

    public class CosSing : Operator, IOperationUnary {
        double IOperationUnary.Operation(double x) {
            return Math.Cos(x);
        }
    }

    public class SinSing : Operator, IOperationUnary {
        double IOperationUnary.Operation(double x) {
            return Math.Sin(x);
        }
    }

    public class PowSing : Operator, IOperationBinary {
        double IOperationBinary.Operation(double x, double y) {
            return Math.Pow(x, y);
        }
    }

    public class ModSing : Operator, IOperationBinary {
        double IOperationBinary.Operation(double x, double y) {
            return x % y;
        }
    }

}
