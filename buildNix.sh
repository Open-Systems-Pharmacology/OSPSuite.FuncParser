#! /bin/sh

#call: buildNix.sh distributionName version
# e.g. buildNix.sh CentOS7 4.0.0.49

echo `uname -m`
if [ `uname -m` == 'x86_64' ]; then
  ARCH=x64
else
  ARCH=Arm64
fi

rm -f OSPSuite.FuncParser4Nix.sln

dotnet restore --source https://ci.appveyor.com/nuget/ospsuite-utility --source https://ci.appveyor.com/nuget/ospsuite-bddhelper --source https://www.nuget.org/api/v2/

# copy the original solution file because it will be modified for dotnet build
cp -p -f OSPSuite.FuncParser.sln OSPSuite.FuncParser4Nix.sln

dotnet sln OSPSuite.FuncParser4Nix.sln remove src/OSPSuite.FuncParserNative/OSPSuite.FuncParserNative.vcxproj

cmake -BBuild/Release/$ARCH/ -Hsrc/OSPSuite.FuncParserNative/ -DCMAKE_BUILD_TYPE=Release
make -C Build/Release/$ARCH
dotnet build OSPSuite.FuncParser4Nix.sln /property:Configuration=Release

cmake -BBuild/Debug/$ARCH/ -Hsrc/OSPSuite.FuncParserNative/ -DCMAKE_BUILD_TYPE=Debug
make -C Build/Debug/$ARCH
dotnet build OSPSuite.FuncParser4Nix.sln /property:Configuration=Debug

dotnet test OSPSuite.FuncParser4Nix.sln --no-build --no-restore --configuration:Release #optionally: run tests

dotnet pack src/OSPSuite.FuncParser/ -p:PackageVersion=$2
