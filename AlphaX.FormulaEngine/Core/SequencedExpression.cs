using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlphaX.FormulaEngine
{
    public sealed class SequencedExpression : IEnumerable<SequencedExpressionSegment>
    {
        private Dictionary<string, SequencedExpressionSegment> _expressions;

        internal IEngineContext Context { get; private set; }

        internal SequencedExpression()
        {
            _expressions = new Dictionary<string, SequencedExpressionSegment>();
            Context = new SequencedExpresionContext(this);
        }

        /// <summary>
        /// Adds an expression segment to this sequenced expression.
        /// </summary>
        /// <param name="segment"></param>
        internal void AddSegment(SequencedExpressionSegment segment)
        {
            _expressions.Add(segment.Key, segment);
        }

        internal SequencedExpressionSegment GetSegment(string key)
        {
            return _expressions[key];
        }

        internal SequencedExpressionSegment GetPreviousSegment()
        {
            return _expressions.Last().Value;
        }

        internal void Dispose()
        {
            (Context as SequencedExpresionContext).Dispose();
            Context = null;
        }

        public IEnumerator<SequencedExpressionSegment> GetEnumerator()
        {
            return _expressions.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _expressions.GetEnumerator();
        }

        private class SequencedExpresionContext : IEngineContext, IDisposable
        {
            private SequencedExpression _expression;

            public SequencedExpresionContext(SequencedExpression expression)
            {
                _expression = expression;
            }

            public object Resolve(string key)
            {
                try
                {
                    return _expression.GetSegment(key).Result;
                }
                catch
                {
                    throw new InvalidOperationException($"Invalid sequenced expression name '{key}'.");
                }
            }

            public void Dispose()
            {
                _expression = null;
            }
        }
    }
}
