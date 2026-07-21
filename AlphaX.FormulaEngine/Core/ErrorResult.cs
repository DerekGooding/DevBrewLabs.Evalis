using AlphaX.Parserz;

namespace AlphaX.FormulaEngine
{
    internal class ErrorResult : IParserResult
    {
        public bool IsValid { get; }
        public object Value { get; }
        public ParserResultType Type { get; }
        public string Message { get; }

        public ErrorResult(string message)
        {
            Message = message;
        }
    }
}
