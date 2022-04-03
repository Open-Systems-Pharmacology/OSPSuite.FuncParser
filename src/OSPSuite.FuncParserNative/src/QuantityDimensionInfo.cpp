#include "FuncParser/QuantityDimensionInfo.h"

namespace FuncParserNative
{
	using namespace std;

	QuantityDimensionInfo::QuantityDimensionInfo()
	{
	}

	QuantityDimensionInfo::QuantityDimensionInfo(const string & quantityName, 
	 	                                         const DimensionInfo & dimensionInfo) :
		_quantityName { quantityName },
		_dimensionInfo { dimensionInfo }
	{
	}

	const string & QuantityDimensionInfo::GetQuantityName() const
	{
		return _quantityName;
	}

	void QuantityDimensionInfo::SetQuantityName(const string & quantityName)
	{
		_quantityName = quantityName;
	}

	DimensionInfo QuantityDimensionInfo::GetDimensionInfo() const
	{
		return _dimensionInfo;
	}

}//.. end "namespace FuncParserNative"
