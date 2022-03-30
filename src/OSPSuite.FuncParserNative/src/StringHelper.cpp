#include "FuncParser/StringHelper.h"
#include <string>

namespace FuncParserNative
{

std::string StringHelper::Capitalize (const std::string & pInString)
{
    std::string newString = pInString;
    newString[0] = toupper(pInString[0]);

    return newString;
}

}//.. end "namespace FuncParserNative"
