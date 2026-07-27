using DevBrewLabs.Parserly;
using System.Text.RegularExpressions;

namespace DevBrewLabs.Evalis.Core.Parsing
{
    public class PeekParser : Parser<BooleanResult>
    {
        private readonly Regex _valueRegex;

        public PeekParser(string value, bool matchCase = false)
        {
            string pattern = $"^{Regex.Escape(value)}";
            _valueRegex = matchCase ? new Regex(pattern) : new Regex(pattern, RegexOptions.IgnoreCase);
        }

        protected override IParserState ParseInput(IParserState inputState)
        {
            if (!_valueRegex.IsMatch(inputState.Input))
            {
                return ParserStates.Error(inputState, new ParserError(inputState.Index, "peek value not found"));
            }

            return ParserStates.Result(inputState, new BooleanResult(true), inputState.Index);
        }
    }
}