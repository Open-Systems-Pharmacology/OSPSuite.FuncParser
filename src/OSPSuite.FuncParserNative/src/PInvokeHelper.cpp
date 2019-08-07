#include "FuncParser/PInvokeHelper.h"

#ifdef _WINDOWS
#include "comdef.h"
#endif

#ifdef linux
#define CoTaskMemAlloc malloc
#include <cstring>
#endif

namespace FuncParserNative
{
   using namespace std;

   char* MarshalString(const char* sourceString)
   {
      // Allocate memory for the string
      size_t length = strlen(sourceString) + 1;
      char* destString = (char*)CoTaskMemAlloc(length);
#ifdef _WINDOWS
      strcpy_s(destString, length, sourceString);
#else
      strncpy(destString, sourceString, length);
#endif
      return destString;
   }

   char* MarshalString(const string& sourceString)
   {
      return MarshalString(sourceString.c_str());
   }

   char* ErrorMessageFrom(FuncParserErrorData& ED)
   {
      return MarshalString(ED.GetDescription());
   }

   char* ErrorMessageFromUnknown(const string& errorSource)
   {
      string message = "Unknown error";
      if (errorSource != "")
         message += " in " + errorSource;

      return MarshalString(message);
   }

}//.. end "namespace FuncParserNative"
