using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserMathStr {

    public interface IExpression {

        List<string> GetExpression(string stringForPars);

    }

    public interface IExpressionPolishEntry {

        List<string> GetExpressionPolishEntry(string stringForPars);

    }

}
