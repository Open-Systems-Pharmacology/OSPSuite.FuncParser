#include "FuncParser/Math.h"
#ifdef _WINDOWS

#include <math.h>
#include <ymath.h>
#include <float.h>
#endif
#ifdef linux
#include <cmath>
#endif

namespace FuncParserNative
{

double Math::GetNaN ()
{
#ifdef _WINDOWS
	return _Nan._Double;
#endif
#ifdef linux 
	return NAN;
#endif
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
#ifdef _WINDOWS
	return _Inf._Double;
#endif
#ifdef linux
	return INFINITY;
#endif
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
