#include "FuncParser/FuncParserErrorData.h"
#include "FuncParser/DimensionParser.h"
#include "FuncParser/ParsedFunction.h"

namespace FuncParserNative
{
	using namespace std;

	DimensionInfo DimensionParser::GetDimensionInfoFor(const string & formula,
		                                               const vector<QuantityDimensionInfo> & quantityDimensions)
	{
		ParsedFunction pf;
		return pf.GetDimensionInfoFor(formula, quantityDimensions);
	}

}//.. end "namespace FuncParserNative"
