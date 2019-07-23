using System;
using System.Collections.Generic;
using System.Text;

namespace OSPSuite.FuncParser
{
   public class DimensionParser
   {
      public enum DimensionParseResult
      {

      }

      /// <summary>
      /// Dimension information (=dimension exponents) for the given formula
      /// </summary>
      /// <param name="formula">formula of interest</param>
      /// <param name="quantityDimensions">dimension information for all quantities used in the formula</param>
      /// <returns> if (parseSuccess = true AND calculateDimensionSuccess = true): dimension information for the given formula
      /// if (parseSuccess = true AND calculateDimensionSuccess = false): dimension information could not be calculated (e.g. formula is "x+y" where x and y have not the same dimension
      /// if (parseSuccess = false): formula is invalid
      /// message contains error description if (parseSuccess = false OR calculateDimensionSuccess = false)
      /// </returns>
      public (DimensionInformation, bool parseSuccess, bool calculateDimensionSuccess, string message) GetDimensionInformationFor(
         string formula, IEnumerable<QuantityDimensionInformation> quantityDimensions)
      {
         throw new Exception("not implemented yet");
      }
   }
}
