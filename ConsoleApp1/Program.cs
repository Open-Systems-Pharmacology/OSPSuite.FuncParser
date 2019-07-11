using System;
using OSPSuite.FuncParser;

namespace ConsoleApp1
{
   class Program
   {
      static void Main(string[] args)
      {
         try
         {
            double p1 = 1, p2 = 2, p3 = 3, x=3, y=4;
            var pf = new ParsedFunction();

            var caseSensitive = pf.CaseSensitive;
            pf.CaseSensitive = !caseSensitive;
            caseSensitive = pf.CaseSensitive;
            Console.WriteLine($"CaseSensitive={caseSensitive}");

            pf.SetVariableNames(new[] { "x", "y" });
            pf.SetParameterNames(new[] { "p1", "p2", "p3" });
            pf.SetParameterValues(new []{p1,p2,p3});
            pf.SetParametersNotToSimplify(new []{"p3"});
            pf.SimplifyParametersAllowed = true;
            pf.LogicOperatorsAllowed = false;
            pf.LogicalNumericMixAllowed = false;
            pf.ComparisonTolerance = 0.1;
            pf.StringToParse = "2*p1+3*p2+p3+x*y"; 
            var stringToParse = pf.StringToParse;

            pf.Parse();
            Console.WriteLine("Parse: OK");

            var value = pf.CalcExpression(new[] {x, y});
            Console.WriteLine($"p1={p1} p2={p2} p3={p3} x={x} y={y}\n");
            Console.WriteLine($"{stringToParse} = {value}\n");

            var xmlString = pf.GetXMLString();
            Console.WriteLine($"{xmlString}\n");
            pf = null; 
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
         }

         Console.WriteLine("Press Enter");
         Console.ReadLine();
      }
   }
}
