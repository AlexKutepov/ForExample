using System;
using System.Runtime.Serialization;

namespace CalcException
{
    public class CalculatorException : ApplicationException {

        public CalculatorException() { }

        public CalculatorException(string message) : base(message) { }

        public CalculatorException(string message, Exception inner) : base(message, inner) { }

        protected CalculatorException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}
