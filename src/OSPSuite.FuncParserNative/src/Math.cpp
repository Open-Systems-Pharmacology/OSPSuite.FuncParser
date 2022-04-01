#include "FuncParser/Math.h"
#include <cmath>


namespace FuncParserNative
{

double Math::GetNaN ()
{
	return NAN;
}

bool Math::IsNaN (double d)
{
	return std::isnan(d);
}

double Math::GetInf ()
{
	return INFINITY;
}

double Math::GetNegInf ()
{
	return -GetInf();
}

bool Math::IsFinite (double d)
{
	return std::isfinite(d);
}

bool Math::IsInf (double d)
{
	return std::isinf(d);
}

bool Math::IsNegInf (double d)
{
	return (d==GetNegInf());
}


const int VAR_INVALID_INDEX = 0;

}//.. end "namespace FuncParserNative"
