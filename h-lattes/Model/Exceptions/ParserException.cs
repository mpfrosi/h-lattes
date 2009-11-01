using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Exceptions
{
    public class ParserException : Exception
    {
        public ParserException(Exception _ex)
            : base("Erro de parser.", _ex)
        { }
    }
}
