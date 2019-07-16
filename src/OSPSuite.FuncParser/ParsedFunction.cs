using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace OSPSuite.FuncParser
{
   static class ParsedFunctionImports
   {

#if _WINDOWS
      private const String FUNCPARSER_NATIVE_DLL = "OSPSuite.FuncParserNative.dll";
#else
      private const String FUNCPARSER_NATIVE_DLL = "OSPSuite.FuncParserNative.so";
#endif

      private const CallingConvention FUNCPARSER_CALLING_CONVENTION = CallingConvention.Cdecl;

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern IntPtr CreateParsedFunction();

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void DisposeParsedFunction(IntPtr parsedFunction);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern bool GetCaseSensitive(IntPtr parsedFunction);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void SetCaseSensitive(IntPtr parsedFunction, bool caseSensitive);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void SetVariableNames(IntPtr parsedFunction, [In, Out] string[] variableNames, int size);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void SetParameterNames(IntPtr parsedFunction, [In, Out] string[] parameterNames, int size);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void SetParameterValues(IntPtr parsedFunction, [In, Out] double[] parameterValues, int size);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void SetParametersNotToSimplify(IntPtr parsedFunction, [In, Out] string[] parameterNames, int size);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern bool GetSimplifyParametersAllowed(IntPtr parsedFunction);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void SetSimplifyParametersAllowed(IntPtr parsedFunction, bool simplifyParametersAllowed);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern bool GetLogicOperatorsAllowed(IntPtr parsedFunction);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void SetLogicOperatorsAllowed(IntPtr parsedFunction, bool logicOperatorsAllowed);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern bool GetLogicalNumericMixAllowed(IntPtr parsedFunction);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void SetLogicalNumericMixAllowed(IntPtr parsedFunction, bool logicalNumericMixAllowed);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern double GetComparisonTolerance(IntPtr parsedFunction);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void SetComparisonTolerance(IntPtr parsedFunction, double comparisonTolerance, out bool success, out string errorMessage);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern string GetStringToParse(IntPtr parsedFunction);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void SetStringToParse(IntPtr parsedFunction, string stringToParse);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern void Parse(IntPtr parsedFunction, out bool success, out string errorMessage);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern double CalcExpression(IntPtr parsedFunction, [In, Out] double[] arguments, int size, out bool success, out string errorMessage);

      [DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = FUNCPARSER_CALLING_CONVENTION)]
      public static extern string GetXMLString(IntPtr parsedFunction, out bool success, out string errorMessage);

      //[DllImport(FUNCPARSER_NATIVE_DLL, CallingConvention = CallingConvention.StdCall)]
      //public static extern void SetStringToParse(IntPtr parsedFunction, [MarshalAs(UnmanagedType.LPStr)] string stringToParse);
   }

   public class ParsedFunction
   {
      private readonly IntPtr _parsedFunction;

      public ParsedFunction()
      {
         _parsedFunction = ParsedFunctionImports.CreateParsedFunction();
      }

      ~ParsedFunction()
      {
         ParsedFunctionImports.DisposeParsedFunction(_parsedFunction);
      }

      public bool CaseSensitive
      {
         get => ParsedFunctionImports.GetCaseSensitive(_parsedFunction);
         set => ParsedFunctionImports.SetCaseSensitive(_parsedFunction, value);
      }

      public void SetVariableNames(IEnumerable<string> variableNames)
      {
         var variableNamesArray = variableNames.ToArray();
         ParsedFunctionImports.SetVariableNames(_parsedFunction, variableNamesArray, variableNamesArray.Length);
      }

      public void SetParameterNames(IEnumerable<string> parameterNames)
      {
         var parameterNamesArray = parameterNames.ToArray();
         ParsedFunctionImports.SetParameterNames(_parsedFunction, parameterNamesArray, parameterNamesArray.Length);
      }

      public void SetParameterValues(IEnumerable<double> parameterValues)
      {
         var parameterValuesArray = parameterValues.ToArray();
         ParsedFunctionImports.SetParameterValues(_parsedFunction, parameterValuesArray, parameterValuesArray.Length);
      }

      public void SetParametersNotToSimplify(IEnumerable<string> parameterNames)
      {
         var parameterNamesArray = parameterNames.ToArray();
         ParsedFunctionImports.SetParametersNotToSimplify(_parsedFunction, parameterNamesArray, parameterNamesArray.Length);
      }

      public bool SimplifyParametersAllowed
      {
         get => ParsedFunctionImports.GetSimplifyParametersAllowed(_parsedFunction);
         set => ParsedFunctionImports.SetSimplifyParametersAllowed(_parsedFunction, value);
      }

      public bool LogicOperatorsAllowed
      {
         get => ParsedFunctionImports.GetLogicOperatorsAllowed(_parsedFunction);
         set => ParsedFunctionImports.SetLogicOperatorsAllowed(_parsedFunction, value);
      }

      public bool LogicalNumericMixAllowed
      {
         get => ParsedFunctionImports.GetLogicalNumericMixAllowed(_parsedFunction);
         set => ParsedFunctionImports.SetLogicalNumericMixAllowed(_parsedFunction, value);
      }

      public double ComparisonTolerance
      {
         get => ParsedFunctionImports.GetComparisonTolerance(_parsedFunction);
         set
         {
            ParsedFunctionImports.SetComparisonTolerance(_parsedFunction, value, out var success, out var errorMessage);

            if (success)
               return;

            throw new Exception(errorMessage);
         }
      }

      public string StringToParse
      {
         get => ParsedFunctionImports.GetStringToParse(_parsedFunction);
         set => ParsedFunctionImports.SetStringToParse(_parsedFunction, value);
      }

      public void Parse()
      {
         ParsedFunctionImports.Parse(_parsedFunction, out var success, out var errorMessage);

         if (success)
            return;

         throw new Exception(errorMessage);
      }

      public double CalcExpression(IEnumerable<double> arguments)
      {
         var argumentsArray = arguments.ToArray();
         var value = ParsedFunctionImports.CalcExpression(_parsedFunction, argumentsArray, argumentsArray.Length, out var success, out var errorMessage);

         if (success)
            return value;

         throw new Exception(errorMessage);
      }

      public string GetXMLString()
      {
         var xmlString = ParsedFunctionImports.GetXMLString(_parsedFunction, out var success, out var errorMessage);

         if (success)
            return xmlString;

         throw new Exception(errorMessage);
      }
   }
}
