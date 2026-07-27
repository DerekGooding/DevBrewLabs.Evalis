using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBrewLabs.Evalis
{
    public interface ISequencedExpression
    {
    }

    internal sealed class SequencedExpression : ISequencedExpression, IEnumerable<SequencedExpressionSegment>
    {
        private readonly Dictionary<string, SequencedExpressionSegment> _expressions;

        internal IEngineContext Context { get; private set; }

        internal SequencedExpression()
        {
            _expressions = new Dictionary<string, SequencedExpressionSegment>();
            Context = new SeqExprContext(this);
        }

        /// <summary>
        /// Adds an expression segment to this sequenced expression.
        /// </summary>
        /// <param name="segment"></param>
        internal void AddSegment(SequencedExpressionSegment segment) => _expressions.Add(segment.Key, segment);

        internal SequencedExpressionSegment GetSegment(string key) => _expressions[key];

        internal void Dispose()
        {
            (Context as SeqExprContext)?.Dispose();
            Context = null;
        }

        public IEnumerator<SequencedExpressionSegment> GetEnumerator() => _expressions.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _expressions.GetEnumerator();

        private class SeqExprContext : IEngineContext, IDisposable
        {
            private SequencedExpression _expression;

            public SeqExprContext(SequencedExpression expression) => _expression = expression;

            public async Task<object> Resolve(string key)
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

            public void Dispose() => _expression = null;
        }
    }
}