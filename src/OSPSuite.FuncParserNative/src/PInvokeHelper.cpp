#include "FuncParser/PInvokeHelper.h"

#ifdef _WINDOWS
#include "comdef.h"
#define STRCPY(DEST, LENGTH, SOURCE) strcpy_s(DEST, LENGTH, SOURCE)
#endif

#ifdef linux
#include <cstring>
#include <string.h>
#define STRCPY(DEST, LENGTH, SOURCE) strcpy(DEST, SOURCE)
#define CoTaskMemAlloc malloc
#endif

#ifdef __APPLE__
#define STRCPY(DEST, LENGTH, SOURCE) strcpy(DEST, SOURCE)
#define CoTaskMemAlloc malloc
#endif 
namespace FuncParserNative
{
   using namespace std;

   char* MarshalString(const char* sourceString)
   {
      // Allocate memory for the string
      size_t length = strlen(sourceString) + 1;
      char* destString = (char*)CoTaskMemAlloc(length);
      STRCPY(destString, length, sourceString);
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
