set PROJECT="Asv.Mavlink"

set /P version=Enter version for %PROJECT%: 
cd src\%PROJECT%\bin\Release\
dotnet nuget push %PROJECT%.%version%.nupkg --source https://api.nuget.org/v3/index.json
dotnet nuget push %PROJECT%.%version%.nupkg --source https://nuget.pkg.github.com/asvol/index.json