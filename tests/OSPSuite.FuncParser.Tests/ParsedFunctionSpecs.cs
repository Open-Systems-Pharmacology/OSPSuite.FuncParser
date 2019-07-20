using System;
using NUnit.Framework;
using OSPSuite.BDDHelper;
using OSPSuite.BDDHelper.Extensions;

namespace OSPSuite.FuncParser.Tests
{
   public abstract class concern_for_parsed_function : ContextSpecification<ParsedFunction>
   {

      protected double x, y, z, p1, p2;

      protected override void Context()
      {
         sut = new ParsedFunction();

         x = 1.0; y = 2.0; z = 3.0; p1 = 4.0; p2 = 5.0;

         sut.CaseSensitive = true;
         sut.LogicOperatorsAllowed = true;
         sut.LogicalNumericMixAllowed = false;
         sut.SimplifyParametersAllowed = true;

         sut.SetVariableNames(new[] { "x", "y", "z" });
         sut.SetParameterNames(new[] { "p1", "p2" });
         sut.SetParameterValues(new[] { p1, p2 });
      }
   }

   //---- Base class for most of test classes in this file
   //Every test defines math. expression to parse and calculates
   //its value directly (via C++ code)
   // After that, 3 ParsedFunction are defined: 
   //  1: SimplifyParametersAllowed = true (which is actually sut)
   //  2: SimplifyParametersAllowed = false
   //  3: SimplifyParametersAllowed = true but some parameters set to "not to be simplified"
   //(besides that, Parsed Functions are equal)
   // For every function, value is calculated via FuncParser and compared to
   // directly calculated value (within given math. rel. tolerance)
   public abstract class when_calculating_expressions : concern_for_parsed_function
   {
      ParsedFunction _notSimplifiedParsedFunction, _partlySimplifiedParsedFunction;
      double _calculatedValueSimplified;       //value calculated via FuncParser (all parameters simplified)
      double _calculatedValueNotSimplified;    //value calculated via FuncParser (parameters not simplified)
      double _calculatedValuePartlySimplified; //value calculated via FuncParser (some parameters simplified)

      protected string _stringToParse;     //math expression to pe parsed (set by inherited test class)
      protected double _calculatedValueDirect;  //value of math expression calculated directly
                                                //(via C++ code; set by inherited class)

      protected override void Because()
      {
         sut.StringToParse = _stringToParse;

         var arguments = new[] { x, y, z };
         _calculatedValueSimplified = sut.CalcExpression(arguments);

         _notSimplifiedParsedFunction = new ParsedFunction();
         _notSimplifiedParsedFunction.UpdateFrom(sut);
         _notSimplifiedParsedFunction.SimplifyParametersAllowed = false;
         _calculatedValueNotSimplified = _notSimplifiedParsedFunction.CalcExpression(arguments);

         _partlySimplifiedParsedFunction = new ParsedFunction();
         _partlySimplifiedParsedFunction.UpdateFrom(sut);

         _partlySimplifiedParsedFunction.SetParametersNotToSimplify(new[] { "p1" });
         _calculatedValuePartlySimplified = _partlySimplifiedParsedFunction.CalcExpression(arguments);
      }

      public virtual void should_return_correct_value_when_simplified()
      {
         _calculatedValueSimplified.ShouldBeEqualTo(_calculatedValueDirect);
      }

      public virtual void should_return_correct_value_when_not_simplified()
      {
         _calculatedValueNotSimplified.ShouldBeEqualTo(_calculatedValueDirect);
      }

      public virtual void should_return_correct_value_when_partly_simplified()
      {
         _calculatedValuePartlySimplified.ShouldBeEqualTo(_calculatedValueDirect);
      }

   }

   public class when_calculating_expr01 : when_calculating_expressions
   {
      protected override void Because()
      {
         _stringToParse = "(x+z)^(x+z)*z+p1-x+y+z+p1*p2-(x+y-z)^2";
         _calculatedValueDirect = Math.Pow((x + z), (x + z)) * z + p1 - x + y + z + p1 * p2 - Math.Pow((x + y - z), 2);

         base.Because();
      }

      [Observation]
      public override void should_return_correct_value_when_simplified()
      {
         base.should_return_correct_value_when_simplified();
      }

      [Observation]
      public override void should_return_correct_value_when_not_simplified()
      {
         base.should_return_correct_value_when_not_simplified();
      }

      [TestCase]
      public override void should_return_correct_value_when_partly_simplified()
      {
         base.should_return_correct_value_when_partly_simplified();
      }

      //static object[] Testdata = new []
      //{

      //}
   }
}
