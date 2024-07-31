PUSHD .
CD tests\LRSchoolV2.Application.Tests\
RMDIR /S /Q StrykerOutput
dotnet stryker -p "src/LRSchoolV2.Application/LRSchoolV2.Application.csproj" -v trace -o
POPD

PUSHD .
CD tests\LRSchoolV2.Infrastructure.Tests\
RMDIR /S /Q StrykerOutput
dotnet stryker -p "src/LRSchoolV2.Infrastructure/LRSchoolV2.Infrastructure.csproj" -v trace --mutate "!**/Migrations/**" --mutate "!**/*DataModelConfiguration.cs" -c 1 -o
POPD