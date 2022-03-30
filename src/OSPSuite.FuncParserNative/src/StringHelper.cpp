#include "FuncParser/StringHelper.h"
#include <string>
#include <algorithm>

namespace FuncParserNative
{

std::string StringHelper::Capitalize (const std::string & pInString)
{
    std::string newString = pInString;
    newString[0] = toupper(pInString[0]);
    std::transform(newString.begin() + 1, newString.end(), newString.begin() + 1, ::tolower);

    return newString;
}

}//.. end "namespace FuncParserNative"
