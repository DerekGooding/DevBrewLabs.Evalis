using AlphaX.FormulaEngine.Core.Parsing;
using AlphaX.FormulaEngine.Formulas;
using AlphaX.Parserz;
using AlphaX.Parserz.Tracing;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AlphaX.FormulaEngine
{
    public class AlphaXFormulaEngine : IFormulaEngine
    {
        private IParser _expressionParser;
        private readonly object _settingsLock = new object();

        #region Internal
        internal Evaluator Evaluator {  get; private set; }
        internal IEngineSettings CurrentSettings { get; private set; }
        #endregion

        public IEngineContext Context { get; set; }
        public IFormulaStore FormulaStore { get; }

        public AlphaXFormulaEngine(IEngineContext context = null, bool loadDefaultFormulas = true)
        {
            Context = context;
            FormulaStore = new FormulaStore(this);
            Evaluator = new Evaluator(FormulaStore);
            ApplySettings(new EngineSettings());

            if (loadDefaultFormulas)
            {
                LoadDefaultFormulas();
            }
        }

        public IEvaluationResult Evaluate(string input)
        {
            return EvaluateInternal(input, Context)
                .GetAwaiter().GetResult();
        }

        public IEvaluationResult Evaluate(ISequencedExpression input)
        {
            IEvaluationResult result = null;
            SequencedExpression expr = input as SequencedExpression;

            foreach (SequencedExpressionSegment expressionSegment in expr)
            {
                result = EvaluateInternal(expressionSegment.Expression, expr.Context)
                        .GetAwaiter().GetResult();

                if (!string.IsNullOrEmpty(result.Error))
                {
                    return result;
                }

                expressionSegment.Result = result.Value;
            }

            expr.Dispose();
            return result;
        }

        public Task<IEvaluationResult> EvaluateAsync(string input)
        {
            return EvaluateInternal(input, Context); ;
        }

        public async Task<IEvaluationResult> EvaluateAsync(ISequencedExpression input)
        {
            IEvaluationResult result = null;
            SequencedExpression expr = input as SequencedExpression;

            foreach (SequencedExpressionSegment expressionSegment in expr)
            {
                result = await EvaluateInternal(expressionSegment.Expression, expr.Context);

                if (!string.IsNullOrEmpty(result.Error))
                {
                    return result;
                }

                expressionSegment.Result = result.Value;
            }

            expr.Dispose();
            return result;
        }

        private async Task<IEvaluationResult> EvaluateInternal(string input, IEngineContext context)
        {
            try
            {
                if (input == null)
                {
                    throw new Exception("Input can't be null");
                }

                var parserState = _expressionParser.Run(input);

                if (parserState.IsError)
                    return new EvaluationResult(parserState.Error.Message);

                object result = await Evaluator.Evaluate(parserState.Result, context);
                return new EvaluationResult(result);
            }
            catch (EvaluationException ex)
            {
                return new EvaluationResult(ex.Message);
            }
            catch (Exception ex)
            {
                ex = UnwrapException(ex);
                return new EvaluationResult(ex.Message);
            }
        }

        public void ApplySettings(IEngineSettings settings)
        {
            if (settings.EngineParseOrder is null || !settings.EngineParseOrder.Any())
                throw new InvalidOperationException("Invalid engine parse order");

            lock (_settingsLock)
            {
                Evaluator.SupportedLogicalOperators = new LogicalOperator(settings.LogicalOperatorMode);
                _expressionParser = new ExpressionParser(settings, Evaluator.SupportedLogicalOperators);
                CurrentSettings = settings;
            }
        }

        private void LoadDefaultFormulas()
        {
            FormulaStore.Add(new OperatorFormula("EQUALS", () => Evaluator.SupportedLogicalOperators.EqualsTo));
            FormulaStore.Add(new OperatorFormula("NOTEQUALS", () => Evaluator.SupportedLogicalOperators.NotEquals));
            FormulaStore.Add(new OperatorFormula("OR", () => Evaluator.SupportedLogicalOperators.OR));
            FormulaStore.Add(new OperatorFormula("AND", () => Evaluator.SupportedLogicalOperators.AND));
            FormulaStore.Add(new OperatorFormula("GREATERTHAN", () => Evaluator.SupportedLogicalOperators.GreaterThan));
            FormulaStore.Add(new OperatorFormula("GREATERTHANEQUALS", () => Evaluator.SupportedLogicalOperators.GreaterThanEqualsTo));
            FormulaStore.Add(new OperatorFormula("LESSTHAN", () => Evaluator.SupportedLogicalOperators.LessThan));
            FormulaStore.Add(new OperatorFormula("LESSTHANEQUALS", () => Evaluator.SupportedLogicalOperators.LessThanEqualsTo));
            FormulaStore.Add(new NotFormula());

            // Arithmetic
            FormulaStore.Add(new SumFormula());
            FormulaStore.Add(new AverageFormula());
            FormulaStore.Add(new CeilingFormula());
            FormulaStore.Add(new FloorFormula());
            FormulaStore.Add(new AbsFormula());
            FormulaStore.Add(new MinFormula());
            FormulaStore.Add(new MaxFormula());
            FormulaStore.Add(new PowerFormula());
            FormulaStore.Add(new RoundFormula());
            FormulaStore.Add(new SqrtFormula());

            // Array
            FormulaStore.Add(new ArrayContainsFormula());
            FormulaStore.Add(new ArrayIncludesFormula());
            FormulaStore.Add(new ArrayFormula());
            FormulaStore.Add(new IndexFormula());
            FormulaStore.Add(new JoinFormula());
            FormulaStore.Add(new CountFormula());

            // String
            FormulaStore.Add(new LowerFormula());
            FormulaStore.Add(new UpperFormula());
            FormulaStore.Add(new TextSplitFormula());
            FormulaStore.Add(new ConcatFormula());
            FormulaStore.Add(new LengthFormula());
            FormulaStore.Add(new ContainsFormula());
            FormulaStore.Add(new StartsWithFormula());
            FormulaStore.Add(new EndsWithFormula());
            FormulaStore.Add(new RegexMatchFormula());
            FormulaStore.Add(new ReplaceFormula());
            FormulaStore.Add(new TrimFormula());
            FormulaStore.Add(new SubstringFormula());
            FormulaStore.Add(new IndexOfFormula());

            // DateTime
            FormulaStore.Add(new TodayFormula());
            FormulaStore.Add(new NowFormula());
            FormulaStore.Add(new DateTimeFormula());
            FormulaStore.Add(new YearFormula());
            FormulaStore.Add(new MonthFormula());
            FormulaStore.Add(new DayFormula());

            // Logical
            FormulaStore.Add(new IFFormula());
            FormulaStore.Add(new CoalesceFormula());
            FormulaStore.Add(new IsNumberFormula());
            FormulaStore.Add(new IsStringFormula());
        }

        private Exception UnwrapException(Exception ex)
        {
            while (true)
            {
                switch (ex)
                {
                    case AggregateException agg when agg.InnerExceptions.Count == 1:
                        ex = agg.InnerExceptions[0];
                        continue;

                    case AggregateException agg when agg.InnerExceptions.Count > 1:
                        return agg.Flatten();

                    default:
                        if (ex.InnerException != null &&
                            (ex is TargetInvocationException || ex.GetType().Name == "EvaluationWrapperException"))
                        {
                            ex = ex.InnerException;
                            continue;
                        }
                        return ex;
                }
            }
        }
    }
}
