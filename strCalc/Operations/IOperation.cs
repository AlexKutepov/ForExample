using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations {

    public interface IExpression {
        List<Operator> GetOperators();
        List<Operator> GetExpression();
        void SetExpressio(List<Operator> operationesInExpression);
    }
}
