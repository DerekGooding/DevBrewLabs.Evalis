using AlphaX.FormulaEngine.Resources;
using AlphaX.Parserz;
using System.Text.RegularExpressions;

namespace AlphaX.FormulaEngine.Core.Parsing
{
    public class VarParser : RegexParser<StringResult>
    {
        public VarParser() : base(new Regex(@"^[a-zA-Z]+[\w\d]"), true)
        {

        }

        protected override StringResult ConvertResult(Match value)
        {
            return new StringResult(value.Value);
        }

        protected override IParserError CreateError(int index, string value)
        {
            return new ParserError(index, string.Format(EngineResources.UnexpectedInput, index, "function name"));
        }
    }
}