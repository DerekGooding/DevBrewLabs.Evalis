using AlphaX.Parserz.Tracing;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaX.FormulaEngine.Benchmark
{
    public class Program
    {  
        static void Main(string[] args)
        {
            var engine = new AlphaXFormulaEngine();

            //Console.WriteLine(string.Join(",\r\n", engine.FormulaStore.GetAll()));
            //Console.WriteLine("---------------------------------");

            //var expr = SequencedExpressionBuilder
            //    .Create("Result1", "SUM(1,2,12)")
            //    .Next("Result2", "AVERAGE(1,2,SUM(1, SUM(1,4)))")
            //    .Next("Result3", "SUM(1,$Result1,$Result2)");

            //var result = engine.Evaluate(expr);

            FormulaEngineBenchmark.RunBenchmarks(engine, 1000);
        }
    } 
}
