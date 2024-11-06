namespace AlphaX.FormulaEngine.Core.Evaluation.Resolver
{
    internal abstract class ArgumentResolver<TIn, TOut>
    {
        protected AlphaXFormulaEngine Engine { get; }

        protected ArgumentResolver(AlphaXFormulaEngine engine)
        {
            Engine = engine;
        }

        public abstract TOut Resolve(TIn input);
    }
}
