#! /bin/sh

#call: buildLinux.sh distributionName version
# e.g. buildLinux.sh CentOS7 4.0.0.49

rm -f OSPSuite.FuncParser4Linux.sln

dotnet restore --source https://ci.appveyor.com/nuget/ospsuite-utility --source https://ci.appveyor.com/nuget/ospsuite-bddhelper --source https://www.nuget.org/api/v2/

# copy the original solution file because it will be modified for dotnet build
cp -p -f OSPSuite.FuncParser.sln OSPSuite.FuncParser4Linux.sln

dotnet sln OSPSuite.FuncParser4Linux.sln remove src/OSPSuite.FuncParserNative/OSPSuite.FuncParserNative.vcxproj

cmake -BBuild/Release/x64/ -Hsrc/OSPSuite.FuncParserNative/ -DCMAKE_BUILD_TYPE=Release
make -C Build/Release/x64
dotnet build OSPSuite.FuncParser4Linux.sln /property:Configuration=Release;Platform=x64

cmake -BBuild/Debug/x64/ -Hsrc/OSPSuite.FuncParserNative/ -DCMAKE_BUILD_TYPE=Debug
make -C Build/Debug/x64
dotnet build OSPSuite.FuncParser4Linux.sln /property:Configuration=Debug;Platform=x64

# dotnet test --no-build --no-restore --configuration:Release #optionally: run tests

nuget pack src/OSPSuite.FuncParser/OSPSuite.FuncParser$1.nuspec -version $2
