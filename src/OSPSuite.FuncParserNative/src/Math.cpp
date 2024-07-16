#include "FuncParser/Math.h"
#ifdef _WINDOWS

#include <math.h>
#include <ymath.h>
#include <float.h>
#endif
#include <cmath>

namespace FuncParserNative
{

double Math::GetNaN ()
{
	return NAN;
}

bool Math::IsNaN (double d)
{
#ifdef _WINDOWS
	return _isnan(d) ? true : false;
#endif
#ifdef linux
	return std::isnan(d);
#endif
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
#ifdef _WINDOWS
	return _finite(d) ? true : false;
#endif
#ifdef linux
	return (finite(d) != 0);
#endif
}

bool Math::IsInf (double d)
{
	return (d==GetInf());
}

bool Math::IsNegInf (double d)
{
	return (d==GetNegInf());
}


const int VAR_INVALID_INDEX = 0;

}//.. end "namespace FuncParserNative"
