using AlphaX.FormulaEngine.Formulas;
using AlphaX.Parserz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaX.FormulaEngine
{
    internal class Evaluator : IEvaluator
    {
        private IFormulaStore _formulaStore;
        private static Dictionary<string, int> _operatorPriority;

        static Evaluator()
        {
            _operatorPriority = new Dictionary<string, int>()
            {
                { ArithmeticOperator.Add, 2 },
                { ArithmeticOperator.Subtract, 2 },
                { ArithmeticOperator.Multiply, 3 },
                { ArithmeticOperator.Divide, 3 },
                { ArithmeticOperator.Modulo, 3 },
            };
        }

        internal LogicalOperator SupportedLogicalOperators { get; set; }

        public Evaluator(IFormulaStore formulaStore)
        {
            _formulaStore = formulaStore;
        }

        public async Task<object> Evaluate(IParserResult result, IEngineContext context)
        {
            if (result is ArrayResult arrResult)
            {
                result = InfixToPostfix(arrResult.Normalize());
            }

            if (result is ArrayResult)
            {
                return await Evaluate(result, context);
            }

            if (result is FormulaResult formulaResult)
            {
                return await EvaluateFormula(formulaResult, context);
            }

            if (result is CustomNameResult customNameResult)
            {
                return await Resolve(customNameResult.Value, context);
            }

            if (result is OperatorResult opResult)
            {
                return await EvaluateOperator(opResult, context);
            }

            if(result == null)
            {
                ThrowInvalidExpressionError();
            }

            return result.Value;
        }

        private async Task<object> EvaluateFormula(FormulaResult result, IEngineContext context)
        {
            var formulaName = result.Value.Name;

            if (!_formulaStore.Contains(formulaName))
                throw new EvaluationException($"Invalid formula '{formulaName}'");

            FormulaBase formula = (_formulaStore as FormulaStore).Get(formulaName);

            var args = result.Value.Args;
            var tasks = new Task<object>[args.Length];

            for (int i = 0; i < args.Length; i++)
            {
                tasks[i] = Evaluate(args[i], context);
            }

            await Task.WhenAll(tasks);
            
            // Materialize results directly
            var arguments = new object[args.Length];
            for (int i = 0; i < tasks.Length; i++)
            {
                var argResult = tasks[i].Result;
                if (argResult is IEvaluationResult evalResult)
                {
                    if (evalResult.Error != null)
                    {
                        if (!formula.HandlesErrors) return evalResult;
                        arguments[i] = evalResult;
                    }
                    else
                    {
                        arguments[i] = evalResult.Value;
                    }
                }
                else
                {
                    arguments[i] = argResult;
                }
            }

            try
            {
                FormulaContext formulaContext = new FormulaContext(arguments)
                {
                    Evaluator = this
                };

                if (formula.IsAsync)
                {
                    return await (formula as AsyncFormula).EvaluateAsync(formulaContext);
                }
                else
                {
                    return (formula as Formula).Evaluate(formulaContext);
                }
            }
            catch (Exception ex)
            {
                throw new EvaluationException($"Failed to evaluate '{formula.Name}' formula. {ex.Message}");
            }
        }

        private async Task<object> EvaluateOperator(OperatorResult result, IEngineContext context)
        {
            var left = Evaluate(result.Child[0], context);
            var right = Evaluate(result.Child[1], context);

            await Task.WhenAll(left, right);
            var leftVal = left.Result;
            if (leftVal is IEvaluationResult leftRes)
            {
                if (leftRes.Error != null) return leftRes;
                leftVal = leftRes.Value;
            }

            var rightVal = right.Result;
            if (rightVal is IEvaluationResult rightRes)
            {
                if (rightRes.Error != null) return rightRes;
                rightVal = rightRes.Value;
            }

            string @operator = result.Value;

            switch (result.Value)
            {
                case ArithmeticOperator.Add:
                    {
                        if (leftVal is double leftOp && rightVal is double rightOp)
                            return EvaluationResult.WithValue(leftOp + rightOp);
                    }
                    return EvaluationResult.WithError($"Invalid operator used with operands. '{leftVal} {@operator} {rightVal}'.");

                case ArithmeticOperator.Subtract:
                    {
                        if (leftVal is double leftOp && rightVal is double rightOp)
                            return EvaluationResult.WithValue(leftOp - rightOp);
                    }
                    return EvaluationResult.WithError($"Invalid operator used with operands. '{leftVal} {@operator} {rightVal}'.");

                case ArithmeticOperator.Divide:
                    {
                        if (leftVal is double leftOp && rightVal is double rightOp)
                        {
                            if (rightOp == 0) return EvaluationResult.WithError("Can't divide by zero.");
                            return EvaluationResult.WithValue(leftOp / rightOp);
                        }
                    }
                    return EvaluationResult.WithError($"Invalid operator used with operands. '{leftVal} {@operator} {rightVal}'.");

                case ArithmeticOperator.Multiply:
                    {
                        if (leftVal is double leftOp && rightVal is double rightOp)
                            return EvaluationResult.WithValue(leftOp * rightOp);
                    }
                    return EvaluationResult.WithError($"Invalid operator used with operands. '{leftVal} {@operator} {rightVal}'.");

                case ArithmeticOperator.Modulo:
                    {
                        if (leftVal is double leftOp && rightVal is double rightOp)
                            return EvaluationResult.WithValue(leftOp % rightOp);
                    }
                    return EvaluationResult.WithError($"Invalid operator used with operands. '{leftVal} {@operator} {rightVal}'.");

                default:
                    return EvaluationResult.WithValue(AlphaXUtil.Compare(leftVal, result.Value, rightVal, SupportedLogicalOperators));
            }
        }

        private IParserResult InfixToPostfix(ArrayResult infixResult)
        {
            var openBracketResult = new OpenBracketResult();
            var closeBracketResult = new CloseBracketResult();

            int openBrackets = 0;
            int closedBrackets = 0;
            var reverse = new IParserResult[infixResult.Value.Length];
            for (int i = 0; i < infixResult.Value.Length; i++)
            {
                var x = infixResult.Value[infixResult.Value.Length - 1 - i];
                if (x is OpenBracketResult)
                {
                    openBrackets++;
                    reverse[i] = closeBracketResult;
                }
                else if (x is CloseBracketResult)
                {
                    closedBrackets++;
                    reverse[i] = openBracketResult;
                }
                else
                {
                    reverse[i] = x;
                }
            }

            if (openBrackets != closedBrackets)
            {
                ThrowInvalidExpressionError();
            }

            var operatorStack = new Stack<IParserResult>();
            var outputList = new List<IParserResult>();

            foreach (var cur in reverse)
            {
                if (cur is CloseBracketResult)
                {
                    var op = operatorStack.Count > 0 ? operatorStack.Pop() : null;

                    while (op != null && op.Type != openBracketResult.Type)
                    {
                        outputList.Add(op);
                        op = operatorStack.Count > 0 ? operatorStack.Pop() : null;
                    }
                }
                else if (cur is OpenBracketResult)
                {
                    operatorStack.Push(cur);
                }
                else if (cur is OperatorResult opResult)
                {
                    int c = operatorStack.Count;
                    // stack is empty, push operator
                    if (c == 0)
                    {
                        operatorStack.Push(cur);
                    }
                    else
                    {
                        var lastOperator = operatorStack.Peek();

                        if (lastOperator is OpenBracketResult || !_operatorPriority.ContainsKey(opResult.Value)
                            || _operatorPriority[opResult.Value] > _operatorPriority[((OperatorResult)lastOperator).Value])
                        {
                            operatorStack.Push(cur);
                        }
                        else
                        {
                            while (lastOperator != null &&
                                lastOperator is OperatorResult lastOpResult &&
                                _operatorPriority[lastOpResult.Value] > _operatorPriority[opResult.Value])
                            {
                                outputList.Add(lastOperator);
                                operatorStack.Pop();
                                lastOperator = operatorStack.Count > 0 ? operatorStack.Peek() : null;
                            }

                            operatorStack.Push(cur);
                        }
                    }
                }
                else
                {
                    outputList.Add(cur);
                }
            }

            while (operatorStack.Count > 0)
            {
                outputList.Add(operatorStack.Pop());
            }

            outputList.Reverse();


            var pendingNodes = new Stack<IParserResult>();
            IParserResult root = null;

            for (var i = 0; i < outputList.Count; i++)
            {
                if (root == null)
                {
                    root = outputList[i];
                }

                if (pendingNodes.Count > 0)
                {
                    var lastPending = pendingNodes.Peek() as OperatorResult;
                    lastPending.Child.Add(outputList[i]);
                    if (lastPending.Child != null && lastPending.Child.Count == 2)
                    {
                        pendingNodes.Pop();
                    }
                }

                if (outputList[i] is OperatorResult)
                {
                    pendingNodes.Push(outputList[i]);
                }
            }

            if (pendingNodes.Count > 0)
            {
                throw new Exception("Invalid operands in expression.");
            }

            return root;
        }

        #region Resolver
        public async Task<object> Resolve(CustomName customName, IEngineContext context = null)
        {
            if (context == null)
            {
                throw new EvaluationException($"No context found to resolve custom name ({customName.Value}).");
            }

            var resolvedValue = await context.Resolve(customName.Value);

            if (resolvedValue == null)
                return resolvedValue;

            return NormalizeValue(resolvedValue);
        }

        private static object NormalizeValue(object value)
        {
            if (value is int || value is byte)
            {
                return Convert.ToDouble(value);
            }

            if (value is Array array)
            {
                var normalized = new object[array.Length];
                for (int i = 0; i < array.Length; i++)
                    normalized[i] = NormalizeValue(array.GetValue(i));
                return normalized;
            }

            return value;
        }

        private void ThrowInvalidOperandsError(object left, string op, object right)
        {
            throw new EvaluationException($"Invalid operator used with operands. '{left} {op} {right}'.");
        }

        private void ThrowInvalidExpressionError()
        {
            throw new EvaluationException("Expression is invalid.");
        }
        #endregion
    }
}
