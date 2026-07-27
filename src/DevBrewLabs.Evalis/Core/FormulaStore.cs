using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DevBrewLabs.Evalis
{
    internal class FormulaStore : IFormulaStore
    {
        private readonly ConcurrentDictionary<string, FormulaBase> _formulas;
        private readonly FormulaEngine _formulaEngine;

        public FormulaStore(FormulaEngine engine)
        {
            _formulas = new ConcurrentDictionary<string, FormulaBase>(StringComparer.OrdinalIgnoreCase);
            _formulaEngine = engine;
        }

        public IEnumerable<FormulaInfo> GetAll() => _formulas.Select(x => x.Value.Info);

        public FormulaBase Get(string formulaName) => _formulas[formulaName];

        public bool Contains(string formulaName) => _formulas.ContainsKey(formulaName);

        public void Add(FormulaBase formula) => _formulas.TryAdd(formula.Name, formula);

        public void Remove(string formulaName)
        {
            if (!_formulas.TryRemove(formulaName, out _))
            {
                throw new InvalidOperationException($"Invalid formula '{formulaName}'");
            }
        }
    }
}