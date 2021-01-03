using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCalc {

    interface IOperationUnary {
        double Operation(double x);
    }

    interface IOperationBinary {
        double Operation(double x, double y);
    }

    interface IExpression {
        List<Operator> GetExpression();
        void SetExpressio();
    }
}
