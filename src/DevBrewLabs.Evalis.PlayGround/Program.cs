namespace DevBrewLabs.Evalis.PlayGround;

public static class Program
{
    private static void Main()
    {
        var engine = new FormulaEngine();

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