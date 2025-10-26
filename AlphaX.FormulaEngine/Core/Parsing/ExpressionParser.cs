using AlphaX.FormulaEngine.Resources;
using AlphaX.Parserz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaX.FormulaEngine.Core.Parsing
{
    internal class ExpressionParser : IParser
    {
        private IParser _formulaParser;
        private IParser _numberParser;
        private IParser _boolParser;
        private IParser _stringParser;
        private IParser _customNameParser;
        private IParser _expressionParser;
        private IParser _nullParser;

        public ExpressionParser(IEngineSettings settings, LogicalOperators @operator)
        {
            BuildParser(settings, @operator);
        }

        private void BuildParser(IEngineSettings settings, LogicalOperators @operator)
        {
            var emtpyStringResult = new StringResult(string.Empty);
            var whiteSpacesParser = Parser.WhiteSpace.Many().MapResult(x => emtpyStringResult);

            _nullParser = Parser.String("null").MapResult(x => new StringResult(null));

            var logicalOperatorParsers = Parser.String(@operator.EqualsTo)
              .Or(Parser.String(@operator.NotEquals))
              .Or(Parser.String(@operator.LessThanEqualsTo))
              .Or(Parser.String(@operator.GreaterThanEqualsTo))
              .Or(Parser.String(@operator.LessThan))
              .Or(Parser.String(@operator.GreaterThan))
              .Or(Parser.String(@operator.AND))
              .Or(Parser.String(@operator.OR))
              .AndThen(whiteSpacesParser)
              .MapResult(x => x.Value[0]);

            _customNameParser = Parser.String(SyntaxTokens.Custom)
                .AndThen(Parser.AnyLetterOrDigit().Many().MapResult(x => x.ToStringResult()))
                .AndThen(whiteSpacesParser)
                .MapResult(x => new CustomNameResult(new CustomName(x.Value[1].Value?.ToString())));

            _stringParser = Parser.StringValue(settings.DoubleQuotedStrings)
                .AndThen(whiteSpacesParser)
                .MapResult(x => x.Value[0]);

            _boolParser = Parser.Boolean
                .AndThen(whiteSpacesParser)
                .MapResult(x => x.Value[0]);

            _numberParser = Parser.Number(true)
                .AndThen(whiteSpacesParser)
                .MapResult(x => x.Value[0]);

            var argSepResult = new FormulaArgumentSeperatorResult(settings.ArgumentsSeparatorSymbol);
            var argSepParser = Parser.String(settings.ArgumentsSeparatorSymbol)
                .AndThen(whiteSpacesParser)
                .MapResult(x => argSepResult);

            var peekParser = new PeekParser(settings.ArgumentsSeparatorSymbol);
            var baseArgumentParser = CreateParserFromParseOrder(settings.EngineParseOrder);

            var formulaArgumentParser = baseArgumentParser
                .Next(leftOperandResult =>
                {
                    ConditionResult conditionResult = null;

                    return peekParser.MapResult(x => leftOperandResult)
                    .Or(
                        logicalOperatorParsers
                        .Next(operatorResult =>
                        {
                            if (operatorResult.Type != ParserResultType.String)
                            {
                                return Parser.FromResult(leftOperandResult);
                            }
                            else
                            {
                                return baseArgumentParser.MapResult(rightOperandResult =>
                                {
                                    if (conditionResult == null)
                                    {
                                        conditionResult = new ConditionResult(new Condition(
                                          leftOperandResult,
                                          operatorResult,
                                          rightOperandResult));
                                    }
                                    else
                                    {
                                        conditionResult = new ConditionResult(new Condition()
                                        {
                                            LeftOperand = conditionResult,
                                            Operator = operatorResult,
                                            RightOperand = rightOperandResult
                                        });
                                    }
                                    return conditionResult;
                                }).MapError(x => new ParserError(x.Index, "Invalid logical expression"));
                            }
                        })
                        .Many()
                        .MapResult(x =>
                        {
                            if (x.Value.Length == 0)
                                return leftOperandResult;

                            return conditionResult;
                        })
                    );
                })
                .MapError(x => new ParserError(x.Index, "Invalid argument found in expression"));

            var openBracketResult = new FormulaBracketResult(settings.OpenBracketSymbol);
            var openBracketParser = Parser.String(settings.OpenBracketSymbol)
                .AndThen(whiteSpacesParser)
                .MapResult(x => openBracketResult);

            var closeBracketResult = new FormulaBracketResult(settings.CloseBracketSymbol);
            var closeBracketParser = Parser.String(settings.CloseBracketSymbol)
                .AndThen(whiteSpacesParser)
                .MapResult(x => closeBracketResult);

            var lettersOrDigitsParser = Parser.Letter.AndThen(Parser.AnyLetterOrDigit().Many())
                .MapError(x => new ParserError(x.Index, string.Format(EngineResources.UnexpectedInput, x.Index, "formula name")))
                .MapResult(x => x.ToStringResult());

            var formulaNameParser = lettersOrDigitsParser
                .AndThen(whiteSpacesParser)
                .MapResult(x => new FormulaNameResult(x.Value[0].Value.ToString()));

            _formulaParser = formulaNameParser
                .AndThen(openBracketParser)
                .AndThen(formulaArgumentParser.ManySeptBy(argSepParser))
                .AndThen(closeBracketParser)
                .MapError(x => new ParserError(x.Index, $"Invalid formula expression. Reason: {x.Message}"));

            _expressionParser = formulaArgumentParser
                               .Or(_formulaParser);
        }

        private IParser CreateParserFromParseOrder(IParseOrder parseOrder, params ParseType[] parseTypesToSkip)
        {
            IParser parser = null;

            foreach (ParseType type in parseOrder)
            {
                if (parseTypesToSkip != null && parseTypesToSkip.Contains(type))
                {
                    continue;
                }

                parser = parser == null ? GetParser(type) : parser.Or(GetParser(type));
            }

            return parser.Or(_nullParser);
        }

        private IParser GetParser(ParseType mode)
        {
            switch (mode)
            {
                case ParseType.Boolean: return _boolParser;
                case ParseType.String: return _stringParser;
                case ParseType.Number: return _numberParser;
                case ParseType.CustomName: return _customNameParser;
                case ParseType.Formula: return Parser.Lazy(() => _formulaParser);
                default:
                    throw new ArgumentException("Invalid parse type");
            }
        }

        public IParserState Run(string input)
        {
            return _expressionParser.Run(input);
        }

        public IParserState Parse(IParserState inputState)
        {
            return _expressionParser.Parse(inputState);
        }
    }
}