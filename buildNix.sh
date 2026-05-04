#! /bin/sh

# Build the native library and run the .NET tests on Linux or macOS.
#
# Usage: buildNix.sh distributionName
#   e.g. buildNix.sh Linux
#
# distributionName is only used to name the test log file (testLog_$1.html).
# Packing the unified multi-RID NuGet package is done in the dedicated CI
# pack-and-publish job, not here, because it requires natives from all three
# platforms to be present under runtimes/.

set -e

if [ "$(uname -m)" = 'x86_64' ]; then
  ARCH=x64
else
  ARCH=Arm64
fi

if [ "$(uname)" = 'Darwin' ]; then
  RID=osx-arm64
  NATIVE_FILE=libOSPSuite.FuncParserNative.dylib
else
  RID=linux-x64
  NATIVE_FILE=libOSPSuite.FuncParserNative.so
fi

# Build native (Release only — the runtimes/<rid>/native/ NuGet convention has no
# Debug/Release axis; consumers needing native debugging build from the source
# shipped in the package under OSPSuite.FuncParserNative/src/ and include/).
cmake -BBuild/Release/$ARCH/ -Hsrc/OSPSuite.FuncParserNative/ -DCMAKE_BUILD_TYPE=Release
make -C Build/Release/$ARCH

# Stage the native binary at runtimes/<rid>/native/ — the canonical location read
# by both OSPSuite.FuncParser.csproj (for in-repo tests) and the unified nuspec
# (for packing).
mkdir -p runtimes/$RID/native
cp Build/Release/$ARCH/$NATIVE_FILE runtimes/$RID/native/

# Build managed projects via a .NET-only solution (the C++ vcxproj is not
# buildable with `dotnet`).
rm -f OSPSuite.FuncParser.NetOnly.sln
cp -p -f OSPSuite.FuncParser.sln OSPSuite.FuncParser.NetOnly.sln
dotnet sln OSPSuite.FuncParser.NetOnly.sln remove src/OSPSuite.FuncParserNative/OSPSuite.FuncParserNative.vcxproj

dotnet restore OSPSuite.FuncParser.NetOnly.sln
dotnet build OSPSuite.FuncParser.NetOnly.sln --configuration Release --no-restore
dotnet test OSPSuite.FuncParser.NetOnly.sln --no-build --no-restore --configuration Release --logger:"html;LogFileName=../../../testLog_$1.html"
