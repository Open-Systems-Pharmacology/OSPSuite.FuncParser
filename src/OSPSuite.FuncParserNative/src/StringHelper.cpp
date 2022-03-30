#include "FuncParser/StringHelper.h"
#include <string>
#include <algorithm>

namespace FuncParserNative
{

std::string StringHelper::Capitalize (const std::string & pInString)
{
    // return early if the string is empty; nothing to change in this case
    if (pInString.empty())
    {
        return pInString;
    }

    std::string newString = pInString;

    // *only* the first character should be uppercase
    newString[0] = toupper(pInString[0]);

    // all other characters should be changed to lowercase
    std::transform(newString.begin() + 1, newString.end(), newString.begin() + 1, ::tolower);

    return newString;
}

}//.. end "namespace FuncParserNative"
