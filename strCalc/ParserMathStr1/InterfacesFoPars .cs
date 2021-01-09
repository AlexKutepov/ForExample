using System.Collections.Generic;
using Operations;

namespace ParserMathStr {

    public interface IExpressionPars{

        List<string> GetExpression(string stringForPars);

    }

    public interface IExpressionPolishEntry {

        List<Operator> GetExpressionPolishEntry(string stringForPars);

    }


}
